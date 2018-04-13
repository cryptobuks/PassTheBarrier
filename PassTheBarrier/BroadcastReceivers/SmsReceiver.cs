using System.Linq;
using Android.App;
using Android.Content;
using Android.Provider;
using Android.Telephony;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;
using PassTheBarier.Core.Logic.Interfaces;
using Uri = Android.Net.Uri;

namespace PassTheBarrier.BroadcastReceivers
{
	[IntentFilter(new[] {Telephony.Sms.Intents.SmsReceivedAction})]
	public class SmsReceiver : BroadcastReceiver
	{
		public override void OnReceive(Context context, Intent intent)
		{
			if (intent.Action != Telephony.Sms.Intents.SmsReceivedAction)
			{
				return;
			}

			var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(context);
			setup.EnsureInitialized();
			var barrierLogic = Mvx.Resolve<IBarrierLogic>();
			var contactLogic = Mvx.Resolve<IContactLogic>();

			var barrier = barrierLogic.GetBarrier();
			var contacts = contactLogic.GetAll();

			SmsMessage[] messages = Telephony.Sms.Intents.GetMessagesFromIntent(intent);
			foreach (var t in messages)
			{
				var sender = t.OriginatingAddress;
				var message = t.MessageBody;
				if (message == barrier.MessageText && contacts.Any(c => c.NumberPrefix.Prefix + c.Number == sender))
				{
					var callIntent = new Intent(Intent.ActionCall);
					callIntent.SetData(Uri.Parse("tel:" + barrier.FullNumber));
					context.StartActivity(callIntent);
				}
			}
		}
	}
}