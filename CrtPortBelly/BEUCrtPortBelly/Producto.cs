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
    
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            this.ProductoEnCarrito = new HashSet<ProductoEnCarrito>();
        }
    
        public int prd_id { get; set; }
        public int cat_id { get; set; }
        public int prm_id { get; set; }
        public string prd_nom { get; set; }
        public string prd_img { get; set; }
        public string prd_tal { get; set; }
        public string prd_crt { get; set; }
        public int prd_cnt { get; set; }
        public decimal prd_prc { get; set; }
        public System.DateTime prd_dateOfCreated { get; set; }
    
        public virtual Categoria Categoria { get; set; }
        public virtual Promocion Promocion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<ProductoEnCarrito> ProductoEnCarrito { get; set; }
    }
}
