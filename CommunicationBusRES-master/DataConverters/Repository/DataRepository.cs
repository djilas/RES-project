using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using DataService.Interfaces.Repository;
using DataService.Repository;

namespace DataConverters.Repository
{
	public class DataRepository : IDataRepository
	{
		private readonly RESDatabaseContext _context;

		public DataRepository(RESDatabaseContext context)
		{
			_context = context;
		}

		public List<Dictionary<string, object>> Select(string sql)
		{
			DbCommand command = new SqlCommand();
			
			command.CommandText = sql;
			command.Connection = _context.Database.Connection;

			try
			{
				command.Connection.Open();
				DbDataReader reader =  command.ExecuteReader();
				string selectColumns = ExtractSelectColumnNames(sql, "SELECT", "FROM");
				return ExtractResult(reader, selectColumns);
			}
			catch (Exception exception)
			{
				return new List<Dictionary<string, object>>();
			}
			finally
			{
				command.Connection.Close();
			}
		}

		public bool ExecuteNoneQuery(string sql)
		{
			DbCommand command = new SqlCommand();

			command.CommandText = sql;
			command.Connection = _context.Database.Connection;

			try
			{
				command.Connection.Open();
				int affectedRecords = command.ExecuteNonQuery();
				return affectedRecords > 0;
			}
			catch
			{
				return false;
			}
			finally
			{
				command.Connection.Close();
			}
		}

		private List<Dictionary<string, object>> ExtractResult(IDataReader dataReader, string selectColumns)
		{
			var list = new List<Dictionary<string, object>>();
			if (string.Equals(selectColumns, "x.*")) {
				IEnumerable<string> columnNames = GetColumnNames(dataReader);
				while (dataReader.Read())
				{
					var record = new Dictionary<string, object>();
					foreach (string columnName in columnNames)
					{
						record.Add(columnName, dataReader[columnName]);
					}

					list.Add(record);
				}
			}
			else
			{
				string[] columnNames = selectColumns.Split(',');
				while (dataReader.Read())
				{
					var record = new Dictionary<string, object>();
					for (int i = 0; i < columnNames.Length; i++)
					{
						record.Add(columnNames[i].Trim(), dataReader[columnNames[i].Trim()]);
					}

					list.Add(record);
				}
			}

			return list;
		}

		private IEnumerable<string> GetColumnNames(IDataReader reader)
		{
			DataTable schemaTable = reader.GetSchemaTable();
			return schemaTable == null
				 ? Enumerable.Empty<string>()
				 : schemaTable.Rows.OfType<DataRow>().Select(row => row["ColumnName"].ToString());
		}

		private string ExtractSelectColumnNames(string value, string FirstString, string LastString)
		{
			int position1 = value.IndexOf(FirstString) + FirstString.Length;
			int position2 = value.IndexOf(LastString);
			string result = value.Substring(position1, position2 - position1);
			return result.Trim();
		}
	}
}
