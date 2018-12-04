namespace USH.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using USH.Models;

    public interface IFaceService
    {
        Task<List<FaceModel>> DetectFacesOnImage(byte[] image);


    }
}
