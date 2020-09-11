using System.Collections.Generic;

namespace DataService.Interfaces.Repository
{
	public interface IDataRepository
	{
		List<Dictionary<string, object>> Select(string sql);

		bool ExecuteNoneQuery(string sql);
	}
}
