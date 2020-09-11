using DataConverters;
using DataModel;
using System;
using System.Web.Script.Serialization;

namespace WebClient
{
	public class Program
	 {
		public static void Main(string[] args)
		{
			Console.WriteLine("Welcome to application");
			Console.WriteLine("Possible request: GET, POST, PATCH, DELETE");

			while (true)
			{
				Console.Write("Please fill with request, resource and id (EXAMPLE: REQUEST/RESOURCE/ID): ");
				string userRequest = Console.ReadLine();
				Console.Write("Please enter the name and the type (EXAMPLE: name='matthew';type=3): ");
				string query = Console.ReadLine();
				Console.Write("Please enter the fileds (EXAMPLE: id;name;description): ");
				string fields = Console.ReadLine();

				ParseResult result = InputRequestParser.Parse(userRequest, query, fields);
				if(result.Success)
				{
					JSONtoXMLAdapter adapter = new JSONtoXMLAdapter();
					
					
					string JsonResult = adapter.ConvertJSONtoXML(result.JSON);
					
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
