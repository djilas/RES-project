using DataService;
using DataService.Interfaces;
using Moq;
using NUnit.Framework;

namespace DataTest.DataService
{	
	[TestFixture]
	public class JsonToXmlAdapterTest
	{
		Mock<ICommunicationBus> busMock;

		IJsonToXmlAdapter jsonToXmlAdapter;

		[SetUp]
		public void Init()
		{
			busMock = new Mock<ICommunicationBus>();
			jsonToXmlAdapter = new JsonToXmlAdapter(busMock.Object);
		}

		[Test]
		public void ConvertJSONToXMLShouldCallCommunicationBus()
		{
			//arrange
			string testJson = @"
			{
				'Verb':'GET',
				'Noun':'/RESOURCE/1',
				'Query':'',
				'Fields':'',
				'ConnectedTo':'',
				'ConnectedType':''
			}";

			//act
			jsonToXmlAdapter.ConvertJSONtoXML(testJson);

			//assert
			busMock.Verify(x => x.Forward(It.IsAny<string>()), Times.Once());
		}

		[Test]
		public void ConvertXMLtoJSONWithArrayPayloadShouldReturnJSONWithPayload()
		{
			string xml = @"
				<RESULT>
					<STATUS>SUCCESS</STATUS>
					<STATUS_CODE>2000</STATUS_CODE>
					<PAYLOAD>[{'Id': 1,'Name': 'X','Description': 'Y','Type_Id': 1}]</PAYLOAD>
				</RESULT>
			";

			string expectedResult = "{ STATUS: 'SUCCESS', STATUS_CODE: 2000, PAYLOAD: [{'Id': 1,'Name': 'X','Description': 'Y','Type_Id': 1}] }";

			string json = jsonToXmlAdapter.ConvertXMLtoJSON(xml);

			Assert.AreEqual(expectedResult, json);
		}

		[Test]
		public void ConvertXMLtoJSONWithStringPayloadShouldReturnJSONWithStringPayload()
		{
			string xml = @"
				<RESULT>
					<STATUS>SUCCESS</STATUS>
					<STATUS_CODE>2000</STATUS_CODE>
					<PAYLOAD>Test message</PAYLOAD>
				</RESULT>
			";

			string expectedResult = "{ STATUS: 'SUCCESS', STATUS_CODE: 2000, PAYLOAD: 'Test message' }";

			string json = jsonToXmlAdapter.ConvertXMLtoJSON(xml);

			Assert.AreEqual(expectedResult, json);
		}
	}
}