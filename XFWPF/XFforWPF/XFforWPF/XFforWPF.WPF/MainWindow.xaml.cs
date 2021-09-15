namespace XFforWPF.WPF
{
	using Xamarin.Forms;

	public partial class MainWindow 
	{
		public MainWindow()
		{
			InitializeComponent();

			Forms.Init();
			LoadApplication(new XFforWPF.App());
		}
	}
}
