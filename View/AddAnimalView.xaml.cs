namespace ScannerManager.View;

public partial class AddAnimalView : ContentPage
{
    public AddAnimalView()
    {
        InitializeComponent();
        BindingContext = new AddAnimalViewModel(new JSONServices(), new DeviceOrientationService());
    }
}