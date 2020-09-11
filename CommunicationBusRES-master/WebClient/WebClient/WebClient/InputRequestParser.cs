using DataModel;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace WebClient
{
	public class InputRequestParser
	{
		private static readonly List<string> ALLOWED_FILEDS = new List<string>() { "id", "name", "description" };
		private static readonly List<string> ALLOWED_REQUESTS = new List<string>() { "GET", "POST", "PATCH", "DELETE" };

		public static ParseResult Parse(string userRequest, string query, string fields)
		{
			string[] split = userRequest.Split(' ', '/');
			string operation = split[0].ToUpper();
			string noun = split[1];
			string id = split[2];

			if(!ALLOWED_REQUESTS.Contains(operation))
			{
				return new ParseResult(5000, "BAD_FORMAT", $"Requested operation: {operation} is not supported!");
			}

			if(!int.TryParse(id, out int intResult) && !string.Equals(operation, ALLOWED_REQUESTS[1]))
			{
				return new ParseResult(5000, "BAD_FORMAT", "Request parameter 'id' must be number!");
			}

			if (!string.IsNullOrEmpty(query))
			{
				string[] queryParts = query.Split(';');
				string[] nameParts = queryParts[0].Split('=');
				string[] typeParts = queryParts[1].Split('=');

				if (!string.Equals("name", nameParts[0]) || !nameParts[1].StartsWith("'") || !nameParts[1].EndsWith("'"))
				{
					return new ParseResult(5000, "BAD_FORMAT", "Wrong query 'name' parameter!");
				}

				if (!string.Equals("type", typeParts[0]) || !int.TryParse(typeParts[1], out int typeResult) )
				{
					return new ParseResult(5000, "BAD_FORMAT", "Wrong query 'type' parameter!");
				}
			}

			if(!string.IsNullOrEmpty(fields) && string.Equals(operation, ALLOWED_REQUESTS[0]))
			{
				string[] fieldsParts = fields.Split(';');
				if(fieldsParts.Length > 0)
				{
					foreach(string filed in fieldsParts)
					{
						if(!ALLOWED_FILEDS.Contains(filed))
						{
							return new ParseResult(5000, "BAD_FORMAT", $"Filed: {filed} not allowed!");
						}
					}
				} 
				else
				{
					return new ParseResult(5000, "BAD_FORMAT", "Something is wrong with the 'fileds' parameter!");
				}
			}

			var request = new Request(operation, $"/{noun}/{id}", query, fields);
			string json = new JavaScriptSerializer().Serialize(request);

			return new ParseResult(json);
		}
	}
}