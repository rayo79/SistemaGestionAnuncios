using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionAnuncios
{
    public partial class Main : System.Web.UI.Page
    {        
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaPagina();
            }
        }

        private async void CargaPagina()
        {
            string resp = await GetHttp();
            List<Models.Response.TipoAnuncioResponse>  tiposAnuncio = JsonConvert.DeserializeObject<List<Models.Response.TipoAnuncioResponse>>(resp);
            ddlTipo.DataValueField = "Id";
            ddlTipo.DataTextField = "Descripcion";
            ddlTipo.DataSource = tiposAnuncio;
            ddlTipo.DataBind();
        }

        private async Task<string> GetHttp()
        {
            string url = ConfigurationManager.AppSettings["UrlTiposAnuncios"];
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            return await reader.ReadToEndAsync();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {            
            lvAnuncios.DataSource = ObtenerAnuncios();
            lvAnuncios.DataBind();
        }

        private List<Models.Response.AnuncioResponse> ObtenerAnuncios()
        {
            string url = ConfigurationManager.AppSettings["UrlAnuncios"];

            Models.Request.AnuncioRequest anuncioReq = new Models.Request.AnuncioRequest();
            anuncioReq.Titulo = txtAnuncio.Text;
            anuncioReq.Precio = txtPrecio.Text;
            anuncioReq.Tipo = ddlTipo.SelectedValue;

            JavaScriptSerializer js = new JavaScriptSerializer();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(anuncioReq);
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";            
            request.ContentType = "application/json;charset=utf-8'";            
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();
            string resp = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                resp = streamReader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<Models.Response.AnuncioResponse>>(resp);

        }

        protected void lvAnuncios_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dtPaginacion.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            lvAnuncios.DataSource = ObtenerAnuncios();
            lvAnuncios.DataBind();
        }

        protected  void btnOk_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string correoDe = ((TextBox)pnlCorreo.FindControl("txtCorreo")).Text;
                MailMessage mensaje = new MailMessage(correoDe, ConfigurationManager.AppSettings["CorreoPara"]);
                mensaje.Subject = "Comentario sistema anuncios";
                mensaje.Body = "<p>SE RECIBIO UN COMENTARIO DESDE EL SISTEMA DE ANUNCIOS EMPRESARIALES CON LA SIGUIENTE INFORMACÓN DE CONTACTO</p>" +
                    "<br /><br />" +
                    "<p><b>Nombre:</b> " + ((TextBox)pnlCorreo.FindControl("txtNombre")).Text + "</p>" +
                    "<p><b>Correo Contacto:</b> " + correoDe + "</p>" +
                    "<p><b>Mensaje:</b><br/></p> " + ((TextBox)pnlCorreo.FindControl("txtMensaje")).Text;

                SmtpClient cliente = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"]);
                cliente.Send(mensaje);
            }
        }

        protected void txtAnuncio_TextChanged(object sender, System.EventArgs e)
        {
            lvAnuncios.DataSource = ObtenerAnuncios();
            lvAnuncios.DataBind();
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvAnuncios.DataSource = ObtenerAnuncios();
            lvAnuncios.DataBind();
        }

        protected void txtPrecio_TextChanged(object sender, System.EventArgs e)
        {
            lvAnuncios.DataSource = ObtenerAnuncios();
            lvAnuncios.DataBind();
        }

    }
}