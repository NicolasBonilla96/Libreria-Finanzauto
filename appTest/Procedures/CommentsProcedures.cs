using appTest.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Xml.Linq;

namespace appTest.Procedures
{
    public class CommentsProcedures
    {
        public static Guid Insert_Comments(Comments comments)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("insert_comments", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@content", SqlDbType.NVarChar));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@user_id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@book_id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters["@id"].Direction = ParameterDirection.Output;
            sda.SelectCommand.Parameters["@content"].Value = comments.content;
            sda.SelectCommand.Parameters["@user_id"].Value = comments.user_id;
            sda.SelectCommand.Parameters["@book_id"].Value = comments.book_id;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            Guid id = Guid.Parse(sda.SelectCommand.Parameters["id"].Value.ToString());
            sda.Dispose();
            con.Close();
            return id;
        }

        public static void Update_Comments(Comments comments)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("update_comments", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@content", SqlDbType.NVarChar));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@user_id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@book_id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters["@id"].Value = comments.id;
            sda.SelectCommand.Parameters["@content"].Value = comments.content;
            sda.SelectCommand.Parameters["@user_id"].Value = comments.user_id;
            sda.SelectCommand.Parameters["@book_id"].Value = comments.book_id;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
        }

        public static void Delete_Comments(Guid id)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("delete_comments", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters["@id"].Value = id;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
        }

        public static DataSet Get_Comments()
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("get_comments", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return ds;
        }

        public static DataSet Get_Comments_Filter(Comments comments)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("get_comments_filter", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@content", SqlDbType.NVarChar));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@user_id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@book_id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters["@content"].Value = comments.content;
            sda.SelectCommand.Parameters["@user_id"].Value = comments.user_id;
            sda.SelectCommand.Parameters["@book_id"].Value = comments.book_id;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return ds;
        }
    }
}