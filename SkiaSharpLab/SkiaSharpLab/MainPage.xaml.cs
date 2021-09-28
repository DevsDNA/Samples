namespace SkiaSharpLab
{
    using SkiaSharp;
    using SkiaSharp.Views.Forms;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        private readonly HttpClient httpClient = new HttpClient();
        private SKCanvasView canvasView;
        private SKMatrix translationMatrix = SKMatrix.CreateIdentity();
        private SKMatrix scaleMatrix = SKMatrix.CreateIdentity();
        private SKMatrix startMatrix = SKMatrix.CreateIdentity();
        private SKMatrix matrix = SKMatrix.CreateIdentity();
        private SKMatrix combinedMatrix = SKMatrix.CreateIdentity();
        private SKBitmap pageBitmap;
        private float currentTranslationX;
        private float currentTranslationY;
        private float currentScale = 1;
        private float totalTranslationX;
        private float totalTranslationY;
        private float totalScale = 1;
        private bool isMoving = false; 
        private bool isChangingScale = false;

        public MainPage()
        {
            canvasView = new SKCanvasView();
            Content = canvasView;

            PanGestureRecognizer panGestureRecognizer = new PanGestureRecognizer();
            panGestureRecognizer.PanUpdated += PanGestureRecognizer_PanUpdated;
            PinchGestureRecognizer pinchGestureRecognizer = new PinchGestureRecognizer();
            pinchGestureRecognizer.PinchUpdated += PinchGestureRecognizer_PinchUpdated;
            canvasView.GestureRecognizers.Add(panGestureRecognizer);
            canvasView.GestureRecognizers.Add(pinchGestureRecognizer);
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            string url = "https://www.dogbible.com/_ipx/f_webp,fit_cover,s_330x330/https://www.dogbible.com/resized/ingles-pointer-temperament_default.webp";

            using (Stream stream = await httpClient.GetStreamAsync(url))
            using (MemoryStream memStream = new MemoryStream())
            {
                await stream.CopyToAsync(memStream);
                memStream.Seek(0, SeekOrigin.Begin);
                pageBitmap = SKBitmap.Decode(memStream);
                canvasView.PaintSurface += CanvasView_PaintSurface;
                canvasView.InvalidateSurface();
            }
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            Debug.WriteLine($"e.PanUpdate = {e.TotalX} - {e.TotalY}");
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    isMoving = true;
                    
                    break;
                case GestureStatus.Running:
                    currentTranslationX = (float)e.TotalX;
                    currentTranslationY = (float)e.TotalY;
                    canvasView.InvalidateSurface();
                    break;
                case GestureStatus.Canceled:
                case GestureStatus.Completed:
                    totalTranslationX += currentTranslationX;
                    totalTranslationY += currentTranslationY;
                    isMoving = false;
                    break;
            }
        }

        private void PinchGestureRecognizer_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            Debug.WriteLine($"e.Scale = {e.Scale}");


            switch (e.Status)
            {
                case GestureStatus.Started:
                    isChangingScale = true;
                    break;
                case GestureStatus.Running:
                    currentScale = (float)e.Scale;
                    Debug.WriteLine(Scale);
                    canvasView.InvalidateSurface();
                    break;
                case GestureStatus.Completed:
                case GestureStatus.Canceled:
                    isChangingScale = false;
                    break;
                default:
                    break;
            }
        }

        private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            if (pageBitmap != null)
            {
                if (isChangingScale)
                {
                    if (currentScale > 1)
                        totalScale += currentScale * 0.5f;
                    else
                        totalScale -= currentScale * 0.5f;
                }
                SKRect rect = new SKRect(currentTranslationX + totalTranslationX, currentTranslationY + totalTranslationY,
                                             currentTranslationX + totalTranslationX + pageBitmap.Width * totalScale,
                                             currentTranslationY + totalTranslationY + pageBitmap.Height * totalScale);
                canvas.DrawBitmap(pageBitmap, rect);
            }
        }
    }
}

