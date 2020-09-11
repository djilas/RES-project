using DataConverters.Adapters.Sql;
using DataConverters.Interfaces;
using DataConverters.Repository;
using DataService;
using DataService.Interfaces;
using DataService.Interfaces.Repository;
using DataService.Repository;
using System;
using System.Data.Entity;
using WebClient.Interfaces;

namespace WebClient
{
	public class Program
	 {
		public static void Main(string[] args)
		{
			using(var context = new RESDatabaseContext())
			{
				ISqlBuilder sqlBuilder = new SqlBuilder();
				IDataRepository repository = new DataRepository(context);
				IXmlToDatabaseAdapter xmlToDatabaseAdapter = new XmlToDatabaseAdapter(sqlBuilder, repository);

				ICommunicationBus communication = new CommunicationBus(xmlToDatabaseAdapter);
				IJsonToXmlAdapter jsonToXmlAdapter = new JsonToXmlAdapter(communication);
				IInputRequestParser inputRequestParser = new InputRequestParser();

				Console.WriteLine("Welcome to application");
				Console.WriteLine("Possible request: GET, POST, PATCH, DELETE");

				while (true)
				{
					Console.Write("Please fill with request, resource and id (EXAMPLE: REQUEST/RESOURCE/ID): ");
					string userRequest = Console.ReadLine();
					Console.Write("Please enter the name and the type (EXAMPLE: name='matthew';type=3) [*not required]: ");
					string query = Console.ReadLine();
					Console.Write("Please enter the fileds (EXAMPLE: id;name;description) [*not required]: ");
					string fields = Console.ReadLine();
					Console.Write("Please enter 'connected to' [*not required]: ");
					string connecteTo = Console.ReadLine();

					Console.Write("Please enter 'connected type' [*not required]: ");
					string connecteType = Console.ReadLine();

					ParseResult result = inputRequestParser.Parse(userRequest, query, fields, connecteTo, connecteType);
					if (result.Success)
					{
						string xml = jsonToXmlAdapter.ConvertJSONtoXML(result.JSON);
						string jsonResult = jsonToXmlAdapter.ConvertXMLtoJSON(xml);
						Console.WriteLine(jsonResult);
					}
					else
					{
						Console.WriteLine("Program executed with error:");
						Console.WriteLine($"- Code: {result.ErrorCode}");
						Console.WriteLine($"- Number: {result.ErrorNumber}");
						Console.WriteLine($"- Message: {result.ErrorMessage}");
					}
				}
			}
		}
	}
}
