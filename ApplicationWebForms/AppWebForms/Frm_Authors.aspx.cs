using ApplicationWebForms.ServiceBook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationWebForms.AppWebForms
{
    public partial class Frm_Authors : System.Web.UI.Page
    {
        ServiceBook.ServiceBookSoapClient servicio = new ServiceBook.ServiceBookSoapClient();
        ServiceBook.Authors authors = new ServiceBook.Authors();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAuthors();
            }
        }

        /// <summary>
        /// METODO PARA CARGAR LA GRILLA DE AUTORES CREADOS
        /// </summary>
        protected void LoadAuthors()
        {
            DataSet dsAuthors = servicio.Get_Author();
            if (dsAuthors.Tables[0].Rows.Count > 0)
            {
                gvAuthors.DataSource = dsAuthors;
                gvAuthors.DataBind();
                ViewState["Authors"] = dsAuthors;
                gvAuthors.Visible = true;
            }
            else
            {
                gvAuthors.Visible = false;
            }
        }

        /// <summary>
        /// BOTÓN PARA LIMPIAR LOS OBJETOS DEL FORMULARIO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Clear_Properties(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtName.Text = "";
            txtLastname.Text = "";
            btnCreate.Visible = true;
            btnUpdate.Visible = false;
        }

        /// <summary>
        /// METODO PARA PINTAR LOS OBJETOS DEL FORMULARIO SEGÚN EL AUTOR SELECCIONADO
        /// </summary>
        /// <param name="id"></param>
        protected void loadProperties(Guid id)
        {
            Authors author = new Authors();
            author.id = id;
            DataSet dsAuthor = servicio.Get_Authors_Filter(author);
            txtId.Text = dsAuthor.Tables[0].Rows[0]["id"].ToString();
            txtName.Text = dsAuthor.Tables[0].Rows[0]["name"].ToString();
            txtLastname.Text = dsAuthor.Tables[0].Rows[0]["lastname"].ToString();
            btnCreate.Visible = false;
            btnUpdate.Visible = true;
        }

        /// <summary>
        /// METODO PARA CREAR EL OBJETO DE LA ENTIDAD AUTOR
        /// </summary>
        /// <returns></returns>
        protected Authors CreateAuthor()
        {
            Authors authors = new Authors();
            if (txtId.Text != "")
            {
                authors.id = Guid.Parse(txtId.Text);
            }
            authors.name = txtName.Text;
            authors.lastname = txtLastname.Text;
            return authors;
        }

        /// <summary>
        /// METODO PARA EL BOTÓN CREAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtLastname.Text != "")
            {
                Authors authors = CreateAuthor();
                authors.id = servicio.Insert_Author(authors);
                LoadAuthors();
                Clear_Properties(null, null);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('El autor fue creado');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Debe diligenciar el Nombre y Apellido del Autor');", true);
            }
        }

        /// <summary>
        /// METODO PARA EL BOTÓN ACTUALIZAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtLastname.Text != "")
            {
                Authors authors = CreateAuthor();
                servicio.Update_Author(authors);
                LoadAuthors();
                Clear_Properties(null, null);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('El autor fue actualizado');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Debe diligenciar el Nombre y Apellido del Autor');", true);
            }
        }

        /// <summary>
        /// METODO ROWDATA_COMMAND PARA TOMAR LOS DATOS DEL AUTOR SELECCIONADO Y HACER LA ACCIÓN DE ACTUALIZAR O ELIMINAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvAuthors_Row(object sender, GridViewCommandEventArgs e)
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
        /// METODO PARA INVOCAR EL CARGUE DE LOS DATOS DEL AUTOR SELECCIONADO
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
        /// METODO PARA ELIMINAR EL AUTOR SELECCIONADO
        /// </summary>
        /// <param name="e"></param>
        protected void Delete(GridViewCommandEventArgs e)
        {
            try
            {
                servicio.Delete_Author(Guid.Parse(e.CommandArgument.ToString()));
                LoadAuthors();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('El autor fue eliminado');", true);
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
        protected void gvAuthors_Index(object sender, GridViewPageEventArgs e)
        {
            gvAuthors.PageIndex = e.NewPageIndex;
            gvAuthors.DataSource = ViewState["Authors"];
            gvAuthors.DataBind();
        }
    }
}