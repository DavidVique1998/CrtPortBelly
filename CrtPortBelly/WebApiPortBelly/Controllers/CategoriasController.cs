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
    [EnableCorsAttribute("*", "*", "*", SupportsCredentials = true)]
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/Categorias")]

    public class CategoriasController : ApiController
    {
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult Post(Categoria producto)
        {
            try
            {
                CategoriaBLL.Create(producto);
                return Content(HttpStatusCode.Created, "Categoría creada correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Administrador, Cliente")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Categoria categoria = CategoriaBLL.Get(id);
                return Content(HttpStatusCode.OK, categoria);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Administrador, Cliente")]
        public IHttpActionResult Get()
        {
            try
            {
                List<Categoria> todos = CategoriaBLL.List();
                return Content(HttpStatusCode.OK, todos);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                CategoriaBLL.Delete(id);
                return Ok("Producto eliminado correctamente");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        //public IHttpActionResult Put(Categoria categoria)
        //{
        //    try
        //    {
        //        bool llave = CategoriaBLL.Updates(categoria);
        //        if (llave)
        //        {
        //            return Content(HttpStatusCode.OK, "Alumno actualizado correctamente");
        //        }
        //        else
        //        {
        //            return Content(HttpStatusCode.BadRequest, "No se puede actualizar el producto");
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message + categoria.ToString());
        //    }
        //}
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult Put(Categoria categoria)
        {
            try
            {
                CategoriaBLL.Updates(categoria);
                return Content(HttpStatusCode.Accepted, "Categoría actualizada correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + categoria.ToString());
            }
        }
    }
}
