namespace UninstallSample
{
	using Xamarin.Forms;

	public partial class MainPage 
	{
		private readonly IPackageService packageService;
		private const string facebookPackageName = "com.facebook.katana";

		public MainPage()
		{
			InitializeComponent();
			packageService = DependencyService.Get<IPackageService>();
			InstallFacebookCommand = new Command(() => packageService.InstallPackage(facebookPackageName));
			UninstallFacebookCommand = new Command(() => packageService.UninstallPackage(facebookPackageName));
			BindingContext = this;
		}

		public Command InstallFacebookCommand { get; }
		public Command UninstallFacebookCommand { get; }


		protected override void OnAppearing()
		{
			base.OnAppearing();
			
		}
	}
}
