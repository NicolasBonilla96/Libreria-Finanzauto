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
    public partial class Frm_Users : System.Web.UI.Page
    {
        ServiceBook.ServiceBookSoapClient servicio = new ServiceBook.ServiceBookSoapClient();
        ServiceBook.Users users = new ServiceBook.Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers();
            }
        }

        /// <summary>
        /// METODO PARA CARGAR LOS USUARIOS CREADOS
        /// </summary>
        protected void LoadUsers()
        {
            DataSet dsUsers = servicio.Get_Users();
            if (dsUsers.Tables[0].Rows.Count > 0)
            {
                gvUsers.DataSource = dsUsers;
                gvUsers.DataBind();
                ViewState["Users"] = dsUsers;
                gvUsers.Visible = true;
            }
            else
            {
                gvUsers.Visible = false;
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
            txtEmail.Text = "";
            btnCreate.Visible = true;
            btnUpdate.Visible = false;
        }

        /// <summary>
        /// METODO PARA CARGAR LAS PROPIEDADES DE LOS OBJETOS DEL FORMULARIO SEGÚN EL USUARIO SELECCIONADO
        /// </summary>
        /// <param name="id"></param>
        protected void loadProperties(Guid id)
        {
            Users user = new Users();
            user.id = id;
            DataSet dsUser = servicio.Get_User_Filter(user);
            txtId.Text = dsUser.Tables[0].Rows[0]["id"].ToString();
            txtName.Text = dsUser.Tables[0].Rows[0]["name"].ToString();
            txtEmail.Text = dsUser.Tables[0].Rows[0]["email"].ToString();
            btnCreate.Visible = false;
            btnUpdate.Visible = true;
        }

        /// <summary>
        /// METODO PARA CREAR EL OBJETO DE LA ENTIDAD USUARIO
        /// </summary>
        /// <returns></returns>
        protected Users CreateUser()
        {
            Users user = new Users();
            if (txtId.Text != "")
            {
                user.id = Guid.Parse(txtId.Text);
            }
            user.name = txtName.Text; 
            user.email = txtEmail.Text;
            return user;
        }

        /// <summary>
        /// METODO PARA EL BOTÓN CREAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCreate_Click(object sender, EventArgs e)
        {
            var regexMail = @"^[^@]+@[^@]+\.[a-zA-Z]{2,}$";
            var match = Regex.Match(txtEmail.Text, regexMail, RegexOptions.IgnoreCase);
            if (txtName.Text != "" && match.Success)
            {
                Users user = CreateUser();
                user.id = servicio.Insert_User(user);
                LoadUsers();
                Clear_Properties(null, null);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('El usuario fue creado');", true);
            }
            else if (!match.Success)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('El Email no tiene el formato correcto');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Debe diligenciar el Nombre y Email del usuario');", true);
            }
        }

        /// <summary>
        /// METODO PARA EL BOTÓN ACTUALIZAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            var regexMail = @"^[^@]+@[^@]+\.[a-zA-Z]{2,}$";
            var match = Regex.Match(txtEmail.Text, regexMail, RegexOptions.IgnoreCase);
            if (txtName.Text != "" && match.Success)
            {
                Users user = CreateUser();
                servicio.Update_User(user);
                LoadUsers();
                Clear_Properties(null, null);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('El usuario fue actualizado');", true);
            }
            else if (!match.Success)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('El Email no tiene el formato correcto');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('Debe diligenciar el Nombre y Email del usuario');", true);
            }
        }

        /// <summary>
        /// METODO ROWDATA_COMMAND PARA TOMAR LOS DATOS DEL USUARIO SELECCIONADO Y HACER LA ACCIÓN DE ACTUALIZAR O ELIMINAR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUsers_Row(object sender, GridViewCommandEventArgs e)
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
        /// METODO PARA INVOCAR EL CARGUE DE LOS DATOS DEL USUARIO SELLECIONADO
        /// </summary>
        /// <param name="e"></param>
        protected void Update(GridViewCommandEventArgs e)
        {
            try
            {
                loadProperties(Guid.Parse(e.CommandArgument.ToString()));    
            }
            catch(Exception ex)
            {
            }
        }

        /// <summary>
        /// METODO PARA ELIMINAR EL USUARIO SELECCIONADO
        /// </summary>
        /// <param name="e"></param>
        protected void Delete(GridViewCommandEventArgs e)
        {
            try
            {
                servicio.Delete_User(Guid.Parse(e.CommandArgument.ToString()));
                LoadUsers();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertIns", "alert('El usuario fue eliminado');", true);
            }
            catch(Exception ex)
            {
            }
        }
        
        /// <summary>
        /// METODO PAGEINDEX PARA LA GRILLA, ACTUALMENTE DESHABILITADO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUsers_Index(object sender, GridViewPageEventArgs e)
        {
            gvUsers.PageIndex = e.NewPageIndex;
            gvUsers.DataSource = ViewState["Users"];
            gvUsers.DataBind();
        }
    }
}