using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Net;
using Android.Provider;
using Android.Telephony;

namespace PassTheBarrier.BroadcastReceivers
{
	[IntentFilter(new string[] { Telephony.Sms.Intents.SmsReceivedAction}, Priority = 1000)]
	public class SmsReceiver : BroadcastReceiver
	{
		private readonly IEnumerable<string> _allowedContactNumbers;
		private readonly string _barrierNumber;
		private readonly string _barrierMessageText;

		public SmsReceiver(IEnumerable<string> allowedContactNumbers, string barrierNumber, string barrierMessageText)
		{
			_allowedContactNumbers = allowedContactNumbers;
			_barrierNumber = barrierNumber;
			_barrierMessageText = barrierMessageText;
		}

		public override void OnReceive(Context context, Intent intent)
		{
			if (intent.Action != Telephony.Sms.Intents.SmsReceivedAction) return;

			SmsMessage[] messages = Telephony.Sms.Intents.GetMessagesFromIntent(intent);
			foreach (var t in messages)
			{
				var sender = t.OriginatingAddress;
				var message = t.MessageBody;
				if (message == _barrierMessageText && _allowedContactNumbers.Any(c => c == sender))
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