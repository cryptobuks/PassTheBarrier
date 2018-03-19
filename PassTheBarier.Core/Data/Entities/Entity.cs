using SQLite;

namespace PassTheBarier.Core.Data.Entities
{
	public class Entity
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public Entity()
		{

		}
	}
}