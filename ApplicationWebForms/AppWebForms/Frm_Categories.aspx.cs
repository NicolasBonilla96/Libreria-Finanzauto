using ApplicationWebForms.ServiceBook;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationWebForms.AppWebForms
{
    public partial class Frm_Categories : System.Web.UI.Page
    {
        ServiceBook.ServiceBookSoapClient servicio = new ServiceBook.ServiceBookSoapClient();
        ServiceBook.Categories categories = new ServiceBook.Categories();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
            }
        }

        /// <summary>
        /// METODO PARA CARGAR LA GRILLA DE CATEGORIAS CREADAS
        /// </summary>
        protected void LoadCategories()
        {
            DataSet dsCategories = servicio.Get_Categories();
            if (dsCategories.Tables[0].Rows.Count > 0)
            {
                gvCategories.DataSource = dsCategories;
                gvCategories.DataBind();
                ViewState["Categories"] = dsCategories;
                gvCategories.Visible = true;
            }
            else
            {
                gvCategories.Visible = false;
            }
        }

        /// <summary>
        /// BOTÓN PARA LIMPIAR LOS DATOS DEL FORMULARIO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Clear_Properties(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtName.Text = "";
            btnCreate.Visible = true;
            btnUpdate.Visible = false;
        }

        /// <summary>
        /// METODO PARA PINTAR LOS DATOS DEL FORMULARIO SEGÚN LA CATEGORÍA SELECCIONADA
        /// </summary>
        /// <param name="id"></param>
        protected void loadProperties(Guid id)
        {
            Categories categorie = new Categories();
            categorie.id = id;
            DataSet dsCategorie = servicio.Get_Categories_Filter(categorie);
            txtId.Text = dsCategorie.Tables[0].Rows[0]["id"].ToString();
            txtName.Text = dsCategorie.Tables[0].Rows[0]["name"].ToString();
            btnCreate.Visible = false;
            btnUpdate.Visible = true;
        }

        /// <summary>
        /// METODO PARA CREAR EL OBJETO DE LA ENTIDAD CATEGORIA
        /// </summary>
        /// <returns></returns>
        protected Categories CreateCategory()
        {
            Categories categories = new Categories();
            if(txtId.Text != "")
            {
                categories.id = Guid.Parse(txtId.Text);
            }
            categories.name = txtName.Text;
            return categories;
        }

        /// <summary>
        /// METODO PARA EL BOTÓN CREAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                Categories categories = CreateCategory();
                categories.id = servicio.Insert_Categories(categories);
                LoadCategories();
                Clear_Properties(null, null);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('La categoria fue creada');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Debe diligenciar el Nombre de la categoria');", true);
            }
        }

        /// <summary>
        /// METODO PARA EL BOTÓN ACTUALIZAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                Categories categories = CreateCategory();
                servicio.Update_Categories(categories);
                LoadCategories();
                Clear_Properties(null, null);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('La categoria fue actualizada');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Debe diligenciar el Nombre de la categoria');", true);
            }
        }

        /// <summary>
        /// METODO ROWDATA_COMMAND PARA TOMAR LOS DATOS DEL REGISTRO SELECCIONADO Y REALIZAR LA ACCIÓN ACTUALIZAR O ELIMINAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCategories_Row(object sender, GridViewCommandEventArgs e)
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
        /// METODO PARA INVOCAR EL CARGUE DE LOS DATOS DEL REGISTRO SELECCIONADO
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
        /// METODO PARA ELIMINAR LA CATEGORIA SELECCIONADO
        /// </summary>
        /// <param name="e"></param>
        protected void Delete(GridViewCommandEventArgs e)
        {
            try
            {
                servicio.Delete_Categories(Guid.Parse(e.CommandArgument.ToString()));
                LoadCategories();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('La categoria fue eliminada');", true);

            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// METODO PAGEINDEX PARA LA GRILLA, ACTUALMENTE DESHABILIDATO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvCategories_Index(object sender, GridViewPageEventArgs e)
        {
            gvCategories.PageIndex = e.NewPageIndex;
            gvCategories.DataSource = ViewState["Categories"];
            gvCategories.DataBind();
        }
    }
}