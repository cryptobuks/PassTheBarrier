using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Models;
using PassTheBarier.Core.Messenger;
using PassTheBarier.Core.Navigation;

namespace PassTheBarier.Core.ViewModels
{
    public class AddressBookViewModel : BaseViewModel
    {
        private readonly IContactLogic _contactLogic;
        private readonly IMvxNavigationService _navigationService;
	    private readonly IMvxMessenger _messenger;

		//Commands
		public IMvxCommand<ContactModel> ContactSelectedCommand { get; }
		public IMvxCommand AddContactCommand { get; }
        public IMvxCommand RefreshContactsCommand { get; }
        public IMvxCommand FetchContactsCommand { get; }

        //Tasks
        public MvxNotifyTask LoadContactsTask { get; private set; }
        public MvxNotifyTask FetchContactsTask { get; private set; }

        //Properties
        private MvxObservableCollection<ContactModel> _contacts;

        public MvxObservableCollection<ContactModel> Contacts
        {
            get => _contacts;
	        set
            {
                _contacts = value;
                RaisePropertyChanged(() => Contacts);
            }
        }

        public AddressBookViewModel(IContactLogic contactLogic, IMvxNavigationService navigationService, IMvxMessenger messenger)
        {
            _contactLogic = contactLogic;
            _navigationService = navigationService;
	        _messenger = messenger;

	        Contacts = new MvxObservableCollection<ContactModel>();

            ContactSelectedCommand = new MvxAsyncCommand<ContactModel>(ContactSelected);

	        AddContactCommand = new MvxAsyncCommand(AddContact);

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
			//edit or delete or nothing
            var result = await _navigationService.Navigate<ContactViewModel, ContactModel, ViewModelResult<ContactModel>>(selectedContact);
	        if (result != null)
	        {
		        var contact = Contacts.FirstOrDefault(c => c.Id == result.Response.Id);
				if (result.Response != null)
		        {
					//edit
			        contact.Name = result.Response.Name;
			        contact.Number = result.Response.Number;
				}
				else
		        {
					//delete
			        Contacts.Remove(contact);
		        }
		        _messenger.Publish(new ContactsMessage(this, Contacts));
			}
		}

	    private async Task AddContact()
	    {
			//add or nothing
		    var result = await _navigationService.Navigate<ContactViewModel, ContactModel, ViewModelResult<ContactModel>>(null);
		    if (result?.Response != null)
		    {
				Contacts.Add(result.Response);
			    _messenger.Publish(new ContactsMessage(this, Contacts));
			}
	    }
    }
}