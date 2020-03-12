namespace GeofencesSample.Services.Geofences
{
	using System.Collections.Generic;
	using Xamarin.Forms.Maps;

	public interface IGeofencesService
	{
		void SetGeofences(List<Position> targets, Position currentPosition);
	}
}