using System;
using Xamarin.Forms;

namespace ImageRecognition
{
    public class IrControl : View
    {
        public Action RunSession { get; set; }
        public Action PauseSession { get; set; }
    }
}
