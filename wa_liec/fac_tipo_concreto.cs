//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace wa_liec
{
    using System;
    using System.Collections.Generic;
    
    public partial class fac_tipo_concreto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public fac_tipo_concreto()
        {
            this.inf_mrp_concreto = new HashSet<inf_mrp_concreto>();
        }
    
        public int id_tipo_concreto { get; set; }
        public string desc_tipo_concreto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inf_mrp_concreto> inf_mrp_concreto { get; set; }
    }
}