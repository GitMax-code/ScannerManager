using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerManager.ViewModel;

[QueryProperty(nameof(Id), "selectedAnimal")]
public partial class DetailsViewModel: ObservableObject
{
    [ObservableProperty]
    public partial string? Id { get; set; }
    [ObservableProperty]
    public partial string? Name { get; set; }
    [ObservableProperty]
    public partial string? Description { get; set; }
    [ObservableProperty]
    public partial string? Picture { get; set; }
    [ObservableProperty]
    public partial string? SerialBufferContent { get; set; }

    readonly DeviceOrientationService MyScanner;
    readonly JSONServices MyJSONService;

    public DetailsViewModel(DeviceOrientationService myScanner, JSONServices myJSONService)
    {
        this.MyScanner = myScanner;
        this.MyJSONService = myJSONService;
        MyScanner.OpenPort();
        myScanner.SerialBuffer.Changed += OnSerialDataReception;
    }
    private void OnSerialDataReception(object sender, EventArgs arg)
    {
        DeviceOrientationService.QueueBuffer MyLocalBuffer = (DeviceOrientationService.QueueBuffer)sender;

        if (MyLocalBuffer.Count > 0)
        {
            SerialBufferContent += MyLocalBuffer.Dequeue().ToString();
            OnPropertyChanged(nameof(SerialBufferContent));
        }
    }
    internal void RefreshPage()
    {
        foreach (var item in Globals.MyStrangeAnimals)
        {
            if (Id == item.Id)
            {
                Name = item.Name;
                Description = item.Description;
                Picture = item.Picture;

                break;
            }
        }
    }
    internal void ClosePage()
    {
        MyScanner.SerialBuffer.Changed -= OnSerialDataReception;
        MyScanner.ClosePort();       
    }

    [RelayCommand]
    internal async void ChangeObjectParameters()
    {
        foreach (var item in Globals.MyStrangeAnimals)
        {
            if (item.Id == Id)
            {
                item.Name = Name ?? string.Empty;
                item.Description = Description ?? string.Empty;
                item.Picture = Picture ?? string.Empty;
            }
        }
        // Sauvegarder les modifications dans le fichier JSON
        await MyJSONService.SetStrangeAnimals();

        // Naviguer vers la page d'accueil
        await Shell.Current.GoToAsync("//MainView");
    }    
}
