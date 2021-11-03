[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.Button), typeof(Visual.Droid.MetroButtonRenderer), new[] { typeof(Visual.MetroVisual) })]
namespace Visual.Droid
{
	using Android.Content;
	using Android.Graphics.Drawables;
	using Xamarin.Forms;
	using Xamarin.Forms.Platform.Android;

	public class MetroButtonRenderer : Xamarin.Forms.Platform.Android.FastRenderers.ButtonRenderer
	{
		public MetroButtonRenderer(Context context) : base(context)
		{

		}

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged(e);
			if(e.NewElement != null)
			{
				Control.SetTextColor(Android.Graphics.Color.White);

				GradientDrawable background = new GradientDrawable();
				background.SetColor(System.Drawing.Color.Black.ToArgb());
				background.SetStroke(5, Android.Graphics.Color.White);
				Control.SetBackground(background);

				Element.HeightRequest = 38;
				Element.TextTransform = TextTransform.None;
			}
		}
	}
}