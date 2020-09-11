namespace DataConverters.Model
{
	public class SqlToXmlResult
	{
		public StatusEnum Status { get; set; }

		public string Payload { get; set; }

		public SqlToXmlResult(string payload, StatusEnum status)
		{
			Status = Status;
			Payload = payload;
		}
	}
}
