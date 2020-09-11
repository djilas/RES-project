namespace DataService.Interfaces
{
	public interface IJsonToXmlAdapter
	{
		string ConvertJSONtoXML(string JSON_string);

		string ConvertXMLtoJSON(string XML_string);
	}
}
