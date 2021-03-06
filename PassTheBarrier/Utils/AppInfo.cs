﻿using Android.App;
using PassTheBarier.Core.Logic.Utils;

namespace PassTheBarrier.Utils
{
	public class AppInfo : IAppInfo
	{
		public int Version => Application.Context.ApplicationContext.PackageManager
			.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionCode;

		public string VersionName => Application.Context.ApplicationContext.PackageManager
			.GetPackageInfo(Application.Context.ApplicationContext.PackageName, 0).VersionName;
	}
}