using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerManager.ViewModel;

public partial class MainViewModel(JSONServices MyJSONService) : BaseViewModel
{
    public ObservableCollection<StrangeAnimal> MyObservableList { get; } = [];

    [RelayCommand]
    internal async Task GoToDetails(string id)
    {
        IsBusy = true;

        await Shell.Current.GoToAsync("DetailsView", true, new Dictionary<string,object>
        {
            {"selectedAnimal",id}
        });

        IsBusy = false;
    }
    [RelayCommand]
    internal async Task SaveJSON()
    {
        IsBusy = true;

        await MyJSONService.SetStrangeAnimals();
    
        IsBusy = false;
    }
    [RelayCommand]
    internal async Task LoadJSON()
    {
        IsBusy = true;

        Globals.MyStrangeAnimals = await MyJSONService.GetStrangeAnimals();

        MyObservableList.Clear();

        foreach (var item in Globals.MyStrangeAnimals)
        {
            MyObservableList.Add(item);
        }

        IsBusy = false;
    }
    

    internal void RefreshPage()
    {
        MyObservableList.Clear ();

        foreach (var item in Globals.MyStrangeAnimals)
        {
            MyObservableList.Add(item);
        }
    }
}
