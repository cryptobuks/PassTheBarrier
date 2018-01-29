using System.Collections.Generic;
using PassTheBarier.Core.Models;

namespace PassTheBarier.Core.Logic.Interfaces
{
    public interface IContactLogic
    {
        IEnumerable<ContactModel> GetAll();

        ContactModel GetById(int id);

        void Add(ContactModel contact);

        void Update(int id, ContactModel contact);

        void Delete(int id);
    }
}