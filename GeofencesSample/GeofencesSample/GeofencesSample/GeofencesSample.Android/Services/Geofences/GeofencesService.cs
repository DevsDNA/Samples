[assembly:Xamarin.Forms.Dependency(typeof(GeofencesSample.Droid.Services.Geofences.GeofencesService))]
namespace GeofencesSample.Droid.Services.Geofences
{
	using Android.App;
	using Android.Content;
	using Android.Gms.Location;
	using Android.Locations;
    using GeofencesSample.Services.Geofences;
    using System.Collections.Generic;
	using System.Linq;
	using Xamarin.Forms.Maps;

	public class GeofencesService : IGeofencesService
	{
		private GeofencingClient geofencingClient;
		private PendingIntent geofencePendingIntent;
		private int numberOfGeofences = 10;

		public GeofencesService()
		{
			geofencingClient = LocationServices.GetGeofencingClient(MainActivity.MainContext);
		}

		public void SetGeofences(List<Position> targets, Position currentPosition)
		{
			List<Position> nearTargets = GetCloseTargets(targets, currentPosition);
			List<IGeofence> geofences = new List<IGeofence>();
			foreach (Position item in nearTargets)
			{
				IGeofence geofence = new GeofenceBuilder()
					.SetRequestId($"{item.Latitude}_{item.Longitude}")
					.SetCircularRegion(item.Latitude, item.Longitude, 1000)
					.SetLoiteringDelay(60 * 1000)
					.SetTransitionTypes(Geofence.GeofenceTransitionEnter | Geofence.GeofenceTransitionExit | Geofence.GeofenceTransitionDwell)
					.SetExpirationDuration((long)365 * 24 * 60 * 60 * 1000)
					.Build();
				geofences.Add(geofence);
			}
			GeofencingRequest geofencingRequest = new GeofencingRequest.Builder()
				.AddGeofences(geofences)
				.Build();

			geofencingClient.AddGeofences(geofencingRequest, GetGeofencePendingIntent());
		}


		private PendingIntent GetGeofencePendingIntent()
		{
			if (geofencePendingIntent != null)
				return geofencePendingIntent;

			Intent intent = new Intent(MainActivity.MainContext, Java.Lang.Class.FromType(typeof(GeofenceBroadcastReceiver)));
			// We use FLAG_UPDATE_CURRENT so that we get the same pending intent back when calling addGeofences() and removeGeofences().
			geofencePendingIntent = PendingIntent.GetBroadcast(MainActivity.MainContext, 0, intent, PendingIntentFlags.UpdateCurrent);
			return geofencePendingIntent;
		}

		private List<Position> GetCloseTargets(List<Position> targets, Position position)
		{
			List<KeyValuePair<double, Position>> distanceAndTargetList = new List<KeyValuePair<double, Position>>();
			Location currentLocation = new Location(string.Empty)
			{
				Longitude = position.Longitude,
				Latitude = position.Latitude
			};

			foreach (Position item in targets)
			{
				Location location = new Location(string.Empty)
				{
					Latitude = item.Latitude,
					Longitude = item.Longitude
				};

				distanceAndTargetList.Add(new KeyValuePair<double, Position>(currentLocation.DistanceTo(location), item));
			}

			IEnumerable<Position> items = distanceAndTargetList.OrderBy(x => x.Key).Select(x => x.Value);

			return items.Count() > numberOfGeofences ? items.Take(numberOfGeofences).ToList() : items.ToList();
		}
	}
}