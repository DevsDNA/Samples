namespace USH.Features.Welcome
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WelcomeView
	{
		public WelcomeView ()
		{
			InitializeComponent ();

            foreach (Image img in RootLayout.Children)
            {
                var imgSource = img.Source as FileImageSource;
                img.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    Command = new Command(() =>
                    {
                        Navigation.PushAsync(new Manipulations.ManipulationView(imgSource));
                    })
                });
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}