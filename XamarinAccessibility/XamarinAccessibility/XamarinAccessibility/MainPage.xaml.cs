using System;
using Xamarin.Forms;

namespace XamarinAccessibility
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btTextSize_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TextSizeView());
        }

        private async void btHighContrast_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HighContrastView());
        }

        private async void btCareWithColors_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CareWithColorsView());
        }

        private async void btButtonSize_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ButtonSizeView());
        }

        private async void btInterfaceDescriptor_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InterfaceDescriptorView());
        }

        private async void btImageAlternativeText_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ImageAlternativeTextView());
        }

        private async void btAccessibleNavigation_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccessibleNavigationView());
        }

        private async void btAccessibleActions_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccessibleActionsView());
        }

        private async void btAccessibleSwipe_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccessibleSwipe());
        }

        private async void btAccessibleDragAndDrop_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccessibleDragAndDrop());
        }
    }
}
