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
        private string id;

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
            var scannedData = _scannerService.SerialBuffer.Dequeue().ToString();
 
            id = scannedData; // Stocker l'ID scanné

            
        }


        [RelayCommand]
        private async Task AddAnimal()
        {
            IsBusy = true;

            // Chemin du dossier des images
            string imagesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Images");

            // Vérifier si l'image existe dans le dossier
            if (string.IsNullOrEmpty(Picture) || !File.Exists(Path.Combine(imagesPath, Picture)))
            {
                Picture = "default.jpg";
            }

            if (string.IsNullOrEmpty(id))
            {
                id = Guid.NewGuid().ToString();
            }
            var newAnimal = new StrangeAnimal
            {
                Id = id,
                Name = Name,
                Description = Description,
                Picture = Picture,
                ModificationCount = 0
            };

            Globals.MyStrangeAnimals.Add(newAnimal);
            await _jsonServices.SetStrangeAnimals(Globals.MyStrangeAnimals);

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