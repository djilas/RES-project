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
	}
}
