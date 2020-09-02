using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUCrtPortBelly.Queris
{
    public class UsuarioBLL
    {
        public static void Create(Usuario a)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        
                        db.Usuario.Add(a);
                        db.SaveChanges();
                        transaction.Commit();
                        Usuario u = GetUsuarioByMail(a.uso_cor);
                        if (u != null)
                        {
                            Cliente c = new Cliente();
                            c.uso_id = u.uso_id;
                            ClienteBLL.Create(c);
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }


        }

        public static Usuario Get(int? id)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                return db.Usuario.Find(id);
            }
        }

        public static void Update(Usuario usuario)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Usuario.Attach(usuario);
                        db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public static void Delete(int? id)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Usuario alumno = db.Usuario.Find(id);
                        db.Entry(alumno).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public static List<Usuario> List()
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                return db.Usuario.ToList();
            }
        }

        public static Usuario GetUsuarioByUsu(string usu)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities()) {
                return db.Usuario.FirstOrDefault(x => x.uso_usu == usu);
            }
        }

        public static Usuario GetUsuarioByMail(string cor)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                return db.Usuario.FirstOrDefault(x => x.uso_cor == cor);
            }
        }

        public static Usuario LoginByMail(string cor, string pass)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                Usuario usu= db.Usuario.FirstOrDefault(x => x.uso_cor == cor);
                if (usu != null)
                {
                    if (usu.uso_con == pass)
                    {
                        return usu;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        public static Usuario LoginByUsu(string usuario, string pass)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                Usuario usu = db.Usuario.FirstOrDefault(x => x.uso_usu == usuario);
                if (usu != null)
                {
                    if (usu.uso_con == pass)
                    {
                        return usu;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
