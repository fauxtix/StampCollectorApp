using StampCollectorApp.Models;
using StampCollectorApp.Resources.Languages;
using System.Collections.ObjectModel;
using System.Globalization;

namespace StampCollectorApp.Services
{
    public class LocalizationOptionsService
    {
        public ObservableCollection<StampConditionDisplayOption> GetConditionOptions()
        {
            return new ObservableCollection<StampConditionDisplayOption>(
                Enum.GetValues(typeof(StampCondition))
                    .Cast<StampCondition>()
                    .Select(c => new StampConditionDisplayOption
                    {
                        Value = c,
                        Display = AppResources.ResourceManager.GetString($"Condition_{c}", AppResources.Culture ?? CultureInfo.CurrentUICulture) ?? c.ToString()
                    })
            );
        }
    }
}
