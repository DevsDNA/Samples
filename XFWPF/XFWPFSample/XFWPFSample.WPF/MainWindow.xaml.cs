namespace XFWPFSample.WPF
{
	using Xamarin.Forms;

	public partial class MainWindow 
	{
		public MainWindow()
		{
			InitializeComponent();
			Forms.Init();
			LoadApplication(new XFWPFSample.App());
		}
	}
}
