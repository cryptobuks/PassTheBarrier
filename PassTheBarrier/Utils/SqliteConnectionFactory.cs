using System.IO;
using PassTheBarier.Core.Data;
using SQLite;

namespace PassTheBarrier.Utils
{
    public class SqliteConnectionFactory : ISqliteConnectionFactory
    {
        public SQLiteConnection GetConnection(string databaseName)
        {
			var connectionString = new SQLiteConnectionString(GetDbPath(databaseName), false);
            var connection = new SQLiteConnectionWithLock(connectionString, SQLiteOpenFlags.ReadWrite);

            return connection;
        }

        public SQLiteAsyncConnection GetAsyncConnection(string databaseName)
        {
			var connection = new SQLiteAsyncConnection(GetDbPath(databaseName), false);

            return connection;
        }

		private string GetDbPath(string databaseName)
		{
			return Path.Combine(System.Environment.
			  GetFolderPath(System.Environment.
			  SpecialFolder.Personal), databaseName);
		}
    }
}