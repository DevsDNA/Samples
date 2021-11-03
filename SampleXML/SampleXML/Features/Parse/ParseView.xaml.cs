namespace SampleXML.Features.Parse
{
	using SampleXML.Common;
	using System.Xml;
	using System.Xml.Serialization;

	public partial class ParseView 
	{
		public ParseView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			using (XmlReader xmlReader = XmlReader.Create(AppKeys.DevsDNAFeed))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Rss));
				Rss rss = (Rss)serializer.Deserialize(xmlReader);
				ListPost.ItemsSource = rss.Channel.Item;
			}
		}
	}
}