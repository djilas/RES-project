using DataConverters.Adapters.Sql;
using DataConverters.Interfaces;
using DataConverters.Model;
using NUnit.Framework;

namespace DataTest.DataService
{
	[TestFixture]
	public class SqlBuilderTest
	{
		public ISqlBuilder sqlBuilder;

		[SetUp]
		public void Init()
		{
			sqlBuilder = new SqlBuilder();
		}

		[Test]
		public void BuildSelectById()
		{
			string SELECT_BY_ID = @"
				<Resource>
					<Verb>GET</Verb>
					<Noun>/RESOURCE/1</Noun>
					<Query></Query>
					<Fields></Fields>
					<ConnectedTo></ConnectedTo>
					<ConnectedType></ConnectedType>
				</Resource>
			";

			XmlToSqlResult result = sqlBuilder.Build(SELECT_BY_ID);

			Assert.True(result.Success);
			Assert.AreEqual("SELECT x.* FROM resources x WHERE x.Id=1", result.Statement);
		}

		[Test]
		public void BuildSelectAllWithQueryAndNameAndConnectedTo()
		{
			string SELECT_ALL_WITH_QUERY_AND_NAME_CONNECTED_TO = @"
				<Resource>
					<Verb>GET</Verb>
					<Noun>/RESOURCE/1</Noun>
					<Query>name='Alek';type=1</Query>
					<Fields>id;name;description</Fields>
					<ConnectedTo>id=1;id=4</ConnectedTo>
					<ConnectedType></ConnectedType>
				</Resource>
			";

			XmlToSqlResult result = sqlBuilder.Build(SELECT_ALL_WITH_QUERY_AND_NAME_CONNECTED_TO);

			Assert.True(result.Success);
			Assert.AreEqual("SELECT x.id, x.name, x.description FROM resources x  JOIN Relations ON FirstResource_Id = x.Id  WHERE x.Id=1 AND x.name='Alek' AND x.type=1 AND FirstResource_Id IN(1,4)", result.Statement);
		}

		[Test]
		public void BuildSelectAllWithQueryAndName()
		{
			string SELECT_ALL_WITH_QUERY_AND_NAME = @"
				<Resource>
					<Verb>GET</Verb>
					<Noun>/RESOURCE/1</Noun>
					<Query>name='Alek';type=1</Query>
					<Fields>id;name;description</Fields>
					<ConnectedTo></ConnectedTo>
					<ConnectedType></ConnectedType>
				</Resource>
			";

			XmlToSqlResult result = sqlBuilder.Build(SELECT_ALL_WITH_QUERY_AND_NAME);

			Assert.True(result.Success);
			Assert.AreEqual("SELECT x.id, x.name, x.description FROM resources x WHERE x.Id=1 AND x.name='Alek' AND x.type=1", result.Statement);
		}

		[Test]
		public void BuildSelectAllWithConnectedTo()
		{
			string SELECT_ALL_WITH_CONNECTED_TO = @"
				<Resource>
					<Verb>GET</Verb>
					<Noun>/RESOURCE</Noun>
					<Query></Query>
					<Fields></Fields>
					<ConnectedTo>id=1;id=4</ConnectedTo>
					<ConnectedType></ConnectedType>
				</Resource>
			";

			XmlToSqlResult result = sqlBuilder.Build(SELECT_ALL_WITH_CONNECTED_TO);

			Assert.True(result.Success);
			Assert.AreEqual("SELECT x.* FROM resources x  JOIN Relations ON FirstResource_Id = x.Id  WHERE FirstResource_Id IN(1,4)", result.Statement);
		}

		[Test]
		public void BuildSelectWithAllParameters()
		{
			string SELECT_ALL_WITH_QUERY_AND_NAME_AND_CONNECTED_TO_AND_CONNECTED_TYPE = @"
				<Resource>
					<Verb>GET</Verb>
					<Noun>/RESOURCE/1</Noun>
					<Query>name='Alek';type=1</Query>
					<Fields>id;name;description</Fields>
					<ConnectedTo>id=1;id=4</ConnectedTo>
					<ConnectedType>id=7;id=8</ConnectedType>
				</Resource>
			";

			XmlToSqlResult result = sqlBuilder.Build(SELECT_ALL_WITH_QUERY_AND_NAME_AND_CONNECTED_TO_AND_CONNECTED_TYPE);

			Assert.True(result.Success);
			Assert.AreEqual("SELECT x.id, x.name, x.description FROM resources x JOIN Relations ON FirstResource_Id = x.Id WHERE x.Id=1 AND x.name='Alek' AND x.type=1 AND FirstResource_Id = 1 AND Secondresource_Id = 4 AND TYPE_Id IN( AND FirstResource_Id = 7 AND Secondresource_Id = 8)", result.Statement);
		}

		[Test]
		public void BuildWithUnknownOperation()
		{
			string SELECT_ALL_WITH_QUERY_AND_NAME_AND_CONNECTED_TO_AND_CONNECTED_TYPE = @"
				<Resource>
					<Verb>GET_TEST</Verb>
					<Noun>/RESOURCE/1</Noun>
					<Query>name='Alek';type=1</Query>
					<Fields>id;name;description</Fields>
					<ConnectedTo>id=1;id=4</ConnectedTo>
					<ConnectedType>id=7;id=8</ConnectedType>
				</Resource>
			";

			XmlToSqlResult result = sqlBuilder.Build(SELECT_ALL_WITH_QUERY_AND_NAME_AND_CONNECTED_TO_AND_CONNECTED_TYPE);

			Assert.False(result.Success);
		}

		[Test]
		public void BuildDelete()
		{
			string DELETE = @"
				<Resource>
					<Verb>DELETE</Verb>
					<Noun>/RESOURCE/1</Noun>
					<Query>name='Alek';type=1</Query>
					<Fields></Fields>
					<ConnectedTo></ConnectedTo>
					<ConnectedType></ConnectedType>
				</Resource>
			";

			XmlToSqlResult result = sqlBuilder.Build(DELETE);
			
			Assert.True(result.Success);
			Assert.AreEqual("DELETE FROM resources WHERE Id=1 AND name='Alek' AND type=1", result.Statement);
		}

		[Test]
		public void BuildUpdate()
		{
			string PATCH = @"
				<Resource>
					<Verb>PATCH</Verb>
					<Noun>/RESOURCE/1</Noun>
					<Query>name='Alek';type=1</Query>
					<Fields></Fields>
					<ConnectedTo></ConnectedTo>
					<ConnectedType></ConnectedType>
				</Resource>
			";

			XmlToSqlResult result = sqlBuilder.Build(PATCH);

			Assert.True(result.Success);

			Assert.AreEqual("UPDATE resources SET name='Alek',type=1 WHERE Id=1", result.Statement);
		}

		[Test]
		public void BuildInsert()
		{
			string POST = @"
				<Resource>
					<Verb>POST</Verb>
					<Noun>/RESOURCE</Noun>
					<Query>name='Alek';type=1</Query>
					<Fields></Fields>
					<ConnectedTo></ConnectedTo>
					<ConnectedType></ConnectedType>
				</Resource>
			";

			XmlToSqlResult result = sqlBuilder.Build(POST);

			Assert.True(result.Success);

			Assert.AreEqual("INSERT INTO resources (name, type) VALUES ('Alek', 1)", result.Statement);
		}
	}
}
