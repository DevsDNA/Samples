namespace SampleXML.Features.Parse
{
	using System.Collections.Generic;
	using System.Xml;
	using System.Xml.Serialization;

	[XmlRoot(ElementName = "channel")]
	public class Channel
	{
		[XmlElement(ElementName = "title")]
		public string Title { get; set; }
		[XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
		public Link Link { get; set; }
		[XmlElement(ElementName = "link")]
		public string Link2 { get; set; }
		[XmlElement(ElementName = "description")]
		public string Description { get; set; }
		[XmlElement(ElementName = "lastBuildDate")]
		public string LastBuildDate { get; set; }
		[XmlElement(ElementName = "language")]
		public string Language { get; set; }
		[XmlElement(ElementName = "updatePeriod", Namespace = "http://purl.org/rss/1.0/modules/syndication/")]
		public string UpdatePeriod { get; set; }
		[XmlElement(ElementName = "updateFrequency", Namespace = "http://purl.org/rss/1.0/modules/syndication/")]
		public string UpdateFrequency { get; set; }
		[XmlElement(ElementName = "generator")]
		public string Generator { get; set; }
		[XmlElement(ElementName = "image")]
		public Image Image { get; set; }
		[XmlElement(ElementName = "item")]
		public List<Item> Item { get; set; }
	}
}