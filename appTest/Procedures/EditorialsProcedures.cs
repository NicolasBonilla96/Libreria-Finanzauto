using appTest.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;

namespace appTest.Procedures
{
    public class EditorialsProcedures
    {
        public static List<Editorials> List_Editorials(DataSet ds)
        {
            List<Editorials> lst_editorials = new List<Editorials>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                int indiceFila = 0;
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    Editorials editorials = new Editorials();
                    foreach (DataColumn columna in ds.Tables[0].Columns)
                    {
                        switch (columna.ColumnName)
                        {
                            case "id":
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString()))
                                {
                                    editorials.id = Guid.Parse(ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString());
                                }
                                break;
                            case "name":
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString()))
                                {
                                    editorials.name = ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString();
                                }
                                break;
                        }
                    }
                    indiceFila++;
                    lst_editorials.Add(editorials);
                }
            }
            return lst_editorials;
        }

        public static Guid Insert_Editorials(Editorials editorials)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("insert_editorials", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@location", SqlDbType.VarChar));
            sda.SelectCommand.Parameters["@id"].Direction = ParameterDirection.Output;
            sda.SelectCommand.Parameters["@name"].Value = editorials.name;
            sda.SelectCommand.Parameters["@location"].Value = editorials.location;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            Guid id = Guid.Parse(sda.SelectCommand.Parameters["@id"].Value.ToString());
            sda.Dispose();
            con.Close();
            return id;
        }

        public static void Update_Editorials(Editorials editorials)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("update_editorials", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@location", SqlDbType.VarChar));
            sda.SelectCommand.Parameters["@id"].Value = editorials.id;
            sda.SelectCommand.Parameters["@name"].Value = editorials.name;
            sda.SelectCommand.Parameters["@location"].Value = editorials.location;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
        }

        public static void Delete_Editorials(Guid id)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("delete_editorials", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters["@id"].Value = id;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
        }

        public static DataSet Get_Editorials()
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("get_editorials", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return ds;
        }

        public static List<Editorials> Get_Editorials_List()
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("get_editorials", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return List_Editorials(ds);
        }

        public static DataSet Get_Editorials_Filter(Editorials editorials)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("get_editorials_filter", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@location", SqlDbType.VarChar));
            sda.SelectCommand.Parameters["@id"].Value = editorials.id;
            sda.SelectCommand.Parameters["@name"].Value = editorials.name;
            sda.SelectCommand.Parameters["@location"].Value = editorials.location;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return ds;
        }
    }
}