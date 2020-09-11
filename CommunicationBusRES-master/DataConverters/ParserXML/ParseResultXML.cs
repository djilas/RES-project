namespace DataConverters
{
	public class ParseResultXML
	{
		public bool Success { get; }
		public string SQL { get; }

		public int ErrorNumber { get; }

		public string ErrorCode { get; }

		public string ErrorMessage { get; }

		public ParseResultXML(string sql)
		{
			SQL = sql;
			Success = true;
		}

		public ParseResultXML(int number, string code, string message)
		{
			ErrorMessage = message;
			ErrorNumber = number;
			ErrorCode = code;
			Success = false;
		}


	}
}
