using PassTheBarier.Core.Data.Entities;
using PassTheBarier.Core.Models;

namespace PassTheBarier.Core.Logic.Mappers
{
    public static class ContactMapper
    {
        public static ContactModel ToContact(Contact contact)
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
    }
}