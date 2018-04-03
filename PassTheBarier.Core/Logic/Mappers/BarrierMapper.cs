using PassTheBarier.Core.Data.Entities;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.Logic.Mappers
{
	public static class BarrierMapper
	{
		public static BarrierModel ToModel(Barrier barrier)
		{
			if (barrier == null)
			{
				return null;
			}

			return new BarrierModel
			{
				Id = barrier.Id,
				Number = barrier.Number,
				MessageText = barrier.MessageText
			};
		}

		public static Barrier ToEntity(BarrierModel barrier)
		{
			if (barrier == null)
			{
				return null;
			}

			return new Barrier
			{
				Id = barrier.Id,
				Number = barrier.Number,
				MessageText = barrier.MessageText
			};
		}
	}
}