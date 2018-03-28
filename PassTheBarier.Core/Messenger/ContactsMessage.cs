using System.Collections.Generic;
using MvvmCross.Plugins.Messenger;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.Messenger
{
	public class ContactsMessage : MvxMessage
	{
		public IEnumerable<ContactModel> Contacts { get; set; }

		public ContactsMessage(object sender, IEnumerable<ContactModel> contacts) : base(sender)
		{
			Contacts = contacts;
		}
	}
}