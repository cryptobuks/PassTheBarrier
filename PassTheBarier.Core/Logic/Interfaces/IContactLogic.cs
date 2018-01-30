using System.Collections.Generic;
using System.Threading.Tasks;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.Logic.Interfaces
{
    public interface IContactLogic
    {
        Task<IEnumerable<ContactModel>> GetAll();

        Task Add(ContactModel contact);

        Task Update(int id, ContactModel contact);

        Task Delete(int id);
    }
}