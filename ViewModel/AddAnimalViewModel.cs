using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace ScannerManager.ViewModel
{
    public partial class AddAnimalViewModel : BaseViewModel
    {
        private readonly JSONServices _jsonServices;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private string picture;

        public event EventHandler AnimalAdded;

        public AddAnimalViewModel(JSONServices jsonServices)
        {
            _jsonServices = jsonServices;

            Globals.MyStrangeAnimals = Globals.MyStrangeAnimals ?? new List<StrangeAnimal>();
        }

        [RelayCommand]
        private async Task AddAnimal()
        {
            IsBusy = true;

            var newAnimal = new StrangeAnimal
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Description = Description,
                Picture = Picture
            };

            Globals.MyStrangeAnimals.Add(newAnimal);
            await _jsonServices.SetStrangeAnimals();

            IsBusy = false;

            // Navigate back to the main page
            await Shell.Current.GoToAsync("//MainView");
        }
    }
}
