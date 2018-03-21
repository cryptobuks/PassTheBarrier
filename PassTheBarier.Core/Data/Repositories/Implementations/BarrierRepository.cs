using PassTheBarier.Core.Data.Entities;
using PassTheBarier.Core.Data.Repositories.Interfaces;

namespace PassTheBarier.Core.Data.Repositories.Implementations
{
	public class BarrierRepository : BaseRepository<Barrier>, IBarrierRepository
	{
		public BarrierRepository(ISqliteConnectionFactory connectionFactory) : base(connectionFactory)
		{
		}
	}
}