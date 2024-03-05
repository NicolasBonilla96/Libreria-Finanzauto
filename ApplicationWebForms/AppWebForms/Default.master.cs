using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationWebForms.AppWebForms
{
    public partial class Default : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void BtnUser_click(object sender, EventArgs e)
        {
            lblTitle.Text = "USUARIOS";
            Response.Redirect("Frm_Users.aspx", false);
        }

        protected void BtnCategory_click(object sender, EventArgs e)
        {
            lblTitle.Text = "CATEGORIAS";
            Response.Redirect("Frm_Categories.aspx", false);
        }

        protected void BtnAuthor_click(object sender, EventArgs e)
        {
            lblTitle.Text = "AUTORES";
            Response.Redirect("Frm_Authors.aspx", false);
        }

        protected void BtnEditorial_click(object sender, EventArgs e)
        {
            lblTitle.Text = "EDITORIALES";
            Response.Redirect("Frm_Editorials.aspx", false);
        }

        protected void BtnBook_click(object sender, EventArgs e)
        {
            lblTitle.Text = "LIBROS";
            Response.Redirect("Frm_Books.aspx", false);
        }
    }
}