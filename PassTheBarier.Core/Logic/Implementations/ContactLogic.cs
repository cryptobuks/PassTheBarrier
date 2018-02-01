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
            var contactModels = contacts.Select(ContactMapper.ToContact).ToList();

            contactModels.Add(new ContactModel
            {
                Id = 1,
                Name = "Vlad Trenea",
                Number = "0742606519"
            });
            contactModels.Add(new ContactModel
            {
                Id = 2,
                Name = "AAAAAA",
                Number = "0742606519"
            });
            contactModels.Add(new ContactModel
            {
                Id =3,
                Name = "BBBB",
                Number = "0742606519"
            });
            contactModels.Add(new ContactModel
            {
                Id = 4,
                Name = "CCCCC",
                Number = "0742606519"
            });

            return contactModels;
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