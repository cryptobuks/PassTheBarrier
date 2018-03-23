using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Views.Attributes;
using PassTheBarier.Core.ViewModels;
using PassTheBarrier.Activities;

namespace PassTheBarrier.Fragments
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.navigation_frame)]
    [Register("passTheBarrier.droid.fragments.MenuFragment")]
    public class MenuFragment : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private NavigationView _navigationView;
        private IMenuItem _previousMenuItem;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.MenuView, null);

            _navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_view);
            _navigationView.SetNavigationItemSelectedListener(this);

			var homeMenuItem = _navigationView.Menu.FindItem(Resource.Id.nav_home);
	        homeMenuItem.SetCheckable(true);
	        homeMenuItem.SetChecked(true);

            return view;
        }

        public bool OnNavigationItemSelected(IMenuItem menuItem)
        {
            if (_previousMenuItem != null)
            {
                _previousMenuItem.SetChecked(false);
            }

            menuItem.SetCheckable(true);
            menuItem.SetChecked(true);

            _previousMenuItem = menuItem;

            Navigate(menuItem.ItemId);

            return true;
        }

        private async Task Navigate(int itemId)
        {
            ((MainActivity)Activity).DrawerLayout.CloseDrawers();
            await Task.Delay(TimeSpan.FromMilliseconds(250));

            switch (itemId)
            {
                case Resource.Id.nav_home:
                    ViewModel.ShowHomeCommand.Execute(null);
                    break;
                case Resource.Id.nav_barrier:
                    ViewModel.ShowBarrierCommand.Execute(null);
                    break;
                case Resource.Id.nav_addressBook:
                    ViewModel.ShowAddressBookCommand.Execute(null);
                    break;
                case Resource.Id.nav_about:
                    ViewModel.ShowAboutCommand.Execute(null);
                    break;
            }
        }
    }
}