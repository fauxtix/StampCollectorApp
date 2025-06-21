using CommunityToolkit.Mvvm.ComponentModel;
using StampCollectorApp.Models;
using System.Collections.ObjectModel;

namespace StampCollectorApp.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {

        [ObservableProperty]
        private StampCondition? selectedCondition;

        [ObservableProperty]
        private StampConditionDisplayOption selectedConditionOption;

        public List<StampConditionDisplayOption> Conditions { get; } =
            LocalizationHelper.GetLocalizedConditions();

        public ObservableCollection<ChartData> StampsByCondition { get; } = new();


    }
}
