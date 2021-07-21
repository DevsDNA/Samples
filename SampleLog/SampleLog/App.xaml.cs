namespace SampleLog
{
	using Microsoft.AppCenter;
	using Microsoft.AppCenter.Analytics;
	using Microsoft.AppCenter.Crashes;
	using SampleLog.Common;
	using SampleLog.Services;
	using Xamarin.Forms;

	public partial class App : Application
	{
		private readonly ILogService logService;

		public App()
		{
			logService = DependencyService.Get<ILogService>();
			InitializeComponent();

			MainPage = new MainPage();
		}

		protected override void OnStart()
		{
			AppCenter.Start(Constants.AppCenterKey, typeof(Analytics), typeof(Crashes));
			Crashes.SetEnabledAsync(true);
			Analytics.SetEnabledAsync(true);
			logService.LogLine("OnStart");
		}

		protected override void OnSleep()
		{
			logService.LogLine("OnSleep");
		}

		protected override void OnResume()
		{
			logService.LogLine("OnResume");
		}
	}
}
