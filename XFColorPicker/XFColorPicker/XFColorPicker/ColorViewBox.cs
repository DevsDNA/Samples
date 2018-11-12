namespace XFColorPicker
{
    using Xamarin.Forms;

    public class ColorViewBox : BoxView
    {
        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(PickerColorModel), typeof(ColorViewBox), null);

        public PickerColorModel SelectedColor
        {
            get => (PickerColorModel)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }
    }
}
