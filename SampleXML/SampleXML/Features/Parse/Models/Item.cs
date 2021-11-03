namespace SampleXML.Features.Parse
{
	using System.Collections.Generic;
	using System.Xml;
	using System.Xml.Serialization;

	[XmlRoot(ElementName = "item")]
	public class Item
	{
		[XmlElement(ElementName = "title")]
		public string Title { get; set; }

		[XmlElement(ElementName = "link")]
		public string Link2 { get; set; }
		
		[XmlElement(ElementName = "comments")]
		public List<string> Comments { get; set; }
		
		[XmlElement(ElementName = "creator", Namespace = "http://purl.org/dc/elements/1.1/")]
		public string Creator { get; set; }
		
		[XmlElement(ElementName = "pubDate")]
		public string PubDate { get; set; }
		
		[XmlElement(ElementName = "category")]
		public List<string> Category { get; set; }
		
		[XmlElement(ElementName = "guid")]
		public Guid Guid { get; set; }
		
		[XmlElement(ElementName = "description")]
		public string Description { get; set; }
		
		[XmlElement(ElementName = "encoded", Namespace = "http://purl.org/rss/1.0/modules/content/")]
		public string Encoded { get; set; }
		
		[XmlElement(ElementName = "commentRss", Namespace = "http://wellformedweb.org/CommentAPI/")]
		public string CommentRss { get; set; }
	}
}