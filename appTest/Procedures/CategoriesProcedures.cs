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
    public class CategoriesProcedures
    {
        public static List<Categories> List_Categories(DataSet ds)
        {
            List<Categories> lst_categories = new List<Categories>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                int indiceFila = 0;
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    Categories categories = new Categories();
                    foreach (DataColumn columna in ds.Tables[0].Columns)
                    {
                        switch (columna.ColumnName)
                        {
                            case "id":
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString()))
                                {
                                    categories.id = Guid.Parse(ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString());
                                }
                                break;
                            case "name":
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString()))
                                {
                                    categories.name = ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString();
                                }
                                break;
                        }
                    }
                    indiceFila++;
                    lst_categories.Add(categories);
                }
            }
            return lst_categories;
        }

        public static Guid Insert_Categories(Categories categories)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("insert_categories", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            sda.SelectCommand.Parameters["@id"].Direction = ParameterDirection.Output;
            sda.SelectCommand.Parameters["@name"].Value = categories.name;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            Guid id = Guid.Parse(sda.SelectCommand.Parameters["@id"].Value.ToString());
            sda.Dispose();
            con.Close();
            return id;
        }

        public static void Update_Category(Categories categories)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("update_categories", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            sda.SelectCommand.Parameters["@id"].Value = categories.id;
            sda.SelectCommand.Parameters["@name"].Value = categories.name;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
        }

        public static void Delete_Category(Guid id)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("delete_categories", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters["@id"].Value = id;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
        }

        public static DataSet Get_Categories()
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("get_categories", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return ds;
        }

        public static List<Categories> Get_Categories_List()
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("get_categories", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return List_Categories(ds);
        }

        public static DataSet Get_Categories_Filter(Categories categories)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("get_categories_filter", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            sda.SelectCommand.Parameters["@id"].Value = categories.id;
            sda.SelectCommand.Parameters["@name"].Value = categories.name;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return ds;
        }
    }
}