namespace GeofencesSample
{
	using GeofencesSample.Services.Geofences;
	using System;
	using System.Collections.Generic;
	using Xamarin.Essentials;
	using Xamarin.Forms;
	using Xamarin.Forms.Maps;

	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			IGeofencesService geofencesService = DependencyService.Get<IGeofencesService>();

			List<Position> list = GetGeofences();
			Location location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(3)));
			geofencesService.SetGeofences(list, new Position(location.Latitude, location.Longitude));
		}

		private List<Position> GetGeofences()
		{
			return new List<Position>
			{
				new Position(36.681062, -6.139933), // Alcazar, Jerez de la Frontera
				new Position(40.416905, -3.703512), // Puerta del Sol, Madrid
				new Position(0, 0),
			};
		}
	}
}
