using DataConverters.Model;

namespace DataConverters.Interfaces
{
	public interface ISqlBuilder
	{
		XmlToSqlResult Build(string xml);
	}
}
