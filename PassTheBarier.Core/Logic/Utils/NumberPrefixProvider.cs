using System.Collections.Generic;
using System.Linq;
using PassTheBarier.Core.Logic.Models;

namespace PassTheBarier.Core.Logic.Utils
{
	public static class NumberPrefixProvider
	{
		private static readonly IList<NumberPrefixModel> Prefixes = new List<NumberPrefixModel>
		{
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Au,
				CountryCode = "AU",
				CountryName = "Australia",
				Prefix = "+61"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.At,
				CountryCode = "AT",
				CountryName = "Austria",
				Prefix = "+43"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Be,
				CountryCode = "BE",
				CountryName = "Belgium",
				Prefix = "+32"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Bg,
				CountryCode = "BG",
				CountryName = "Bulgaria",
				Prefix = "+359"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Ca,
				CountryName = "Canada",
				CountryCode = "CA",
				Prefix = "+1"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Cn,
				CountryName = "China",
				CountryCode = "CN",
				Prefix = "+86"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Hr,
				CountryName = "Croatia",
				CountryCode = "HR",
				Prefix = "+385"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Cy,
				CountryName = "Cyprus",
				CountryCode = "CY",
				Prefix = "+357"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Cz,
				CountryName = "Czech Republic",
				CountryCode = "CZ",
				Prefix = "+420"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Dk,
				CountryName = "Denmark",
				CountryCode = "DK",
				Prefix = "+45"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.De,
				CountryName = "Germany",
				CountryCode = "DE",
				Prefix = "+49"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Ee,
				CountryName = "Estonia",
				CountryCode = "EE",
				Prefix = "+372"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Fi,
				CountryName = "Finland",
				CountryCode = "FI",
				Prefix = "+358"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Fr,
				CountryName = "France",
				CountryCode = "FR",
				Prefix = "+33"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Gr,
				CountryName = "Greece",
				CountryCode = "GR",
				Prefix = "+30"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Hu,
				CountryName = "Hungary",
				CountryCode = "HU",
				Prefix = "+36"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Is,
				CountryName = "Iceland",
				CountryCode = "IS",
				Prefix = "+354"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Ie,
				CountryName = "Ireland",
				CountryCode = "IE",
				Prefix = "+353"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Il,
				CountryName = "Israel",
				CountryCode = "IL",
				Prefix = "+972"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.It,
				CountryName = "Italy",
				CountryCode = "IT",
				Prefix = "+39"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Jp,
				CountryName = "Japan",
				CountryCode = "JP",
				Prefix = "+81"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Lv,
				CountryName = "Latvia",
				CountryCode = "LV",
				Prefix = "+371"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Lt,
				CountryName = "Lithuania",
				CountryCode = "LT",
				Prefix = "+370"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Lu,
				CountryName = "Luxembourg",
				CountryCode = "LU",
				Prefix = "+352"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Mk,
				CountryName = "Macedonia",
				CountryCode = "MK",
				Prefix = "+389"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Mt,
				CountryName = "Malta",
				CountryCode = "MT",
				Prefix = "+356"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Md,
				CountryName = "Moldova",
				CountryCode = "MD",
				Prefix = "+373"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Nl,
				CountryName = "Netherlands",
				CountryCode = "NL",
				Prefix = "+31"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.No,
				CountryName = "Norway",
				CountryCode = "NO",
				Prefix = "+47"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Pl,
				CountryName = "Poland",
				CountryCode = "PL",
				Prefix = "+48"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Pt,
				CountryName = "Portugal",
				CountryCode = "PT",
				Prefix = "+351"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Ro,
				CountryName = "Romania",
				CountryCode = "RO",
				Prefix = "+40"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Ru,
				CountryName = "Russia",
				CountryCode = "RU",
				Prefix = "+7"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Rs,
				CountryName = "Serbia",
				CountryCode = "RS",
				Prefix = "+381"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Sk,
				CountryName = "Slovakia",
				CountryCode = "SK",
				Prefix = "+421"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Sl,
				CountryName = "Slovenia",
				CountryCode = "SI",
				Prefix = "+386"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Es,
				CountryName = "Spain",
				CountryCode = "ES",
				Prefix = "+34"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Se,
				CountryName = "Sweden",
				CountryCode = "SE",
				Prefix = "+46"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Ch,
				CountryName = "Switzerland",
				CountryCode = "CH",
				Prefix = "+41"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Tr,
				CountryName = "Turkey",
				CountryCode = "TR",
				Prefix = "+90"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Ua,
				CountryName = "Ukraine",
				CountryCode = "UA",
				Prefix = "+380"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Gb,
				CountryName = "United Kingdom",
				CountryCode = "GB",
				Prefix = "+44"
			},
			new NumberPrefixModel
			{
				Id = (int) CountryEnum.Us,
				CountryName = "United States",
				CountryCode = "US",
				Prefix = "+1"
			}
		};

		public static IList<NumberPrefixModel> GetNumberPrefixes()
		{
			return Prefixes.OrderBy(p => p.CountryName).ToList();
		}

		public static NumberPrefixModel GetById(int id)
		{
			return Prefixes.FirstOrDefault(p => p.Id == id);
		}
	}
}