using MvvmCross.Plugins.Messenger;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.Messenger
{
	public class BarrierMessage : MvxMessage
	{
		public BarrierModel Barrier { get; set; }

		public BarrierMessage(object sender, BarrierModel barrier) : base(sender)
		{
			Barrier = barrier;
		}
	}
}