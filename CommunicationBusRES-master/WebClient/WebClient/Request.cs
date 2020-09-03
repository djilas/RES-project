namespace DataModel
{
	public class Request
	{
		public string Verb { get; set; }
		public string Noun { get; set; }

		public string Query { get; set; }

		public string Fields { get; set; }

		public Request() { }

		public Request(string verb, string noun, string query, string fileds)
		{
			Verb = verb;
			Noun = noun;
			Query = query;
			Fields = fileds;
		}
	}
}