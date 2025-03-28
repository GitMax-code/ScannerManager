﻿

using Microsoft.Maui.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScannerManager.Service;

public class JSONServices
{

    internal async Task<List<StrangeAnimal>> GetStrangeAnimals()
    {
        //var url = "http://localhost:32774/json?FileName=MyStrangeAnimals.json";
        var url = "https://185.157.245.38:5000/json?FileName=MaximilienDargent.json";

        List<StrangeAnimal> MyList = new();

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };

        HttpClient _httpClient = new HttpClient(handler);

        var response = await _httpClient.GetAsync(url);


        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStreamAsync();

            MyList = JsonSerializer.Deserialize<List<StrangeAnimal>>(content) ?? new List<StrangeAnimal>();
        }

        return MyList ?? new List<StrangeAnimal>();
    }

    internal async Task SetStrangeAnimals(List<StrangeAnimal> MyList)
    {
        //var url = "http://localhost:32774/json";
        var url = "https://185.157.245.38:5000/json";

        MemoryStream mystream = new();

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };

        HttpClient _httpClient = new HttpClient(handler);

        JsonSerializer.Serialize(mystream, MyList);

        mystream.Position = 0;

        var fileContent = new ByteArrayContent(mystream.ToArray());

        var content = new MultipartFormDataContent
        {
            { fileContent, "file", "MaximilienDargent.json"}
        };

        var response = await _httpClient.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            // Notifier l'utilisateur du succès
        }
        else
        {
            // Gérer les erreurs
        }
    }
}

/*

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScannerManager.Service;

public class JSONServices
{
    internal async Task<List<StrangeAnimal>> GetStrangeAnimals()
    {
        List<StrangeAnimal> MyList = new();

        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MyAnimals.json");
        //string filePath = Path.Combine("C:\\Users\\maxim\\helb\\DotNetIII\\ScannerManager\\Resources", "JSONFiles", "MyAnimals.json");

        try
        {
            using var stream = File.Open(filePath, FileMode.Open);

            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            MyList = JsonSerializer.Deserialize<List<StrangeAnimal>>(contents) ?? new List<StrangeAnimal>();
        }
        catch (Exception ex) 
        {
            await Shell.Current.DisplayAlert("JSON load Error!", ex.Message, "OK");
        }
        
        return MyList ?? new List<StrangeAnimal>();
    }

    internal async Task SetStrangeAnimals()
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MyAnimals.json");
        //string filePath = Path.Combine("C:\\Users\\maxim\\helb\\DotNetIII\\ScannerManager\\Resources", "JSONFiles", "MyAnimals.json");

        try
        {
            using FileStream fileStream = File.Create(filePath);

            JsonSerializer.Serialize(fileStream, Globals.MyStrangeAnimals);
            fileStream.Dispose();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("JSON save Error!", ex.Message, "OK");
        }
    }
}
*/
