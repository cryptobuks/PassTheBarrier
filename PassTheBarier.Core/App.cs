using Acr.UserDialogs;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using PassTheBarier.Core.Data.Repositories.Implementations;
using PassTheBarier.Core.Data.Repositories.Interfaces;
using PassTheBarier.Core.Logic.Implementations;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Utils;
using PassTheBarier.Core.ViewModels;

namespace PassTheBarier.Core
{
	public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

			Mvx.RegisterSingleton(() => UserDialogs.Instance);
	        Mvx.LazyConstructAndRegisterSingleton<IMvxMessenger, MvxMessengerHub>();
			InitializeData();
            InitializeLogic();
            InitializeStartNavigation();
        }

        private void InitializeLogic()
        {
	        Mvx.RegisterType<IActionHelper, ActionHelper>();
            Mvx.RegisterType<IContactLogic, ContactLogic>();
			Mvx.RegisterType<IBarrierLogic, BarrierLogic>();
		}

		private void InitializeData()
        {
            Mvx.RegisterType<IContactRepository, ContactRepository>();
			Mvx.RegisterType<IBarrierRepository, BarrierRepository>();
		}

		private void InitializeStartNavigation()
        {
            RegisterNavigationServiceAppStart<MainViewModel>();
        }
    }
}