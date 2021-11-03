namespace SampleXML.Features.Parse
{
	using System.Xml;
	using System.Xml.Serialization;

	[XmlRoot(ElementName = "rss")]
	public class Rss
	{
		[XmlElement(ElementName = "channel")]
		public Channel Channel { get; set; }

		[XmlAttribute(AttributeName = "version")]
		public string Version { get; set; }

		[XmlAttribute(AttributeName = "content", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Content { get; set; }
		
		[XmlAttribute(AttributeName = "wfw", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Wfw { get; set; }
		
		[XmlAttribute(AttributeName = "dc", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Dc { get; set; }
		
		[XmlAttribute(AttributeName = "atom", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Atom { get; set; }
		
		[XmlAttribute(AttributeName = "sy", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Sy { get; set; }
		
		[XmlAttribute(AttributeName = "slash", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Slash { get; set; }
	} }