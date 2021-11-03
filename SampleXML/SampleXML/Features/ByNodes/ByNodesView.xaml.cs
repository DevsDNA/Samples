namespace SampleXML.Features.ByNodes
{
	using SampleXML.Common;
	using System.Collections.Generic;
	using System.Xml;

	public partial class ByNodesView
	{
		public ByNodesView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();


			BlogDevsDNA blog = new BlogDevsDNA();
			using (XmlReader xmlReader = XmlReader.Create(AppKeys.DevsDNAFeed))
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(xmlReader);

				XmlElement rss = xmlDocument["rss"];


				// channel
				XmlElement channel = rss["channel"];
				string value = channel.Value;

				// title
				XmlElement titleNode = channel["title"];
				blog.Title = titleNode.InnerText;

				// post
				XmlNodeList items = channel.GetElementsByTagName("item");
				blog.Posts = new List<Post>();

				foreach (XmlNode item in items)
				{
					Post post = new Post
					{
						Title = item["title"].InnerText,
						Description = item["description"].InnerText
					};
					blog.Posts.Add(post);
				}
			}

			ListPost.ItemsSource = blog.Posts;
		}
	}
}