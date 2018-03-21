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

		int Add(T entity);

		Task<int> AddAsync(T entity);

		void Update(T entity);

		Task UpdateAsync(T entity);

	    void DeleteEntity(T entity);

	    Task DeleteEntityAsync(T entity);

		void DeleteById(int id);

	    Task DeleteByIdAsync(int id);
    }
}