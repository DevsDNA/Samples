using Sharpnado.Shades;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Shadows.Sample
{
    public partial class MainPage : ContentPage
    {
        readonly Random random = new Random();

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ((ObservableCollection<Shade>)DynamicShadow.Shades).Add(new Shade()
            {
                Color = Color.Red,
                Offset = GetRandomPoint()
            });
        }

        private Point GetRandomPoint()
        {
            return new Point(random.Next(-25,26), random.Next(-25, 26));
        }
    }
}
