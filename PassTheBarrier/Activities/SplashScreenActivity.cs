using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;
using MvvmCross.Droid.Views.Attributes;

namespace PassTheBarrier.Activities
{
    [MvxActivityPresentation]
    [Activity(
        MainLauncher = true,
        Theme = "@style/PassTheBarrier.Splash", 
        NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreenActivity : MvxSplashScreenActivity
    {
        public SplashScreenActivity()
            : base(Resource.Layout.SplashScreen)
        {

        }
    }
}