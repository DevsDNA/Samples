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
        IAlarmService alarmService;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            alarmService = DependencyService.Get<IAlarmService>();

            CreateButton.Clicked += async (s, e) =>
            {
                string alarmId = await alarmService.CreateAlarmAsync("Prueba 1", "Alarma creada desde una app xamarin forms", DateTime.Now.AddMinutes(-55), DateTime.Now.AddHours(2), 4);
                if (string.IsNullOrWhiteSpace(alarmId))
                    await DisplayAlert("Error", "No se ha podido crear la alerta", "ok");
                else
                    await DisplayAlert("Exito", $"Alerta creada con id:{alarmId}", "ok");
            };
        }
    }
}
