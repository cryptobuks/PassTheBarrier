using System;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using PassTheBarier.Core.Messenger;
using PassTheBarier.Core.Navigation;

namespace PassTheBarier.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
	    private readonly IMvxMessenger _messenger;
	    private MvxSubscriptionToken _navigationSubscriptionToken;

		public MenuViewModel(IMvxNavigationService navigationService, IMvxMessenger messenger)
        {
            _navigationService = navigationService;
	        _messenger = messenger;

	        ShowHomeCommand = new MvxAsyncCommand(async () =>
            {
	            await _navigationService.Navigate<HomeViewModel>(new CleartStackPresentationBundle());
            });
            ShowBarrierCommand = new MvxAsyncCommand(async () =>
            {
	            await _navigationService.Navigate<BarrierViewModel>(new CleartStackPresentationBundle());
            });
            ShowAddressBookCommand = new MvxAsyncCommand(async () =>
            {
	            await _navigationService.Navigate<AddressBookViewModel>(new CleartStackPresentationBundle());
            });
			ShowSettingsCommand = new MvxAsyncCommand(async () =>
				{
					await _navigationService.Navigate<SettingsViewModel>(new CleartStackPresentationBundle());
				});
            ShowAboutCommand = new MvxAsyncCommand(async () =>
            {
	            await _navigationService.Navigate<AboutViewModel>(new CleartStackPresentationBundle());
            });
        }

        // MVVM Commands
        public IMvxCommand ShowHomeCommand { get; }
        public IMvxCommand ShowBarrierCommand { get; }
        public IMvxCommand ShowAddressBookCommand { get; }
		public IMvxCommand ShowSettingsCommand { get; }
        public IMvxCommand ShowAboutCommand { get; }

	    public void SetSubscription(Action<NavigationMessage> function)
	    {
		    _navigationSubscriptionToken = _messenger.Subscribe<NavigationMessage>(function);
	    }
	}
}