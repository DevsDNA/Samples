[assembly: Xamarin.Forms.ExportRenderer(typeof(GamingWithMaps.MapCustomPin), typeof(GamingWithMaps.Droid.MapCustomPinRenderer))]
namespace GamingWithMaps.Droid
{
	using Android.Content;
	using Android.Gms.Maps;
	using Android.Gms.Maps.Model;
	using ReactiveUI;
	using System;
	using System.Linq;
	using Xamarin.Essentials;
	using Xamarin.Forms.Maps;
	using Xamarin.Forms.Maps.Android;
	using Xamarin.Forms.Platform.Android;

	public class MapCustomPinRenderer : MapRenderer
	{
		private MapCustomPin formsMap;
		private MapView nativeMap;
		private IDisposable userLocationSubscription;
		private MarkerOptions userMarkerOptions;
		private Location lastLocation;
		private GoogleMap googleMap;
		private Marker userMaker;

		public MapCustomPinRenderer(Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Maps.Map> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				userLocationSubscription?.Dispose();
				userLocationSubscription = null;
			}

			if (e.NewElement != null)
			{
				formsMap = (MapCustomPin)e.NewElement;
				nativeMap = Control;

				userLocationSubscription = formsMap.WhenAnyValue(fm => fm.UserCurrentLocation).WhereNotNull().Subscribe(UserLocationSubscription, ex => Console.WriteLine(ex.Message));
			}
		}

		private void UserLocationSubscription(Location location)
		{
			if (location == lastLocation)
				return;
			lastLocation = location;

			Position position = new Position(location.Latitude, location.Longitude);

			formsMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(1)));

			if (userMaker != null)
			{
				userMaker.Position = new LatLng(lastLocation.Latitude, lastLocation.Longitude);
				userMaker.Rotation = (float)(lastLocation?.Course ?? 0);
			}
		}

		protected override void OnMapReady(GoogleMap map)
		{
			base.OnMapReady(map);
			googleMap = map;
			userMaker = googleMap.AddMarker(CreateUserMarker());
			userMaker.Flat = true;
		}

		private MarkerOptions CreateUserMarker()
		{
			userMarkerOptions = new MarkerOptions();
			userMarkerOptions.SetPosition(new LatLng(lastLocation?.Latitude ?? 30, lastLocation?.Longitude ?? 30));
			userMarkerOptions.SetTitle("UserPostion");
			userMarkerOptions.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.icono));
			userMarkerOptions.SetRotation((float)UnitConverters.DegreesToRadians(lastLocation?.Course ?? 0));
			return userMarkerOptions;
		}
	}
}