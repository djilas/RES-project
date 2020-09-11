namespace WebClient
{
	public class ParseResult
	{
		public bool Success { get; }
		public string JSON { get; }

		public int ErrorNumber { get; }

		public string ErrorCode { get; }

		public string ErrorMessage { get; }

		public ParseResult(string json)
		{
			JSON = json;
			Success = true;
		}

		public ParseResult(int number, string code, string message)
		{
			ErrorMessage = message;
			ErrorNumber = number;
			ErrorCode = code;
			Success = false;
		}
	}
}