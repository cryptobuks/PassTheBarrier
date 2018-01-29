using MvvmCross.Core.ViewModels;
using PassTheBarier.Core.ViewModels;

namespace PassTheBarier.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            InitializeLogic();
            InitializeData();
            InitializeStartNavigation();
        }

        private void InitializeLogic()
        {

        }

        private void InitializeData()
        {

        }

        private void InitializeStartNavigation()
        {
            RegisterNavigationServiceAppStart<MainViewModel>();
        }
    }
}