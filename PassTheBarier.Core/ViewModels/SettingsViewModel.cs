using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Models;
using PassTheBarier.Core.Logic.ResourcesFiles;
using PassTheBarier.Core.Logic.Utils;

namespace PassTheBarier.Core.ViewModels
{
	public class SettingsViewModel : BaseViewModel
	{
		private readonly IActionHelper _actionHelper;
		private readonly IMvxNavigationService _navigationService;
		private readonly ISettingLogic _settingLogic;

		public IMvxCommand LoadSettingsCommand { get; }
		public IMvxCommand SaveSettingsCommand { get; }

		public SettingsViewModel(ISettingLogic settingLogic, IActionHelper actionHelper, IMvxNavigationService navigationService)
		{
			_settingLogic = settingLogic;
			_actionHelper = actionHelper;
			_navigationService = navigationService;

			LoadSettingsCommand = new MvxAsyncCommand(() => _actionHelper.DoAction(LoadSettings));
			SaveSettingsCommand = new MvxAsyncCommand(() => _actionHelper.DoAction(SaveSettings));
			NumberPrefixes = NumberPrefixProvider.GetNumberPrefixes();
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

		private SettingsModel _settings;

		public override Task Initialize()
		{
			LoadSettingsCommand.Execute();

			return base.Initialize();
		}

		private async Task LoadSettings()
		{
			_settings = await _settingLogic.GetSettingsAsync();
			NumberPrefix = _settings.NumberPrefix;
		}

		private async Task SaveSettings()
		{
			_settings.NumberPrefix = NumberPrefix;
			await _settingLogic.SaveSettingsAsync(_settings);

			_actionHelper.DisplayToast(Messages.SettingsSavedSuccessfully);
			await _navigationService.Close(this);
		}
	}
}