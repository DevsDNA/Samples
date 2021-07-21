namespace SampleLog.Services
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Runtime.CompilerServices;
	using System.Text;

	public interface ILogService
	{
		void LogLine(string line);
		void LogError(Exception ex);

		ObservableCollection<string> Log { get; }
	}
}
