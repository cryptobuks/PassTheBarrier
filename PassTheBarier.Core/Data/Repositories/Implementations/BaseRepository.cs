using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PassTheBarier.Core.Data.Repositories.Interfaces;
using SQLite;

namespace PassTheBarier.Core.Data.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T: class, new()
    {
        protected const string DatabaseName = "PassTheBarrierDb.db";

        private readonly ISqliteConnectionFactory _connectionFactory;

        public BaseRepository(ISqliteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var connection = await GetAsyncConnection();

            return await connection.Table<T>().ToListAsync();
        }

        public IEnumerable<T> GetAll()
        {
            var connection = GetConnection();

            return connection.Table<T>().ToList();
        }

        protected async Task<SQLiteAsyncConnection> GetAsyncConnection()
        {
            var connection = _connectionFactory.GetAsyncConnection(DatabaseName);
            await connection.CreateTableAsync<T>();

            return connection;
        }

        protected SQLiteConnection GetConnection()
        {
            var connection = _connectionFactory.GetConnection(DatabaseName);
            connection.CreateTable<T>();

            return connection;
        }
    }
}