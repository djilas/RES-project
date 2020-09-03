using System.Data;

namespace DataConverters
{
	public class RequestParserXML
	{
		public static ParseResultXML ParseXML(string xmlString)
		{
			string[] split = xmlString.Split('>');
			string getOperation = split[2];
			string[] split1 = getOperation.Split('<');
			string operation = split1[0]; // We get operation here from xml. (GET)
			string getBaseandType = split[4];
			string[] split2 = getBaseandType.Split('<');
			string[] split3 = split2[0].Split('/');
			string Base = split3[1]; //We cetch base to select from (resource)
			string Type = split3[2]; // type i guess its id (3)
			string getNameAndType = split[6];
			string[] getNameAndType1 = getNameAndType.Split('<');
			string getName = getNameAndType1[0].Split(';')[0]; // NAME (name='pera')
			string getType = getNameAndType1[0].Split(';')[1]; // TYPE (type=2)
			string getFields = split[8];
			string[] fields = getFields.Split('<'); //(id;name;description)
			string field = fields[0].Replace(';', ','); //(id,name,description)
			System.Console.WriteLine(field);




			return null; 
		}
	}
}
