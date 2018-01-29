using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using PassTheBarier.Core.Data;
using PassTheBarier.Core.Data.Repositories.Implementations;
using PassTheBarier.Core.Data.Repositories.Interfaces;
using PassTheBarier.Core.Logic.Implementations;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.ViewModels;

namespace PassTheBarier.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            InitializeData();
            InitializeLogic();
            InitializeStartNavigation();
        }

        private void InitializeLogic()
        {
            Mvx.RegisterType<IContactLogic, ContactLogic>();
        }

        private void InitializeData()
        {
            Mvx.RegisterType<ISqliteConnectionFactory, SqliteConnectionFactory>();
            Mvx.RegisterType<IContactRepository, ContactRepository>();
        }

        private void InitializeStartNavigation()
        {
            RegisterNavigationServiceAppStart<MainViewModel>();
        }
    }
}