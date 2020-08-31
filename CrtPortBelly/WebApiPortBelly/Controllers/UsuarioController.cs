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
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : ApiController
    {
        public IHttpActionResult Post(Usuario usuario)
        {
            try
            {
                UsuarioBLL.Create(usuario);
                return Content(HttpStatusCode.Created, "Usuario creado correctamente");
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
                Usuario usuario = UsuarioBLL.Get(id);
                return Content(HttpStatusCode.OK, usuario);
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
                List<Usuario> todos = UsuarioBLL.List();
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
                UsuarioBLL.Delete(id);
                return Ok("Usuario eliminado correctamente");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

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

        public IHttpActionResult Login(string parametro, string password)
        {
            Usuario usu;
            if (parametro.Contains('@') && parametro.Contains('.'))
            {
                usu = UsuarioBLL.LoginByMail(parametro, password);
            }
            else
            {
                usu = UsuarioBLL.LoginByUsu(parametro, password);
            }
           
            if (usu != null)
            {
                return Content(HttpStatusCode.OK, usu);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, "Usuario no encontrado");
            }
        }
        public IHttpActionResult Usuario(string parametro)
        {
            Usuario usu;
            if (parametro.Contains('@') && parametro.Contains('.'))
            {
                usu = UsuarioBLL.GetUsuarioByMail(parametro);          
            }
            else
            {
                usu = UsuarioBLL.GetUsuarioByUsu(parametro);
            }
            if (usu != null)
            {
                return Content(HttpStatusCode.OK, true);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, "Usuario no encontrado");
            }
        }
    }
}
