using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace PassTheBarier.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowHomeCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<HomeViewModel>());
            ShowBarrierCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<BarrierViewModel>());
            ShowAddressBookCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<AddressBookViewModel>());
            ShowAboutCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<AboutViewModel>());
        }

        // MVVM Commands
        public IMvxCommand ShowHomeCommand { get; private set; }
        public IMvxCommand ShowBarrierCommand { get; private set; }
        public IMvxCommand ShowAddressBookCommand { get; private set; }
        public IMvxCommand ShowAboutCommand { get; private set; }
    }
}