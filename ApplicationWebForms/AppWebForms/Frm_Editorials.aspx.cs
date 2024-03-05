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
    public partial class Frm_Editorials : System.Web.UI.Page
    {
        ServiceBook.ServiceBookSoapClient servicio = new ServiceBook.ServiceBookSoapClient();
        ServiceBook.Editorials editorials = new ServiceBook.Editorials();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEditorials();
            }
        }

        /// <summary>
        /// METODO PARA CARGAR LA GRILLA CON LAS EDITORIALES CREADAS
        /// </summary>
        protected void LoadEditorials()
        {
            DataSet dsEditorials = servicio.Get_Editorials();
            if (dsEditorials.Tables[0].Rows.Count > 0)
            {
                gvEditorials.DataSource = dsEditorials;
                gvEditorials.DataBind();
                ViewState["Editorials"] = dsEditorials;
                gvEditorials.Visible = true;
            }
            else
            {
                gvEditorials.Visible = false;
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
            txtLocation.Text = "";
            btnCreate.Visible = true;
            btnUpdate.Visible = false;
        }

        /// <summary>
        /// METODO PARA PINTAR PAS PROPIEDADES DEL FORMULARIO SEGÚN LA EDITORIAL SELECCIONADA
        /// </summary>
        /// <param name="id"></param>
        protected void loadProperties(Guid id)
        {
            Editorials editorial = new Editorials();
            editorial.id = id;
            DataSet dsEditorial = servicio.Get_Editorials_Filter(editorial);
            txtId.Text = dsEditorial.Tables[0].Rows[0]["id"].ToString();
            txtName.Text = dsEditorial.Tables[0].Rows[0]["name"].ToString();
            txtLocation.Text = dsEditorial.Tables[0].Rows[0]["location"].ToString();
            btnCreate.Visible = false;
            btnUpdate.Visible = true;
        }

        /// <summary>
        /// METODO PARA CREAR EL OBJETO DE LA ENTIDAD EDITORIAL
        /// </summary>
        /// <returns></returns>
        protected Editorials CreateEditorial()
        {
            Editorials editorials = new Editorials();
            if(txtId.Text != "")
            {
                editorials.id = Guid.Parse(txtId.Text);
            }
            editorials.name = txtName.Text;
            editorials.location = txtLocation.Text;
            return editorials;
        }

        /// <summary>
        /// METODO PARA EL BOTÓN CREAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtLocation.Text != "")
            {
                Editorials editorials = CreateEditorial();
                editorials.id = servicio.Insert_Editorials(editorials);
                LoadEditorials();
                Clear_Properties(null, null);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('La editorial fue creada');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Debe diligenciar el Nombre y la Ubicación de la Editorial');", true);
            }
        }

        /// <summary>
        /// METODO PARA EL BOTÓN ACTUALIZAD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtLocation.Text != "")
            {
                Editorials editorials = CreateEditorial();
                servicio.Update_Editorials(editorials);
                LoadEditorials();
                Clear_Properties(null, null);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('La editorial fue actualizada');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Debe diligenciar el Nombre y la Ubicación de la Editorial');", true);
            }
        }

        /// <summary>
        /// METODO ROWDATA_COMMAND PARA TOMAR LOS DATOS DE LA EDITORIAL SELECCIONADA Y HACER LA ACCIÓN DE ACTUALIZAR O ELIMINAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvEditorials_Row(object sender, GridViewCommandEventArgs e)
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
        /// METODO PARA INVOCAR EL CARGUE DE LAS PROPIEDADES DE LA EDITORIAL SELECCIONADA
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
        /// METODO PARA ELIMINAR LA EDITORIAL SELECCIONADA
        /// </summary>
        /// <param name="e"></param>
        protected void Delete(GridViewCommandEventArgs e)
        {
            try
            {
                servicio.Delete_Editorials(Guid.Parse(e.CommandArgument.ToString()));
                LoadEditorials();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('La editorial fue eliminada');", true);

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
        protected void gvEditorials_Index(object sender, GridViewPageEventArgs e)
        {
            gvEditorials.PageIndex = e.NewPageIndex;
            gvEditorials.DataSource = ViewState["Authors"];
            gvEditorials.DataBind();
        }
    }
}