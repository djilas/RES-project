using DataConverters.Model;

namespace DataService.Interfaces
{
	public interface IXmlToDatabaseAdapter
	{
		string Execute(string xmlstring);

		string ConvertToXMLResult(StatusEnum status, string result = "");
	}
}