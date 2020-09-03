using System.Collections.Generic;

namespace DataModel
{
	public class JSONFormat
	{
		public string Verb { get; set; }
		public string Noun { get; set; }

		public JSONFormat() { }
		public JSONFormat(string Verb, string Noun)
		{
			this.Verb = Verb;
			this.Noun = Noun;
		}
	}
}
