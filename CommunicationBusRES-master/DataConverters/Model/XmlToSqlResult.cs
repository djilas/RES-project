namespace DataConverters.Model
{
	public enum SqlStatementTypeEnum { NONE, SELECT, INSERT, UPDATE, DELETE };

	public enum TableEnum { None, Resource, ResourceType, Relation, RelationType }

	public class XmlToSqlResult
	{
		public bool Success { get; set; }
		public SqlStatementTypeEnum Type { get; set; }
		public string Statement { get; set; }

		public string ErrorMessage { get; set; }

		public XmlToSqlResult(string statement, SqlStatementTypeEnum type)
		{
			Success = true;
			Type = type;
			Statement = statement;
		}

		public XmlToSqlResult(string message)
		{
			Success = false;
			ErrorMessage = message;
		}
	}
}