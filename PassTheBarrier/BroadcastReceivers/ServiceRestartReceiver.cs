using Android.App;
using Android.Content;
using PassTheBarrier.Extensions;
using PassTheBarrier.Services;

namespace PassTheBarrier.BroadcastReceivers
{
	[BroadcastReceiver(Name = "passTheBarrier.broadcastReceivers.serviceRestartReceiver", Enabled = true)]
	[IntentFilter(new[] {"passTheBarrier.intents.serviceRestart"})]
	public class ServiceRestartReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			if (!context.IsServiceRunning(typeof(SmsService)))
			{
				var startServiceIntent = new Intent(context, typeof(SmsService));
				startServiceIntent.PutExtra(context.Resources.GetString(Resource.String.startIntentExtra), true);
				context.StartService(startServiceIntent);
			}
		}
	}
}