namespace SkiaSharpLab
{
    using SkiaSharp;
    using SkiaSharp.Views.Forms;
    using System;
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
        private SKMatrix currentmatrix = SKMatrix.CreateIdentity();
        private SKMatrix combinedMatrix = SKMatrix.CreateIdentity();
        private SKBitmap pageBitmap;
        private float totalScale = 1;
        private float translationSpeedUp = 2;

        private bool isMoving = false; 
        private bool isChangingScale = false;

        public MainPage()
        {
            canvasView = new SKCanvasView();
            Content = canvasView;

            PanGestureRecognizer panGestureRecognizer = new PanGestureRecognizer();
            panGestureRecognizer.PanUpdated += PanGestureRecognizer_PanUpdated;

            canvasView.GestureRecognizers.Add(panGestureRecognizer);
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
                    startMatrix = currentmatrix;
                    scaleMatrix = SKMatrix.CreateIdentity();
                    break;
                case GestureStatus.Running:
                    translationMatrix = SKMatrix.CreateTranslation((float)e.TotalX* translationSpeedUp, (float)e.TotalY * translationSpeedUp);
                    canvasView.InvalidateSurface();
                    break;
                case GestureStatus.Canceled:
                case GestureStatus.Completed:
                    isMoving = false;
                    break;
            }
        }

        private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            SKMatrix.Concat(ref currentmatrix, translationMatrix, startMatrix);
            canvas.SetMatrix(currentmatrix);
            canvas.Clear();
            if (pageBitmap != null)
            {
                canvas.DrawBitmap(pageBitmap, 0,0);
            }
        }
    }
}


