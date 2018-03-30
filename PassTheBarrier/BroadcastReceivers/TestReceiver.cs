using Android.App;
using Android.Content;
using Android.Util;
using PassTheBarrier.Services;

namespace PassTheBarrier.BroadcastReceivers
{
	//	[BroadcastReceiver(Name = "passTheBarrier.broadcastReceivers.testReceiver", Enabled = true, Exported = true)]
	[IntentFilter(new string[] { "passTheBarrier.intents.restart" })]
	public class TestReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			Log.Info("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Starting server");
			context.StartService(new Intent(context, typeof(SmsService)));
		}
	}
}