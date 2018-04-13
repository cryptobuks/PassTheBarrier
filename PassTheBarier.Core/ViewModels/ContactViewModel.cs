using System.Collections.Generic;
using System.Linq;
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
		private readonly IActionHelper _actionHelper;
		private readonly IContactLogic _contactLogic;
		private readonly IMvxNavigationService _navigationService;

		public IMvxCommand SaveContactCommand { get; }

		public ContactViewModel(IActionHelper actionHelper, IContactLogic contactLogic, IMvxNavigationService navigationService)
		{
			_contactLogic = contactLogic;
			_navigationService = navigationService;
			_actionHelper = actionHelper;

			SaveContactCommand = new MvxAsyncCommand(() => _actionHelper. DoAction(SaveContact));
			NumberPrefixes = NumberPrefixProvider.GetNumberPrefixes();
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

		private NumberPrefixModel _numberPrefix;

		public NumberPrefixModel NumberPrefix
		{
			get => _numberPrefix;
			set
			{
				_numberPrefix = value;
				RaisePropertyChanged(() => NumberPrefix);
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

		private IList<NumberPrefixModel> _numberPrefixes;

		public IList<NumberPrefixModel> NumberPrefixes
		{
			get => _numberPrefixes;
			set
			{
				_numberPrefixes = value;
				RaisePropertyChanged(() => NumberPrefixes);
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
				NumberPrefix = NumberPrefixes.FirstOrDefault();
			}
			else
			{
				_contact = selectedContact;
				NumberPrefix = _contact.NumberPrefix;
			}

			Name = _contact.Name;
			Number = _contact.Number;
		}

		private async Task SaveContact()
		{
			_isSubmitted = true;

			if (IsValid())
			{
				_contact.Number = Number;
				_contact.Name = Name;
				_contact.NumberPrefix = NumberPrefix;

				if (_addMode)
				{
					_contact = await _contactLogic.AddAsync(_contact);
				}
				else
				{
					_contact = await _contactLogic.UpdateAsync(_contact);
				}

				_actionHelper.DisplayToast(Messages.ContactSavedSuccessfully);

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