using BEUCrtPortBelly.Clases;
using BEUCrtPortBelly.Queris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiPortBelly.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Correos")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class CorreosController : ApiController
    {
        public IHttpActionResult Post(Correos cor)
        {
            try
            {
                CorreoBLL.GetEmail(cor);
                return Content(HttpStatusCode.Created, "Correo enviado");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("error al enviar");
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("EnviarFactura")]
        [Authorize(Roles = "Cliente")]
        public IHttpActionResult EnviarFactura(Correos cor)
        {
            try
            {
                CorreoBLL.SendEmail(cor);
                return Content(HttpStatusCode.Created, "Factura enviada");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("error al enviar");
                return BadRequest(ex.Message);
            }
        }
    }
}
