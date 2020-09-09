using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUCrtPortBelly.Queris
{

    public class PagoBLL
    {
        public static void Create(Pago a)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Pago.Add(a);
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

        public static Pago Get(int? id)
        {
            PortBellyDBEntities db = new PortBellyDBEntities();
            return db.Pago.Find(id);
        }

        public static void Update(Pago Pago)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Pago.Attach(Pago);
                        db.Entry(Pago).State = System.Data.Entity.EntityState.Modified;
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

        public static bool Updates(Pago p)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Pago pgo = new Pago();
                        var order = db.Pago.AsNoTracking().Where(s => s.pgo_id == p.pgo_id).FirstOrDefault();
                        pgo.pgo_id = order.pgo_id;
                        pgo.pgo_nom = p.pgo_nom;
                        pgo.pgo_cseg = p.pgo_cseg;
                        pgo.pgo_fven = p.pgo_fven;
                        pgo.pgo_ntg = p.pgo_ntg;
                        
                        db.Entry(pgo).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
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
                        Pago Pago = db.Pago.Find(id);
                        db.Entry(Pago).State = System.Data.Entity.EntityState.Deleted;
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

        public static List<Pago> List()
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                return db.Pago.Include(p => p.Cliente).ToList();
            }
        }

        public static List<Pago> List(int cln_id)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                return db.Pago.Where(x => x.cln_id.Equals(cln_id)).ToList();
            }
        }

        public static Pago GetUnicoPago(int cln_id)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                return db.Pago.FirstOrDefault(x => x.cln_id==cln_id);
            }
        }
    }
}
