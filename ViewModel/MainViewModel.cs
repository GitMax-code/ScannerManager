using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerManager.ViewModel;

public partial class MainViewModel(JSONServices MyJSONService, CSVServices MyCSVService) : BaseViewModel
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
    internal async Task GoToAdd()
    {
        IsBusy = true;

        // Utiliser une navigation relative pour ajouter la page à la pile de navigation
        await Shell.Current.GoToAsync("AddAnimalView");

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

    [RelayCommand]
    internal async Task PrintCSV()
    {
        IsBusy = true;

        await MyCSVService.PrintData(Globals.MyStrangeAnimals);

        IsBusy = false;
    }
    [RelayCommand]
    internal async Task LoadCSV()
    {
        IsBusy = true;

        Globals.MyStrangeAnimals = await MyCSVService.LoadData();
        await RefreshPage();

        IsBusy = false;
    }

    internal async Task RefreshPage()
    {
        MyObservableList.Clear ();

        if (Globals.MyStrangeAnimals.Count == 0) Globals.MyStrangeAnimals = await MyJSONService.GetStrangeAnimals();


        foreach (var item in Globals.MyStrangeAnimals)
        {
            MyObservableList.Add(item);
        }
    }
}
