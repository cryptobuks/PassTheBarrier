using System.Linq;
using System.Threading.Tasks;
using PassTheBarier.Core.Data.Entities;
using PassTheBarier.Core.Data.Repositories.Interfaces;

namespace PassTheBarier.Core.Data.Repositories.Implementations
{
	public class SettingRepository : BaseRepository<Setting>, ISettingRepository
	{
		public SettingRepository(ISqliteConnectionFactory connectionFactory) : base(connectionFactory)
		{
		}

		public Setting GetByKey(string key)
		{
			var settings = GetAll();

			return settings.FirstOrDefault(s => s.Key == key);
		}

		public async Task<Setting> GetByKeyAsync(string key)
		{
			var settings = await GetAllAsync();

			return settings.FirstOrDefault(s => s.Key == key);
		}
	}
}