namespace DisplayService
{
    using System.Collections.Generic;
    using Xamarin.Forms;

    public interface IDisplayService
    {
        IEnumerable<DisplayModel> GetPresentationDisplays();

        void PresentContentOnDisplay(ContentPage content, int displayId);
    }
}
