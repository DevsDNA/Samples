namespace InAppPurchases
{
	using Plugin.InAppBilling;
	using System.Linq;
	using Xamarin.Forms;

	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			if (CrossInAppBilling.IsSupported)
			{
				var billing = CrossInAppBilling.Current;
				try
				{
					var connected = await billing.ConnectAsync();
					if (connected)
					{
						var subscriptionsList = await billing.GetProductInfoAsync(ItemType.Subscription, new string[] { "demo_suscription_1", "demo_suscription_2" });
						if (subscriptionsList.Any())
						{

						}
					}
				}
				catch (System.Exception ex)
				{
				}
				finally
				{
					await billing.DisconnectAsync();
				}
			}
		}
	}
}
