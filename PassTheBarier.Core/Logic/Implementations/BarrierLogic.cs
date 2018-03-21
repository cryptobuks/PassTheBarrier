using System.Linq;
using PassTheBarier.Core.Data.Entities;
using PassTheBarier.Core.Data.Repositories.Interfaces;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Mappers;
using PassTheBarier.Core.Logic.Models;
using System.Threading.Tasks;

namespace PassTheBarier.Core.Logic.Implementations
{
	public class BarrierLogic : IBarrierLogic
	{
		private readonly IBarrierRepository _barrierRepository;

		public BarrierLogic(IBarrierRepository barrierRepository)
		{
			_barrierRepository = barrierRepository;
		}

		public BarrierModel GetBarrier()
		{
			return BarrierMapper.ToModel(GetBarrierEntity());
		}

		public async Task<BarrierModel> GetBarrierAsync()
		{
			return BarrierMapper.ToModel(await GetBarrierEntityAsync());
		}

		public void SaveBarrier(BarrierModel barrierModel)
		{
			var barrier = GetBarrierEntity();

			if (barrier == null)
			{
				_barrierRepository.Add(BarrierMapper.ToEntity(barrierModel));
			}
			else
			{
				_barrierRepository.Update(BarrierMapper.ToEntity(barrierModel));
			}
		}

		public async Task SaveBarrierAsync(BarrierModel barrierModel)
		{
			var barrier = await GetBarrierEntityAsync();
			if (barrier == null)
			{
				await _barrierRepository.AddAsync(BarrierMapper.ToEntity(barrierModel));
			}
			else
			{
				await _barrierRepository.UpdateAsync(BarrierMapper.ToEntity(barrierModel));
			}
		}

		private Barrier GetBarrierEntity()
		{
			var barrier = _barrierRepository.GetAll().FirstOrDefault();

			return barrier;
		}

		private async Task<Barrier> GetBarrierEntityAsync()
		{
			var barriers = await _barrierRepository.GetAllAsync();
			var barrier = barriers.FirstOrDefault();

			return barrier;
		}
	}
}