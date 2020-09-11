using DataService;
using DataService.Interfaces;
using Moq;
using NUnit.Framework;

namespace DataTest.DataService
{
	[TestFixture]
	public class ComunicationBusTest
	{
		Mock<IXmlToDatabaseAdapter> mockAdapter;

		ICommunicationBus communicationBus;

		[SetUp]
		public void Init()
		{
			mockAdapter = new Mock<IXmlToDatabaseAdapter>();
			communicationBus = new CommunicationBus(mockAdapter.Object);
		}

		[TestCase("XML_MOCK_1", "RESULT_MOCK_1")]
		[TestCase("XML_MOCK_2", "RESULT_MOCK_2")]
		public void ForwardCallsXmlToDatabaseAdapter(string xmlMock, string resultMock)
		{
			//arrange
			mockAdapter.Setup(x => x.Execute(It.IsAny<string>())).Returns(resultMock);

			string forwardResult = communicationBus.Forward(xmlMock);

			Assert.AreEqual(forwardResult, resultMock);
		}
	}
}
