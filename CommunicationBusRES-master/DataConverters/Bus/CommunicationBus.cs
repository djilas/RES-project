using DataService.Interfaces;

namespace DataService
{
	public class CommunicationBus : ICommunicationBus
	{
		private readonly IXmlToDatabaseAdapter _adapter;

		public CommunicationBus(IXmlToDatabaseAdapter adapter)
		{
			_adapter = adapter;
		}

		public string Forward(string xmlString)
		{
			string xmlResultString = _adapter.Execute(xmlString);
			return xmlResultString;
		}
	}
}