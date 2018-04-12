using System.Threading.Tasks;
using PassTheBarier.Core.Data.Entities;

namespace PassTheBarier.Core.Data.Repositories.Interfaces
{
	public interface IContactRepository : IBaseRepository<Contact>
	{
		Contact GetByName(string name);

		Task<Contact> GetByNameAsync(string name);

		Contact GetByPrefixAndNumber(string prefix, string number);

		Task<Contact> GetByPrefixAndNumberAsync(string prefix, string number);
	}
}