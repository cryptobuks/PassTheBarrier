using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace PassTheBarier.Core.Converters
{
	public class CountryFlagConverter: MvxValueConverter<string, string>
	{
		protected override string Convert(string value, Type targetType, object parameter, CultureInfo culture)
		{
			return value.ToLower();
		}
	}
}