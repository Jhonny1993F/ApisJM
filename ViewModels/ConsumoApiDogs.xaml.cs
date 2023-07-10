using ApisJM.Models;
using ApisJM.Service;
using Newtonsoft.Json;
using System.Net;

namespace ApisJM.ViewModels;

public partial class ConsumoApiDogs : ContentPage
{
	public ConsumoApiDogs()
	{
		InitializeComponent();
	}
	public async void Button_Clicked(object sender, EventArgs e)
	{
		string cadena = Buscador.Text;
		var request = new HttpRequestMessage();
		request.RequestUri = new Uri("https://api.thecatapi.com/v1/images/search");
        request.RequestUri = new Uri("https://api.thecatapi.com/v1/images/search?" + cadena);
        request.Method = HttpMethod.Get;
		request.Headers.Add("Accept", "application/json");

		var client = new HttpClient();

		HttpResponseMessage response = await client.SendAsync(request);
		if (response.StatusCode == HttpStatusCode.OK)
		{
			String content = await response.Content.ReadAsStringAsync();
			var resultado = JsonConvert.DeserializeObject<List<ApiDogs>>(content);
			Lista.ItemsSource = resultado;
		}
    }

	private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
        if (e.CurrentSelection.FirstOrDefault() is not DogsJM item)
            return;
        Shell.Current.GoToAsync(nameof(DogsItemPage), true, new Dictionary<string, object>
        {
            ["Item"] = item
        });
    }
}