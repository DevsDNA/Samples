namespace AndroidScreenRecorder
{
    using AndroidScreenRecorder.Services;
    using System;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        private IScreenRecordingService service;

        public MainPage()
        {
            InitializeComponent();

            service = DependencyService.Get<IScreenRecordingService>();
        }

        private void StartRec_Clicked(object sender, EventArgs e)
        {
            service.AskForStartRecording();
        }

        private void StopRec_Clicked(object sender, EventArgs e)
        {
            service.StopRecording();
        }
    }
}
