[assembly: Xamarin.Forms.Dependency(typeof(UninstallSample.Droid.Services.PackageService))]
namespace UninstallSample.Droid.Services
{
	using Android.Content;
	using System;

	public class PackageService : IPackageService
	{
		public void InstallPackage(string packageName)
		{
			try
			{
				Context context = MainActivity.CurrentActivity;
				Intent intent = null;

				Android.Net.Uri uri = Android.Net.Uri.Parse($"market://details?id={packageName}");
				intent = new Intent(Intent.ActionView, uri);
				intent.AddFlags(ActivityFlags.NoHistory | ActivityFlags.ClearWhenTaskReset | ActivityFlags.MultipleTask | ActivityFlags.NewTask);

				context.StartActivity(intent);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}

		public void UninstallPackage(string packageName)
		{
			try
			{
				Context context = MainActivity.CurrentActivity;

				Intent intent = new Intent(Intent.ActionDelete);
				intent.SetData(Android.Net.Uri.Parse($"package:{packageName}"));
				context.StartActivity(intent);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}
	}
}