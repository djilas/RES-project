namespace WebClient.Interfaces
{
	public interface IInputRequestParser
	{
		ParseResult Parse(string userRequest, string query, string fields, string connectedTo, string contentedType);
	}
}
