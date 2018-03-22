using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using PassTheBarier.Core.Logic.Utils;

namespace PassTheBarier.Core.Navigation
{
	public class CleartStackPresentationBundle : MvxBundle
	{
		public CleartStackPresentationBundle()
			: base(new Dictionary<string, string>
			{
				[Constants.NavigationCommandKey] = Constants.NavigationCommandClearStackValue
			})
		{
		}
	}
}