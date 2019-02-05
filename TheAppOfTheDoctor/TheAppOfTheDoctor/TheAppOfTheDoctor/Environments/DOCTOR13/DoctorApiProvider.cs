[assembly: Xamarin.Forms.Dependency(typeof(TheAppOfTheDoctor.Features.Doctor.DoctorApiProvider))]
namespace TheAppOfTheDoctor.Features.Doctor
{
    using System;
    using System.Threading.Tasks;


    public class DoctorApiProvider : IDoctorApiProvider
    {
        public async Task<string> GetDescriptionFromServer()
        {
            string response = @"La Decimotercer Doctor es la reencarnación actual de El Doctor, el protagonista ficticio de la serie de televisión de la BBC Doctor Who. El personaje es interpretado por la actriz británica Jodie Whittaker, la primera mujer en retratar al personaje en la serie. En la narración de la serie, el Doctor es un viajero en el tiempo, un extraterrestre humanoide de una raza conocida como los Señores del Tiempo. Para explicar la retirada de los actores en la serie, la serie introdujo el concepto narrativo de regeneración, un medio para que un Señor del Tiempo obtenga una nueva fisiología, apariencia y una nueva personalidad distinta cuando el El Doctor se acerca al final de la encarnación actual.

Jodie Whittaker apareció por primera vez al final del Especial de Navidad de 2017 Twice Upon a Time y actua como El Doctor desde 2018 en adelante en la undécima temporada.";
            await Task.Delay(TimeSpan.FromSeconds(2));
            return response;
        }
    }
}
