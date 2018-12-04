[assembly: Xamarin.Forms.Dependency(typeof(USH.Droid.Services.OpenCVService))]
namespace USH.Droid.Services
{
    using Android.App;
    using Android.Content.Res;
    using Android.Graphics;
    using System.IO;
    using System.Threading.Tasks;
    using USH.Droid.Binding;
    using USH.Services;

    public class OpenCVService : IOpenCVService
    {
        OpenCVWrapper openCV = new OpenCVWrapper();
        public async Task<byte[]> FixImagePerspective(string filename)
        {
            byte[] bitmapData = null;
            AssetManager assetManager = Application.Context.Assets;
            var istr = assetManager.Open(filename);
            var bitmap = await BitmapFactory.DecodeStreamAsync(istr);

            var detectedBorders = openCV.DetectBorders(bitmap);
            if (detectedBorders != null)
            {
                var fixedImage = openCV.FixPerspective();
                using (var stream = new MemoryStream())
                {
                    fixedImage.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    bitmapData = stream.ToArray();
                }
            }
            return bitmapData;
        }
    }
}