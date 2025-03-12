using CommunityToolkit.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Service
{
    public class CSVServices
    {
        public async Task<List<StrangeAnimal>> LoadData()
        {
            List<StrangeAnimal> myList = new List<StrangeAnimal>();

            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Sélectionnez votre fichier csv",
            });

            if (result != null)
            {
                var lines = await File.ReadAllLinesAsync(result.FullPath, Encoding.UTF8);

                var headers = lines[0].Split(';');

                var properties = typeof(StrangeAnimal).GetProperties();

                for (int i = 1; i < lines.Length; i++)
                {
                    StrangeAnimal obj = new();

                    var values = lines[i].Split(';');

                    for (int j = 0; j < headers.Length; j++)
                    {
                        var property = properties.FirstOrDefault(p => p.Name.Equals(headers[j], StringComparison.OrdinalIgnoreCase));

                        if (property != null && j < values.Length)
                        {
                            object value = Convert.ChangeType(values[j], property.PropertyType);
                            property.SetValue(obj, value);
                        }
                    }
                    myList.Add(obj);
                }
            }

            return myList;
        }

        public async Task PrintData<T>(List<T> myList)
        {
            var csv = new StringBuilder();
            var properties = typeof(T).GetProperties();

            csv.AppendLine(string.Join(";", properties.Select(p => p.Name)));

            foreach (var item in myList)
            {
                var values = properties.Select(p => p.GetValue(item)?.ToString() ?? "");
                csv.AppendLine(string.Join(";", values));
            }

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(csv.ToString()));

            var fileSaverResult = await FileSaver.Default.SaveAsync("collection.csv", stream);

            if (fileSaverResult.IsSuccessful)
            {
                await Shell.Current.DisplayAlert("Fichier sauvegardé", "Le fichier a été sauvegardé avec succès", "OK");
            }
        }
    }
}
