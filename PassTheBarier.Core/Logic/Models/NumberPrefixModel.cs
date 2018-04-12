namespace PassTheBarier.Core.Logic.Models
{
	public class NumberPrefixModel
	{
		public int Id { get; set; }

		public string Name => $"{CountryName} ({Prefix}) ";

		public string CountryName { get; set; }

		public string CountryCode { get; set; }

		public string Prefix { get; set; }
	}
}