namespace XamComToolkitSample
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xamarin.CommunityToolkit.Effects;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            SafeAreaEffect.SetSafeArea(root, new Xamarin.CommunityToolkit.Helpers.SafeArea(true));
        }

        private void CameraView_MediaCaptured(object sender, Xamarin.CommunityToolkit.UI.Views.MediaCapturedEventArgs e)
        {

        }
    }
}
