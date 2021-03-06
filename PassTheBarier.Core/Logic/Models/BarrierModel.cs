﻿namespace PassTheBarier.Core.Logic.Models
{
	public class BarrierModel
	{
		public int Id { get; set; }

		public string MessageText { get; set; }

		public NumberPrefixModel NumberPrefix { get; set; }

		public string Number { get; set; }

		public string FullNumber => NumberPrefix.Prefix + Number;
	}
}