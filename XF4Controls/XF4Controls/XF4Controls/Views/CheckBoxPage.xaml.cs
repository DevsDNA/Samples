using System.ComponentModel;
using Xamarin.Forms;

namespace XF4Controls.Views
{
    public partial class CheckBoxPage : ContentPage
    {
        public CheckBoxPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            DisplayAlert("Información", "CheckBox cambiado", "OK");
        }
    }
}