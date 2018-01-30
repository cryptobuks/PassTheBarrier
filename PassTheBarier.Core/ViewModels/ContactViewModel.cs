using MvvmCross.Core.Navigation;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.ViewModels
{
    public class ContactViewModel : BaseViewModel<ContactModel>
    {
        private IContactLogic _contactLogic;
        private IMvxNavigationService _navigationService;

        public ContactViewModel(IContactLogic contactLogic, IMvxNavigationService navigationService)
        {
            _contactLogic = contactLogic;
            _navigationService = navigationService;
        }

        private ContactModel _contact;
        public ContactModel Contact
        {
            get { return _contact; }
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
    }
}