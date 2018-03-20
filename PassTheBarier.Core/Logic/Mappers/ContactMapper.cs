using PassTheBarier.Core.Data.Entities;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.Logic.Mappers
{
	public static class ContactMapper
	{
		public static ContactModel ToModel(Contact contact)
		{
			if (contact == null)
			{
				return null;
			}

			return new ContactModel
			{
				Id = contact.Id,
				Name = contact.Name,
				Number = contact.Number
			};
		}

		public static Contact ToEntity(ContactModel contact)
		{
			if (contact == null)
			{
				return null;
			}

			return new Contact
			{
				Id = contact.Id,
				Name = contact.Name,
				Number = contact.Number
			};
		}
	}
}