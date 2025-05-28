using StampCollectorApp.ViewModels;

namespace StampCollectorApp.Views
{
    public partial class AddStampPage : ContentPage
    {
        public AddStampViewModel ViewModel { get; }

        public AddStampPage(AddStampViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            BindingContext = ViewModel;
        }
    }
}
