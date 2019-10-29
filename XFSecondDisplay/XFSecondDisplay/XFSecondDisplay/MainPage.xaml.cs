namespace XFSecondDisplay
{
    using DisplayService;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using Xamarin.Forms;

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Present_Clicked(object sender, EventArgs e)
        {
            var service = DependencyService.Get<IDisplayService>();
            var displays = service.GetPresentationDisplays();
            service.PresentContentOnDisplay(new SecondaryScreenView(), displays.Last().Id);
        }
    }
}
