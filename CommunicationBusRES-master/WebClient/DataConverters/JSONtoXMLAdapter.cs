using Newtonsoft.Json;
using System.IO;
using System.Xml;

namespace DataConverters
{
	public class JSONtoXMLAdapter
	{
		public string ConvertJSONtoXML(string JSON_string)
		{

			XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(JSON_string, "Resource");
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			doc.WriteTo(xmlTextWriter);

			CommunicationBus adapter = new CommunicationBus();
			adapter.ForwardToXMLtoDBAdapter(stringWriter.ToString());


			return stringWriter.ToString();
		}

		public string ConvertXMLtoJSON(string XML_string)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(XML_string);
			string jsonString = JsonConvert.SerializeXmlNode(doc);
			return jsonString;
		}

	}
}
