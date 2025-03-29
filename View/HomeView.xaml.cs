using Microsoft.Maui.Controls;
using ScannerManager.ViewModel;

namespace ScannerManager.View
{
    public partial class HomeView : ContentPage
    {

        HomeViewModel viewmodel;
        public HomeView(HomeViewModel viewmodel)
        {

            this.viewmodel = viewmodel;
            InitializeComponent();
            BindingContext = viewmodel;

        }
    }
}