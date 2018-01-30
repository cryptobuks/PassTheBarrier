using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassTheBarier.Core.Data.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetAllAsync();

        IEnumerable<T> GetAll();
    }
}