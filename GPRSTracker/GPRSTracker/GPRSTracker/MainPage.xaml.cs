namespace GPRSTracker
{
    using GPRSTracker.Commons;
    using GPRSTracker.Services;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void GPRSRegisterButton_Clicked(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(GPRSNumberEntry.Text))
            {
                DependencyService.Get<ISMS>().Send(new Models.SMSMessage { To = GPRSNumberEntry.Text, Message = Messages.Register });
            }
        }
    }
}