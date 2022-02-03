namespace SampleJson.Features.SerializeSample
{
	using SampleJson.Models;
	using System.IO;
	using System.Text;

	public partial class SerializeSampleView
	{
		public SerializeSampleView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			string json = GetJson();

			// Newtonsoft
			JsonModel model1 = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonModel>(json);
			ListNewtonsoft.ItemsSource = model1.Enterprises;

			// System.Text.Json
			JsonModel model2 = System.Text.Json.JsonSerializer.Deserialize<JsonModel>(json);
			ListSystemText.ItemsSource = model2.Enterprises;
		}


		private string GetJson()
		{
			using (StreamReader sr = new StreamReader("SampleJsonFile.json", Encoding.UTF8))
			{
				return sr.ReadToEnd();
			}
		}
	}
}