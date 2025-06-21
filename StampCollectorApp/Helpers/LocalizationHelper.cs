using StampCollectorApp.Models;
using StampCollectorApp.Resources.Languages;
using System.Globalization;

public static class LocalizationHelper
{
    public static string GetEnumDisplayName(Enum value)
    {
        var key = $"StampCondition_{value}";
        var localized = AppResources.ResourceManager.GetString(key, CultureInfo.CurrentUICulture);
        return localized ?? value.ToString();
    }
    public static List<StampConditionDisplayOption> GetLocalizedConditions()
    {
        return Enum.GetValues(typeof(StampCondition))
            .Cast<StampCondition>()
            .Select(c => new StampConditionDisplayOption
            {
                Value = c,
                Display = GetEnumDisplayName(c)
            }).ToList();
    }
}