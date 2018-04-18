using System.Linq;
using System.Threading.Tasks;
using PassTheBarier.Core.Data;
using PassTheBarier.Core.Data.Entities;
using PassTheBarier.Core.Data.Repositories.Interfaces;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Models;
using PassTheBarier.Core.Logic.Utils;

namespace PassTheBarier.Core.Logic.Implementations
{
	public class SettingLogic : ISettingLogic
	{
		private readonly ISettingRepository _settingRepository;

		public SettingLogic(ISettingRepository settingRepository)
		{
			_settingRepository = settingRepository;
		}

		public SettingsModel GetSettings()
		{
			var settingsModel = new SettingsModel();

			var defaultNumberPrefixSetting = _settingRepository.GetByKey(DataConstants.SettingDefaultNumberPrefixKey);
			if (defaultNumberPrefixSetting == null)
			{
				settingsModel.NumberPrefix = NumberPrefixProvider.GetNumberPrefixes().FirstOrDefault();
			}
			else
			{
				settingsModel.NumberPrefix = NumberPrefixProvider.GetById(int.Parse(defaultNumberPrefixSetting.Value));
			}

			return settingsModel;
		}

		public async Task<SettingsModel> GetSettingsAsync()
		{
			var settingsModel = new SettingsModel();

			var defaultNumberPrefixSetting = await _settingRepository.GetByKeyAsync(DataConstants.SettingDefaultNumberPrefixKey);
			if (defaultNumberPrefixSetting == null)
			{
				settingsModel.NumberPrefix = NumberPrefixProvider.GetNumberPrefixes().FirstOrDefault();
			}
			else
			{
				settingsModel.NumberPrefix = NumberPrefixProvider.GetById(int.Parse(defaultNumberPrefixSetting.Value));
			}

			return settingsModel;
		}

		public void SaveSettings(SettingsModel settings)
		{
			var defaultNumberPrefixSetting = _settingRepository.GetByKey(DataConstants.SettingDefaultNumberPrefixKey);
			if (defaultNumberPrefixSetting == null)
			{
				defaultNumberPrefixSetting = new Setting
				{
					Key = DataConstants.SettingDefaultNumberPrefixKey,
					Value = settings.NumberPrefix.Id.ToString()
				};
				_settingRepository.Add(defaultNumberPrefixSetting);
			}
			else
			{
				defaultNumberPrefixSetting.Value = settings.NumberPrefix.Id.ToString();
				_settingRepository.Update(defaultNumberPrefixSetting);
			}
		}

		public async Task SaveSettingsAsync(SettingsModel settings)
		{
			var defaultNumberPrefixSetting = await _settingRepository.GetByKeyAsync(DataConstants.SettingDefaultNumberPrefixKey);
			if (defaultNumberPrefixSetting == null)
			{
				defaultNumberPrefixSetting = new Setting
				{
					Key = DataConstants.SettingDefaultNumberPrefixKey,
					Value = settings.NumberPrefix.Id.ToString()
				};
				await _settingRepository.AddAsync(defaultNumberPrefixSetting);
			}
			else
			{
				defaultNumberPrefixSetting.Value = settings.NumberPrefix.Id.ToString();
				await _settingRepository.UpdateAsync(defaultNumberPrefixSetting);
			}
		}
	}
}