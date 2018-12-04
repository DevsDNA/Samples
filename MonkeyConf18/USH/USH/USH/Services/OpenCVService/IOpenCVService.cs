namespace USH.Services
{
    using System.Threading.Tasks;

    public interface IOpenCVService
    {
        Task<byte[]> FixImagePerspective(string filename);
    }
}
