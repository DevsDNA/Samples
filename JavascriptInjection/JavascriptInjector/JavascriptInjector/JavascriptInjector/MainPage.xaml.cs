using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JavascriptInjector
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Cwv_SearchPreseed(object sender, EventArgs e)
        {
            DisplayAlert("Alert", "Button pressed", "OK");
        }

        private void BtnText_Clicked(object sender, EventArgs e)
        {
            cwv.SetSearchText("DevsDNA");
        }

        private void BtnSearch_Clicked(object sender, EventArgs e)
        {
            cwv.DoSearch();
        }

        private void BtnObserve_Clicked(object sender, EventArgs e)
        {
            cwv.Observe();
        }
    }
}
