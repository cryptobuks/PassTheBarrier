using System;
using MvvmCross.Platform.Converters;

namespace PassTheBarier.Core.Converters
{
	public class NullableConverter : MvxValueConverter
	{
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
			{
				return true;
			}

			return false;
		}
	}
}