//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NextLoad.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class saResInventario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public saResInventario()
        {
            this.saResInventarioReng = new HashSet<saResInventarioReng>();
        }
    
        public string num_resinv { get; set; }
        public string des_resinv { get; set; }
        public System.DateTime fecha { get; set; }
        public string co_invfisico { get; set; }
        public string co_alma { get; set; }
        public string co_mone { get; set; }
        public decimal tasa { get; set; }
        public string campo1 { get; set; }
        public string campo2 { get; set; }
        public string campo3 { get; set; }
        public string campo4 { get; set; }
        public string campo5 { get; set; }
        public string campo6 { get; set; }
        public string campo7 { get; set; }
        public string campo8 { get; set; }
        public string co_us_in { get; set; }
        public string co_sucu_in { get; set; }
        public System.DateTime fe_us_in { get; set; }
        public string co_us_mo { get; set; }
        public string co_sucu_mo { get; set; }
        public System.DateTime fe_us_mo { get; set; }
        public string revisado { get; set; }
        public string trasnfe { get; set; }
        public byte[] validador { get; set; }
        public System.Guid rowguid { get; set; }
    
        public virtual saAlmacen saAlmacen { get; set; }
        public virtual saInventarioFisico saInventarioFisico { get; set; }
        public virtual saMoneda saMoneda { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saResInventarioReng> saResInventarioReng { get; set; }
    }
}
