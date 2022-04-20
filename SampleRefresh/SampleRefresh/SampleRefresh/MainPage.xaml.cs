namespace SampleRefresh
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Xamarin.Forms;

	public partial class MainPage
	{
		private Command updateCommand;
		private Command leftUpdateCommand;

		public MainPage()
		{
			updateCommand = new Command<string>(async p => await UpdateCommandExecuteAsync(p));
			leftUpdateCommand = new Command<string>(async p => await UpdateLeftCommandAsync(p));

			InitializeComponent();

			RefreshLeft.CommandParameter = "izquierda";
			RefreshLeft.Command = leftUpdateCommand;
			RefreshCenter.CommandParameter = "central";
			RefreshCenter.Command = updateCommand;
			RefreshRight.CommandParameter = "derecha";
			RefreshRight.Command = updateCommand;
		}

		private async Task UpdateLeftCommandAsync(string p)
		{
			await Task.Delay(1);
			LeftList.ItemsSource = Enumerable.Range(0, 15).Select(i => $"Lista actualizada{i}");
			RefreshLeft.IsRefreshing = false;
		}

		private async Task UpdateCommandExecuteAsync(string p)
		{
			await DisplayAlert("Información.", $"Actualizada lista {p}.", "OK");
			RefreshCenter.IsRefreshing = false;
			RefreshRight.IsRefreshing = false;
		}
	}
}
