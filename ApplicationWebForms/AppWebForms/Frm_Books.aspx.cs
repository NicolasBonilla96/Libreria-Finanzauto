using ApplicationWebForms.ServiceBook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationWebForms.AppWebForms
{
    public partial class Frm_Books : System.Web.UI.Page
    {
        ServiceBook.ServiceBookSoapClient servicio = new ServiceBook.ServiceBookSoapClient();
        ServiceBook.Books books = new ServiceBook.Books();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //MEDOTOS PARA CARGAR LAS LISTAS DESPLEGABLES Y LA GRILLA DE LIBROS EXISTENTES
                LoadDdls();
                LoadBooks();
            }
        }

        /// <summary>
        /// CARGAR LISTAS DESPLEGABLES
        /// </summary>
        protected void LoadDdls()
        {
            Authors[] authors = servicio.Get_Author_List();
            if (authors.Length != 0)
            {
                ddlAuthor.DataSource = authors;
                ddlAuthor.DataTextField = "name";
                ddlAuthor.DataValueField = "id";
                ddlAuthor.DataBind();
                ddlAuthor.Items.Insert(0,"Seleccionar");
            }

            Categories[] categories = servicio.Get_CAtegories_List();
            if (categories.Length != 0)
            {
                ddlCategory.DataSource = categories;
                ddlCategory.DataTextField = "name";
                ddlCategory.DataValueField = "id";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, "Seleccionar");
            }

            Editorials[] editorials = servicio.Get_Editorials_List();
            if (editorials.Length != 0)
            {
                ddlEditorial.DataSource = editorials;
                ddlEditorial.DataTextField = "name";
                ddlEditorial.DataValueField = "id";
                ddlEditorial.DataBind();
                ddlEditorial.Items.Insert(0, "Seleccionar");
            }
        }

        /// <summary>
        /// CARGAR LIBROS CREADOS
        /// </summary>
        protected void LoadBooks()
        {
            DataSet dsBooks = servicio.Get_Book();
            if (dsBooks.Tables[0].Rows.Count > 0)
            {
                gvBooks.DataSource = dsBooks;
                gvBooks.DataBind();
                ViewState["Books"] = dsBooks;
                gvBooks.Visible = true;
            }
            else
            {
                gvBooks.Visible = false;
            }
        }

        /// <summary>
        /// METODO PARA EL BOTÓN LIMPIAR OBJETOS DEL FORMULARIO
        /// </summary>
        protected void Clear_Properties(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtName.Text = "";
            txtPublication.Text = "";
            ddlAuthor.SelectedValue = "Seleccionar";
            ddlCategory.SelectedValue = "Seleccionar";
            ddlEditorial.SelectedValue = "Seleccionar";
            btnCreate.Visible = true;
            btnUpdate.Visible = false;
        }

        /// <summary>
        /// METODO PARA PINTAR LOS OBJETOS DEL FORMULARIO SEGÚN EL LIBRO SELECCIONADO
        /// </summary>
        /// <param name="id"></param>
        protected void loadProperties(Guid id)
        {
            Books book = new Books();
            book.id = id;
            DataSet dsbook = servicio.Get_Book_Filter(book);
            txtId.Text = dsbook.Tables[0].Rows[0]["id"].ToString();
            txtName.Text = dsbook.Tables[0].Rows[0]["name"].ToString();
            txtPublication.Text = dsbook.Tables[0].Rows[0]["publication_year"].ToString();
            ddlAuthor.SelectedValue = dsbook.Tables[0].Rows[0]["author_id"].ToString();
            ddlCategory.SelectedValue = dsbook.Tables[0].Rows[0]["category_id"].ToString();
            ddlEditorial.SelectedValue = dsbook.Tables[0].Rows[0]["editorial_id"].ToString();
            btnCreate.Visible = false;
            btnUpdate.Visible = true;
        }

        /// <summary>
        /// METODO PARA CREAR EL OBJETO DE LA ENTIDAD LIBRO
        /// </summary>
        /// <returns></returns>
        protected Books CreateBook()
        {
            Books books = new Books();
            if(txtId.Text != "")
            {
                books.id = Guid.Parse(txtId.Text);
            }
            books.name = txtName.Text;
            books.publication_year = txtPublication.Text;
            books.author_id = Guid.Parse(ddlAuthor.SelectedValue.ToString());
            books.category_id = Guid.Parse(ddlCategory.SelectedValue.ToString());
            books.editorial_id = Guid.Parse(ddlEditorial.SelectedValue.ToString());
            return books;
        }

        /// <summary>
        /// METODO PARA EL BOTÓN CREAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtPublication.Text != "" && ddlEditorial.SelectedValue != "Seleccionar" && ddlCategory.SelectedValue != "Seleccionar" && ddlAuthor.SelectedValue != "Seleccionar")
            {
                Books books = CreateBook();
                books.id = servicio.Insert_Book(books);
                LoadBooks();
                Clear_Properties(null, null);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('El libro fue creado');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Debe diligenciar todos los datos para el Libro');", true);
            }
        }

        /// <summary>
        /// METODO PARA EL BOTÓN ACTUALIZAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtPublication.Text != "" && ddlEditorial.SelectedValue != "Seleccionar" && ddlCategory.SelectedValue != "Seleccionar" && ddlAuthor.SelectedValue != "Seleccionar")
            {
                Books books = CreateBook();
                servicio.Update_Book(books);
                LoadBooks();
                Clear_Properties(null, null);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('El libro fue actualizado');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Debe diligenciar todos los datos para el Libro');", true);
            }
        }

        /// <summary>
        /// METODO ROWDATA_COMMAND PARA TOMAR LOS DATOS DEL REGISTRO DE LA GRILLA SELECCIONADO Y REALIZAR LA ACCIÓN ACTUALIZAR O ELIMINAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBooks_Row(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Actualizar")
                    Update(e);

                if (e.CommandName == "Eliminar")
                    Delete(e);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// METODO PARA INVOCAR EL LLENADO DEL FORMULARIO
        /// </summary>
        /// <param name="e"></param>
        protected void Update(GridViewCommandEventArgs e)
        {
            try
            {
                loadProperties(Guid.Parse(e.CommandArgument.ToString()));
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// METODO PARA LA ELIMINACIÓN DEL LIBRO SELECCIONADO
        /// </summary>
        /// <param name="e"></param>
        protected void Delete(GridViewCommandEventArgs e)
        {
            try
            {
                servicio.Delete_Book(Guid.Parse(e.CommandArgument.ToString()));
                LoadBooks();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('El libro fue eliminado');", true);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// METODO PAGEINDEX PARA LA GRILLA, ACTUALMENTE DESHABILITADO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBooks_Index(object sender, GridViewPageEventArgs e)
        {
            gvBooks.PageIndex = e.NewPageIndex;
            gvBooks.DataSource = ViewState["Books"];
            gvBooks.DataBind();
        }
    }
}