using PassTheBarier.Core.Logic.Interfaces;

namespace PassTheBarier.Core.ViewModels
{
    public class AddressBookViewModel : BaseViewModel
    {
        private IContactLogic _contactLogic;

        public AddressBookViewModel(IContactLogic contactLogic)
        {
            _contactLogic = contactLogic;
        }
    }
}