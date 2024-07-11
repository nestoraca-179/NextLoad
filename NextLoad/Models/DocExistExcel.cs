using System;

namespace NextLoad.Models
{
	public class DocExistExcel
	{
        public string doc_num { get; set; }
        public int reng_num { get; set; }
        public string co_art { get; set; }
        public string num_lote { get; set; }
		public int total_art { get; set; }
		public DateTime fec_elab { get; set; }
		public DateTime fec_venc { get; set; }
	}
}