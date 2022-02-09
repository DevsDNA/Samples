namespace SampleJson.Features.Steps
{
	using Newtonsoft.Json.Linq;
	using SampleJson.Features.SerializeSample;
	using SampleJson.Models;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Text.Json.Nodes;
	using Xamarin.Forms;

	public partial class StepsView : ContentPage
	{
		public StepsView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			string json = GetJson();
			ReadSystemTextJson(json);
			ReadNewtonsoft(json);
		}

		private void ReadSystemTextJson(string json)
		{
			JsonObject jsonObject = JsonNode.Parse(json).AsObject();

			JsonNode root = jsonObject.Root;
			JsonArray enterprisesNode = root["Enterprises"].AsArray();

			List<Enterprise> list = new List<Enterprise>();
			foreach (JsonNode item in enterprisesNode)
			{
				JsonObject enterpriseJsonObject = item.AsObject();

				var enterprise = new Enterprise
				{
					JsonId = enterpriseJsonObject["id"].GetValue<int>(),
					FullCompanyName = enterpriseJsonObject["Name"].ToString(),
					Description = enterpriseJsonObject["Description"].ToString(),
				};

				JsonArray personsJsonArray = enterpriseJsonObject["Persons"].AsArray();
				enterprise.Persons = personsJsonArray.Select(j => j.ToString()).ToList();

				list.Add(enterprise);
			}

			ListSystemText.ItemsSource = list;
		}

		private void ReadNewtonsoft(string json)
		{
			JObject jObject = JObject.Parse(json);
			JToken root = jObject.Root;

			JArray enterpriseJArray = root["Enterprises"] as JArray;

			List<Enterprise> list = new List<Enterprise>();
			foreach (JToken item in enterpriseJArray)
			{
				JObject enterpriseJObject = item as JObject;
				var enterprise = new Enterprise
				{
					JsonId = enterpriseJObject["id"].Value<int>(),
					FullCompanyName = enterpriseJObject["Name"].ToString(),
					Description = enterpriseJObject["Description"].ToString(),
				};

				JArray personsJsonArray = enterpriseJObject["Persons"] as JArray;
				enterprise.Persons = personsJsonArray.Select(j => j.ToString()).ToList();

				list.Add(enterprise);
			}

			ListNewtonsoft.ItemsSource = list;
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