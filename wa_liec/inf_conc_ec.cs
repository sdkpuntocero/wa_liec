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
    
    public partial class inf_conc_ec
    {
        public System.Guid id_conc_ec { get; set; }
        public Nullable<int> clave_ensa_a { get; set; }
        public string calve_ensa_b { get; set; }
        public Nullable<decimal> masa_a { get; set; }
        public Nullable<decimal> masa_b { get; set; }
        public Nullable<decimal> altura_a { get; set; }
        public Nullable<decimal> altura_b { get; set; }
        public Nullable<decimal> lados_a { get; set; }
        public Nullable<decimal> lados_b { get; set; }
        public Nullable<int> directo_a { get; set; }
        public Nullable<int> directo_b { get; set; }
        public string intensidad_a { get; set; }
        public string intensidad_b { get; set; }
        public Nullable<decimal> area_a { get; set; }
        public Nullable<decimal> area_b { get; set; }
        public Nullable<decimal> presion_a { get; set; }
        public Nullable<decimal> presion_b { get; set; }
        public Nullable<decimal> esfuerzo_a { get; set; }
        public Nullable<decimal> esfuerzo_b { get; set; }
        public Nullable<decimal> eprom_a { get; set; }
        public Nullable<decimal> e_prom_b { get; set; }
        public Nullable<decimal> masa_vol_a { get; set; }
        public Nullable<decimal> masa_vol_b { get; set; }
        public Nullable<decimal> masa_volprom_a { get; set; }
        public Nullable<decimal> masa_volprom_b { get; set; }
        public string tipofalla_a { get; set; }
        public string tipofalla_b { get; set; }
        public string dif_ab { get; set; }
        public Nullable<System.DateTime> fecha_registro { get; set; }
        public System.Guid id_mrp_concreto { get; set; }
    
        public virtual inf_mrp_concreto inf_mrp_concreto { get; set; }
    }
}