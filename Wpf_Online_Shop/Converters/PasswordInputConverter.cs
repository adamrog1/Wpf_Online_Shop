using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Wpf_Online_Shop.Converters
{
	/// <summary>
	/// Konwerter umożlwiający bindowanie wielu obiektów jako parametry do komendy
	/// </summary>
    public class PasswordInputConverter : IMultiValueConverter
    {
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			return values.Clone();
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
