using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmValidation;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Models;
using PassTheBarier.Core.Logic.ResourcesFiles;
using PassTheBarier.Core.Logic.Utils;
using PassTheBarier.Core.Navigation;

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

	    private string _name;
	    public string Name
	    {
		    get => _name;
		    set
		    {
			    _name = value;
				RaisePropertyChanged(() => Name);
			    if (_isSubmitted)
			    {
				    IsValid();
			    }
		    }
	    }

	    private string _number;
	    public string Number
	    {
			get => _number;
		    set
		    {
			    _number = value;
				RaisePropertyChanged(() => Number);
			    if (_isSubmitted)
			    {
				    IsValid();
			    }
		    }
	    }

	    private ContactModel _contact;
		private bool _addMode;
	    private bool _isSubmitted;

        public override void Prepare(ContactModel selectedContact)
        {
	        if (selectedContact == null)
	        {
		        _addMode = true;
				_contact = new ContactModel();
			}
	        else
	        {
		        _contact = selectedContact;
	        }
	        Name = _contact.Name;
	        Number = _contact.Number;
		}

	    private async Task SaveContact()
	    {
		    _isSubmitted = true;

			if (IsValid())
		    {
			    _modalLogic.DisplayLoading();
			    if (_addMode)
			    {
				    _contact = await _contactLogic.AddAsync(_contact);
			    }
			    else
			    {
				    _contact = await _contactLogic.UpdateAsync(_contact);
				}
				_modalLogic.HideLoading();
			    _modalLogic.DisplayToast(Messages.ContactSavedSuccessfully);

			    await _navigationService.Close(this, new ViewModelResult<ContactModel>(_contact));
		    }
		}

	    protected override bool IsValid()
	    {
			var validator = new ValidationHelper();
		    var regex = new Regex(Constants.NumberRegex);

		    validator.AddRequiredRule(() => Name, Messages.FieldRequired);

			validator.AddRequiredRule(() => Number, Messages.FieldRequired);
		    validator.AddRule(() => Number,
			    () => RuleResult.Assert(regex.IsMatch(Number), Messages.InvalidNumber));

		    var result = validator.ValidateAll();
		    Errors = result.AsObservableDictionary();

		    return result.IsValid;
		}
    }
}