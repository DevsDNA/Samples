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


		private async void Button_Clicked(object sender, System.EventArgs e)
		{
			if (CrossInAppBilling.IsSupported)
			{
				var billing = CrossInAppBilling.Current;
				try
				{
					var connected = await billing.ConnectAsync();
					if (connected)
					{
						var subscriptionsList = await billing.GetProductInfoAsync(ItemType.InAppPurchase, new string[] { "demo_product" });
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
