namespace SampleXML.Features.Parse
{
	using System.Xml;
	using System.Xml.Serialization;

	[XmlRoot(ElementName = "image")]
	public class Image
	{
		[XmlElement(ElementName = "url")]
		public string Url { get; set; }
		[XmlElement(ElementName = "title")]
		public string Title { get; set; }
		[XmlElement(ElementName = "link")]
		public string Link2 { get; set; }
		[XmlElement(ElementName = "width")]
		public string Width { get; set; }
		[XmlElement(ElementName = "height")]
		public string Height { get; set; }
	}
}