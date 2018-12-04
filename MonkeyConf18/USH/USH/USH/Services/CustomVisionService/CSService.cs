[assembly: Xamarin.Forms.Dependency(typeof(USH.Services.CSService))]
namespace USH.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using USH.Models;

    public class CSService : ICSService
    {
        private const string CSBaseUrl = "https://southcentralus.api.cognitive.microsoft.com/customvision/v2.0/Prediction";
        private const string CSPredictionKey = "YOURKEYHERE";
        private const string CSProjectId = "YOURKEYHERE";

        public async Task<CustomVisionModel> DetectPersonByImage(byte[] faceImageData)
        {
            CustomVisionModel result = null;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Prediction-key", CSPredictionKey);
            string url = $"{CSBaseUrl}/{CSProjectId}/image";

            using (ByteArrayContent content = new ByteArrayContent(faceImageData))
            {
                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    string responseJson = await response.Content.ReadAsStringAsync();
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomVisionModel>(responseJson);
                }
            }

            return result;
        }
    }
}
