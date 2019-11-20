using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinCApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private readonly ICcodeCaller cCodeCaller;

        public MainPage()
        {
            InitializeComponent();

            cCodeCaller = DependencyService.Get<ICcodeCaller>();
        }

        private void BtnCallC_Clicked(object sender, EventArgs e)
        {
            LblHelloC.Text = cCodeCaller.GetHelloC();
        }
    }
}
