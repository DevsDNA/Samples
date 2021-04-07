namespace GamingWithMaps
{
	using System;
	using System.Threading.Tasks;
	using Xamarin.Essentials;
	using Xamarin.Forms;

	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			Task.Run(GetPosition);
		}

		private async void GetPosition()
		{
			await Device.InvokeOnMainThreadAsync(async () => await Permissions.RequestAsync<Permissions.LocationAlways>());
			while (true)
			{
				Location position = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(1)));
				Device.BeginInvokeOnMainThread(() => MainMap.UserCurrentLocation = position);

				//await Task.Delay(TimeSpan.FromSeconds(1));
			}
		}
	}
}
