using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace PassTheBarier.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowHomeViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<HomeViewModel>());
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MenuViewModel>());
        }

        // MVVM Commands
        public IMvxCommand ShowHomeViewModelCommand { get; private set; }
        public IMvxCommand ShowMenuViewModelCommand { get; private set; }
    }
}