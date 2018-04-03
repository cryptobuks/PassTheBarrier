using System;
using Android.App;
using Android.Content;

namespace PassTheBarrier.Extensions
{
	public static class ContextExtensions
	{
		public static bool IsServiceRunning(this Context context, Type serviceType)
		{
			ActivityManager manager = (ActivityManager) context.GetSystemService(Context.ActivityService);
			var runningServices = manager.GetRunningServices(int.MaxValue);
			var classTypeOfName = Java.Lang.Class.FromType(serviceType).CanonicalName;

			foreach (var service in runningServices)
			{
				if (service.Service.ClassName == classTypeOfName)
				{
					return true;
				}
			}

			return false;
		}
	}
}