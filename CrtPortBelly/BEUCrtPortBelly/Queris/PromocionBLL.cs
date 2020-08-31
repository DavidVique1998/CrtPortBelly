using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUCrtPortBelly.Queris
{
    public class PromocionBLL
    {
        public static void Create(Promocion a)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Promocion.Add(a);
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

        public static Promocion Get(int? id)
        {
            PortBellyDBEntities db = new PortBellyDBEntities();
            return db.Promocion.Find(id);
        }

        public static void Update(Promocion Promocion)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Promocion.Attach(Promocion);
                        db.Entry(Promocion).State = System.Data.Entity.EntityState.Modified;
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
        public static bool Updates(Promocion p)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Promocion prm = new Promocion();
                        var order = db.Promocion.AsNoTracking().Where(s => s.prm_id == p.prm_id).FirstOrDefault();
                        prm.prm_id = order.prm_id;
                        prm.prm_nom = p.prm_nom;
                        prm.prm_tip = p.prm_tip;
                        prm.prm_can = p.prm_can;
                        prm.prm_por = p.prm_por;
                        db.Entry(prm).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception )
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
                        Promocion alumno = db.Promocion.Find(id);
                        db.Entry(alumno).State = System.Data.Entity.EntityState.Deleted;
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

        public static List<Promocion> List()
        {
            PortBellyDBEntities db = new PortBellyDBEntities();

            return db.Promocion.ToList();
        }

    }
}
