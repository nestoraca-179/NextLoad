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
    
    public partial class saConciliacionAutoReng
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public saConciliacionAutoReng()
        {
            this.saConciliacionDetalle = new HashSet<saConciliacionDetalle>();
        }
    
        public string co_auto_con { get; set; }
        public string cod_cta { get; set; }
        public int mesArchivo { get; set; }
        public int anoArchivo { get; set; }
        public System.DateTime fecImpor { get; set; }
        public string status { get; set; }
        public byte[] archivo { get; set; }
        public decimal saldoEc { get; set; }
        public int tamanoPaquete { get; set; }
        public int totalMov { get; set; }
        public int totalCon { get; set; }
        public int totalRep { get; set; }
        public string co_us_in { get; set; }
        public string co_sucu_in { get; set; }
        public System.DateTime fe_us_in { get; set; }
        public string co_us_mo { get; set; }
        public string co_sucu_mo { get; set; }
        public System.DateTime fe_us_mo { get; set; }
        public string revisado { get; set; }
        public string trasnfe { get; set; }
        public System.Guid rowguid { get; set; }
    
        public virtual saCuentaBancaria saCuentaBancaria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saConciliacionDetalle> saConciliacionDetalle { get; set; }
    }
}
