using StampCollectorApp.Models;
using System.Globalization;

namespace StampCollectorApp.Converters
{
    public class StampConditionDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "Todas as condições"; // ou o texto que preferir para o filtro nulo

            // Mostra o nome do enum, pode customizar se quiser exibir mais bonito
            return ((StampCondition)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
