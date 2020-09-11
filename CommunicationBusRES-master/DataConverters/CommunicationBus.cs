namespace DataConverters
{
	public class CommunicationBus
	{
		public void ForwardToXMLtoDBAdapter(string xmlstring)
		{

			XMLtoDBadapter adapter = new XMLtoDBadapter();
			adapter.ConvertXMLtoQuery(xmlstring);
		}

	}
}
