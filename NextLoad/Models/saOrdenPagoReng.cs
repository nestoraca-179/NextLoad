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
    
    public partial class saOrdenPagoReng
    {
        public int reng_num { get; set; }
        public string ord_num { get; set; }
        public string co_cta_ingr_egr { get; set; }
        public string co_islr { get; set; }
        public decimal monto_d { get; set; }
        public decimal monto_h { get; set; }
        public decimal monto_iva { get; set; }
        public decimal porc_retn { get; set; }
        public Nullable<decimal> monto_obj { get; set; }
        public decimal sustraendo { get; set; }
        public decimal monto_reten { get; set; }
        public string tipo_imp { get; set; }
        public string descrip { get; set; }
        public string dis_cen { get; set; }
        public string co_us_in { get; set; }
        public string co_sucu_in { get; set; }
        public System.DateTime fe_us_in { get; set; }
        public string co_us_mo { get; set; }
        public string co_sucu_mo { get; set; }
        public System.DateTime fe_us_mo { get; set; }
        public string revisado { get; set; }
        public string trasnfe { get; set; }
        public System.Guid rowguid { get; set; }
    
        public virtual saConISLR saConISLR { get; set; }
        public virtual saCuentaIngEgr saCuentaIngEgr { get; set; }
        public virtual saOrdenPago saOrdenPago { get; set; }
    }
}
