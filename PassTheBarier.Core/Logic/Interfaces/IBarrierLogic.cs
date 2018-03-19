using PassTheBarier.Core.Logic.Models;
using System.Threading.Tasks;

namespace PassTheBarier.Core.Logic.Interfaces
{
	public interface IBarrierLogic
	{
		BarrierModel GetBarrier();

		Task<BarrierModel> GetBarrierAsync();

		void SaveBarrier(BarrierModel barrier);

		Task SaveBarrierAsync(BarrierModel barrier);
	}
}