using PassTheBarier.Core.Data.Entities;
using PassTheBarier.Core.Data.Repositories.Interfaces;

namespace PassTheBarier.Core.Data.Repositories.Implementations
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(ISqliteConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }
    }
}