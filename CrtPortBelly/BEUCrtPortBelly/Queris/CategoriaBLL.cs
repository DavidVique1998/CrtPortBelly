using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUCrtPortBelly.Queris
{
    public class CategoriaBLL
    {
        public static void Create(Categoria a)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Categoria.Add(a);
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

        public static Categoria Get(int? id)
        {
            PortBellyDBEntities db = new PortBellyDBEntities();
            return db.Categoria.Find(id);
        }

        public static void Update(Categoria Categoria)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Categoria.Attach(Categoria);
                        db.Entry(Categoria).State = System.Data.Entity.EntityState.Modified;
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

         public static bool Updates(Categoria p)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Categoria cat = new Categoria();
                        var order = db.Categoria.AsNoTracking().Where(s => s.cat_id == p.cat_id).FirstOrDefault();
                        cat.cat_id = order.cat_id;
                        cat.cat_nom = p.cat_nom;
                        db.Entry(cat).State = System.Data.Entity.EntityState.Modified;
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
                        Categoria alumno = db.Categoria.Find(id);
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

        public static List<Categoria> List()
        {
            PortBellyDBEntities db = new PortBellyDBEntities();

            return db.Categoria.ToList();
        }

    }
}
