namespace SampleXML.Features.CreateXML
{
	using System;
	using System.IO;
	using System.Text;
	using System.Xml;
	using Xamarin.Forms;

	public partial class CreateXMLView
	{
		public CreateXMLView()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			XmlDocument xmlDocument = CreateXmlDocument();

			BtnPretty.CommandParameter = xmlDocument;
			BtnPretty.Command = new Command<XmlDocument>(xml => SaveXml(xml));

			LblXML.Text = xmlDocument.OuterXml;
		}

		private XmlDocument CreateXmlDocument()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;

			xmlDocument.AppendChild(CreateRootElement(xmlDocument));
			return xmlDocument;
		}

		private XmlElement CreateRootElement(XmlDocument xmlDocument)
		{
			XmlElement root = xmlDocument.CreateElement("Enterprise");

			// Data of enterprise
			XmlElement name = xmlDocument.CreateElement("Name");
			name.InnerText = "DevsDNA";
			root.AppendChild(name);

			XmlElement description = xmlDocument.CreateElement("Description");
			description.InnerText = "Ayudarte a desarrollar increíbles aplicaciones móviles está en nuestro ADN.";
			root.AppendChild(description);


			// Persons
			XmlNode persons = xmlDocument.CreateNode(XmlNodeType.Element, "Persons", string.Empty);
			persons.AppendChild(CreatePerson(xmlDocument, "Beatriz", "Directora ejecutiva"));
			persons.AppendChild(CreatePerson(xmlDocument, "Yeray", "Director técnico"));
			persons.AppendChild(CreatePerson(xmlDocument, "Ciani", "Desarrollador"));
			persons.AppendChild(CreatePerson(xmlDocument, "Marco", "Desarrollador"));
			persons.AppendChild(CreatePerson(xmlDocument, "Jorge", "Desarrollador"));
			persons.AppendChild(CreatePerson(xmlDocument, "Luis", "Desarrollador"));
			persons.AppendChild(CreatePerson(xmlDocument, "Alicia", "Diseñadora de experiencias de usuario"));
			root.AppendChild(persons);

			return root;
		}

		private XmlElement CreatePerson(XmlDocument xmlDocument, string name, string job)
		{
			var person = xmlDocument.CreateElement("Person");

			// name
			person.SetAttribute("Name", name);

			// job
			var attribute = xmlDocument.CreateAttribute("Job");
			attribute.Value = job;
			person.Attributes.Append(attribute);


			return person;
		}




		private void SaveXml(XmlDocument xmlDocument)
		{
			StringBuilder sb = new StringBuilder();

			XmlWriterSettings settings = new XmlWriterSettings
			{
				Indent = true,
				IndentChars = "  ",
				NewLineChars = "\r\n",
				NewLineHandling = NewLineHandling.Replace
			};

			using (XmlWriter writer = XmlWriter.Create(sb, settings))
			{
				xmlDocument.Save(writer);
			}
			LblPrettyXML.Text = sb.ToString();
		}
	}
}