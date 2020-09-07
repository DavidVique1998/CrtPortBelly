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

    }
}
