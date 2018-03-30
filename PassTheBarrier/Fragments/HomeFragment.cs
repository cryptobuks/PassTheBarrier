using System;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Views.Attributes;
using PassTheBarier.Core.ViewModels;
using PassTheBarrier.Services;

namespace PassTheBarrier.Fragments
{
	[MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame)]
	[Register("passTheBarrier.droid.fragments.HomeFragment")]
	public class HomeFragment : BaseFragment<HomeViewModel>
	{
		protected override int FragmentId => Resource.Layout.HomeView;

		private const int PermissionRequestCode = 15;

		private Intent _serviceIntent;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = base.OnCreateView(inflater, container, savedInstanceState);

			_serviceIntent = new Intent(Activity, typeof(SmsService));
			var isRunning = IsServiceRunning(typeof(SmsService));
//			if (ViewModel.Barrier != null && ViewModel.Barrier.Enabled && !IsServiceRunning(typeof(SmsService)))
//			{
//				Activity.StartService(_serviceIntent);
//			}
			if (!IsServiceRunning(typeof(SmsService)))
			{
				Activity.StartService(_serviceIntent);
			}

			Point size = new Point();
			Activity.WindowManager.DefaultDisplay.GetSize(size);

			var startImageBtn = view.FindViewById<ImageButton>(Resource.Id.startServiceBtn);
			startImageBtn.LayoutParameters.Width = size.X * 45 / 100;
			startImageBtn.LayoutParameters.Height = size.X * 45 / 100;
			startImageBtn.Click += OnStartServiceClick;

			var stopImageBtn = view.FindViewById<ImageButton>(Resource.Id.stopServiceBtn);
			stopImageBtn.LayoutParameters.Width = size.X * 45 / 100;
			stopImageBtn.LayoutParameters.Height = size.X * 45 / 100;
			stopImageBtn.Click += OnStopServiceClick;

			return view;
		}

		public override void OnDestroy()
		{
			if (ViewModel.IsServiceRunning)
			{
				Activity.StopService(_serviceIntent);
			}
			base.OnDestroy();
		}

		private void OnStartServiceClick(object sender, EventArgs e)
		{
			if (!HasRequiredPermissionsGranted())
			{
				ActivityCompat.RequestPermissions(Activity,
					new[] {Manifest.Permission.ReceiveSms, Manifest.Permission.ReadSms, Manifest.Permission.CallPhone}, PermissionRequestCode);
			}
			else
			{
				ViewModel.TriggerBarrierStateCommand.Execute();
				if (!IsServiceRunning(typeof(SmsService)))
				{
					Activity.StartService(_serviceIntent);
				}
			}
		}

		private void OnStopServiceClick(object sender, EventArgs e)
		{
			Activity.StopService(_serviceIntent);
			ViewModel.TriggerBarrierStateCommand.Execute();
		}

		private bool IsServiceRunning(Type classTypeof)
		{
			ActivityManager manager = (ActivityManager) Activity.ApplicationContext.GetSystemService(Context.ActivityService);
			var runningServices = manager.GetRunningServices(int.MaxValue);
			var classTypeOfName = Java.Lang.Class.FromType(classTypeof).CanonicalName;

			foreach (var service in runningServices)
			{
				if (service.Service.ClassName == classTypeOfName)
				{
					return true;
				}
			}

			return false;
		}

		private bool HasRequiredPermissionsGranted()
		{
			return ContextCompat.CheckSelfPermission(Activity, Manifest.Permission.ReceiveSms) == (int) Permission.Granted
			       && ContextCompat.CheckSelfPermission(Activity, Manifest.Permission.ReadSms) == (int) Permission.Granted
			       && ContextCompat.CheckSelfPermission(Activity, Manifest.Permission.CallPhone) == (int) Permission.Granted;
		}
	}
}