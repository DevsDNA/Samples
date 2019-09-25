namespace UninstallSample
{
	using System.Threading.Tasks;

	public interface IPackageService
	{
		void InstallPackage(string packageName);
		void UninstallPackage(string packageName);
	}
}
