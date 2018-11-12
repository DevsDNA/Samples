namespace XFColorPicker
{
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            colorPicker.PropertyChanged += ColorPicker_PropertyChanged;

            PickerColorModel colorRed = new PickerColorModel(Color.FromRgb(0.960784316062927, 0.0196078438311815, 0.24705882370472), 18.15784f, 239.9193f, 464.033590170904, 250);
            PickerColorModel colorBlue = new PickerColorModel(Color.FromRgb(0.23137255012989, 0.631372570991516, 0.807843148708344), 197.3824f, 95.76613f, 464.033590170904, 250);

            Button redButton = new Button()
            {
                BackgroundColor = Color.FromRgb(0.960784316062927, 0.0196078438311815, 0.24705882370472),
                WidthRequest = 100,
                HeightRequest = 60
            };

            redButton.Clicked += (s, o) =>
            {
                colorPicker.SelectedColor = colorRed;
            };

            Button blueButton = new Button()
            {
                BackgroundColor = Color.FromRgb(0.23137255012989, 0.631372570991516, 0.807843148708344),
                WidthRequest = 100,
                HeightRequest = 60
            };

            blueButton.Clicked += (s, o) =>
            {
                colorPicker.SelectedColor = colorBlue;
            };

            ButtonBar.Children.Add(redButton);
            ButtonBar.Children.Add(blueButton);
        }

        private void ColorPicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ColorViewBox.SelectedColor))
            {
                if (colorPicker.SelectedColor != null)
                {
                    if (colorPicker.SelectedColor.X <= colorPicker.Width && colorPicker.SelectedColor.Y <= colorPicker.Height && colorPicker.SelectedColor.X >= 0 && colorPicker.SelectedColor.Y >= 0)
                    {
                        colorSelected.BackgroundColor = colorPicker.SelectedColor.Color;
                        marker.IsVisible = true;
                        var scaleX = colorPicker.Width / colorPicker.SelectedColor.Width;
                        var scaleY = colorPicker.Height / colorPicker.SelectedColor.Height;
                        marker.Margin = new Thickness(colorPicker.SelectedColor.X*scaleX, colorPicker.SelectedColor.Y*scaleY, 0, 0);
                    }
                }
            }
        }
    }
}
