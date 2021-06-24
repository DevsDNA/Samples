using AppSignalR.Services;
using System;
using Xamarin.Forms;

namespace AppSignalR
{
    public partial class MainPage : ContentPage
    {
        private readonly ISignalRService signalRService;

        public MainPage()
        {
            signalRService = DependencyService.Get<ISignalRService>();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            signalRService.MessageReceived += SignalRService_MessageReceived;
            signalRService.Connecting += SignalRService_Connecting;
            signalRService.Connected += SignalRService_Connected;
        }

        protected override void OnDisappearing()
        {
            signalRService.MessageReceived -= SignalRService_MessageReceived; 
            signalRService.Connecting -= SignalRService_Connecting;
            signalRService.Connected -= SignalRService_Connected;
            base.OnDisappearing();
        }

        private void btConnect_Clicked(object sender, EventArgs e)
        {
            SignalRService.DeviceId = Convert.ToInt32(enId.Text);
            signalRService.StartWithReconnectionAsync();
        }

        private void SignalRService_Connected(object sender, EventArgs e)
        {
            lbState.Text = "Connected";
        }

        private void SignalRService_Connecting(object sender, EventArgs e)
        {
            lbState.Text = "Connecting...";
        }

        private void btSentToAll_Clicked(object sender, EventArgs e)
        {
            signalRService.SendMessageToAll(new MessageItem
            {
                Message = enMessage.Text,
                SourceId = Convert.ToInt32(enId.Text),
                TargetId = Convert.ToInt32(enTargetId.Text)
            });
        }

        private void btSentToDevice_Clicked(object sender, EventArgs e)
        {
            signalRService.SendMessageToDevice(new MessageItem
            {
                Message = enMessage.Text,
                SourceId = Convert.ToInt32(enId.Text),
                TargetId = Convert.ToInt32(enTargetId.Text)
            });
        }

        private async void SignalRService_MessageReceived(object sender, MessageItem e)
        {
            await DisplayAlert("Message reveived", e.Message, "Ok");
        }
    }
}
