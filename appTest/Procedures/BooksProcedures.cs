using appTest.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;

namespace appTest.Procedures
{
    public class BooksProcedures
    {
        public static List<Books> List_Books(DataSet ds)
        {
            List<Books> lst_books = new List<Books>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                int indiceFila = 0;
                foreach (DataRow fila in ds.Tables[0].Rows)
                {
                    Books books = new Books();
                    foreach (DataColumn columna in ds.Tables[0].Columns)
                    {
                        switch (columna.ColumnName)
                        {
                            case "id":
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString()))
                                {
                                    books.id = Guid.Parse(ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString());
                                }
                                break;
                            case "name":
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString()))
                                {
                                    books.name = ds.Tables[0].Rows[indiceFila][columna.ColumnName].ToString();
                                }
                                break;
                        }
                    }
                    indiceFila++;
                    lst_books.Add(books);
                }
            }
            return lst_books;
        }

        public static Guid Insert_Book(Books books)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("insert_book", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@author_id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@editorial_id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@category_id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@publication_year", SqlDbType.VarChar));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            sda.SelectCommand.Parameters["@id"].Direction = ParameterDirection.Output;
            sda.SelectCommand.Parameters["@author_id"].Value = books.author_id;
            sda.SelectCommand.Parameters["@editorial_id"].Value = books.editorial_id;
            sda.SelectCommand.Parameters["@category_id"].Value = books.category_id;
            sda.SelectCommand.Parameters["@publication_year"].Value = books.publication_year;
            sda.SelectCommand.Parameters["@name"].Value = books.name;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            Guid id = Guid.Parse(sda.SelectCommand.Parameters["@id"].Value.ToString());
            sda.Dispose();
            con.Close();
            return id;
        }

        public static void Update_Book(Books books)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("update_book", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@author_id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@editorial_id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@category_id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@publication_year", SqlDbType.VarChar));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar));
            sda.SelectCommand.Parameters["@id"].Value = books.id;
            sda.SelectCommand.Parameters["@author_id"].Value = books.author_id;
            sda.SelectCommand.Parameters["@editorial_id"].Value = books.editorial_id;
            sda.SelectCommand.Parameters["@category_id"].Value = books.category_id;
            sda.SelectCommand.Parameters["@publication_year"].Value = books.publication_year;
            sda.SelectCommand.Parameters["@name"].Value = books.name;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
        }

        public static void Delete_Book(Guid id)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("delete_book", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters["@id"].Value = id;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
        }

        public static DataSet Get_Book()
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("get_book", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return ds;
        }

        public static List<Books> Get_Book_List()
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("get_book", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return List_Books(ds);
        }

        public static DataSet Get_Book_Filter(Books books)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["dbTestNB"].ConnectionString);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("get_book_Filter", con);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            sda.SelectCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@author_id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@editorial_id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@category_id", SqlDbType.UniqueIdentifier));
            sda.SelectCommand.Parameters.Add(new SqlParameter("@publication_year", SqlDbType.VarChar));
            sda.SelectCommand.Parameters["@id"].Value = books.id;
            sda.SelectCommand.Parameters["@author_id"].Value = books.author_id;
            sda.SelectCommand.Parameters["@editorial_id"].Value = books.editorial_id;
            sda.SelectCommand.Parameters["@category_id"].Value = books.category_id;
            sda.SelectCommand.Parameters["@publication_year"].Value = books.publication_year;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            sda.Dispose();
            con.Close();
            return ds;
        }
    }
}