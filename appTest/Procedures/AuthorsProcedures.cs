using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using appTest.Entities;
using System.ComponentModel;

namespace appTest.Procedures
{
    public class AuthorsProcedures
    {
        public static List<Authors> List_Authors(DataSet ds)
        {
            List<Authors> lst_authors = new List<Authors>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                int indiceFila = 0;
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    Authors authors = new Authors();
                    foreach (DataColumn columna in ds.Tables[0].Columns)
                    {
                        switch (columna.ColumnName)
                        {
                            case "id":
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString()))
                                {
                                    authors.id = Guid.Parse(ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString());
                                }
                                break;
                            case "name":
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString()))
                                {
                                    authors.name = ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString();
                                }
                                break;
                        }
                    }
                    indiceFila++;
                    lst_authors.Add(authors);
                }
            }
            return lst_authors;
        }

        public static Guid Insert_Author(Authors authors)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("insert_authors", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@lastname", SqlDbType.VarChar));
            sda.SelectCommand.Parameters["@id"].Direction = ParameterDirection.Output;
            sda.SelectCommand.Parameters["@name"].Value = authors.name;
            sda.SelectCommand.Parameters["@lastname"].Value = authors.lastname;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            Guid id = Guid.Parse(sda.SelectCommand.Parameters["@id"].Value.ToString());
            sda.Dispose();
            con.Close();
            return id;
        }

        public static void Update_Author(Authors authors)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("update_authors", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@lastname", SqlDbType.VarChar));
            sda.SelectCommand.Parameters["@id"].Value = authors.id;
            sda.SelectCommand.Parameters["@name"].Value = authors.name;
            sda.SelectCommand.Parameters["@lastname"].Value = authors.lastname;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
        }

        public static void Delete_Author(Guid id)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("delete_authors", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters["@id"].Value = id;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
        }

        public static DataSet Get_Authors()
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("get_authors", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return ds;
        }

        public static List<Authors> Get_Authors_List()
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("get_authors", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return List_Authors(ds);
        }

        public static DataSet Get_Authors_Filter(Authors authors)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("get_authors_filter", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@lastname", SqlDbType.VarChar));
            sda.SelectCommand.Parameters["@id"].Value = authors.id;
            sda.SelectCommand.Parameters["@name"].Value = authors.name;
            sda.SelectCommand.Parameters["@lastname"].Value = authors.lastname;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return ds;
        }
    }
}