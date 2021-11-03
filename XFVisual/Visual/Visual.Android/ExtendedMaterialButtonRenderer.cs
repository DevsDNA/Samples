[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.Button), typeof(Visual.Droid.ExtendedMaterialButtonRenderer), new[] { typeof(Xamarin.Forms.VisualMarker.MaterialVisual), typeof(Xamarin.Forms.VisualMarker.DefaultVisual) })]
namespace Visual.Droid
{
	using Android.Content;
	using Xamarin.Forms.Material.Android;
	using Xamarin.Forms.Platform.Android;

	public class ExtendedMaterialButtonRenderer : MaterialButtonRenderer
	{
		public ExtendedMaterialButtonRenderer(Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged(e);

			if(e.NewElement != null)
			{
				Control.SetBackgroundColor(Android.Graphics.Color.Red);
			}
		}
	}
}