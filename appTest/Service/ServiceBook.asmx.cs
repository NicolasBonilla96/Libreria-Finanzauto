using appTest.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace appTest.Service
{
    /// <summary>
    /// Servicio Soap para el consumo de los metodos para realizar el crud de las diferentes entidades de la libreria
    /// </summary>
    [WebService(Namespace = "http://nicolas_bonilla_finanzauto.net/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ServiceBook : WebService
    {
        #region AUTHORS
        [WebMethod]
        public Guid Insert_Author(Authors authors)
        {
            return Procedures.AuthorsProcedures.Insert_Author(authors);
        }

        [WebMethod]
        public void Update_Author(Authors authors)
        {
            Procedures.AuthorsProcedures.Update_Author(authors);
        }

        [WebMethod]
        public void Delete_Author(Guid id)
        {
            Procedures.AuthorsProcedures.Delete_Author(id);
        }

        [WebMethod]
        public DataSet Get_Author()
        {
            return Procedures.AuthorsProcedures.Get_Authors();
        }

        [WebMethod]
        public List<Authors> Get_Author_List()
        {
            return Procedures.AuthorsProcedures.Get_Authors_List();
        }

        [WebMethod]
        public DataSet Get_Authors_Filter(Authors authors)
        {
            return Procedures.AuthorsProcedures.Get_Authors_Filter(authors);
        }
        #endregion

        #region BOOKS
        [WebMethod]
        public Guid Insert_Book(Books books)
        {
            return Procedures.BooksProcedures.Insert_Book(books);
        }

        [WebMethod]
        public void Update_Book(Books books)
        {
            Procedures.BooksProcedures.Update_Book(books);
        }

        [WebMethod]
        public void Delete_Book(Guid id)
        {
            Procedures.BooksProcedures.Delete_Book(id);
        }

        [WebMethod]
        public DataSet Get_Book()
        {
            return Procedures.BooksProcedures.Get_Book();
        }

        [WebMethod]
        public List<Books> Get_Book_List()
        {
            return Procedures.BooksProcedures.Get_Book_List();
        }

        [WebMethod]
        public DataSet Get_Book_Filter(Books books)
        {
            return Procedures.BooksProcedures.Get_Book_Filter(books);
        }
        #endregion

        #region CATEGORIES
        [WebMethod]
        public Guid Insert_Categories(Categories categories)
        {
            return Procedures.CategoriesProcedures.Insert_Categories(categories);
        }

        [WebMethod]
        public void Update_Categories(Categories categories)
        {
            Procedures.CategoriesProcedures.Update_Category(categories);
        }

        [WebMethod]
        public void Delete_Categories(Guid id)
        {
            Procedures.CategoriesProcedures.Delete_Category(id);
        }

        [WebMethod]
        public DataSet Get_Categories()
        {
            return Procedures.CategoriesProcedures.Get_Categories();
        }

        [WebMethod]
        public List<Categories> Get_CAtegories_List()
        {
            return Procedures.CategoriesProcedures.Get_Categories_List();
        }

        [WebMethod]
        public DataSet Get_Categories_Filter(Categories categories)
        {
            return Procedures.CategoriesProcedures.Get_Categories_Filter(categories);
        }
        #endregion

        #region COMMENTS
        [WebMethod]
        public Guid Insert_Comments(Comments comments)
        {
            return Procedures.CommentsProcedures.Insert_Comments(comments);
        }

        [WebMethod]
        public void Update_Comments(Comments comments)
        {
            Procedures.CommentsProcedures.Update_Comments(comments);
        }

        [WebMethod]
        public void Delete_Comments(Guid id)
        {
            Procedures.CommentsProcedures.Delete_Comments(id);
        }

        [WebMethod]
        public DataSet Get_Comments()
        {
            return Procedures.CommentsProcedures.Get_Comments();
        }

        [WebMethod]
        public DataSet Get_Comments_Filter(Comments comments)
        {
            return Procedures.CommentsProcedures.Get_Comments_Filter(comments);
        }
        #endregion

        #region EDITORIALS
        [WebMethod]
        public Guid Insert_Editorials(Editorials editorials)
        {
            return Procedures.EditorialsProcedures.Insert_Editorials(editorials);
        }

        [WebMethod]
        public void Update_Editorials(Editorials editorials)
        {
            Procedures.EditorialsProcedures.Update_Editorials(editorials);
        }

        [WebMethod]
        public void Delete_Editorials(Guid id)
        {
            Procedures.EditorialsProcedures.Delete_Editorials(id);
        }

        [WebMethod]
        public DataSet Get_Editorials()
        {
            return Procedures.EditorialsProcedures.Get_Editorials();
        }

        [WebMethod]
        public List<Editorials> Get_Editorials_List()
        {
            return Procedures.EditorialsProcedures.Get_Editorials_List();
        }

        [WebMethod]
        public DataSet Get_Editorials_Filter(Editorials editorials)
        {
            return Procedures.EditorialsProcedures.Get_Editorials_Filter(editorials);
        }
        #endregion

        #region USERS
        [WebMethod]
        public Guid Insert_User(Users users)
        {
            return Procedures.UsersProcedures.Insert_User(users);
        }

        [WebMethod]
        public void Update_User(Users users)
        {
            Procedures.UsersProcedures.Update_User(users);
        }

        [WebMethod]
        public void Delete_User(Guid id)
        {
            Procedures.UsersProcedures.Delete_User(id);
        }

        [WebMethod]
        public DataSet Get_Users()
        {
            return Procedures.UsersProcedures.Get_Users();
        }

        [WebMethod]
        public List<Users> Get_Users_List()
        {
            return Procedures.UsersProcedures.Get_Users_List();
        }

        [WebMethod]
        public DataSet Get_User_Filter(Users users)
        {
            return Procedures.UsersProcedures.Get_User_Filter(users);
        }
        #endregion
    }
}
