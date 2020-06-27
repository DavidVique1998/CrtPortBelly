using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUCrtPortBelly.Queris
{
    public class ClienteBLL
    {
        public static void Create(Cliente a)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        a.cln_tipo = "Nuevo";
                        a.cln_dateOfCreated = DateTime.Now;
                        db.Cliente.Add(a);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public static Cliente Get(int? id)
        {
            PortBellyDBEntities db = new PortBellyDBEntities();
            return db.Cliente.Find(id);
        }

        public static void Update(Cliente Cliente)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Cliente.Attach(Cliente);
                        db.Entry(Cliente).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
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
                        Cliente Cliente = db.Cliente.Find(id);
                        db.Entry(Cliente).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public static List<Cliente> List()
        {
            PortBellyDBEntities db = new PortBellyDBEntities();
            return db.Cliente.Include(c => c.Usuario).ToList();
        }

        public static List<Cliente> List(int uso_usu)
        {
            PortBellyDBEntities db = new PortBellyDBEntities();
            return db.Cliente.Where(x => x.uso_id.Equals(uso_usu)).ToList();
        }

        /*public static List<Cliente> ListToNames()
        {
            PortBellyDBEntities db = new PortBellyDBEntities();
            List<Cliente> result = new List<Cliente>();
            db.Cliente.ToList().ForEach(x =>
                result.Add(
                    new Cliente
                    {
                        nombre = x.nrc + "-" + x.nombre,
                        idCliente = x.idCliente
                    }));
            return result;
        }*/

    }
}
