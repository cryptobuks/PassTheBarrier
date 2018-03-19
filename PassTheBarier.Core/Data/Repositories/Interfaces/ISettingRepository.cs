using PassTheBarier.Core.Data.Entities;
using System.Threading.Tasks;

namespace PassTheBarier.Core.Data.Repositories.Interfaces
{
	public interface ISettingRepository: IBaseRepository<Setting>
	{
		Setting GetByKey(string key);

		Task<Setting> GetByKeyAsync(string key);
	}
}