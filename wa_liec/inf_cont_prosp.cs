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
    
    public partial class inf_cont_prosp
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public inf_cont_prosp()
        {
            this.inf_seg_prospecto = new HashSet<inf_seg_prospecto>();
        }
    
        public System.Guid id_cont_prosp { get; set; }
        public string dpto { get; set; }
        public string contacto { get; set; }
        public string tel1 { get; set; }
        public string tel2 { get; set; }
        public string email1 { get; set; }
        public string email2 { get; set; }
        public Nullable<System.DateTime> fecha_registro { get; set; }
        public System.Guid id_usuario { get; set; }
        public System.Guid id_prospecto { get; set; }
    
        public virtual inf_prospectos inf_prospectos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<inf_seg_prospecto> inf_seg_prospecto { get; set; }
    }
}
