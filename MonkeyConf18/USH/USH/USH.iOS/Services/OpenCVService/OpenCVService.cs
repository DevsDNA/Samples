[assembly: Xamarin.Forms.Dependency(typeof(USH.iOS.Services.OpenCVService))]
namespace USH.iOS.Services
{
    using System;
    using System.Threading.Tasks;
    using USH.Services;
    using USH.iOS.Binding;
    using Foundation;
    using UIKit;

    public class OpenCVService : IOpenCVService
    {
        public async Task<byte[]> FixImagePerspective(string filename)
        {
            byte[] imageBytes = null;

            DocScanner ocv = new DocScanner();

            UIImage image = UIImage.FromBundle(filename);
            NSObject[] result = ocv.DetectBorders(image);
            if (result != null)
            {
                UIImage fixedImage = ocv.FixPerspective();
                if (fixedImage != null)
                {
                    using (NSData imageData = fixedImage.AsJPEG())
                    {
                        imageBytes = new byte[imageData.Length];
                        System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, imageBytes, 0, Convert.ToInt32(imageData.Length));
                    }
                    image.Dispose();
                    fixedImage.Dispose();
                }
            }

            return imageBytes;
        }
    }
}