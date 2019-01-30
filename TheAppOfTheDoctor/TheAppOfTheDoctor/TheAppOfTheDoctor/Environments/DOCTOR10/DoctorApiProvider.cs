[assembly: Xamarin.Forms.Dependency(typeof(TheAppOfTheDoctor.Features.Doctor.DoctorApiProvider))]
namespace TheAppOfTheDoctor.Features.Doctor
{
    using System;
    using System.Threading.Tasks;


    public class DoctorApiProvider : IDoctorApiProvider
    {
        public async Task<string> GetDescriptionFromServer()
        {
            string response = @"El Décimo Doctor es la décima encarnación del protagonista de la longeva serie de televisión británica de ciencia ficción de la BBC Doctor Who. Es interpretado por David Tennant, que aparece durante tres temporadas y ocho episodios especiales. Como anteriores encarnaciones del Doctor, el personaje también ha aparecido en otras publicaciones.

En la narrativa de la serie, el Doctor es un alienígena de siglos de edad, de la raza de los Señores del Tiempo del planeta Gallifrey que viaja a través del tiempo a bordo de su TARDIS, frecuentemente junto a compañeros. Cuando el Doctor es herido mortalmente, puede regenerar su cuerpo; al hacerlo, su forma física y su personalidad cambian. Tennant interpreta la décima encarnación del personaje.Los compañeros de esta encarnación han sido la ayudante de tienda de clase obrera Rose Tyler(Billie Piper) (a quién ya había conocido el Noveno Doctor), la estudiante de medicina Martha Jones(Freema Agyeman) y la feroz trabajadora eventual Donna Noble(Catherine Tate); finalmente se separa de todos ellos para el final de la temporada de 2008, en El fin del viaje, tras lo cual intentó viajar en solitario durante los especiales de 2008 a 2010.";
            await Task.Delay(TimeSpan.FromSeconds(2));
            return response;
        }
    }
}
