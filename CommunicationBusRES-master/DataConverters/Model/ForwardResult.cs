namespace DataConverters.Model
{
	public enum StatusEnum { REJECTED = 3000, BAD_FORMAT = 5000, SUCCESS = 2000 }

	public class ForwardResult
	{
		public StatusEnum Status { get; set; }

		public string Payload { get; set; }

		public ForwardResult(StatusEnum status)
		{
			Status = status;
			Payload = "";
		}
	}
}
