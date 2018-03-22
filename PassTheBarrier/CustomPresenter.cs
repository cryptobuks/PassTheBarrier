using System.Collections.Generic;
using System.Reflection;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using PassTheBarier.Core.Logic.Utils;

namespace PassTheBarrier
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
				while (CurrentFragmentManager.BackStackEntryCount > 0)
				{
					CurrentFragmentManager.PopBackStackImmediate();
				}
			}
			base.Show(request);
		}
	}
}