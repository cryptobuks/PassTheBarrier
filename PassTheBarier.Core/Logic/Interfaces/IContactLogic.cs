using System.Collections.Generic;
using System.Threading.Tasks;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.Logic.Interfaces
{
    public interface IContactLogic
    {
	    IEnumerable<ContactModel> GetAll();

        Task<IEnumerable<ContactModel>> GetAllAsync();

	    ContactModel Add(ContactModel contact);

		Task<ContactModel> AddAsync(ContactModel contact);

	    ContactModel Update(ContactModel contact);

        Task<ContactModel> UpdateAsync(ContactModel contact);

	    void Delete(int id);

        Task DeleteAsync(int id);
    }
}