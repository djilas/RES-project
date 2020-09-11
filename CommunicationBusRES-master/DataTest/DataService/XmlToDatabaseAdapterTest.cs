using DataConverters.Interfaces;
using DataConverters.Model;
using DataService;
using DataService.Interfaces;
using DataService.Interfaces.Repository;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DataTest.DataService
{
	[TestFixture]
	public class XmlToDatabaseAdapterTest
	{
		Mock<ISqlBuilder> sqlBuilderMock;
		Mock<IDataRepository> dataRepositoryMock;

		IXmlToDatabaseAdapter adapter;

		[SetUp]
		public void Init()
		{
			sqlBuilderMock = new Mock<ISqlBuilder>();
			dataRepositoryMock = new Mock<IDataRepository>();
			adapter = new XmlToDatabaseAdapter(sqlBuilderMock.Object, dataRepositoryMock.Object);
		}

		[Test]
		public void ExecuteReturnBadRequest()
		{
			sqlBuilderMock.Setup(x => x.Build(It.IsAny<string>())).Returns(new XmlToSqlResult("ERROR"));

			string xmlResult = adapter.Execute("MOCK_XML_STRING");

			Assert.True(xmlResult.Contains("ERROR"));
			Assert.True(xmlResult.Contains("BAD_FORMAT"));
			Assert.True(xmlResult.Contains("5000"));
		}

		[Test]
		public void ExecuteSelect()
		{
			sqlBuilderMock.Setup(x => x.Build(It.IsAny<string>())).Returns(new XmlToSqlResult("SQL", SqlStatementTypeEnum.SELECT));
			dataRepositoryMock.Setup(x => x.Select(It.IsAny<string>())).Returns(new List<Dictionary<string, object>>());

			string xmlResult = adapter.Execute("MOCK_XML_STRING");

			Assert.True(xmlResult.Contains("[]"));
			Assert.True(xmlResult.Contains("SUCCESS"));
			Assert.True(xmlResult.Contains("2000"));
		}

		[Test]
		public void ExecuteInsertSuccess()
		{
			sqlBuilderMock.Setup(x => x.Build(It.IsAny<string>())).Returns(new XmlToSqlResult("SQL", SqlStatementTypeEnum.INSERT));
			dataRepositoryMock.Setup(x => x.ExecuteNoneQuery(It.IsAny<string>())).Returns(true);

			string xmlResult = adapter.Execute("MOCK_XML_STRING");

			Assert.True(xmlResult.Contains("Operation 'INSERT' success!"));
			Assert.True(xmlResult.Contains("SUCCESS"));
			Assert.True(xmlResult.Contains("2000"));
		}

		[Test]
		public void ExecuteInsertRejected()
		{
			sqlBuilderMock.Setup(x => x.Build(It.IsAny<string>())).Returns(new XmlToSqlResult("SQL", SqlStatementTypeEnum.INSERT));
			dataRepositoryMock.Setup(x => x.ExecuteNoneQuery(It.IsAny<string>())).Returns(false);

			string xmlResult = adapter.Execute("MOCK_XML_STRING");

			Assert.True(xmlResult.Contains("Operation 'INSERT' rejected!"));
			Assert.True(xmlResult.Contains("REJECTED"));
			Assert.True(xmlResult.Contains("3000"));
		}

		[Test]
		public void ExecuteUpdateSuccess()
		{
			sqlBuilderMock.Setup(x => x.Build(It.IsAny<string>())).Returns(new XmlToSqlResult("SQL", SqlStatementTypeEnum.UPDATE));
			dataRepositoryMock.Setup(x => x.ExecuteNoneQuery(It.IsAny<string>())).Returns(true);

			string xmlResult = adapter.Execute("MOCK_XML_STRING");

			Assert.True(xmlResult.Contains("Operation 'UPDATE' success!"));
			Assert.True(xmlResult.Contains("SUCCESS"));
			Assert.True(xmlResult.Contains("2000"));
		}

		[Test]
		public void ExecuteUpdateRejected()
		{
			sqlBuilderMock.Setup(x => x.Build(It.IsAny<string>())).Returns(new XmlToSqlResult("SQL", SqlStatementTypeEnum.UPDATE));
			dataRepositoryMock.Setup(x => x.ExecuteNoneQuery(It.IsAny<string>())).Returns(false);

			string xmlResult = adapter.Execute("MOCK_XML_STRING");

			Assert.True(xmlResult.Contains("Operation 'UPDATE' rejected!"));
			Assert.True(xmlResult.Contains("REJECTED"));
			Assert.True(xmlResult.Contains("3000"));
		}

		[Test]
		public void ExecuteDeleteSuccess()
		{
			sqlBuilderMock.Setup(x => x.Build(It.IsAny<string>())).Returns(new XmlToSqlResult("SQL", SqlStatementTypeEnum.DELETE));
			dataRepositoryMock.Setup(x => x.ExecuteNoneQuery(It.IsAny<string>())).Returns(true);

			string xmlResult = adapter.Execute("MOCK_XML_STRING");

			Assert.True(xmlResult.Contains("Operation 'DELETE' success!"));
			Assert.True(xmlResult.Contains("SUCCESS"));
			Assert.True(xmlResult.Contains("2000"));
		}

		[Test]
		public void ExecuteDeleteRejected()
		{
			sqlBuilderMock.Setup(x => x.Build(It.IsAny<string>())).Returns(new XmlToSqlResult("SQL", SqlStatementTypeEnum.DELETE));
			dataRepositoryMock.Setup(x => x.ExecuteNoneQuery(It.IsAny<string>())).Returns(false);

			string xmlResult = adapter.Execute("MOCK_XML_STRING");

			Assert.True(xmlResult.Contains("Operation 'DELETE' rejected!"));
			Assert.True(xmlResult.Contains("REJECTED"));
			Assert.True(xmlResult.Contains("3000"));
		}
	}
}
