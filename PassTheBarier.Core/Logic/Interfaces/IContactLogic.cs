using System.Collections.Generic;
using System.Threading.Tasks;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.Logic.Interfaces
{
    public interface IContactLogic
    {
        Task<IEnumerable<ContactModel>> GetAllAsync();

        Task<ContactModel> AddAsync(ContactModel contact);

        Task<ContactModel> UpdateAsync(ContactModel contact);

        Task DeleteAsync(int id);
    }
}