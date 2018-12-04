namespace USH.Services
{
    using System.Threading.Tasks;
    using USH.Models;

    public interface ICropService
    {
        Task<byte[]> CropImage(byte[] image, FaceRectangle bound);
    }
}
