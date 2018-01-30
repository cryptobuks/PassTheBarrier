using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PassTheBarier.Core.Data.Repositories.Interfaces;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Mappers;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.Logic.Implementations
{
    public class ContactLogic : IContactLogic
    {
        private readonly IContactRepository _contactRepository;

        public ContactLogic(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<ContactModel>> GetAll()
        {
            var contacts = await _contactRepository.GetAllAsync();

            return contacts.Select(ContactMapper.ToContact);
        }

        public Task Add(ContactModel contact)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(int id, ContactModel contact)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}