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

	    public IEnumerable<ContactModel> GetAll()
	    {
		    var contacts = _contactRepository.GetAll();
		    var contactModels = contacts.Select(ContactMapper.ToModel).ToList();

		    return contactModels;
		}

		public async Task<IEnumerable<ContactModel>> GetAllAsync()
        {
            var contacts = await _contactRepository.GetAllAsync();
            var contactModels = contacts.Select(ContactMapper.ToModel).ToList();

	        return contactModels;
        }

	    public ContactModel Add(ContactModel contactModel)
	    {
			var contactWithName = _contactRepository.GetByName(contactModel.Name);
		    if (contactWithName != null)
		    {
			    throw new ValidationException(Messages.AContactAlreadyExists);
		    }

		    var id = _contactRepository.Add(ContactMapper.ToEntity(contactModel));
		    contactModel.Id = id;

		    return contactModel;
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

	    public ContactModel Update(ContactModel contactModel)
	    {
		    var contact = _contactRepository.GetById(contactModel.Id);
		    var contactWithName = _contactRepository.GetByName(contactModel.Name);

		    if (contact == null)
		    {
			    throw new NotFoundException(Messages.ContactNotFound);
		    }

		    if (contactWithName != null && contact.Id != contactWithName.Id)
		    {
			    throw new ValidationException(Messages.AContactAlreadyExists);
		    }

		    _contactRepository.Update(ContactMapper.ToEntity(contactModel));

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

	    public void Delete(int id)
	    {
		    var contact = _contactRepository.GetById(id);
		    if (contact == null)
		    {
			    throw new NotFoundException(Messages.ContactNotFound);
		    }

		    _contactRepository.DeleteEntity(contact);
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