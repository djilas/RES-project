using DataConverters.Interfaces;
using DataConverters.Model;
using DataService.Interfaces;
using DataService.Interfaces.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DataService
{
	public enum StatusEnum { REJECTED = 3000, BAD_FORMAT = 5000, SUCCESS = 2000 }

	public class XmlToDatabaseAdapter : IXmlToDatabaseAdapter
	{
		public static readonly string RESULT_TAG = "RESULT";
		public static readonly string STATUS_TAG = "STATUS";
		public static readonly string PAYLOAD_TAG = "PAYLOAD";
		public static readonly string STATUS_CODE_TAG = "STATUS_CODE";

		private static readonly XDeclaration XML_DECLARATION = new XDeclaration("0.1", "utf-8", "yes");

		private readonly ISqlBuilder _sqlBuilder;
		private readonly IDataRepository _dataRepository;

		public XmlToDatabaseAdapter(ISqlBuilder sqlBuilder, IDataRepository dataRepository)
		{
			_sqlBuilder = sqlBuilder;
			_dataRepository = dataRepository;
		}


		public string Execute(string xmlstring)
		{
			XmlToSqlResult xmlToSqlResult = _sqlBuilder.Build(xmlstring);
			if (xmlToSqlResult.Success)
			{
					switch (xmlToSqlResult.Type)
					{
						case SqlStatementTypeEnum.SELECT:
							List<Dictionary<string, object>> selectResult = _dataRepository.Select(xmlToSqlResult.Statement);
							return ConvertToXMLResult(StatusEnum.SUCCESS, JsonConvert.SerializeObject(selectResult, Formatting.Indented));
						case SqlStatementTypeEnum.DELETE:
						case SqlStatementTypeEnum.UPDATE:
						case SqlStatementTypeEnum.INSERT:
						return _dataRepository.ExecuteNoneQuery(xmlToSqlResult.Statement)
									? ConvertToXMLResult(StatusEnum.SUCCESS, $"Operation '{xmlToSqlResult.Type}' success!")
									: ConvertToXMLResult(StatusEnum.REJECTED, $"Operation '{xmlToSqlResult.Type}' rejected!");
					default:
							return ConvertToXMLResult(StatusEnum.REJECTED, $"Operation '{xmlToSqlResult.Type}' rejected!");
					}
				
			} 
			else
			{
				return ConvertToXMLResult(StatusEnum.BAD_FORMAT, $"Error: ${xmlToSqlResult.ErrorMessage}");
			}
		}

		public string ConvertToXMLResult(StatusEnum status, string payload = "")
		{
			XDocument document = new XDocument(XML_DECLARATION,
				new XElement(RESULT_TAG,
					new XElement(STATUS_TAG, status.ToString()),
					new XElement(STATUS_CODE_TAG, (int)status),
					new XElement(PAYLOAD_TAG, payload)));

			return document.ToString();
		}
	}
}