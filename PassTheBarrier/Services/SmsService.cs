using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using PassTheBarier.Core.Logic.Utils;
using PassTheBarrier.BroadcastReceivers;

namespace PassTheBarrier.Services
{
	[Service(Name = "passTheBarrier.services.smsService", Label = "SmsService", Enabled = true)]
	public class SmsService : Service
	{
		private SmsReceiver _smsReceiver;

		public override IBinder OnBind(Intent intent)
		{
			return null;
		}

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			var isStarted = intent.GetBooleanExtra(Constants.SmsServiceIsStartedIntent, false);
			if (!isStarted)
			{
				var contacts = intent.GetBundleExtra(Constants.ContactsBundleIntentExtras)
					.GetStringArrayList(Constants.ContactNumbersIntentExtras);
				var barrierNumber = intent.GetStringExtra(Constants.BarrierNumberIntentExtras);
				var barrierMessageText = intent.GetStringExtra(Constants.BarrierTextIntentExtras);

				_smsReceiver = new SmsReceiver(contacts, barrierNumber, barrierMessageText);
				RegisterReceiver(_smsReceiver, new IntentFilter(Telephony.Sms.Intents.SmsReceivedAction));
			}

			return StartCommandResult.Sticky;
		}

		public override void OnDestroy()
		{
			UnregisterReceiver(_smsReceiver);
			_smsReceiver = null;

			base.OnDestroy();
		}
	}
}