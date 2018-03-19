using PassTheBarier.Core.Data.Entities;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.Logic.Mappers
{
	public static class BarrierMapper
	{
		public static BarrierModel ToModel(Setting setting)
		{
			if (setting == null)
			{
				return null;
			}

			return new BarrierModel
			{
				Number = setting.Value
			};
		}
	}
}