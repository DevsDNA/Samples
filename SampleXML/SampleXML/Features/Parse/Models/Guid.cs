namespace SampleXML.Features.Parse
{
	using System.Xml;
	using System.Xml.Serialization;

	[XmlRoot(ElementName = "guid")]
	public class Guid
	{
		[XmlAttribute(AttributeName = "isPermaLink")]
		public string IsPermaLink { get; set; }
		[XmlText]
		public string Text { get; set; }
	}
}