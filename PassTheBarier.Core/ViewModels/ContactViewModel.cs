using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.ViewModels
{
    public class ContactViewModel : BaseViewModel<ContactModel>
    {
	    private readonly IModalLogic _modalLogic;
        private readonly IContactLogic _contactLogic;
        private IMvxNavigationService _navigationService;

	    public IMvxCommand SaveContactCommand { get; private set; }

        public ContactViewModel(IModalLogic modalLogic, IContactLogic contactLogic, IMvxNavigationService navigationService)
        {
            _contactLogic = contactLogic;
            _navigationService = navigationService;
	        _modalLogic = modalLogic;

	        SaveContactCommand = new MvxAsyncCommand(SaveContact);
        }

        private ContactModel _contact;
        public ContactModel Contact
        {
            get => _contact;
	        set
            {
                _contact = value;
                RaisePropertyChanged(() => Contact);
            }
        }

        public override void Prepare(ContactModel selectedContact)
        {
            Contact = selectedContact;
        }

	    private async Task SaveContact()
	    {
			//TODO: validation
		    await _contactLogic.UpdateAsync(Contact);
		    _modalLogic.DisplayToast("Barrier saved successfully");
		}
	}
}