namespace XFCustomTemplates
{
    using System;
    using Xamarin.Forms;
    using XFCustomTemplates.Views;

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void BtViewTemplateOne_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewTemplateExampleOnePage());
        }

        private async void BtViewTemplateTwo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewTemplateExampleTwoPage());
        }

        private async void BtViewTemplateWithStyle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewTemplateWithStyleExamplePage());
        }

        private async void BtReplaceControlTemplate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReplaceControlTemplateExamplePage());
        }

        private async void BtPageTemplate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageTemplateExamplePage());
        }
    }
}
