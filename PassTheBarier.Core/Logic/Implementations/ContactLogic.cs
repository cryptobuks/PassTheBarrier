using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PassTheBarier.Core.Data.Repositories.Interfaces;
using PassTheBarier.Core.Logic.Exceptions;
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

        public async Task<IEnumerable<ContactModel>> GetAllAsync()
        {
            var contacts = await _contactRepository.GetAllAsync();
            var contactModels = contacts.Select(ContactMapper.ToModel).ToList();

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

        public async Task AddAsync(ContactModel contactModel)
        {
	        var contactWithName = await _contactRepository.GetByNameAsync(contactModel.Name);
	        if (contactWithName != null)
	        {
				throw new ValidationException("A contact with that name already exists.");
	        }

	        await _contactRepository.AddAsync(ContactMapper.ToEntity(contactModel));
        }

        public async Task UpdateAsync(ContactModel contactModel)
        {
	        var contact = await _contactRepository.GetByIdAsync(contactModel.Id);
	        var contactWithName = await _contactRepository.GetByNameAsync(contactModel.Name);

	        if (contact == null)
	        {
				throw new NotFoundException("Contact not found");
	        }

	        if (contact.Id != contactWithName.Id)
	        {
				throw new ValidationException("A contact with that name already exists");
	        }

			await _contactRepository.UpdateAsync(ContactMapper.ToEntity(contactModel));
        }

        public async Task DeleteAsync(int id)
        {
	        var contact = await _contactRepository.GetByIdAsync(id);
	        if (contact == null)
	        {
				throw new NotFoundException("Contact not found");
	        }

	        await _contactRepository.DeleteEntityAsync(contact);
        }
    }
}