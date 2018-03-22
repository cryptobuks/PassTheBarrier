using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace PassTheBarier.Core.Converters
{
	public class NegativeBoolConverter : MvxValueConverter<bool>
	{
		protected override object Convert(bool value, Type targetType, object parameter, CultureInfo culture)
		{
			return !value;
		}
	}
}
