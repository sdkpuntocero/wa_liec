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
    
    public partial class fact_tipo_rubro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public fact_tipo_rubro()
        {
            this.inf_rubro = new HashSet<inf_rubro>();
        }
    
        public int id_tipo_rubro { get; set; }
        public string tipo_rubro { get; set; }
        public string desc_tipo_rubro { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inf_rubro> inf_rubro { get; set; }
    }
}
