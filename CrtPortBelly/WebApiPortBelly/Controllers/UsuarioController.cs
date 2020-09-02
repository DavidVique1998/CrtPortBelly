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
    [RoutePrefix("api/Usuario")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    
    public class UsuarioController : ApiController
    {
        public IHttpActionResult Post(Usuario usuario)
        {
            try
            {
                //Si existe un correo retorna un true sino existe el retorna retorna un false
                if (UsuarioBLL.GetUsuarioByMail(usuario.uso_cor) != null)
                {
                    //400
                    return Content(HttpStatusCode.Conflict, "El correo ya existe, intenta con otro");
                }
                else
                {
                    usuario.uso_rol = "Cliente";

                    UsuarioBLL.Create(usuario);
                    //201
                    return Content(HttpStatusCode.Created, "Usuario creado correctamente");
                } 
            }
            catch (Exception ex)
            {
                //400
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Administrador, Cliente")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Usuario usuario = UsuarioBLL.Get(id);
                return Content(HttpStatusCode.OK, usuario);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Administrador, Cliente")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                UsuarioBLL.Delete(id);
                return Ok("Usuario eliminado correctamente");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }
        [Authorize(Roles = "Administrador, Cliente")]
        public IHttpActionResult Put(Usuario usuario)
        {
            try
            {
                UsuarioBLL.Update(usuario);

                return Content(HttpStatusCode.Accepted, "Usuario actualizado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + usuario.ToString());
            }
        }

        //public IHttpActionResult Usuario(string parametro)
        //{
        //    Usuario usu;
        //    if (parametro.Contains('@') && parametro.Contains('.'))
        //    {
        //        usu = UsuarioBLL.GetUsuarioByMail(parametro);          
        //    }
        //    else
        //    {
        //        usu = UsuarioBLL.GetUsuarioByUsu(parametro);
        //    }
        //    if (usu != null)
        //    {
        //        return Content(HttpStatusCode.OK, true);
        //    }
        //    else
        //    {
        //        return Content(HttpStatusCode.NotFound, "Usuario no encontrado");
        //    }
        //}



    }
}
