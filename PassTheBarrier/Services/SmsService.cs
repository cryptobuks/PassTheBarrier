using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Util;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarrier.BroadcastReceivers;

namespace PassTheBarrier.Services
{
	[Service(Name = "passTheBarrier.services.smsService", Label = "SmsService", Enabled = true)]
	public class SmsService : Service
	{
		private SmsReceiver _smsReceiver;

		private IContactLogic _contactLogic;
		private IBarrierLogic _barrierLogic;

		public override IBinder OnBind(Intent intent)
		{
			return null;
		}

		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
		{
			base.OnStartCommand(intent, flags, startId);

			var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(this.ApplicationContext);
			setup.EnsureInitialized();
			Log.Info("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb", "Resolving barrierLogic");
			_barrierLogic = Mvx.Resolve<IBarrierLogic>();
			_contactLogic = Mvx.Resolve<IContactLogic>();
			Log.Info("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb", "Resolved barrierLogic");
			//			_smsReceiver = new SmsReceiver();
			var barrier = _barrierLogic.GetBarrier();
			var contacts = _contactLogic.GetAll();
			_smsReceiver = new SmsReceiver(contacts.Select(c => c.Number), barrier.Number, barrier.MessageText, barrier.Enabled);
			RegisterReceiver(_smsReceiver, new IntentFilter(Telephony.Sms.Intents.SmsReceivedAction));
			Log.Info("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Starting service and registering receiver");
//			}

			return StartCommandResult.Sticky;
		}

		public override void OnDestroy()
		{
			UnregisterReceiver(_smsReceiver);
			_smsReceiver = null;
			base.OnDestroy();

			Log.Info("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "DESTROYING SERVICE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			Intent restartServiceIntent = new Intent("passTheBarrier.intents.restart");
			SendBroadcast(restartServiceIntent);
			Log.Info("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "SENDING BROADCAST TO TESTRECEIVER!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
		}
	}
}