namespace PassTheBarier.Core.Logic.Models
{
    public class ContactModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

	    public NumberPrefixModel NumberPrefix { get; set; }

		public string Number { get; set; }

	    public string FullNumber => NumberPrefix.Prefix + Number;

	    public string FormattedPrefix => $"({NumberPrefix.Prefix}) ";
    }
}