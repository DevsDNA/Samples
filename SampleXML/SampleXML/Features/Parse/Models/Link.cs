namespace SampleXML.Features.Parse
{
	using System.Xml;
	using System.Xml.Serialization;

	[XmlRoot(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
	public class Link
	{
		[XmlAttribute(AttributeName = "href")]
		public string Href { get; set; }
		[XmlAttribute(AttributeName = "rel")]
		public string Rel { get; set; }
		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }
	}
}