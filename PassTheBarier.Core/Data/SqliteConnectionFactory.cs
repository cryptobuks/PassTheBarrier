using SQLite;

namespace PassTheBarier.Core.Data
{
    public class SqliteConnectionFactory : ISqliteConnectionFactory
    {
        public SQLiteConnection GetConnection(string databaseName)
        {
            var connectionString = new SQLiteConnectionString(databaseName, true);
            var connection = new SQLiteConnectionWithLock(connectionString, SQLiteOpenFlags.ReadWrite);

            return connection;
        }

        public SQLiteAsyncConnection GetAsyncConnection(string databaseName)
        {
            var connection = new SQLiteAsyncConnection(databaseName);

            return connection;
        }
    }
}