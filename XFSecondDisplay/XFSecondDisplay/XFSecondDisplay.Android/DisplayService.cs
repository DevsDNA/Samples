[assembly: Xamarin.Forms.Dependency(typeof(DisplayService.Droid.DisplayService))]
namespace DisplayService.Droid
{
    using Android.App;
    using Android.Hardware.Display;
    using Android.Views;
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;
    using XFSecondDisplay.Droid;

    public class DisplayService : IDisplayService
    {
        private readonly DisplayManager displayManager;

        public DisplayService()
        {
            displayManager = (DisplayManager)MainActivity.CurrentActivity.GetSystemService(Android.Content.Context.DisplayService);
        }

        public IEnumerable<DisplayModel> GetPresentationDisplays()
        {
            List<DisplayModel> displays = new List<DisplayModel>();
            var nativeDisplays = displayManager.GetDisplays(DisplayManager.DisplayCategoryPresentation);
            foreach (var display in nativeDisplays)
            {
                displays.Add(new DisplayModel(display.DisplayId, display.Name));
            }

            return displays;
        }

        public void PresentContentOnDisplay(ContentPage content, int displayId)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            var display = displayManager.GetDisplay(displayId);
            if (display != null)
            {
                using (Presentation presentation = new Presentation(MainActivity.CurrentActivity, display))
                {
                    presentation.SetContentView(ConvertFormsToNative(content.Content, display));
                    presentation.Show();
                }
            }
        }

        private static Android.Views.View ConvertFormsToNative(Xamarin.Forms.View view, Display display)
        {
            Android.Graphics.Point size = new Android.Graphics.Point();
            display.GetRealSize(size);
            var rectSize = new Rectangle(0, 0, size.X / 1.5, size.Y / 1.5);

            var renderer = Platform.CreateRendererWithContext(view, Android.App.Application.Context);
            var nativeView = renderer.View;
            renderer.Tracker.UpdateLayout();
            var layoutParams = new ViewGroup.LayoutParams((int)rectSize.Width, (int)rectSize.Height);
            nativeView.LayoutParameters = layoutParams;
            view.Layout(rectSize);
            nativeView.Layout(0, 0, (int)view.WidthRequest, (int)view.HeightRequest);

            return nativeView;
        }
    }
}