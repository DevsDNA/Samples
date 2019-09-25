using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace FingerprintReader
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void BtnStatus_Clicked(object sender, EventArgs e)
        {
            FingerprintAvailability status = await CrossFingerprint.Current.GetAvailabilityAsync();
            LblStatus.Text = status.ToString();
        }

        private async void BtnAuthenticate_Clicked(object sender, EventArgs e)
        {
            FingerprintAuthenticationResult result = await CrossFingerprint.Current.AuthenticateAsync("Tap the fingerprint sensor");
            if (result.Authenticated)
            {
                LblAuthenticate.Text = "VALIDATION DONE";
                LblAuthenticate.TextColor = Color.Green;
            }
            else
            {
                LblAuthenticate.Text = "VALIDATION FAILED";
                LblAuthenticate.TextColor = Color.Red;
            }
        }
    }
}
