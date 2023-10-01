using Microsoft.Maui.Networking;

namespace MyWeatherApp;

public partial class WelcomePage : ContentPage
{
    NetworkAccess connectivity;
    public WelcomePage()
	{
		InitializeComponent();
       
    }

    private async void BtnGetStarted_Clicked(object sender, EventArgs e)
    {
        connectivity = Connectivity.Current.NetworkAccess;
        if (connectivity != NetworkAccess.Internet)
        {
            await DisplayAlert("Opps", "No Internet Access", "OK");
            
        }else
        {
             await Navigation.PushModalAsync(new WeatherPage());
        }
        
    }
}