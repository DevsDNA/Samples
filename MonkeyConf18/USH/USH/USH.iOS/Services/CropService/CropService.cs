[assembly: Xamarin.Forms.Dependency(typeof(USH.iOS.Services.CropService))]
namespace USH.iOS.Services
{
    using CoreGraphics;
    using Foundation;
    using System;
    using System.Threading.Tasks;
    using UIKit;
    using USH.Models;
    using USH.Services;

    public class CropService : ICropService
    {
        public async Task<byte[]> CropImage(byte[] image, FaceRectangle bound)
        {
            byte[] croppedBytes = null;
            NSData data = NSData.FromArray(image);
            UIImage uiImage = UIImage.LoadFromData(data);

            CGRect rect = new CGRect(bound.X, bound.Y, bound.Width, bound.Height);

            using (CGImage cr = uiImage.CGImage.WithImageInRect(rect))
            {
                UIImage cropped = UIImage.FromImage(cr);
                using (NSData imageData = cropped.AsJPEG())
                {
                    croppedBytes = new byte[imageData.Length];
                    System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, croppedBytes, 0, Convert.ToInt32(imageData.Length));
                }
            }

            return croppedBytes;
        }
    }
}