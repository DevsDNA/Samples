namespace XFColorPicker
{
    using Xamarin.Forms;

    public class PickerColorModel
    {
        public PickerColorModel(Color selectedColor, float x, float y, double width, double height)
        {
            Color = selectedColor;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Color Color { get; set; }

        public float X { get; set; }

        public float Y { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }
    }
}
