using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScannerManager.ViewModel
{
    public partial class AddAnimalViewModel : BaseViewModel
    {
        private readonly JSONServices _jsonServices;
        private readonly DeviceOrientationService _scannerService;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private string picture;

        public event EventHandler AnimalAdded;

        public AddAnimalViewModel(JSONServices jsonServices, DeviceOrientationService scannerService)
        {
            _jsonServices = jsonServices;
            _scannerService = scannerService;

            Globals.MyStrangeAnimals = Globals.MyStrangeAnimals ?? new List<StrangeAnimal>();

            // Ouvrir le port du scanner
            _scannerService.OpenPort();

            // S'abonner à l'événement de réception des données
           _scannerService.SerialBuffer.Changed += OnSerialDataReception;

        }

        private void OnSerialDataReception(object sender, EventArgs e)
        {



            // Récupérer les données du scanner
            var scannedData = _scannerService.SerialBuffer.Dequeue().ToString();

            // Supposons que les données scannées sont au format "Nom;Description;URLImage"
            var dataParts = scannedData.Split(';');

            if (dataParts.Length >= 3)
            {
                Name = dataParts[0]; // Remplir le champ Nom
                Description = dataParts[1]; // Remplir le champ Description
                Picture = dataParts[2]; // Remplir le champ Image
            }
        }

        [RelayCommand]
        private async Task AddAnimal()
        {
            IsBusy = true;

            if (string.IsNullOrEmpty(Picture))
            {
                Picture = "default.jpg";
            };

            var newAnimal = new StrangeAnimal
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Description = Description,
                Picture = Picture,
                ModificationCount = 0
            };

            Globals.MyStrangeAnimals.Add(newAnimal);
            await _jsonServices.SetStrangeAnimals();

            IsBusy = false;

            // Naviguer vers la page d'accueil
            await Shell.Current.GoToAsync("//MainView");
        }

        public void Dispose()
        {
            // Se désabonner de l'événement et fermer le port
            _scannerService.SerialBuffer.Changed -= OnSerialDataReception;
            _scannerService.ClosePort();
        }
    }
}