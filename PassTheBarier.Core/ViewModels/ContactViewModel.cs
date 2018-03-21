using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmValidation;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Models;
using PassTheBarier.Core.Logic.ResourcesFiles;
using PassTheBarier.Core.Logic.Utils;

namespace PassTheBarier.Core.ViewModels
{
    public class ContactViewModel : InputViewModel<ContactModel, ViewModelResult<ContactModel>>
    {
	    private readonly IModalLogic _modalLogic;
        private readonly IContactLogic _contactLogic;
        private readonly IMvxNavigationService _navigationService;

	    public IMvxCommand SaveContactCommand { get; }

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

	    private bool _addMode;

        public override void Prepare(ContactModel selectedContact)
        {
	        if (selectedContact == null)
	        {
		        _addMode = true;
				Contact = new ContactModel();
	        }
	        else
	        {
		        Contact = selectedContact;
			}
		}

	    private async Task SaveContact()
	    {
		    if (IsValid())
		    {
			    _modalLogic.DisplayLoading();
			    if (_addMode)
			    {
				    Contact = await _contactLogic.AddAsync(Contact);
			    }
			    else
			    {
				    Contact = await _contactLogic.UpdateAsync(Contact);
				}
				_modalLogic.HideLoading();
			    _modalLogic.DisplayToast(Messages.ContactSavedSuccessfully);

			    await _navigationService.Close(this, new ViewModelResult<ContactModel>(Contact));
		    }
		}

	    protected override bool IsValid()
	    {
			var validator = new ValidationHelper();
		    var regex = new Regex(Constants.NumberRegex);

		    validator.AddRequiredRule(() => Contact.Name, Messages.FieldRequired);

			validator.AddRequiredRule(() => Contact.Number, Messages.FieldRequired);
		    validator.AddRule(() => Contact.Number,
			    () => RuleResult.Assert(regex.IsMatch(Contact.Number), Messages.InvalidNumber));

		    var result = validator.ValidateAll();
		    Errors = result.AsObservableDictionary();

		    return result.IsValid;
		}
    }
}