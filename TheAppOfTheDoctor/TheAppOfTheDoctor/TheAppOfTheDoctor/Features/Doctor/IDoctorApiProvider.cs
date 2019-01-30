namespace TheAppOfTheDoctor.Features.Doctor
{
    using System.Threading.Tasks;

    public interface IDoctorApiProvider
    {
        Task<string> GetDescriptionFromServer();
    }
}