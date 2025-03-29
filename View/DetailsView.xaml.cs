using ScannerManager.ViewModel;

namespace ScannerManager.View;

public partial class DetailsView : ContentPage
{
    readonly DetailsViewModel viewModel;
    public DetailsView(DetailsViewModel viewModel)
    {
        this.viewModel = viewModel;
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        BindingContext = null;
        viewModel.RefreshPage();    // R�initialise la observablecollection
        BindingContext = viewModel;
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        viewModel.ClosePage();
    }

    private async void MyAnimatedButton_Clicked(object sender, EventArgs e)
    {
        if (sender is VisualElement visualElement) // Add this type check and cast
        {
            await visualElement.ScaleTo(1.1, 100);
            await visualElement.ScaleTo(1.0, 100);
        }
    }
}
