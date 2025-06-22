using StampCollectorApp.Models;
using System.Globalization;

namespace StampCollectorApp.Converters
{
    public class StampConditionDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "Todas as condições";

            return ((StampCondition)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
