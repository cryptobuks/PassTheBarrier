namespace PassTheBarier.Core.Logic.Utils
{
	public static class Constants
	{
		public const string NumberRegex = @"^[0-9]+$";
		public const string NavigationCommandKey = "NavigationCommand";
		public const string NavigationCommandClearStackValue = "StackClear";
		public const string SmsServiceIsStartedIntent = "started";
		public const string PhoneCallIntentNumberInfo = "number";
		public const string BarrierNumberIntentExtras = "barrierNumber";
		public const string BarrierTextIntentExtras = "barrierText";
		public const string ContactNumbersIntentExtras = "contacts";
		public const string ContactsBundleIntentExtras = "contactsBundle";
	}
}