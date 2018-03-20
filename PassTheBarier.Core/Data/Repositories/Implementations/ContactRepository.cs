using System.Linq;
using System.Threading.Tasks;
using PassTheBarier.Core.Data.Entities;
using PassTheBarier.Core.Data.Repositories.Interfaces;

namespace PassTheBarier.Core.Data.Repositories.Implementations
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(ISqliteConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

	    public Contact GetByName(string name)
	    {
		    var contacts = GetAll();

		    return contacts.FirstOrDefault(s => s.Name == name);
		}

	    public async Task<Contact> GetByNameAsync(string name)
	    {
		    var contacts = await GetAllAsync();

		    return contacts.FirstOrDefault(s => s.Name == name);
		}
    }
}