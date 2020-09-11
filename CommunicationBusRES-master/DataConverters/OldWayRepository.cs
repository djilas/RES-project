using DataModel.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DataService
{
	public class OldWayRepository
    {
        private readonly string ConnString = @"Data Source =DESKTOP-4SMJ7EN\DATALAB; Initial Catalog=master; uid=sa; pwd=DataLab123DataLab";


        // Radi, testirano!
        public List<Resource> GetResurs(string query)
        {
            List<Resource> resursi = new List<Resource>();
            SqlConnection conn = new SqlConnection(this.ConnString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                Resource res = new Resource();
                res.Id = Convert.ToInt32(sdr["ID"].ToString());
                res.Name = sdr["Name"].ToString();
                res.Description = sdr["Description"].ToString();

                foreach (ResourceType tip in GetTipoveResursa())
                {
                    if (Convert.ToInt32(sdr["Tip"].ToString()) == tip.Id)
                    {
                        res.Type = tip;
                    }
                }
                resursi.Add(res);
            }

            return resursi;
        }


        // Radi, testirano!
        public void DeleteResurs(string query)
        {
            try
            {
                SqlConnection conn = new SqlConnection(this.ConnString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();
            }
            catch(Exception e)
            {
                Trace.TraceInformation(e.Message);
            }

        }


        // Radi, testirano!
        public List<ResourceType> GetTipoveResursa()
        {
            List<ResourceType> tipoviResursa = new List<ResourceType>();
            SqlConnection conn = new SqlConnection(this.ConnString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM [ResSeptBase].[dbo].[RelationTip]", conn);
            cmd.CommandType = System.Data.CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                ResourceType tip = new ResourceType();
                tip.Id = Convert.ToInt32(sdr["ID"].ToString());
                tip.Name = sdr["Name"].ToString();
                tipoviResursa.Add(tip);
            }

            return tipoviResursa;
        }
    }
}
