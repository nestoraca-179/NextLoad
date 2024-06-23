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
    
    public partial class saFacturaCompra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public saFacturaCompra()
        {
            this.saDatosDeImportacion = new HashSet<saDatosDeImportacion>();
            this.saFacturaCompraReng = new HashSet<saFacturaCompraReng>();
        }
    
        public string doc_num { get; set; }
        public string nro_fact { get; set; }
        public string descrip { get; set; }
        public string co_prov { get; set; }
        public string co_mone { get; set; }
        public string co_cond { get; set; }
        public string porc_desc_glob { get; set; }
        public string porc_reca { get; set; }
        public string status { get; set; }
        public string n_control { get; set; }
        public System.DateTime fec_emis { get; set; }
        public System.DateTime fec_venc { get; set; }
        public System.DateTime fec_reg { get; set; }
        public decimal tasa { get; set; }
        public decimal saldo { get; set; }
        public decimal total_bruto { get; set; }
        public decimal total_neto { get; set; }
        public decimal monto_desc_glob { get; set; }
        public decimal monto_reca { get; set; }
        public decimal otros1 { get; set; }
        public decimal otros2 { get; set; }
        public decimal otros3 { get; set; }
        public decimal monto_imp { get; set; }
        public decimal monto_imp2 { get; set; }
        public decimal monto_imp3 { get; set; }
        public bool anulado { get; set; }
        public bool impresa { get; set; }
        public Nullable<int> seriales_e { get; set; }
        public string salestax { get; set; }
        public string dis_cen { get; set; }
        public Nullable<System.DateTime> feccom { get; set; }
        public Nullable<int> numcom { get; set; }
        public string dir_ent { get; set; }
        public string comentario { get; set; }
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
        public Nullable<bool> nac { get; set; }
        public string co_cta_ingr_egr { get; set; }
    
        public virtual saCondicionPago saCondicionPago { get; set; }
        public virtual saCuentaIngEgr saCuentaIngEgr { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saDatosDeImportacion> saDatosDeImportacion { get; set; }
        public virtual saMoneda saMoneda { get; set; }
        public virtual saProveedor saProveedor { get; set; }
        public virtual saTax saTax { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saFacturaCompraReng> saFacturaCompraReng { get; set; }
    }
}
