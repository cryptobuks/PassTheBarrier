using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PassTheBarier.Core.Data.Repositories.Interfaces;
using PassTheBarier.Core.Logic.Exceptions;
using PassTheBarier.Core.Logic.Interfaces;
using PassTheBarier.Core.Logic.Mappers;
using PassTheBarier.Core.Logic.Models;
using PassTheBarier.Core.Logic.ResourcesFiles;

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

	        return contactModels;
        }

        public async Task<ContactModel> AddAsync(ContactModel contactModel)
        {
	        var contactWithName = await _contactRepository.GetByNameAsync(contactModel.Name);
	        if (contactWithName != null)
	        {
				throw new ValidationException(Messages.AContactAlreadyExists);
	        }

	        var id = await _contactRepository.AddAsync(ContactMapper.ToEntity(contactModel));
	        contactModel.Id = id;

	        return contactModel;
        }

        public async Task<ContactModel> UpdateAsync(ContactModel contactModel)
        {
	        var contact = await _contactRepository.GetByIdAsync(contactModel.Id);
	        var contactWithName = await _contactRepository.GetByNameAsync(contactModel.Name);

	        if (contact == null)
	        {
				throw new NotFoundException(Messages.ContactNotFound);
	        }

	        if (contactWithName != null && contact.Id != contactWithName.Id)
	        {
				throw new ValidationException(Messages.AContactAlreadyExists);
	        }

			await _contactRepository.UpdateAsync(ContactMapper.ToEntity(contactModel));

	        return contactModel;
        }

        public async Task DeleteAsync(int id)
        {
	        var contact = await _contactRepository.GetByIdAsync(id);
	        if (contact == null)
	        {
				throw new NotFoundException(Messages.ContactNotFound);
	        }

	        await _contactRepository.DeleteEntityAsync(contact);
        }
    }
}