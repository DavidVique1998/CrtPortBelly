using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUCrtPortBelly.Queris
{
    public class CabezaFacturaBLL
    {
        public static void Create(CabezaFactura a)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        db.CabezaFactura.Add(a);
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

        public static CabezaFactura Get(int? id)
        {
            PortBellyDBEntities db = new PortBellyDBEntities();
            return db.CabezaFactura.Find(id);
        }

        public static void Update(CabezaFactura CabezaFactura)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.CabezaFactura.Attach(CabezaFactura);
                        db.Entry(CabezaFactura).State = System.Data.Entity.EntityState.Modified;
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
                        CabezaFactura CabezaFactura = db.CabezaFactura.Find(id);
                        db.Entry(CabezaFactura).State = System.Data.Entity.EntityState.Deleted;
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

        public static List<CabezaFactura> List()
        {
            PortBellyDBEntities db = new PortBellyDBEntities();
            return db.CabezaFactura.Include(c => c.Cliente).ToList();
        }

        public static List<CabezaFactura> List(int cln_id)
        {
            PortBellyDBEntities db = new PortBellyDBEntities();
            return db.CabezaFactura.Where(x => x.cln_id.Equals(cln_id)).ToList();
        }

    }
}
