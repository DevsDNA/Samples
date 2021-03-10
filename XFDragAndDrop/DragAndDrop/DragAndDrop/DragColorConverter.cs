namespace DragAndDrop
{
    using System;
    using System.Globalization;
    using Xamarin.Forms;


    public class DragColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isBeingDragged = (bool?)value;
            var result = (isBeingDragged ?? false) ? Color.LightGray : Color.Azure;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}