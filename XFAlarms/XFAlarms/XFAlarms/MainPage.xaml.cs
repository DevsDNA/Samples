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
        private string alarmId;
        private IAlarmService alarmService;

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
                alarmId = await alarmService.CreateAlarmAsync("Prueba 1", "Alarma creada desde una app xamarin forms", DateTime.Now.AddMinutes(5), DateTime.Now.AddHours(2), 4);
                if (string.IsNullOrWhiteSpace(alarmId))
                    await DisplayAlert("Error", "No se ha podido crear la alerta", "ok");
                else
                    await DisplayAlert("Exito", $"Alerta creada con id:{alarmId}", "ok");
            };

            CheckButton.Clicked += async (s, e) =>
            {
                bool exist = await alarmService.CheckIfAlarmAlreadyExistAsync(alarmId);
                if (exist)
                    await DisplayAlert("Info", "La alarma existe", "ok");
                else
                    await DisplayAlert("Info", "La alarma no existe", "ok");
            };

            DeleteButton.Clicked += async (s, e) =>
            {
                bool deleted = await alarmService.DeleteAlarmAsync(alarmId);
                if (deleted)
                    await DisplayAlert("Info", "La alarma ha sido eliminada", "ok");
                else
                    await DisplayAlert("error", "No se ha podido eliminar la alarma", "ok");
            };
        }
    }
}
