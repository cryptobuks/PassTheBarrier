using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Support.V4.App;
using PassTheBarrier.Activities;
using PassTheBarrier.BroadcastReceivers;

namespace PassTheBarrier.Services
{
	[Service(Name = "passTheBarrier.services.smsService", Label = "SmsService", Enabled = true)]
	public class SmsService : Service
	{
		private const int NotificationId = 5555;
		private bool _isServiceRunning;
		private SmsReceiver _smsReceiver;

		public override IBinder OnBind(Intent intent)
		{
			return null;
		}

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			base.OnStartCommand(intent, flags, startId);

			var toStart = intent.GetBooleanExtra(GetString(Resource.String.startIntentExtra), true);
			if (toStart)
			{
				Intent notificationIntent = new Intent(ApplicationContext, typeof(MainActivity));
				notificationIntent.SetAction(Intent.ActionMain);
				notificationIntent.AddCategory(Intent.CategoryLauncher);
				notificationIntent.AddFlags(ActivityFlags.NewTask);

				PendingIntent pendingIntent =
					PendingIntent.GetActivity(this, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);

				NotificationCompat.Builder builder = new NotificationCompat.Builder(this.ApplicationContext)
					.SetWhen(DateTime.Now.Millisecond)
					.SetSmallIcon(Resource.Drawable.barrier_icon4)
					.SetContentTitle(GetString(Resource.String.serviceRunningNotificationTitle))
					.SetContentText(GetString(Resource.String.serviceRunningNotificationText))
					.SetContentIntent(pendingIntent)
					.SetOngoing(true);

				StartForeground(NotificationId, builder.Build());

				_smsReceiver = new SmsReceiver();
				RegisterReceiver(_smsReceiver, new IntentFilter(Telephony.Sms.Intents.SmsReceivedAction));
				_isServiceRunning = true;
			}
			else
			{
				StopSelf();
				_isServiceRunning = false;
			}

			return StartCommandResult.Sticky;
		}

		public override void OnDestroy()
		{
			base.OnDestroy();
			UnregisterReceiver(_smsReceiver);
			_smsReceiver = null;

			if (_isServiceRunning)
			{
				Intent restartServiceIntent = new Intent("passTheBarrier.intents.serviceRestart");
				SendBroadcast(restartServiceIntent);
			}
			else
			{
				_isServiceRunning = false;
			}
		}
	}
}