using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using PassTheBarier.Core.Navigation;

namespace PassTheBarier.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

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
    }
}