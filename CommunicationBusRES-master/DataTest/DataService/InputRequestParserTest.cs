using NUnit.Framework;
using WebClient;
using WebClient.Interfaces;

namespace DataTest.DataService
{
	[TestFixture]
	public class InputRequestParserTest
	{
		IInputRequestParser parser;


		static readonly string USER_REQUEST = "GET/RESOURCE/1";
		static readonly string QUERY = "name='TEST';description='TEST'";
		static readonly string FIELDS = "id;name;description";

		[SetUp]
		public void Init()
		{
			parser = new InputRequestParser();
		}

		[Test]
		public void ParseNoDataThrowExceptionAndReturnBadFormat()
		{
			ParseResult result = parser.Parse("", "", "", "", "");

			Assert.NotNull(result);
			Assert.False(result.Success);
			Assert.AreEqual(result.ErrorNumber, 5000);
			Assert.AreEqual(result.ErrorCode, "BAD_FORMAT");
		}

		[Test]
		public void ParseAllOK()
		{
			ParseResult result = parser.Parse(USER_REQUEST, QUERY, FIELDS, "", "");
			Assert.NotNull(result);
			Assert.True(result.Success);
		}

		[Test]
		public void ParseGetALLOK()
		{
			ParseResult result = parser.Parse("GET/RESOURCES", QUERY, FIELDS, "", "");
			Assert.NotNull(result);
			Assert.True(result.Success);
		}

		[Test]
		public void ParsePostAllOK()
		{
			ParseResult result = parser.Parse("POST/RESOURCES", QUERY, FIELDS, "", "");
			Assert.NotNull(result);
			Assert.True(result.Success);
		}

		[Test]
		public void ParseUnknownOperation()
		{
			ParseResult result = parser.Parse("ZZZ/RESOURCES", QUERY, FIELDS, "", "");

			Assert.NotNull(result);
			Assert.False(result.Success);
			Assert.AreEqual(result.ErrorNumber, 5000);
			Assert.AreEqual(result.ErrorCode, "BAD_FORMAT");
		}

		[Test]
		public void ParseWrongQuery()
		{
			ParseResult result = parser.Parse(USER_REQUEST, "xyz", FIELDS, "", "");

			Assert.NotNull(result);
			Assert.False(result.Success);
			Assert.AreEqual(result.ErrorNumber, 5000);
			Assert.AreEqual(result.ErrorCode, "BAD_FORMAT");
		}

		[Test]
		public void ParseWrongFields()
		{
			ParseResult result = parser.Parse(USER_REQUEST, QUERY, "xyz", "", "");

			Assert.NotNull(result);
			Assert.False(result.Success);
			Assert.AreEqual(result.ErrorNumber, 5000);
			Assert.AreEqual(result.ErrorCode, "BAD_FORMAT");
		}
	}
}
