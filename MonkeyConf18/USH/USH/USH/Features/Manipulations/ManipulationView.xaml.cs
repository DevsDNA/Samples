namespace USH.Features.Manipulations
{
    using System.IO;
    using System.Linq;
    using USH.Services;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ManipulationView : ContentPage
	{
        private string imageFilename;
        private byte[] fixedImageBytes;
        private byte[] faceImageBytes;

        public ManipulationView ()
		{
			InitializeComponent ();
		}

        public ManipulationView(string filename) : this()
        {
            imageFilename = filename;
            Title = imageFilename;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            FileImageSource src = new FileImageSource()
            {
                File = imageFilename
            };

            ImageBase.Source = src;

            OpencvBtn.Clicked += async (s, e) =>
            {
                var ocvService = DependencyService.Get<IOpenCVService>();
                fixedImageBytes = await ocvService.FixImagePerspective(imageFilename);
                if (fixedImageBytes != null)
                {
                    Image image = new Image();
                    Stream stream = new MemoryStream(fixedImageBytes);
                    ImageBase.Source = ImageSource.FromStream(() => { return stream; });
                }
            };

            DetectBtn.Clicked += async (s, e) =>
            {
                var faceService = DependencyService.Get<IFaceService>();
                var result = await faceService.DetectFacesOnImage(fixedImageBytes);
                if (result != null)
                {
                    var cropService = DependencyService.Get<ICropService>();
                    faceImageBytes = await cropService.CropImage(fixedImageBytes, result.FirstOrDefault().BoundingBox);
                    if (faceImageBytes != null)
                    {
                        Image image = new Image();
                        Stream stream = new MemoryStream(faceImageBytes);
                        ImageBase.Source = ImageSource.FromStream(() => { return stream; });
                    }
                    InfoLabel.Text = $"Age: {result.FirstOrDefault().Attributes.Age}, Gender: {result.FirstOrDefault().Attributes.Gender}";

                }
            };

            UncoverBtn.Clicked += async (s, e) =>
            {
                var csService = DependencyService.Get<ICSService>();
                var result = await csService.DetectPersonByImage(faceImageBytes);
                if (result != null && result.Predictions != null)
                {
                    InfoLabel.Text += $", Superhero: {result.Predictions.FirstOrDefault().TagName}, sure at {result.Predictions.FirstOrDefault().Probability * 100}%";
                }
            };
        }
    }
}