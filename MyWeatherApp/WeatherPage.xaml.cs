
using MyWeatherApp.Services;

namespace MyWeatherApp;

public partial class WeatherPage : ContentPage
{
    public List<Models.List> WeatherList;
    private double latitude, longitude;
    public WeatherPage()
	{
		InitializeComponent();
        WeatherList =new List<Models.List>();

    }
    protected async override void OnAppearing()
    {
        //GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
         //Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);
        base.OnAppearing();
        await GetLocation();
        await GetWeatherByLocation(latitude, longitude);

    }
    private async void TapLocation_Tapped(object sender, EventArgs e)
    {
        await GetLocation();
        await GetWeatherByLocation(latitude, longitude);
    }

    private async Task GetWeatherByLocation(double latitude, double longitude)
    {
        /*Location location = await Geolocation.Default.GetLastKnownLocationAsync();
        double lat = location.Latitude; double lon = location.Longitude;*/
        var result = await ApiService.GetWeather(latitude, longitude);
        UpdateUi(result);
    }
    public async Task GetLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        latitude = location.Latitude;
        longitude = location.Longitude;

    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var respone = await DisplayPromptAsync(title:"",message:"",placeholder:"Search Weather by city",accept: "Search",cancel:"Cancel");
        if(respone !=null)
        {
            await GetWeatherDataByCity(respone);
        }
    }
    private async Task GetWeatherDataByCity(string city)
    {
        var result = await ApiService.GetWeatherByCity(city);
         UpdateUi(result);
    }

    public void UpdateUi(dynamic result)
    {
        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }
        CvWeather.ItemsSource = WeatherList;


        lblCity.Text = result.city.name;
        lblWeatherDescription.Text = result.list[0].weather[0].description;
        LblTemperature.Text = result.list[0].main.temperature + "°C";
        LblHumidity.Text = result.list[0].main.humidity + "%";
        LblWind.Text = result.list[0].wind.speed + "km/h";

        ImgWeatherIcon.Source = result.list[0].weather[0].fullIconUrl;
    }
}