using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUCrtPortBelly.Queris
{
    public class ProductoEnCarritoBLL
    {
        public static void Create(ProductoEnCarrito a)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        a.pcr_est = "Pendiente";
                        a.pcr_dateOfCreated = DateTime.Now;
                        db.ProductoEnCarrito.Add(a);
                        db.SaveChanges();
                        transaction.Commit();
                        ProductoBLL.Update(a.prd_id, a.pcr_cnt);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public void ActualizarPrducto(int prd_id, int prd_cnt)
        {
            Producto prd = ProductoBLL.Get(prd_id);
            prd.prd_cnt -= prd_cnt;
            ProductoBLL.Update(prd);
        }

        public static ProductoEnCarrito Get(int? id)
        {
            PortBellyDBEntities db = new PortBellyDBEntities();
            return db.ProductoEnCarrito.Find(id);
        }

        public static void Update(ProductoEnCarrito ProductoEnCarrito)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.ProductoEnCarrito.Attach(ProductoEnCarrito);
                        db.Entry(ProductoEnCarrito).State = System.Data.Entity.EntityState.Modified;
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
                        ProductoEnCarrito ProductoEnCarrito = db.ProductoEnCarrito.Find(id);
                        db.Entry(ProductoEnCarrito).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        transaction.Commit();
                        ProductoBLL.Update(ProductoEnCarrito.prd_id, (-1 * ProductoEnCarrito.pcr_cnt));
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public static List<ProductoEnCarrito> List()
        {
            PortBellyDBEntities db = new PortBellyDBEntities();
            return db.ProductoEnCarrito.Include(p => p.Carrito).Include(p => p.Producto).ToList();
        }

        public static List<ProductoEnCarrito> List(int car_id)
        {
            PortBellyDBEntities db = new PortBellyDBEntities();
            return db.ProductoEnCarrito.Where(x => x.car_id.Equals(car_id)).ToList();
        }

        public static List<ProductoEnCarrito> GetProdutsInCarByState(string pcr_est)
        {
            PortBellyDBEntities db = new PortBellyDBEntities();
            return db.ProductoEnCarrito.Where(x => x.pcr_est.Equals(pcr_est)).ToList();
        }

    }
}
