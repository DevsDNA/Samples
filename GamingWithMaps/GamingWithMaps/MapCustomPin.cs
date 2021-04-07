namespace GamingWithMaps
{
	using Xamarin.Essentials;
	using Xamarin.Forms;

	public class MapCustomPin : Xamarin.Forms.Maps.Map
	{
		public static readonly BindableProperty UserCurrentLocationProperty = BindableProperty.Create(nameof(UserCurrentLocation), typeof(Location), typeof(MapCustomPin), null, BindingMode.TwoWay);
		

		public MapCustomPin()
		{
			IsShowingUser = false;
		}


		public Location UserCurrentLocation
		{
			get => (Location)GetValue(UserCurrentLocationProperty);
			set => SetValue(UserCurrentLocationProperty, value);
		}
	}
}
