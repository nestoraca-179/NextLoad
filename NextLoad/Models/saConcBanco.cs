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
    
    public partial class saConcBanco
    {
        public string co_auto_con { get; set; }
        public int reng_num { get; set; }
        public string mov_num { get; set; }
        public System.DateTime fec_conc { get; set; }
        public bool con_auto { get; set; }
        public string co_us_in { get; set; }
        public string co_sucu_in { get; set; }
        public System.DateTime fe_us_in { get; set; }
        public string co_us_mo { get; set; }
        public string co_sucu_mo { get; set; }
        public System.DateTime fe_us_mo { get; set; }
        public string revisado { get; set; }
        public string trasnfe { get; set; }
        public System.Guid rowguid { get; set; }
    
        public virtual saConciliacionDetalle saConciliacionDetalle { get; set; }
        public virtual saMovimientoBanco saMovimientoBanco { get; set; }
    }
}
