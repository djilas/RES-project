namespace DataConverters
{
	public class CommunicationBus
	{
		public string ForwardToXMLtoDBAdapter(string xmlstring)
		{

			XMLtoDBadapter adapter = new XMLtoDBadapter();
			adapter.ConvertXMLtoQuery(xmlstring);

			return "";
		}

	}
}
