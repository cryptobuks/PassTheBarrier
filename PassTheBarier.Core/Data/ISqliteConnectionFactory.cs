using SQLite;

namespace PassTheBarier.Core.Data
{
    public interface ISqliteConnectionFactory
    {
        SQLiteConnection GetConnection(string databaseName);

        SQLiteAsyncConnection GetAsyncConnection(string databaseName);
    }
}