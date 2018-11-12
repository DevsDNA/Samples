[assembly: Xamarin.Forms.ExportRenderer(typeof(XFColorPicker.ColorViewBox), typeof(XFColorPicker.Droid.ColorViewBoxRenderer))]
namespace XFColorPicker.Droid
{
    using Android.Content;
    using Android.Graphics;
    using Android.Views;
    using Xamarin.Forms.Platform.Android;

    public class ColorViewBoxRenderer : ViewRenderer
    {
        private static readonly int[] colors = new[]
        {
            new Color(255,0,0,255).ToArgb(),
            new Color(255,0,255,255).ToArgb(),
            new Color(0,0,255,255).ToArgb(),
            new Color(0,255,255,255).ToArgb(),
            new Color(0,255,0,255).ToArgb(),
            new Color(255,255,0,255).ToArgb(),
            new Color(255,0,0,255).ToArgb()
        };

        private static readonly int[] illumination = new[]
        {
            new Color(255,255,255,255).ToArgb(),
            new Color(0,123,123,123).ToArgb(),
            new Color(255,0,0,0).ToArgb()
        };

        private ColorViewBox cViewBox;
        private Bitmap gradientBitmap;
        private ComposeShader cShader;
        private Paint drawPaint;

        public ColorViewBoxRenderer(Context context) : base(context)
        {
            SetWillNotDraw(false);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                cViewBox = (ColorViewBox)e.NewElement;
                this.Touch += GradientView_Touch;
                SetLayerType(LayerType.Software, null);
            }
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            if (cShader == null)
                cShader = CreateLinearGradient();
            if (drawPaint == null)
            {
                CreatePaint();
            }

            if (gradientBitmap == null)
            {
                gradientBitmap = Bitmap.CreateBitmap(this.Width, this.Height, Bitmap.Config.Argb8888);
                Canvas canvasBitmap = new Canvas(gradientBitmap);
                canvasBitmap.DrawPaint(drawPaint);
                canvas.DrawBitmap(gradientBitmap, 0, 0, drawPaint);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Touch -= GradientView_Touch;
                if (gradientBitmap != null)
                {
                    gradientBitmap.Recycle();
                    gradientBitmap.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void GradientView_Touch(object sender, TouchEventArgs e)
        {
            if (gradientBitmap == null)
                return;

            if (e.Event.Action == MotionEventActions.Down || e.Event.Action == MotionEventActions.Move)
            {
                var x = e.Event.GetX();
                var y = e.Event.GetY();
                var previewColor = GetCurrentColor(gradientBitmap, (int)x, (int)y);
                previewColor.A = 255;
                var scaleX = this.Width / cViewBox.Width;
                var scaleY = this.Height / cViewBox.Height;
                cViewBox.SelectedColor = new PickerColorModel(previewColor.ToColor(), x / (float)scaleX, y / (float)scaleY, cViewBox.Width, cViewBox.Height);
            }
        }

        private Color GetCurrentColor(Bitmap bitmap, int x, int y)
        {
            if (bitmap == null)
                return new Color(255, 255, 255, 255);

            if (x < 0)
                x = 0;
            if (y < 0)
                y = 0;
            if (x >= bitmap.Width)
                x = bitmap.Width - 1;
            if (y >= bitmap.Height)
                y = bitmap.Height - 1;

            int color = bitmap.GetPixel(x, y);
            return new Color(color);
        }

        private ComposeShader CreateLinearGradient()
        {
            var colorGradient = new LinearGradient(0, 0, this.Width, 0, colors, null, Shader.TileMode.Clamp);
            var illuminationGradient = new LinearGradient(0, 0, 0, this.Height, illumination, null, Shader.TileMode.Clamp);
            ComposeShader merged = new ComposeShader(colorGradient, illuminationGradient, PorterDuff.Mode.Darken);
            ComposeShader final = new ComposeShader(merged, illuminationGradient, PorterDuff.Mode.Lighten);
            return final;
        }

        private void CreatePaint()
        {
            drawPaint = new Paint();
            drawPaint.AntiAlias = true;
            drawPaint.FilterBitmap = true;
            drawPaint.SetShader(cShader);
        }
    }
}