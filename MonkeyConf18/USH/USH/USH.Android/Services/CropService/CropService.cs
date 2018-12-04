[assembly: Xamarin.Forms.Dependency(typeof(USH.Droid.Services.CropService))]
namespace USH.Droid.Services
{
    using Android.Graphics;
    using System.IO;
    using System.Threading.Tasks;
    using USH.Models;
    using USH.Services;

    public class CropService : ICropService
    {
        public async Task<byte[]> CropImage(byte[] image, FaceRectangle bound)
        {
            byte[] croppedBitmapData = null;
            var bitmap = await BitmapFactory.DecodeByteArrayAsync(image, 0, image.Length);

            var croppedBitmap = Bitmap.CreateBitmap(bitmap, bound.X, bound.Y, bound.Width, bound.Height);
            using (var stream = new MemoryStream())
            {
                croppedBitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                croppedBitmapData = stream.ToArray();
            }

            return croppedBitmapData;
        }
    }
}