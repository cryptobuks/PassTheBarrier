using System.Collections.Generic;
using PassTheBarier.Core.Data.Repositories.Interfaces;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Models;

namespace PassTheBarier.Core.Logic.Implementations
{
    public class ContactLogic : IContactLogic
    {
        private IContactRepository _contactRepository;

        public ContactLogic(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public IEnumerable<ContactModel> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public ContactModel GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(ContactModel contact)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, ContactModel contact)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}