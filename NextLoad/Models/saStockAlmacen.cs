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
    
    public partial class saStockAlmacen
    {
        public string co_alma { get; set; }
        public string co_art { get; set; }
        public string tipo { get; set; }
        public decimal stock { get; set; }
        public string revisado { get; set; }
        public string trasnfe { get; set; }
        public byte[] validador { get; set; }
    
        public virtual saAlmacen saAlmacen { get; set; }
        public virtual saArticulo saArticulo { get; set; }
    }
}
