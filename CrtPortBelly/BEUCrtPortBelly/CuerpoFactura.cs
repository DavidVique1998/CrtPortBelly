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
    
    public partial class CuerpoFactura
    {
        public int crf_id { get; set; }
        public int cbf_id { get; set; }
        public int car_id { get; set; }
        public System.DateTime crf_dateOfCreated { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual CabezaFactura CabezaFactura { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual Carrito Carrito { get; set; }
    }
}
