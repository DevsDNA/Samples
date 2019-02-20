[assembly:Xamarin.Forms.Dependency(typeof(TheAppOfTheDoctor.Features.Doctor.DoctorApiProvider))]
namespace TheAppOfTheDoctor.Features.Doctor
{
    using System;
    using System.Threading.Tasks;


    public class DoctorApiProvider : IDoctorApiProvider
    {
        public async Task<string> GetDescriptionFromServer()
        {
            string response = @"La TARDIS (Time And Relative Dimensions In Space; Tiempo Y Dimensiones Relativas en el Espacio) es una nave espacio-temporal de ficción que forma parte de la serie británica de ciencia ficción Doctor Who. Tiene la forma de las legendarias cabinas de policía británicas de los años sesenta, y es recordada por ser muchísimo más grande por dentro que por fuera.
                Fue nombrada TARDIS por primera vez por la nieta del Doctor, Susan Foreman, en el primer episodio de la serie, An Unearthly Child.

La TARDIS es el producto del avanzado conocimiento de los Señores del Tiempo, civilización extraterrestre a la cual pertenece el personaje principal de la serie, el Doctor.Una TARDIS apropiadamente mantenida y pilotada puede transportar a sus ocupantes a cualquier punto en el espacio - tiempo. El interior de una TARDIS es mucho más grande que su exterior, que puede cambiar su forma con el 'circuito camaleónico' de la nave, pudiendo así camuflarse en el entorno. En la serie, el Doctor pilota una TARDIS Tipo 40 ya obsoleta, cuyo circuito camaleónico está dañado, dejando a la nave atascada con la apariencia de una cabina de policía de Londres de los años 60, después de una visita allí en 1963 en el primer episodio de la serie.

Doctor Who está tan arraigado en la cultura popular británica, que no solo la forma de la cabina de policía es inmediatamente asociada con la serie, en vez de asociarse con la policía; sino que la palabra 'TARDIS' es usada para describir cualquier objeto que parezca más grande por dentro que por fuera.La palabra 'TARDIS' es marca registrada de la British Broadcasting Corporation.";
            await Task.Delay(TimeSpan.FromSeconds(2));
            return response;
        }
    }
}
