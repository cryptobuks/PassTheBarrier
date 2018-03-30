using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Net;
using Android.Provider;
using Android.Telephony;
using Android.Util;

namespace PassTheBarrier.BroadcastReceivers
{
	[IntentFilter(new string[] { Telephony.Sms.Intents.SmsReceivedAction})]
	public class SmsReceiver : BroadcastReceiver
	{
		private readonly IEnumerable<string> _allowedContactNumbers;
		private readonly string _barrierNumber;
		private readonly string _barrierMessageText;
		private readonly bool _barrierEnabled;

		public SmsReceiver()
		{
			
		}

		public SmsReceiver(IEnumerable<string> allowedContactNumbers, string barrierNumber, string barrierMessageText, bool barrierEnabled)
		{
			_allowedContactNumbers = allowedContactNumbers;
			_barrierNumber = barrierNumber;
			_barrierMessageText = barrierMessageText;
			_barrierEnabled = barrierEnabled;
		}

		public override void OnReceive(Context context, Intent intent)
		{
			Log.Info("SmsReceiver", "RECEIVED SMS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			if (intent.Action != Telephony.Sms.Intents.SmsReceivedAction) return;

			SmsMessage[] messages = Telephony.Sms.Intents.GetMessagesFromIntent(intent);
			foreach (var t in messages)
			{
				var sender = t.OriginatingAddress;
				var message = t.MessageBody;
				if (_barrierEnabled && message == _barrierMessageText && _allowedContactNumbers.Any(c => c == sender))
//				if (message == _barrierMessageText)
				{
					//call 
					var callIntent = new Intent(Intent.ActionCall);
					callIntent.SetData(Uri.Parse("tel:" + _barrierNumber));
					context.StartActivity(callIntent);
				}
			}
		}
	}
}