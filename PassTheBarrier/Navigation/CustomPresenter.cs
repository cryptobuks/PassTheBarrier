using System.Collections.Generic;
using System.Reflection;
using Android.Support.V4.App;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using PassTheBarier.Core.Logic.Utils;

namespace PassTheBarrier.Navigation
{
	public class CustomPresenter : MvxAppCompatViewPresenter
	{
		public CustomPresenter(IEnumerable<Assembly> androidViewAssemblies) : base(androidViewAssemblies)
		{
		}

		public override void Show(MvxViewModelRequest request)
		{
			if (request.PresentationValues?[Constants.NavigationCommandKey] == Constants.NavigationCommandClearStackValue)
			{
				for (int i = 0; i < CurrentFragmentManager.BackStackEntryCount; i++)
				{
					CurrentFragmentManager.PopBackStack(null, FragmentManager.PopBackStackInclusive);
				}
			}

			base.Show(request);
		}
	}
}