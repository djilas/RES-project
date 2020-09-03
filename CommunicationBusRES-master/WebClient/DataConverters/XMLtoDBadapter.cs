using System;

namespace DataConverters
{
	public class XMLtoDBadapter
	{
		public string ConvertXMLtoQuery(string xmlstring)
		{

			ParseResultXML result = RequestParserXML.ParseXML(xmlstring);

			return "";
		}
	}
}
