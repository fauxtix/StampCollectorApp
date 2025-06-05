using StampCollectorApp.Models;
using System.Globalization;

namespace StampCollectorApp.Converters;

public class StampConditionToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is StampCondition condition)
        {
            return condition switch
            {
                StampCondition.Com_Charneira => "Com Charneira",
                StampCondition.Sem_Charneira => "Sem Charneira",
                _ => condition.ToString()
            };
        }
        return value?.ToString() ?? string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
