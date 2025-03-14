namespace ScannerManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DetailsView), typeof(DetailsView));
            Routing.RegisterRoute(nameof(AddAnimalView), typeof(AddAnimalView));
            Routing.RegisterRoute(nameof(MainView), typeof(MainView)); // Updated to MainView
        }
    }
}
