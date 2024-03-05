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
    public class UsersProcedures
    {
        public static List<Users> List_Users(DataSet ds)
        {
            List<Users> lst_users = new List<Users>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                int indiceFila = 0;
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    Users users = new Users();
                    foreach (DataColumn columna in ds.Tables[0].Columns)
                    {
                        switch (columna.ColumnName)
                        {
                            case "id":
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString()))
                                {
                                    users.id = Guid.Parse(ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString());
                                }
                                break;
                            case "name":
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString()))
                                {
                                    users.name = ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString();
                                }
                                break;
                        }
                    }
                    indiceFila++;
                    lst_users.Add(users);
                }
            }
            return lst_users;
        }

        public static Guid Insert_User(Users users)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("insert_user", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar));
            sda.SelectCommand.Parameters["@id"].Direction = ParameterDirection.Output;
            sda.SelectCommand.Parameters["@name"].Value = users.name;
            sda.SelectCommand.Parameters["@email"].Value = users.email;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            Guid id = Guid.Parse(sda.SelectCommand.Parameters["@id"].Value.ToString());
            sda.Dispose();
            con.Close();
            return id;
        }

        public static void Update_User(Users users)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("update_user", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar));
            sda.SelectCommand.Parameters["@id"].Value = users.id;
            sda.SelectCommand.Parameters["@name"].Value = users.name;
            sda.SelectCommand.Parameters["@email"].Value = users.email;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
        }

        public static void Delete_User(Guid id)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("delete_user", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters["@id"].Value = id;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
        }

        public static DataSet Get_Users()
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Get_users", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return ds;
        }

        public static List<Users> Get_Users_List()
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Get_users", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return List_Users(ds);
        }

        public static DataSet Get_User_Filter(Users users)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Get_user", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@ID", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar));
            sda.SelectCommand.Parameters["@id"].Value = users.id;
            sda.SelectCommand.Parameters["@name"].Value = users.name;
            sda.SelectCommand.Parameters["@email"].Value = users.email;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return ds;
        }
    }
}