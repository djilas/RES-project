using DataConverters.Model;

namespace DataService.Interfaces
{
	public interface ICommunicationBus
	{
		string Forward(string xmlstring);
	}
}
