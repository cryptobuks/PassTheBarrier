﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using MvvmValidation;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Models;
using PassTheBarier.Core.Logic.ResourcesFiles;
using PassTheBarier.Core.Logic.Utils;
using PassTheBarier.Core.Messenger;

namespace PassTheBarier.Core.ViewModels
{
    public class BarrierViewModel : InputViewModel
    {
	    private readonly IActionHelper _actionHelper;
	    private readonly IBarrierLogic _barrierLogic;
	    private readonly ISettingLogic _settingLogic;
	    private readonly IMvxNavigationService _navigationService;
	    private readonly IMvxMessenger _messenger;

		public IMvxCommand LoadBarrierCommand { get; }
		public IMvxCommand SaveBarrierCommand { get; }

		public BarrierViewModel(IBarrierLogic barrierLogic, ISettingLogic settingLogic, IActionHelper actionHelper, IMvxNavigationService navigationService, IMvxMessenger messenger)
		{
			_barrierLogic = barrierLogic;
			_settingLogic = settingLogic;
			_actionHelper = actionHelper;
			_navigationService = navigationService;
			_messenger = messenger;

			SaveBarrierCommand = new MvxAsyncCommand(() => _actionHelper.DoAction(SaveBarrier));
			LoadBarrierCommand = new MvxAsyncCommand(() => _actionHelper.DoAction(LoadBarrier));
			NumberPrefixes = NumberPrefixProvider.GetNumberPrefixes();
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

		private string _messageText;

	    public string MessageText
	    {
		    get => _messageText;
		    set
		    {
			    _messageText = value;
				RaisePropertyChanged(() => MessageText);
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

		private BarrierModel _barrier;
	    private bool _isSubmitted;

	    public override Task Initialize()
	    {
			LoadBarrierCommand.Execute();

			return base.Initialize();
	    }

	    private async Task LoadBarrier()
		{
			var barrier = await _barrierLogic.GetBarrierAsync();
			if (barrier == null)
			{
				barrier = new BarrierModel();
				var settings = await _settingLogic.GetSettingsAsync();
				NumberPrefix = settings.NumberPrefix;
			}
			else
			{
				NumberPrefix = barrier.NumberPrefix;
			}

			_barrier = barrier;
			Number = _barrier.Number;
			MessageText = _barrier.MessageText;
		}

		private async Task SaveBarrier()
		{
			_isSubmitted = true;
			if (IsValid())
			{
				_barrier.Number = Number;
				_barrier.MessageText = MessageText;
				_barrier.NumberPrefix = NumberPrefix;
				await _barrierLogic.SaveBarrierAsync(_barrier);
				var message = new BarrierMessage(
					this,
					_barrier
				);
				_messenger.Publish(message);
				_actionHelper.DisplayToast(Messages.BarrierSavedSuccessfully);

				GoBack();
			}
		}

	    protected override bool IsValid()
	    {
		    var validator = new ValidationHelper();
		    var regex = new Regex(Constants.NumberRegex);

		    validator.AddRule(() => Number,
			    () => RuleResult.Assert(!string.IsNullOrWhiteSpace(Number), Messages.FieldRequired));
			validator.AddRule(() => Number,
			    () => RuleResult.Assert(regex.IsMatch(Number), Messages.InvalidNumber));

		    validator.AddRule(() => MessageText, 
			    () => RuleResult.Assert(!string.IsNullOrWhiteSpace(MessageText), Messages.FieldRequired));
			
		    var result = validator.ValidateAll();
		    Errors = result.AsObservableDictionary();

		    return result.IsValid;
		}

	    //TODO: should be refactored
		private async void GoBack()
	    {
		    await _navigationService.Close(this);
		    _messenger.Publish(new NavigationMessage(this));
		}
    }
}