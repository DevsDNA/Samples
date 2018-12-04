[assembly: Xamarin.Forms.Dependency(typeof(USH.Services.FaceService))]
namespace USH.Services
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using USH.Models;

    public class FaceService : IFaceService
    {
        private const string FaceSubscriptionKey = "YOURKEYHERE";
        private const string FaceRequestParameters = "returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=age,gender";
        private string FaceUrl = $"https://westeurope.api.cognitive.microsoft.com/face/v1.0/detect?{FaceRequestParameters}";

        public async Task<List<FaceModel>> DetectFacesOnImage(byte[] imageData)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", FaceSubscriptionKey);

            using (ByteArrayContent content = new ByteArrayContent(imageData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                var response = await client.PostAsync(FaceUrl, content);

                string contentString = await response.Content.ReadAsStringAsync();

                var responseResult = JsonConvert.DeserializeObject<List<FaceModel>>(contentString);

                if (responseResult != null)
                    return responseResult;
                else
                {
                    await App.Current.MainPage.DisplayAlert("Ops...", "no se ha identificado ninguna cara...", "ok");
                    return null;
                }
            }
        }
    }
}
