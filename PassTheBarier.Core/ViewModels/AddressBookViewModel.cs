using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.ViewModels
{
    public class AddressBookViewModel : BaseViewModel
    {
        private readonly IContactLogic _contactLogic;
        private readonly IMvxNavigationService _navigationService;

        //Commands
        public IMvxCommand<ContactModel> ContactSelectedCommand { get; private set; }
        public IMvxCommand RefreshContactsCommand { get; private set; }
        public IMvxCommand FetchContactsCommand { get; private set; }

        //Tasks
        public MvxNotifyTask LoadContactsTask { get; private set; }
        public MvxNotifyTask FetchContactsTask { get; private set; }

        //Properties
        private MvxObservableCollection<ContactModel> _contacts;

        public MvxObservableCollection<ContactModel> Contacts
        {
            get { return _contacts; }
            set
            {
                _contacts = value;
                RaisePropertyChanged(() => Contacts);
            }
        }

        public AddressBookViewModel(IContactLogic contactLogic, IMvxNavigationService navigationService)
        {
            _contactLogic = contactLogic;
            _navigationService = navigationService;

            Contacts = new MvxObservableCollection<ContactModel>();

            ContactSelectedCommand = new MvxAsyncCommand<ContactModel>(ContactSelected);

            FetchContactsCommand = new MvxCommand(
                () =>
                {
                    FetchContactsTask = MvxNotifyTask.Create(LoadContacts);
                    RaisePropertyChanged(() => FetchContactsTask);
                });
            RefreshContactsCommand = new MvxCommand(RefreshContacts);
        }

        public override Task Initialize()
        {
            LoadContactsTask = MvxNotifyTask.Create(LoadContacts);
            RaisePropertyChanged(() => LoadContactsTask);

            return Task.FromResult(0);
        }

        private async Task LoadContacts()
        {
            var contacts = await _contactLogic.GetAllAsync();
            Contacts.ReplaceWith(contacts);
        }

        private void RefreshContacts()
        {
            LoadContactsTask = MvxNotifyTask.Create(LoadContacts);
            RaisePropertyChanged(() => LoadContactsTask);
        }

        private async Task ContactSelected(ContactModel selectedContact)
        {
            await _navigationService.Navigate<ContactViewModel, ContactModel>
                (selectedContact);
        }
    }
}