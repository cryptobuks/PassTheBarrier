using PassTheBarier.Core.Data;
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
		private ISettingRepository _settingRepository;

		public BarrierLogic(ISettingRepository settingRepository)
		{
			_settingRepository = settingRepository;
		}

		public BarrierModel GetBarrier()
		{
			var barrierSetting = _settingRepository.GetByKey(DataConstants.BarrierKey);

			return BarrierMapper.ToModel(barrierSetting);
		}

		public async Task<BarrierModel> GetBarrierAsync()
		{
			var barrierSetting = await _settingRepository.GetByKeyAsync(DataConstants.BarrierKey);

			return BarrierMapper.ToModel(barrierSetting);
		}

		public void SaveBarrier(BarrierModel barrier)
		{
			var barrierSetting = _settingRepository.GetByKey(DataConstants.BarrierKey);
			if (barrierSetting == null)
			{
				barrierSetting = new Setting
				{
					Key = DataConstants.BarrierKey,
					Value = barrier.Number
				};
				_settingRepository.Add(barrierSetting);
			}
			else
			{
				barrierSetting.Value = barrier.Number;
				_settingRepository.Update(barrierSetting);
			}
		}

		public async Task SaveBarrierAsync(BarrierModel barrier)
		{
			var barrierSetting = await _settingRepository.GetByKeyAsync(DataConstants.BarrierKey);
			if (barrierSetting == null)
			{
				barrierSetting = new Setting
				{
					Key = DataConstants.BarrierKey,
					Value = barrier.Number
				};
				await _settingRepository.AddAsync(barrierSetting);
			}
			else
			{
				barrierSetting.Value = barrier.Number;
				await _settingRepository.UpdateAsync(barrierSetting);
			}
		}
	}
}