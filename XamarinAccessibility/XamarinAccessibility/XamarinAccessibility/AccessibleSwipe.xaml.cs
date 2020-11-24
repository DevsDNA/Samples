using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinAccessibility
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccessibleSwipe : ContentPage
    {
        public AccessibleSwipe()
        {
            InitializeComponent();
        }

        private void SwipeItem_Favourite_Invoked(object sender, EventArgs e)
        {

        }

        private void SwipeItem_Delete_Invoked(object sender, EventArgs e)
        {

        }
    }
}