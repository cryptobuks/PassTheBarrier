namespace PassTheBarier.Core.Logic.Models
{
    public class ContactModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

	    public string Prefix { get; set; }

		public string Number { get; set; }

	    public string FullNumber => Prefix + Number;

	    public string FormattedPrefix => $"({Prefix}) ";
    }
}