//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BEUCrtPortBelly
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductoEnCarrito
    {
        public int pcr_id { get; set; }
        public int car_id { get; set; }
        public int prd_id { get; set; }
        public string pcr_est { get; set; }
        public int pcr_cnt { get; set; }
        public System.DateTime pcr_dateOfCreated { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual Carrito Carrito { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
