namespace SampleLog
{
	using SampleLog.Services;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Xamarin.Forms;

	public partial class MainPage : ContentPage
	{
		private readonly ILogService logService;
		public MainPage()
		{
			logService = DependencyService.Get<ILogService>();
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			await Task.Delay(5000);
			logService.LogError(new Exception("SAMPLELOG EXCEPTION"));
		}
	}
}
