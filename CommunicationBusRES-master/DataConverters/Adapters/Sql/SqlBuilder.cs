using DataConverters.Interfaces;
using DataConverters.Model;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace DataConverters.Adapters.Sql
{
	public class SqlBuilder : ISqlBuilder
	{
		private static readonly string VERB_TAG = "Verb";
		private static readonly string NOUN_TAG = "Noun";
		private static readonly string QUERY_TAG = "Query";
		private static readonly string FIELDS_TAG = "Fields";
		private static readonly string CONNECTED_TO_TAG = "ConnectedTo";
		private static readonly string CONNECTED_TYPE_TAG = "ConnectedType";

		/// <summary>
		/// {0} - Select values - this we take from 'Fields'
		/// {1} - Table name - this we take from 'Noun'
		/// {2} - Where query - this we take from 'Query'
		/// </summary>
		private static readonly string SELECT_TEMPLATE = "SELECT {0} FROM {1} x {2}";

		private static readonly string SELECT_WITH_JOIN_TEMPLATE = "SELECT {0} FROM {1} x {2} {3}";

		public XmlToSqlResult Build(string xml)
		{
			XDocument xmlResource = XDocument.Parse(xml);
			try
			{
				string verb = xmlResource.Root.Element(VERB_TAG).Value;
				string noun = xmlResource.Root.Element(NOUN_TAG).Value;
				string query = xmlResource.Root.Element(QUERY_TAG).Value;
				string fields = xmlResource.Root.Element(FIELDS_TAG).Value;
				string connectedTo = xmlResource.Root.Element(CONNECTED_TO_TAG).Value;
				string connectedType = xmlResource.Root.Element(CONNECTED_TYPE_TAG).Value;

				switch (verb)
				{
					case "GET": return BuildSelectSQL(noun, query, fields, connectedTo, connectedType);
					case "DELETE": return BuildDeleteSQL(noun, query);
					case "PATCH": return BuildUpdateSQL(noun, query);
					case "POST": return BuildInsertSql(noun, query);
					default:
						throw new Exception($"Verb {verb} not implemented!");
				}
			}
			catch (Exception exception)
			{
				return new XmlToSqlResult(exception.Message);
			}
		}

		private XmlToSqlResult BuildInsertSql(string noun, string query)
		{
			string tableName = GetTableNameFromNoun(noun);
			string statement = "INSERT INTO " + tableName;
			var columnNames = new List<string>();
			var columnValues = new List<string>();
			
			foreach (string part in query.Split(';'))
			{
				string[] parts = part.Split('=');
				columnNames.Add(parts[0]);
				columnValues.Add(parts[1]);
			}

			statement += " (" + string.Join(", ", columnNames) + ") VALUES ("+ string.Join(", ", columnValues) +")";

			return new XmlToSqlResult(statement, SqlStatementTypeEnum.INSERT);
		}

		private XmlToSqlResult BuildUpdateSQL(string noun, string query)
		{
			int id = GetIdFromNoun(noun);
			string tableName = GetTableNameFromNoun(noun);
			string statement = "UPDATE " + tableName + " ";
			string where = id > 0 ? " WHERE Id=" + id : "";

			string queryAdd = "SET ";
			foreach (string part in query.Split(';')) 
			{
				queryAdd += $"{part},";
			}

			queryAdd = queryAdd.Substring(0,queryAdd.Length - 1);

			statement += queryAdd + where;

			return new XmlToSqlResult(statement, SqlStatementTypeEnum.UPDATE);
		}

		private XmlToSqlResult BuildSelectSQL(string noun, string query, string fields, string connectedTo, string connectedType)
		{
			string joinPart = "";
			string statement = "";
			string tableName = GetTableNameFromNoun(noun);
			bool hasJoin = !string.IsNullOrEmpty(connectedTo) || !string.IsNullOrEmpty(connectedType);

			string selectPart = BuildSelectFields(fields);
			string wherePart = BuildWhere(noun, query, "x.");

			if (hasJoin)
			{
				joinPart = " JOIN Relations ON FirstResource_Id = x.Id ";
				if (!string.IsNullOrEmpty(connectedTo))
				{
					if (wherePart.StartsWith("WHERE"))
					{
						//wherePart += " AND FirstResource_Id IN(" + GetJoinIds(connectedTo) + ")";
						wherePart += GetJoinIds(connectedTo);
					}
					else
					{
						wherePart += GetJoinIds(connectedTo);
					}
				}

				if (!string.IsNullOrEmpty(connectedType))
				{
					if (wherePart.StartsWith("WHERE")) 
					{
						wherePart += " AND TYPE_Id IN(" + GetJoinIds(connectedType) + ")";
					}
					else
					{
						wherePart += " WHERE SecondResource_Id IN(" + GetJoinIds(connectedType) + ")";
					}
				}

				statement = string.Format(SELECT_WITH_JOIN_TEMPLATE, selectPart, tableName, joinPart, wherePart);
			}
			else
			{
				statement = string.Format(SELECT_TEMPLATE, selectPart, tableName, wherePart);
			}

			return new XmlToSqlResult(statement, SqlStatementTypeEnum.SELECT);
		}

		private XmlToSqlResult BuildDeleteSQL(string noun, string query)
		{
			int id = GetIdFromNoun(noun);
			string tableName = GetTableNameFromNoun(noun);
			string wherePart = BuildWhere(noun, query);
			string statement = "DELETE FROM " + tableName + " " + wherePart;

			return new XmlToSqlResult(statement, SqlStatementTypeEnum.DELETE);
		}

		private string BuildSelectFields(string fields)
		{
			string selectPart = "";
			if (!string.IsNullOrEmpty(fields))
			{
				var xFileds = new List<string>();
				foreach (string filed in fields.Split(';'))
				{
					xFileds.Add("x." + filed.Trim());

				}
				selectPart = string.Join(", ", xFileds);
			}
			else
			{
				selectPart = "x.*";
			}

			return selectPart;
		}

		private string BuildWhere(string noun, string query, string tablePrefix = "")
		{
			string where = "";
			int id = GetIdFromNoun(noun);
			if (id > 0)
			{
				where = $"WHERE {tablePrefix}Id={id}";
			}

			if (!string.IsNullOrEmpty(query))
			{
				string[] parts = query.Split(';');
				if (where.StartsWith("WHERE"))
				{
					foreach (string part in parts)
					{
						where += " AND " +tablePrefix + part;
					}
				}
				else
				{
					where += " WHERE ";
					bool isFirst = true;
					foreach (string part in parts)
					{
						if (isFirst)
						{
							isFirst = false;
							where += tablePrefix + part;
						}
						else
						{
							where += " AND " + tablePrefix + part;
						}
					}
				}
			}

			return where;
		}

		private string GetJoinIds(string list) // id=1;id=4
		{
			string join = "";
			int id1;

			string[] split = list.Split('=',';');

			string where = $" AND FirstResource_Id = {split[1]} AND Secondresource_Id = {split[3]}";

			return where;
		}


		private int GetIdFromNoun(string noun)
		{
			string[] parts = noun.Split('/');
			string id = parts[parts.Length - 1];
			if (int.TryParse(id, out int result))
			{
				return result;
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Table name ends with "s" - Generated like that by EntityFramework
		/// </summary>
		private string GetTableNameFromNoun(string noun)
		{
			string[] parts = noun.Split('/');
			int index = parts.Length == 3 ? parts.Length - 2 : parts.Length - 1;
			string name = parts[index].ToLower();
			if (name.EndsWith("s"))
			{
				return name;
			}
			else
			{
				name += "s";
				return name;
			}
		}
	}
}