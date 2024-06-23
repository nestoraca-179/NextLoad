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
    
    public partial class saArtUnidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public saArtUnidad()
        {
            this.saAjusteReng = new HashSet<saAjusteReng>();
            this.saAjusteReng1 = new HashSet<saAjusteReng>();
            this.saArtCompuesto = new HashSet<saArtCompuesto>();
            this.saArtCompuestoGen = new HashSet<saArtCompuestoGen>();
            this.saArtCompuestoGenReng = new HashSet<saArtCompuestoGenReng>();
            this.saArtCompuestoReng = new HashSet<saArtCompuestoReng>();
            this.saArtIdentificadorReng = new HashSet<saArtIdentificadorReng>();
            this.saCotizacionClienteReng = new HashSet<saCotizacionClienteReng>();
            this.saCotizacionClienteReng1 = new HashSet<saCotizacionClienteReng>();
            this.saCotizacionProveedorReng = new HashSet<saCotizacionProveedorReng>();
            this.saCotizacionProveedorReng1 = new HashSet<saCotizacionProveedorReng>();
            this.saDevolucionClienteReng = new HashSet<saDevolucionClienteReng>();
            this.saDevolucionClienteReng1 = new HashSet<saDevolucionClienteReng>();
            this.saDevolucionProveedorReng = new HashSet<saDevolucionProveedorReng>();
            this.saDevolucionProveedorReng1 = new HashSet<saDevolucionProveedorReng>();
            this.saDocumentoCompraReng = new HashSet<saDocumentoCompraReng>();
            this.saDocumentoVentaReng = new HashSet<saDocumentoVentaReng>();
            this.saFacturaCompraReng = new HashSet<saFacturaCompraReng>();
            this.saFacturaCompraReng1 = new HashSet<saFacturaCompraReng>();
            this.saFacturaVentaReng = new HashSet<saFacturaVentaReng>();
            this.saFacturaVentaReng1 = new HashSet<saFacturaVentaReng>();
            this.saNotaDespachoVentaReng = new HashSet<saNotaDespachoVentaReng>();
            this.saNotaDespachoVentaReng1 = new HashSet<saNotaDespachoVentaReng>();
            this.saNotaEntregaVentaReng = new HashSet<saNotaEntregaVentaReng>();
            this.saNotaEntregaVentaReng1 = new HashSet<saNotaEntregaVentaReng>();
            this.saNotaRecepcionCompraReng = new HashSet<saNotaRecepcionCompraReng>();
            this.saNotaRecepcionCompraReng1 = new HashSet<saNotaRecepcionCompraReng>();
            this.saOrdenCompraReng = new HashSet<saOrdenCompraReng>();
            this.saOrdenCompraReng1 = new HashSet<saOrdenCompraReng>();
            this.saPedidoVentaReng = new HashSet<saPedidoVentaReng>();
            this.saPedidoVentaReng1 = new HashSet<saPedidoVentaReng>();
            this.saPlantillaCompraReng = new HashSet<saPlantillaCompraReng>();
            this.saPlantillaCompraReng1 = new HashSet<saPlantillaCompraReng>();
            this.saPlantillaVentaReng = new HashSet<saPlantillaVentaReng>();
            this.saPlantillaVentaReng1 = new HashSet<saPlantillaVentaReng>();
            this.saResInventarioReng = new HashSet<saResInventarioReng>();
            this.saResInventarioReng1 = new HashSet<saResInventarioReng>();
            this.saTrasladoReng = new HashSet<saTrasladoReng>();
            this.saTrasladoReng1 = new HashSet<saTrasladoReng>();
        }
    
        public string co_art { get; set; }
        public string co_uni { get; set; }
        public bool relacion { get; set; }
        public decimal equivalencia { get; set; }
        public bool uso_venta { get; set; }
        public bool uso_compra { get; set; }
        public bool uni_principal { get; set; }
        public bool uso_principal { get; set; }
        public bool uni_secundaria { get; set; }
        public bool uso_secundaria { get; set; }
        public bool uso_numDecimales { get; set; }
        public int num_decimales { get; set; }
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saAjusteReng> saAjusteReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saAjusteReng> saAjusteReng1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saArtCompuesto> saArtCompuesto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saArtCompuestoGen> saArtCompuestoGen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saArtCompuestoGenReng> saArtCompuestoGenReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saArtCompuestoReng> saArtCompuestoReng { get; set; }
        public virtual saArticulo saArticulo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saArtIdentificadorReng> saArtIdentificadorReng { get; set; }
        public virtual saUnidad saUnidad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saCotizacionClienteReng> saCotizacionClienteReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saCotizacionClienteReng> saCotizacionClienteReng1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saCotizacionProveedorReng> saCotizacionProveedorReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saCotizacionProveedorReng> saCotizacionProveedorReng1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saDevolucionClienteReng> saDevolucionClienteReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saDevolucionClienteReng> saDevolucionClienteReng1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saDevolucionProveedorReng> saDevolucionProveedorReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saDevolucionProveedorReng> saDevolucionProveedorReng1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saDocumentoCompraReng> saDocumentoCompraReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saDocumentoVentaReng> saDocumentoVentaReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saFacturaCompraReng> saFacturaCompraReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saFacturaCompraReng> saFacturaCompraReng1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saFacturaVentaReng> saFacturaVentaReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saFacturaVentaReng> saFacturaVentaReng1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saNotaDespachoVentaReng> saNotaDespachoVentaReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saNotaDespachoVentaReng> saNotaDespachoVentaReng1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saNotaEntregaVentaReng> saNotaEntregaVentaReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saNotaEntregaVentaReng> saNotaEntregaVentaReng1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saNotaRecepcionCompraReng> saNotaRecepcionCompraReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saNotaRecepcionCompraReng> saNotaRecepcionCompraReng1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saOrdenCompraReng> saOrdenCompraReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saOrdenCompraReng> saOrdenCompraReng1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saPedidoVentaReng> saPedidoVentaReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saPedidoVentaReng> saPedidoVentaReng1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saPlantillaCompraReng> saPlantillaCompraReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saPlantillaCompraReng> saPlantillaCompraReng1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saPlantillaVentaReng> saPlantillaVentaReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saPlantillaVentaReng> saPlantillaVentaReng1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saResInventarioReng> saResInventarioReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saResInventarioReng> saResInventarioReng1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saTrasladoReng> saTrasladoReng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<saTrasladoReng> saTrasladoReng1 { get; set; }
    }
}
