[assembly: Xamarin.Forms.Dependency(typeof(TheAppOfTheDoctor.Features.Doctor.DoctorApiProvider))]
namespace TheAppOfTheDoctor.Features.Doctor
{
    using System;
    using System.Threading.Tasks;


    public class DoctorApiProvider : IDoctorApiProvider
    {
        public async Task<string> GetDescriptionFromServer()
        {
            string response = @"El Duodécimo Doctor es la duodécima encarnación del protagonista de la serie británica de ciencia ficción de la BBC Doctor Who. Es interpretado por Peter Capaldi,1​ tras la marcha de Matt Smith como el Undécimo Doctor en el especial navideño de 2013.2​ Capaldi ya había aparecido anteriormente en la serie interpretando a Lucius Caecilius Iucundus en el episodio de la cuarta temporada moderna Los fuegos de Pompeya, y había interpretado al burócrata John Frobisher en Los niños de la Tierra, un serial de 2009 del spin-off Torchwood.

Capaldi hizo su primera aparición como el Doctor en el especial navideño de 2013. Lo acompaña en la décima temporada moderna de 2017 los acompañantes del Nardole (Matt Lucas) y Bill Potts (Pearl Mackie).

En la narrativa de la serie, el Doctor es un alienígena de siglos de edad, de la raza de los Señores del Tiempo del planeta Gallifrey, que viaja por el tiempo y el espacio en su TARDIS, frecuentemente con acompañantes. Cuando el Doctor es herido mortalmente, puede regenerar su cuerpo, pero al hacerlo gana una nueva apariencia física y con ella, una nueva personalidad distintiva.

El 30 de enero de 2017, Capaldi confirmo que la décima temporada será la última que cuente con su participación, siendo el especial de navidad su ultima aparición como el Duodécimo Doctor.";
            await Task.Delay(TimeSpan.FromSeconds(2));
            return response;
        }
    }
}
