using System;
using Xamarin.Forms;

namespace PdfViewerPill
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {            
            DependencyService.Get<IPdfViewer>().Open("https://aka.ms/xamebook");            
        }
    }
}
