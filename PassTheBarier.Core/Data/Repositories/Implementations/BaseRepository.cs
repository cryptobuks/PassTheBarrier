using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PassTheBarier.Core.Data.Entities;
using PassTheBarier.Core.Data.Repositories.Interfaces;
using SQLite;

namespace PassTheBarier.Core.Data.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T: Entity, new()
    {
        protected const string DatabaseName = DataConstants.DatabaseName;

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

		public T GetById(int id)
		{
            var connection = GetConnection();

			return connection.Get<T>(id);
		}

		public async Task<T> GetByIdAsync(int id)
		{
			var connection = await GetAsyncConnection();

			return await connection.GetAsync<T>(id);
		}

		public void Add(T entity)
		{
			var connection = GetConnection();

			connection.Insert(entity);
		}

		public async Task AddAsync(T entity)
		{
			var connection = await GetAsyncConnection();

			await connection.InsertAsync(entity);
		}

		public void Update(T entity)
		{
			var connection = GetConnection();

			connection.Update(entity);
		}

		public async Task UpdateAsync(T entity)
		{
			var connection = await GetAsyncConnection();

			await connection.UpdateAsync(entity);
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