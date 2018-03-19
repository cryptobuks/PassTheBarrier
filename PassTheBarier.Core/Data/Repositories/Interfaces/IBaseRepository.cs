using PassTheBarier.Core.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PassTheBarier.Core.Data.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();

		Task<IEnumerable<T>> GetAllAsync();

		T GetById(int id);

		Task<T> GetByIdAsync(int id);

		void Add(T entity);

		Task AddAsync(T entity);

		void Update(T entity);

		Task UpdateAsync(T entity);
    }
}