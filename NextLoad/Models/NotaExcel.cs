using System;

namespace NextLoad.Models
{
    public class NotaExcel
    {
        public string co_ent { get; set; }
        public string co_art { get; set; }
        public string co_alma { get; set; }
        public string co_uni { get; set; }
        public string co_sucur { get; set; }
        public string nro_doc { get; set; }
        public string co_mone { get; set; }
        public decimal tasa { get; set; }
        public decimal val_unit { get; set; }
        public decimal monto_imp { get; set; }
        public string num_lote { get; set; }
        public int total_art { get; set; }
        public DateTime fec_elab { get; set; }
        public DateTime fec_venc { get; set; }
    }
}