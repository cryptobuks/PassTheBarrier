using System.Threading.Tasks;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.Logic.Interfaces
{
	public interface ISettingLogic
	{
		SettingsModel GetSettings();

		Task<SettingsModel> GetSettingsAsync();

		void SaveSettings(SettingsModel settings);

		Task SaveSettingsAsync(SettingsModel settings);
	}
}