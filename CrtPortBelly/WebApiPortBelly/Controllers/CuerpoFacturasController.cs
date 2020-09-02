using BEUCrtPortBelly;
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
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class CuerpoFacturasController : ApiController
    {

        public IHttpActionResult Post(CuerpoFactura cuerpoFactura)
        {
            try
            {
                CuerpoFacturaBLL.Create(cuerpoFactura);
                return Content(HttpStatusCode.Created, "CuerpoFacturas creado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                CuerpoFactura cuerpoFactura = CuerpoFacturaBLL.Get(id);
                return Content(HttpStatusCode.OK, cuerpoFactura);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public IHttpActionResult Get()
        {
            try
            {
                List<CuerpoFactura> todos = CuerpoFacturaBLL.List();
                return Content(HttpStatusCode.OK, todos);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        public IHttpActionResult Delete(int id)
        {
            try
            {
                CuerpoFacturaBLL.Delete(id);
                return Ok("CuerpoFacturas eliminado correctamente");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        public IHttpActionResult Put(CuerpoFactura cuerpoFactura)
        {
            try
            {
                CuerpoFacturaBLL.Update(cuerpoFactura);

                return Content(HttpStatusCode.OK, "Alumno actualizado correctamente");



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + cuerpoFactura.ToString());
            }
        }
    }
}
