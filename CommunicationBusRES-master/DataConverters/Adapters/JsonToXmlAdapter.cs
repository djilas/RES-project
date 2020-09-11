using DataService.Interfaces;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace DataService
{
	public class JsonToXmlAdapter : IJsonToXmlAdapter
	{
		private readonly ICommunicationBus _communicationBus;

		public JsonToXmlAdapter(ICommunicationBus communicationBus)
		{
			_communicationBus = communicationBus;
		}

		public string ConvertJSONtoXML(string json)
		{
			XmlDocument doc = JsonConvert.DeserializeXmlNode(json, "Resource");
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

			doc.WriteTo(xmlTextWriter);

			return _communicationBus.Forward(stringWriter.ToString());
		}

		public string ConvertXMLtoJSON(string xmlString)
		{
			XDocument document = XDocument.Parse(xmlString);
			string status = document.Root.Element("STATUS").Value;
			string payload = document.Root.Element("PAYLOAD").Value;
			string statusCode = document.Root.Element("STATUS_CODE").Value;
		
			if(!payload.StartsWith("["))
			{
				payload = $"'{payload}'";
			}

			return "{ STATUS: '"+ status + "', STATUS_CODE: " + statusCode + ", PAYLOAD: "+ payload + " }";
		}
	}
}