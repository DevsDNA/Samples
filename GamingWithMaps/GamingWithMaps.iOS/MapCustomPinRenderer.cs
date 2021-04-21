[assembly: Xamarin.Forms.ExportRenderer(typeof(GamingWithMaps.MapCustomPin), typeof(GamingWithMaps.iOS.MapCustomPinRenderer))]
namespace GamingWithMaps.iOS
{
	using CoreGraphics;
	using CoreLocation;
	using MapKit;
	using ReactiveUI;
	using System;
	using System.Linq;
	using System.Reactive.Linq;
	using UIKit;
	using Xamarin.Essentials;
	using Xamarin.Forms;
	using Xamarin.Forms.Maps;
	using Xamarin.Forms.Maps.iOS;
	using Xamarin.Forms.Platform.iOS;

	public class MapCustomPinRenderer : MapRenderer
	{
		private MapCustomPin formsMap;
		private MKMapView nativeMap;
		private IDisposable userLocationSubscription;
		private Location lastLocation;
		private MKAnnotationView userAnnotationView;

		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				MKMapView oldMap = Control as MKMapView;
				if (oldMap != null)
				{
					oldMap.RemoveAnnotations(oldMap.Annotations);
					oldMap.GetViewForAnnotation = null;
					userLocationSubscription?.Dispose();
					userLocationSubscription = null;
				}
			}

			if (e.NewElement != null)
			{
				formsMap = (MapCustomPin)e.NewElement;
				nativeMap = Control as MKMapView;

				nativeMap.GetViewForAnnotation = GetViewForAnnotation;
				userLocationSubscription = formsMap.WhenAnyValue(fm => fm.UserCurrentLocation).WhereNotNull().Subscribe(UserLocationSubscription, ex => Console.WriteLine(ex.Message));
			}
		}

		private void UserLocationSubscription(Location location)
		{
			if (location == lastLocation)
				return;
			lastLocation = location;

			Position position = new Position(location.Latitude, location.Longitude);
			if (formsMap.Pins.Any())
				formsMap.Pins[0].Position = position;
			else
				formsMap.Pins.Add(new Pin { Position = position, Label = "UserLocation", Type = PinType.Generic });

			if (userAnnotationView != null)
				userAnnotationView.Transform = CGAffineTransform.MakeRotation((float)UnitConverters.DegreesToRadians(lastLocation?.Course ?? 0));

			//formsMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(1)));
			nativeMap.SetRegion(MKCoordinateRegion.FromDistance(new CLLocationCoordinate2D(position.Latitude, position.Longitude), 1000, 1000), false);
		}

		protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
		{
			if (annotation is MKUserLocation)
				return null;

			MKAnnotationView annotationView = mapView.DequeueReusableAnnotation("UserLocation");
			if (annotationView == null)
			{
				annotationView = new MKAnnotationView(annotation, "UserLocation");
				annotationView.Image = UIImage.FromFile("icono.jpeg");
				annotationView.CalloutOffset = new CGPoint(0, 0);
			}

			userAnnotationView = annotationView;
			annotationView.CanShowCallout = true;

			return annotationView;
		}
		private void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
		{
			if (!e.View.Selected)
			{
				e.View?.RemoveFromSuperview();
				e.View?.Dispose();
				e.View = null;
				//customPinView.RemoveFromSuperview();
				//customPinView.Dispose();
				//customPinView = null;
			}
		}
	}
}