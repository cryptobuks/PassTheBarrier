using PassTheBarier.Core.Data.Repositories.Interfaces;

namespace PassTheBarier.Core.Data.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T: class, new()
    {
        protected const string DatabaseName = "PassTheBarrierDb";

        private readonly ISqliteConnectionFactory _connectionFactory;

        public BaseRepository(ISqliteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
    }
}