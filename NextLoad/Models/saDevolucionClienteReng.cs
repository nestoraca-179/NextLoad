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
    
    public partial class saDevolucionClienteReng
    {
        public int reng_num { get; set; }
        public string doc_num { get; set; }
        public string co_art { get; set; }
        public string des_art { get; set; }
        public string co_alma { get; set; }
        public decimal total_art { get; set; }
        public decimal stotal_art { get; set; }
        public string co_uni { get; set; }
        public string sco_uni { get; set; }
        public string co_precio { get; set; }
        public decimal prec_vta { get; set; }
        public Nullable<decimal> prec_vta_om { get; set; }
        public string porc_desc { get; set; }
        public decimal monto_desc { get; set; }
        public string tipo_imp { get; set; }
        public string tipo_imp2 { get; set; }
        public string tipo_imp3 { get; set; }
        public decimal porc_imp { get; set; }
        public decimal porc_imp2 { get; set; }
        public decimal porc_imp3 { get; set; }
        public decimal monto_imp { get; set; }
        public decimal monto_imp2 { get; set; }
        public decimal monto_imp3 { get; set; }
        public decimal reng_neto { get; set; }
        public decimal pendiente { get; set; }
        public decimal pendiente2 { get; set; }
        public string tipo_doc { get; set; }
        public string num_doc { get; set; }
        public Nullable<System.Guid> rowguid_doc { get; set; }
        public decimal total_dev { get; set; }
        public decimal monto_dev { get; set; }
        public decimal otros { get; set; }
        public string comentario { get; set; }
        public bool lote_asignado { get; set; }
        public string dis_cen { get; set; }
        public decimal monto_desc_glob { get; set; }
        public decimal monto_reca_glob { get; set; }
        public decimal otros1_glob { get; set; }
        public decimal otros2_glob { get; set; }
        public decimal otros3_glob { get; set; }
        public decimal monto_imp_afec_glob { get; set; }
        public decimal monto_imp2_afec_glob { get; set; }
        public decimal monto_imp3_afec_glob { get; set; }
        public string co_us_in { get; set; }
        public string co_sucu_in { get; set; }
        public System.DateTime fe_us_in { get; set; }
        public string co_us_mo { get; set; }
        public string co_sucu_mo { get; set; }
        public System.DateTime fe_us_mo { get; set; }
        public string revisado { get; set; }
        public string trasnfe { get; set; }
        public System.Guid rowguid { get; set; }
    
        public virtual saAlmacen saAlmacen { get; set; }
        public virtual saArtUnidad saArtUnidad { get; set; }
        public virtual saArtUnidad saArtUnidad1 { get; set; }
        public virtual saDevolucionCliente saDevolucionCliente { get; set; }
    }
}
