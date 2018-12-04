namespace USH.Services
{
    using System.Threading.Tasks;
    using USH.Models;

    public interface ICSService
    {
        Task<CustomVisionModel> DetectPersonByImage(byte[] faceImageData);
    }
}
