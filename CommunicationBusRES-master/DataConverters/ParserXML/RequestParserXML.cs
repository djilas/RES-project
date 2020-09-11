using System;
using System.Data;

namespace DataConverters
{
	public class RequestParserXML
	{
		public string ParseXML(string xmlString)
		{
			string SQLQuery = "";

			string[] split = xmlString.Split('>');
			string getOperation = split[2];
			string[] split1 = getOperation.Split('<');
			string operation = split1[0]; // We get operation here from xml. (GET)
			string getBaseandType = split[4];
			string[] split2 = getBaseandType.Split('<');
			string[] split3 = split2[0].Split('/');
			string Base = split3[1]; //We cetch base to select from (resource)
			string Type = split3[2]; // type i guess its id (3)
			string getNameAndType = split[6];
			string[] getNameAndType1 = getNameAndType.Split('<');
			string getName = getNameAndType1[0].Split(';')[0]; // NAME (name='pera')
			string getType = getNameAndType1[0].Split(';')[1]; // TYPE (type=2)
			string getFields = split[8];
			string[] fields = getFields.Split('<'); //(id;name;description)
			string field = fields[0].Replace(';', ','); //(id,name,description)

			if (operation.ToUpper() == "GET")
			{
				if (!string.IsNullOrEmpty(getName) && !string.IsNullOrEmpty(getType))
				{
					SQLQuery = " SELECT " + field + Environment.NewLine + "FROM " + Base + Environment.NewLine + "WHERE " + getName + " and " + getType;
				}else if(!string.IsNullOrEmpty(getName) && string.IsNullOrEmpty(getType))
				{
					SQLQuery = " SELECT " + field + Environment.NewLine + "FROM " + Base + Environment.NewLine + "WHERE " + getName; 
				}
				else if(string.IsNullOrEmpty(getName) && !string.IsNullOrEmpty(getType))
				{
					SQLQuery = " SELECT " + field + Environment.NewLine + "FROM " + Base + Environment.NewLine + "WHERE " + getType;
				}else if(string.IsNullOrEmpty(field))
				{
					SQLQuery = " SELECT " + " * " + Environment.NewLine + "FROM " + Base + Environment.NewLine + "WHERE " + getName + " and " + getType;
				}
				else if (string.IsNullOrEmpty(field) && !string.IsNullOrEmpty(getName) && string.IsNullOrEmpty(getType))
				{
					SQLQuery = " SELECT " + field + Environment.NewLine + "FROM " + Base + Environment.NewLine + "WHERE " + getName;
				}
				else if (string.IsNullOrEmpty(field) && string.IsNullOrEmpty(getName) && !string.IsNullOrEmpty(getType))
				{
					SQLQuery = " SELECT " + field + Environment.NewLine + "FROM " + Base + Environment.NewLine + "WHERE " + getType;
				}

			} else if(operation.ToUpper() == "POST")
			{
				SQLQuery = " INSER INTO " + Base + Environment.NewLine + "VALUES (" + getName + "," + " NO DESCRIPTION, " + getType + " )";
				Console.WriteLine(SQLQuery);
			} else if (operation.ToUpper() == "DELETE")
			{
				if (!string.IsNullOrEmpty(getName) && !string.IsNullOrEmpty(getType))
				{
					SQLQuery = " DELETE " + field + Environment.NewLine + "FROM " + Base + Environment.NewLine + "WHERE " + getName + " and " + getType;
					Console.WriteLine(SQLQuery);
				}
				else if (!string.IsNullOrEmpty(getName) && string.IsNullOrEmpty(getType))
				{
					SQLQuery = " DELETE " + field + Environment.NewLine + "FROM " + Base + Environment.NewLine + "WHERE " + getName;
				}
				else if (string.IsNullOrEmpty(getName) && !string.IsNullOrEmpty(getType))
				{
					SQLQuery = " DELETE " + field + Environment.NewLine + "FROM " + Base + Environment.NewLine + "WHERE " + getType;
				}
			}
			else if (operation.ToUpper() == "PATCH")
			{
				//how to update sql
			}

			return SQLQuery; 
		}
	}
}
