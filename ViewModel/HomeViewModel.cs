using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ScannerManager.ViewModel
{
    public partial class  HomeViewModel : BaseViewModel
    {

        [RelayCommand]
        public async Task GoToMain()
        {
          
                IsBusy = true;
                await Shell.Current.GoToAsync("MainView");
                IsBusy = false;
          
        }

    }
}
