using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFAlarms.Services;

namespace XFAlarms
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            CreateButton.Clicked += (s, e) =>
            {
                DependencyService.Get<IAlarmService>().CreateAlarm("Prueba 1", "Alarma creada desde una app xamarin forms", DateTime.Now.AddMinutes(-55), DateTime.Now.AddHours(2), 4, "");
            };
        }
    }
}
