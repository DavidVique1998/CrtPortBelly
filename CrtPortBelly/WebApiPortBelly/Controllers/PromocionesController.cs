﻿using BEUCrtPortBelly;
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
    //[EnableCorsAttribute("*", "*", "*", SupportsCredentials = true)]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/Promociones")]
    public class PromocionesController : ApiController
    {
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult Post(Promocion promocion)
        {
            try
            {
                PromocionBLL.Create(promocion);
                return Content(HttpStatusCode.Created, "Promoción creada correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Administrador,Cliente")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Promocion Promocion = PromocionBLL.Get(id);
                return Content(HttpStatusCode.OK, Promocion);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Administrador")]
        public IHttpActionResult Get()
        {
            try
            {
                List<Promocion> todos = PromocionBLL.List();
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
                PromocionBLL.Delete(id);
                return Ok("Promoción eliminada correctamente");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        [Authorize(Roles = "Administrador")]
        public IHttpActionResult Put(Promocion promocion)
        {
            try
            {
                PromocionBLL.Updates(promocion);

                return Content(HttpStatusCode.Accepted, "Promoción actualizada correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + promocion.ToString());
            }
        }
    }
}
