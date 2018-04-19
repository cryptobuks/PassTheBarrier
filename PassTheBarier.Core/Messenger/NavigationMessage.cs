using MvvmCross.Plugins.Messenger;

namespace PassTheBarier.Core.Messenger
{
	public class NavigationMessage : MvxMessage
	{
		public NavigationMessage(object sender) : base(sender)
		{
		}
	}
}