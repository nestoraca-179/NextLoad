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
    
    public partial class saCobroRetenIvaReng
    {
        public int reng_num { get; set; }
        public System.Guid rowguid_reng_cob { get; set; }
        public string rif_contribuyente { get; set; }
        public decimal periodo_impositivo { get; set; }
        public System.DateTime fecha_documento { get; set; }
        public string tipo_operacion { get; set; }
        public string tipo_documento { get; set; }
        public string rif_comprador { get; set; }
        public string numero_documento { get; set; }
        public string numero_control_documento { get; set; }
        public decimal monto_documento { get; set; }
        public decimal base_imponible { get; set; }
        public decimal monto_ret_imp { get; set; }
        public string numero_documento_afectado { get; set; }
        public string num_comprobante { get; set; }
        public decimal monto_excento { get; set; }
        public decimal alicuota { get; set; }
        public bool reten_tercero { get; set; }
        public string numero_expediente { get; set; }
        public string co_us_in { get; set; }
        public string co_sucu_in { get; set; }
        public System.DateTime fe_us_in { get; set; }
        public string co_us_mo { get; set; }
        public string co_sucu_mo { get; set; }
        public System.DateTime fe_us_mo { get; set; }
        public string revisado { get; set; }
        public string trasnfe { get; set; }
        public System.Guid rowguid { get; set; }
    }
}
