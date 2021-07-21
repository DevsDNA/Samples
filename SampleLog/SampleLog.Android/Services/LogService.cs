[assembly: Xamarin.Forms.Dependency(typeof(SampleLog.Droid.Services.LogService))]
namespace SampleLog.Droid.Services
{
	using Java.Lang;
	using System;
	using System.IO;

	public class LogService : SampleLog.Services.LogService
	{
		private int i = 0;

		public LogService()
		{
		}

		public override void LogLine(string line)
		{
			base.LogLine(line);
			Android.Util.Log.Verbose("SAMPLELOG", $"{i++:D6}:{DateTimeOffset.UtcNow} {line}");
		}

		protected override string GetLog()
		{
			//return base.GetLog();
			string log = "";
			string str;
			try
			{
				Process process = Runtime.GetRuntime().Exec("logcat -t 10");
				Stream st = process.InputStream;
				StreamReader streamReader = new StreamReader(st);
				log = streamReader.ReadToEnd();
			}
			catch (Java.IO.IOException e)
			{
				Android.Util.Log.Error("SAMPLELOG", tr: e, e.Message);
			}
			Xamarin.Forms.Application.Current.MainPage.DisplayAlert("", log, "OK");
			return log;
		}
	}
}