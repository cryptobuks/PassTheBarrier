using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Views.InputMethods;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using PassTheBarier.Core.ViewModels;
using PassTheBarrier.Extensions;
using PassTheBarrier.Fragments;
using PassTheBarrier.Services;

namespace PassTheBarrier.Activities
{
    [Activity(Label = "Pass the barrier",
        Theme = "@style/PassTheBarrier.Main",
        LaunchMode = LaunchMode.SingleTop,
        Name = "passTheBarrier.droid.activities.MainActivity")]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>
    {
        public DrawerLayout DrawerLayout { get; set; }

		protected override void OnCreate(Bundle bundle)
        {
	        this.RequestedOrientation = ScreenOrientation.Portrait;

			base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.MainView);
			UserDialogs.Init(() => Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity);

			DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            if (bundle == null)
            {
	            ViewModel.ShowMenuViewModelCommand.Execute(null);
				ViewModel.ShowHomeViewModelCommand.Execute(null);
            }
		}

        public override void OnBackPressed()
        {
            if (DrawerLayout != null && DrawerLayout.IsDrawerOpen(GravityCompat.Start))
            {
                DrawerLayout.CloseDrawers();
            }
            else
            {
	            var supportFragmentsCount = SupportFragmentManager.BackStackEntryCount;

				if (supportFragmentsCount == 1)
				{
					var menuFragment = (MenuFragment)SupportFragmentManager.FindFragmentById(Resource.Id.navigation_frame);
					var navigationMenu = menuFragment.View.FindViewById<NavigationView>(Resource.Id.navigation_view);

					menuFragment.MarkSelectedMenuItem(navigationMenu.Menu.FindItem(Resource.Id.nav_home));
				}

	            base.OnBackPressed();
			}
		}

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    DrawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public void HideSoftKeyboard()
        {
            if (CurrentFocus == null)
                return;

            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

            CurrentFocus.ClearFocus();
        }

	    protected override void OnDestroy()
	    {
			if (this.IsServiceRunning(typeof(SmsService)))
		    {
			    StopService(new Intent(this, typeof(SmsService)));
		    }
		    base.OnDestroy();
	    }
	}
}