using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFMacOS
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

        void Button_ListViewPage_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ListViewPage());
        }

        void Button_TabbedPage_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new TabbedPage());
        }

        void Button_CollectionViewPage_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new CollectionViewPage());
        }
    }
}
