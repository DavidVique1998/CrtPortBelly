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
    public class CarritosController : ApiController
    {
        
        public IHttpActionResult Post(Carrito carrito)
        {
            try
            {
                CarritoBLL.Create(carrito);
                return Content(HttpStatusCode.Created, "Carritos creado correctamente");
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
                Carrito carrito = CarritoBLL.Get(id);
                return Content(HttpStatusCode.OK, carrito);
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
                List<Carrito> todos = CarritoBLL.List();
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
                CarritoBLL.Delete(id);
                return Ok("Carritos eliminado correctamente");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        public IHttpActionResult Put(Carrito carrito)
        {
            try
            {
                CarritoBLL.Update(carrito);

                return Content(HttpStatusCode.Accepted, "Alumno actualizado correctamente");



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + carrito.ToString());
            }
        }
    }
}
