using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUCrtPortBelly.Queris
{
    public class InformeBLL
    {   
        // Get Productos en Carrito Por catergorias
        public static List<PestadosProductosPorCategoria_Result> GetProductosCarritoPorCategorias(string estado)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                return db.PestadosProductosPorCategoria(estado).ToList();
            }
        }
        public static List<PestadosProductosPorPromocion_Result> GetProductosCarritoPorPromociones(string estado)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                return db.PestadosProductosPorPromocion(estado).ToList();
            }
        }
        public static List<PventasPorMesesSegunCategoria_Result> GetProductoVendidosPorMesesPorCategoria()
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                return db.PventasPorMesesSegunCategoria().ToList();
            }
        }

        public static List<PproductosExistentesPorCategoria_Result> GetProductosExistentesPorCategoria()
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                return db.PproductosExistentesPorCategoria().ToList();
            }
        }

        public static List<PventasPorMesesSegunCategoriaEnAnio_Result> GetProductoVendidosPorMesesPorCategoriaEnAnio(int year)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                return db.PventasPorMesesSegunCategoriaEnAnio(year).ToList();
            }
        }

        public static List<PventPorMesSegCatEnAnio_Result> GetPrdVenPorMesPorCatEnAnio(int year)
        {
            using (PortBellyDBEntities db = new PortBellyDBEntities())
            {
                return db.PventPorMesSegCatEnAnio(year).ToList();
            }
        }



    }
}
