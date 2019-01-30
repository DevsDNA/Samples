[assembly: Xamarin.Forms.Dependency(typeof(TheAppOfTheDoctor.Features.Doctor.DoctorApiProvider))]
namespace TheAppOfTheDoctor.Features.Doctor
{
    using System;
    using System.Threading.Tasks;


    public class DoctorApiProvider : IDoctorApiProvider
    {
        public async Task<string> GetDescriptionFromServer()
        {
            string response = @"El Undécimo Doctor es la undécima encarnación del protagonista de la longeva serie de televisión de ciencia ficción de la BBC Doctor Who. Es interpretado por Matt Smith,​ y fue presentado en la conclusión del especial El fin del tiempo, sucediendo en el papel a David Tennant, que interpretó al Décimo Doctor. Smith interpretó al personaje durante tres temporadas, incluido el especial 50 aniversario de la serie y abandonó el rol en el episodio especial de Navidad de 2013.

En la narrativa de la serie, el Doctor es un alienígena de siglos de edad, de la raza de los Señores del Tiempo del planeta Gallifrey, que viaja por el tiempo y el espacio en su TARDIS, frecuentemente con acompañantes. Cuando el Doctor es herido mortalmente, puede regenerar su cuerpo, pero al hacerlo gana una nueva apariencia física y con ella, una nueva personalidad distintiva. Smith interpreta a la undécima encarnación, un hombre de genio vivo, pero compasivo cuya apariencia juvenil no concuerda con su temperamento más perspicaz y cansado del mundo.

Su voz para Latinoamérica es hecha por Luís Carreño, conocido por doblar a Bob Esponja en la serie homónima. Mientras que para España es hecha por Alejandro Martínez, quien también dobló al actor original en The Crown.";
            await Task.Delay(TimeSpan.FromSeconds(2));
            return response;
        }
    }
}
