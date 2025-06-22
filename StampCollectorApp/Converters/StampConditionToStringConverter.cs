using StampCollectorApp.Models;
using System.Globalization;

namespace StampCollectorApp.Converters;
public class StampConditionToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is StampCondition condition)
            return LocalizationHelper.GetEnumDisplayName(condition);
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}