using ExcelDataReader;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections.Generic;
using NextLoad.Models;
using System.Data.Entity;

namespace NextLoad.Controllers
{
    public class DataController : ProfitAdmManager
    {
        public bool IsExcel(HttpPostedFile file)
        {
            string ext = Path.GetExtension(file.FileName).ToLower();

            return ext == ".xls" || ext == ".xlsx";
        }

        // PROCESOS DE AJUSTE E/S
        public List<AjusteExcel> ProcessExcelAdjust(string path, bool output)
        {
            List<AjusteExcel> rows = new List<AjusteExcel>();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    bool firstRow = true;
                    while (reader.Read()) // RECORRIENDO FILAS
                    {
                        if (firstRow)
                        {
                            firstRow = false;
                        }
                        else
                        {
                            int r = reader.Depth + 1; // FILA ACTUAL DEL READER
                            for (int column = 0; column < reader.FieldCount - 1; column++) // SE EVALUA QUE NINGUNA CELDA ESTE EN BLANCO
                            {
                                string value = reader.GetValue(column) != null ? reader.GetValue(column).ToString() : "";
                                if (string.IsNullOrEmpty(value))
                                {
                                    char c = (char)('A' + column);
                                    throw new Exception(string.Format("CELDA EN BLANCO: {0}{1}", c, r));
                                }
                            }

                            AjusteExcel row = new AjusteExcel();
                            row.co_art = reader.GetValue(0).ToString();
                            row.co_alma = reader.GetValue(1).ToString();
                            row.co_uni = reader.GetValue(2).ToString();
                            row.co_sucur = reader.GetValue(3).ToString() != "N/A" ? reader.GetValue(3).ToString() : null;
                            row.co_mone = reader.GetValue(4).ToString();
                            row.tasa = decimal.Parse(reader.GetValue(5).ToString().Replace(".", ","));
                            row.cost_unit = decimal.Parse(reader.GetValue(6).ToString().Replace(".", ","));
                            row.num_lote = reader.GetValue(7).ToString();
                            row.total_art = int.Parse(reader.GetValue(8).ToString());
                            if (!output)
                            {
                                row.fec_elab = Convert.ToDateTime(reader.GetValue(9));
                                row.fec_venc = Convert.ToDateTime(reader.GetValue(10));
                            }

                            rows.Add(row);
                        }
                    }
                }
            }

            return rows;
        }
        
        public void ProcessDataAdjust(List<AjusteExcel> data, bool output)
        {
            var uniques = data.GroupBy(r => r.co_art).Select(g => g.First().co_art).ToList();

            if (!output)
            {
                foreach (AjusteExcel row in data)
                {
                    // ARTICULO CON FECHA DE ELABORACION MAYOR O IGUAL A LA FECHA DE VENCIMIENTO
                    if (row.fec_elab >= row.fec_venc)
                        throw new Exception(string.Format("Articulo {0}/Lote #{1} (fec_elab >= fec_venc)", row.co_art, row.num_lote));
                }
            }

            using (ProfitAdmEntities context = new ProfitAdmEntities(entity.ToString()))
            {
                foreach (string u in uniques)
                {
                    saArticulo art = context.saArticulo.AsNoTracking().SingleOrDefault(a => a.co_art.Trim() == u);
                    
                    if (art != null)
                    {
                        if (art.anulado) // ARTICULO INACTIVO
                            throw new Exception(string.Format("ARTICULO {0} INACTIVO", u));

                        if (!art.maneja_lote) // ARTICULO NO MANEJA LOTE
                            throw new Exception(string.Format("ARTICULO {0} NO MANEJA LOTE", u));

                        if (!art.maneja_lote_venc) // ARTICULO NO MANEJA FECHA DE VENCIMIENTO
                            throw new Exception(string.Format("ARTICULO {0} NO MANEJA LOTE CON FECHA DE VENCIMIENTO", u));

                        var num_lotes = data.Select(r => r.num_lote).ToList();
                        if (num_lotes.Distinct().Count() < num_lotes.Count) // ARTICULO CON NUMEROS DE LOTES REPETIDOS
                        {
                            string rep = num_lotes.GroupBy(r => r).Where(g => g.Count() > 1).Select(g => g.Key).First();
                            throw new Exception(string.Format("El numero de lote {0} se encuentra repetido para el Articulo {1}", rep, u));
                        }

                        // CODIGOS DE SUCURSALES DIFERENTES
                        var sucs = data.GroupBy(r => r.co_sucur).Select(g => g.First().co_sucur).ToList();
                        if (sucs.Count > 1)
                            throw new Exception("Todos los items deben tener el mismo codigo de sucursal");

                        // CODIGOS DE MONEDAS DIFERENTES
                        var mons = data.Where(r => r.co_art == u).GroupBy(r => r.co_mone).Select(g => g.First().co_mone).ToList();
                        if (mons.Count > 1)
                            throw new Exception("Todos los items deben tener el mismo codigo de moneda");

                        // VALORES DE TASAS DIFERENTES
                        var tasas = data.Where(r => r.co_art == u).GroupBy(r => r.tasa).Select(g => g.First().tasa).ToList();
                        if (tasas.Count > 1)
                            throw new Exception("Todos los items deben tener el mismo valor de tasa de cambio");

                        // ARTICULO CON CODIGOS DE UNIDADES DIFERENTES
                        var unds = data.Where(r => r.co_art == u).GroupBy(r => r.co_uni).Select(g => g.First().co_uni).ToList();
                        if (unds.Count > 1)
                            throw new Exception(string.Format("Articulo {0} con codigos de unidades diferentes", u));

                        // ARTICULO CON VALORES DE COSTOS DIFERENTES
                        var alms = data.Where(r => r.co_art == u).GroupBy(r => r.co_alma).Select(g => g.First().co_alma).ToList();
                        foreach (string a in alms)
                        {
                            var costs = data.Where(r => r.co_art == u && r.co_alma == a).GroupBy(r => r.cost_unit).Select(g => g.First().cost_unit).ToList();
                            if (costs.Count > 1)
                                throw new Exception(string.Format("Articulo {0} / Almacen {1} con valores de costos diferentes", u, a));
                        } 
                    }
                    else
                        throw new Exception(string.Format("ARTICULO {0} NO ENCONTRADO", u)); // ARTICULO NO EXISTE EN PROFIT
                }
            }
        }

		public string InsertDataAdjust(List<AjusteExcel> data, string user, bool output)
        {
            string num_aj = "";
            string sucur = data.Select(r => r.co_sucur).First();
            string mone = data.Select(r => r.co_mone).First();
            string tipo = output ? "S01" : "E01";
            decimal tasa = data.Select(r => r.tasa).First();

            var uniques = data.GroupBy(r => r.co_art).Select(g => g.First().co_art).ToList();
            using (ProfitAdmEntities context = new ProfitAdmEntities(entity.ToString()))
            {
                using (DbContextTransaction tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        int reng = 1;
                        bool uso_suc = false;

                        // SERIE USA SUCURSAL
                        var sp_1 = context.pSeleccionarUsoSucursalConsecutivoTipo("AJUS_NUM").GetEnumerator();
                        if (sp_1.MoveNext())
                            uso_suc = sp_1.Current.UsoSucursal;
                        sp_1.Dispose();

                        // NUMERO DE AJUSTE
                        if (string.IsNullOrEmpty(num_aj))
                            num_aj = GetNextConsec(context, uso_suc ? sucur : "", "AJUS_NUM");

                        // INGRESO DE AJUSTE
                        var sp_2 = context.pInsertarAjusteEntradaSalida(num_aj, mone, "AJUSTE", DateTime.Now, tasa, false, null, null, null, null, null, null,
                            null, null, null, null, null, null, user, sucur, "NEXTLOAD", null, null);
                        sp_2.Dispose();

                        Guid rowguid = context.saAjuste.Single(r => r.ajue_num == num_aj).rowguid;

                        foreach (string u in uniques)
                        {
                            var alms = data.Where(r => r.co_art == u).GroupBy(r => r.co_alma).Select(g => g.First().co_alma).ToList();
                            foreach (string a in alms)
                            {
                                List<AjusteExcel> rows = data.Where(r => r.co_art == u && r.co_alma == a).ToList();

                                int cantidad = rows.Select(r => r.total_art).Sum();
                                string unidad = rows.Select(r => r.co_uni).First();
                                decimal cost = rows.Select(r => r.cost_unit).First();

                                // EN CASO DE SALIDA VALIDA SI HAY STOCK DISPONIBLE
                                if (output)
                                {
                                    var sp_out = context.pConsultarStockArticulo(u, a, "ACT").GetEnumerator();
                                    if (sp_out.MoveNext())
                                    {
                                        decimal stock = sp_out.Current.stock;
                                        if (cantidad > stock)
                                            throw new Exception(string.Format("ARTICULO {0} / ALMACEN {1} / TOTAL SALIDA {2} - STOCK ACTUAL {3}", 
                                                u, a, cantidad, stock.ToString("N2")));
                                    }
                                    sp_out.Dispose();
                                }

                                // ACTUALIZAR STOCK
                                var sp_3 = context.pStockActualizar(a, u, unidad, cantidad, "ACT", !output, false);
                                sp_3.Dispose();

                                // INGRESO DE RENGLON AJUSTE
                                var sp_4 = context.pInsertarRenglonesAjusteEntradaSalida(num_aj, reng, tipo, u, a, unidad, null, null, cantidad, 0, cost, 0, 0, 0, 
                                    user, "NEXTLOAD", sucur, null, null);
                                sp_4.Dispose();

                                Guid rowguid_reng = context.saAjusteReng.Single(r => r.ajue_num == num_aj && r.reng_num == reng).rowguid;
                                reng++;

                                int r_lot = 1;
                                foreach (AjusteExcel row in rows)
                                {
                                    // INGRESO DE RENGLON LOTE AJUSTE
                                    saLoteEntrada lote = context.saLoteEntrada.SingleOrDefault(r => r.numero_lote == row.num_lote);

                                    if (output)
                                    {
                                        if (lote != null)
                                        {
                                            var sp_8 = context.pActualizarLote(lote.rowguid, lote.numero_lote, row.total_art, false, false);
                                            sp_8.Dispose();

                                            var sp_9 = context.pInsertarRenglonesLoteSalida(rowguid_reng, r_lot, "AJUS", u, a, lote.numero_lote, lote.rowguid,
                                                row.total_art, 1, user, "NEXTLOAD", sucur, null, null);
                                            sp_9.Dispose();
                                        }
                                        else
                                        {
                                            // LOTE NO ESTA REGISTRADO
                                            throw new Exception(string.Format("El lote #{0} para el articulo {1} no se encuentra registrado", row.num_lote, u));
                                        }
                                    }
                                    else
                                    {
                                        if (lote != null)
                                        {
                                            // LOTE YA ESTA REGISTRADO
                                            throw new Exception(string.Format("El lote #{0} para el articulo {1} ya se encuentra registrado", row.num_lote, u));
                                        }
                                        else
                                        {
                                            var sp_5 = context.pInsertarRenglonesLote(rowguid_reng, r_lot, "AJUS", u, a, row.num_lote, row.fec_elab, row.fec_venc,
                                                row.total_art, 1, user, "NEXTLOAD", sucur, null, null);
                                            sp_5.Dispose();
                                        }
                                    }
                                    
                                    r_lot++;
                                }

                                // ASIGNACION DE LOTE
                                int sp_6 = context.pActualizaCampoLoteAsignado("AJUS", true, rowguid_reng);
                            }
                        }

                        // ACTUALIZACION DE AJUSTE
                        var sp_7 = context.pActualizarEncabezadoAjusteEntradaSalida(rowguid, user, sucur, "NEXTLOAD", null);
                        sp_7.Dispose();

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
            }

            LogController.CreateLog(user, "AJUSTE " + (output ? "SALIDA" : "ENTRADA"), num_aj, "I", null);
            return num_aj.Trim();
        }

        // PROCESOS DE NOTAS (R/E)
        public List<NotaExcel> ProcessExcelNote(string path, bool output)
        {
            List<NotaExcel> rows = new List<NotaExcel>();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    bool firstRow = true;
                    while (reader.Read()) // RECORRIENDO FILAS
                    {
                        if (firstRow)
                        {
                            firstRow = false;
                        }
                        else
                        {
                            int r = reader.Depth + 1; // FILA ACTUAL DEL READER
                            for (int column = 0; column < reader.FieldCount - 1; column++) // SE EVALUA QUE NINGUNA CELDA ESTE EN BLANCO
                            {
                                string value = reader.GetValue(column) != null ? reader.GetValue(column).ToString() : "";
                                if (string.IsNullOrEmpty(value))
                                {
                                    char c = (char)('A' + column);
                                    throw new Exception(string.Format("CELDA EN BLANCO: {0}{1}", c, r));
                                }
                            }

                            NotaExcel row = new NotaExcel();
                            row.co_ent = reader.GetValue(0).ToString();
                            row.co_art = reader.GetValue(1).ToString();
                            row.co_alma = reader.GetValue(2).ToString();
                            row.co_uni = reader.GetValue(3).ToString();
                            row.co_sucur = reader.GetValue(4).ToString() != "N/A" ? reader.GetValue(4).ToString() : null;
                            row.co_mone = reader.GetValue(5).ToString();
                            row.tasa = decimal.Parse(reader.GetValue(6).ToString().Replace(".", ","));
                            row.val_unit = decimal.Parse(reader.GetValue(7).ToString().Replace(".", ","));
                            row.monto_imp = decimal.Parse(reader.GetValue(8).ToString().Replace(".", ","));
                            row.num_lote = reader.GetValue(9).ToString();
                            row.total_art = int.Parse(reader.GetValue(10).ToString());
                            if (!output)
                            {
                                row.nro_doc = reader.GetValue(11).ToString();
                                row.fec_elab = Convert.ToDateTime(reader.GetValue(12));
                                row.fec_venc = Convert.ToDateTime(reader.GetValue(13));
                            }

                            rows.Add(row);
                        }
                    }
                }
            }

            return rows;
		}

		public void ProcessDataNote(List<NotaExcel> data, bool output)
        {
            var uniques = data.GroupBy(r => r.co_art).Select(g => g.First().co_art).ToList();

            if (!output)
            {
                foreach (NotaExcel row in data)
                {
                    // ARTICULO CON FECHA DE ELABORACION MAYOR O IGUAL A LA FECHA DE VENCIMIENTO
                    if (row.fec_elab >= row.fec_venc)
                        throw new Exception(string.Format("Articulo {0}/Lote #{1} (fec_elab >= fec_venc)", row.co_art, row.num_lote));
                }
            }

            using (ProfitAdmEntities context = new ProfitAdmEntities(entity.ToString()))
            {
                foreach (string u in uniques)
                {
                    saArticulo art = context.saArticulo.AsNoTracking().SingleOrDefault(a => a.co_art.Trim() == u);

                    if (art != null)
                    {
                        if (art.anulado) // ARTICULO INACTIVO
                            throw new Exception(string.Format("ARTICULO {0} INACTIVO", u));

                        if (!art.maneja_lote) // ARTICULO NO MANEJA LOTE
                            throw new Exception(string.Format("ARTICULO {0} NO MANEJA LOTE", u));

                        if (!art.maneja_lote_venc) // ARTICULO NO MANEJA FECHA DE VENCIMIENTO
                            throw new Exception(string.Format("ARTICULO {0} NO MANEJA LOTE CON FECHA DE VENCIMIENTO", u));

                        var num_lotes = data.Select(r => r.num_lote).ToList();
                        if (num_lotes.Distinct().Count() < num_lotes.Count) // ARTICULO CON NUMEROS DE LOTES REPETIDOS
                        {
                            string rep = num_lotes.GroupBy(r => r).Where(g => g.Count() > 1).Select(g => g.Key).First();
                            throw new Exception(string.Format("El numero de lote {0} se encuentra repetido para el Articulo {1}", rep, u));
                        }

                        // CODIGOS DE ENTIDADES DIFERENTES
                        var ents = data.GroupBy(r => r.co_ent).Select(g => g.First().co_ent).ToList();
                        if (ents.Count > 1)
                            throw new Exception("Todos los items deben tener el mismo codigo de entidad");

                        string co_ent = ents[0];
                        if (output) // BUSCAR CLIENTE
                        {
                            saCliente cli = context.saCliente.AsNoTracking().SingleOrDefault(c => c.co_cli.Trim() == co_ent);
                            if (cli == null)
                                throw new Exception(string.Format("CLIENTE {0} NO ENCONTRADO", ents[0])); // CLIENTE NO EXISTE EN PROFIT
                        }
                        else // BUSCAR PROVEEDOR
                        {
                            saProveedor prov = context.saProveedor.AsNoTracking().SingleOrDefault(p => p.co_prov.Trim() == co_ent);
                            if (prov == null)
                                throw new Exception(string.Format("PROVEEDOR {0} NO ENCONTRADO", ents[0])); // PROVEEDOR NO EXISTE EN PROFIT
                        }

                        // CODIGOS DE SUCURSALES DIFERENTES
                        var sucs = data.GroupBy(r => r.co_sucur).Select(g => g.First().co_sucur).ToList();
                        if (sucs.Count > 1)
                            throw new Exception("Todos los items deben tener el mismo codigo de sucursal");

                        // CODIGOS DE MONEDAS DIFERENTES
                        var mons = data.Where(r => r.co_art == u).GroupBy(r => r.co_mone).Select(g => g.First().co_mone).ToList();
                        if (mons.Count > 1)
                            throw new Exception("Todos los items deben tener el mismo codigo de moneda");

                        // VALORES DE TASAS DIFERENTES
                        var tasas = data.Where(r => r.co_art == u).GroupBy(r => r.tasa).Select(g => g.First().tasa).ToList();
                        if (tasas.Count > 1)
                            throw new Exception("Todos los items deben tener el mismo valor de tasa de cambio");

                        // ARTICULO CON CODIGOS DE UNIDADES DIFERENTES
                        var unds = data.Where(r => r.co_art == u).GroupBy(r => r.co_uni).Select(g => g.First().co_uni).ToList();
                        if (unds.Count > 1)
                            throw new Exception(string.Format("Articulo {0} con codigos de unidades diferentes", u));

                        // ARTICULO CON VALORES DE COSTOS DIFERENTES O MONTOS DE IMPUESTOS DIFERENTES
                        var alms = data.Where(r => r.co_art == u).GroupBy(r => r.co_alma).Select(g => g.First().co_alma).ToList();
                        foreach (string a in alms)
                        {
                            var vals = data.Where(r => r.co_art == u && r.co_alma == a).GroupBy(r => r.val_unit).Select(g => g.First().val_unit).ToList();
                            if (vals.Count > 1)
                                throw new Exception(string.Format("Articulo {0} / Almacen {1} con valores de precios/costos diferentes", u, a));

                            var imps = data.Where(r => r.co_art == u && r.co_alma == a).GroupBy(r => r.monto_imp).Select(g => g.First().monto_imp).ToList();
                            if (imps.Count > 1)
                                throw new Exception(string.Format("Articulo {0} / Almacen {1} con montos de impuestos diferentes", u, a));
                        }

                        // NUMEROS DE DOCUMENTOS DIFERENTES
                        // BUSCAR NOTA EXISTENTE POR NRO DE DOCUMENTO
                        if (!output)
                        {
                            var docs = data.GroupBy(r => r.nro_doc).Select(g => g.First().nro_doc).ToList();
                            if (docs.Count > 1)
                                throw new Exception("Todos los items deben tener el mismo numero de documento");

                            string n_doc = docs[0];
                            saNotaRecepcionCompra nota = context.saNotaRecepcionCompra.AsNoTracking().SingleOrDefault(n => n.nro_fact.Trim() == n_doc);
                            if (nota != null)
                                throw new Exception(string.Format("YA EXISTE UNA NOTA DE RECEPCION CON NRO. DE DOCUMENTO {0}", docs[0]));
                        }
                    }
                    else
                        throw new Exception(string.Format("ARTICULO {0} NO ENCONTRADO", u)); // ARTICULO NO EXISTE EN PROFIT
                }
            }
        }

        public string InsertDataNote(List<NotaExcel> data, string user, bool output)
        {
            string num_note = "";
            string ent = data.Select(r => r.co_ent).First();
            string sucur = data.Select(r => r.co_sucur).First();
            string doc = data.Select(r => r.nro_doc).First();
            string mone = data.Select(r => r.co_mone).First();
            string tipo = output ? "S01" : "E01";
            string serie = output ? "NENT_VTA_N_CON" : "NREC_NUM";
            decimal tasa = data.Select(r => r.tasa).First();
            decimal total_bruto = data.Select(r => (r.val_unit * r.total_art)).Sum();
            decimal monto_imp = data.Select(r => (r.monto_imp * r.total_art)).Sum();
            decimal total_neto = total_bruto + monto_imp;

            var uniques = data.GroupBy(r => r.co_art).Select(g => g.First().co_art).ToList();
            using (ProfitAdmEntities context = new ProfitAdmEntities(entity.ToString()))
            {
                using (DbContextTransaction tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        int reng = 1;
                        bool uso_suc = false;
                        string co_tran = context.saTransporte.First().co_tran;
                        string co_ven = context.saVendedor.First().co_ven;

                        // SERIE USA SUCURSAL
                        var sp_1 = context.pSeleccionarUsoSucursalConsecutivoTipo(serie).GetEnumerator();
                        if (sp_1.MoveNext())
                            uso_suc = sp_1.Current.UsoSucursal;
                        sp_1.Dispose();

                        // NUMERO DE NOTA
                        if (string.IsNullOrEmpty(num_note))
                            num_note = GetNextConsec(context, uso_suc ? sucur : "", serie);

                        // INGRESO DE NOTA
                        Guid rowguid;
                        if (output)
                        {
                            string num_control_note = GetNextConsec(context, uso_suc ? sucur : "", "NENT_VTA_N_CON");

                            var sp_2 = context.pInsertarNotaEntregaVenta(num_note, "NOTA DE ENTREGA", ent, co_tran, mone, null, co_ven, "01", DateTime.Now, DateTime.Now, 
                                DateTime.Now, false, "0", tasa, num_control_note, null, null, 0, null, 0, total_neto, total_bruto, monto_imp, 0, 0, 0, 0, 0, total_neto, 
                                null, null, null, true, false, null, null, null, false, null, null, null, null, null, null, null, null, user, sucur, null, null, "NEXTLOAD");
                            sp_2.Dispose();

                            rowguid = context.saNotaEntregaVenta.Single(n => n.doc_num == num_note).rowguid;
                        }
                        else
                        {
                            var sp_2 = context.pInsertarNotaRecepcionCompra(num_note, doc, "NOTA DE RECEPCION", ent, null, mone, "01", null, null, DateTime.Now, DateTime.Now,
                                DateTime.Now, false, "0", tasa, null, total_neto, total_bruto, total_neto, 0, 0, 0, 0, 0, monto_imp, 0, 0, null, null, false, null, null,
                                null, null, null, null, null, null, null, null, null, null, user, sucur, "NEXTLOAD", false);
                            sp_2.Dispose();

                            rowguid = context.saNotaRecepcionCompra.Single(n => n.doc_num == num_note).rowguid;
                        }

                        foreach (string u in uniques)
                        {
                            var alms = data.Where(r => r.co_art == u).GroupBy(r => r.co_alma).Select(g => g.First().co_alma).ToList();
                            foreach (string a in alms)
                            {
                                List<NotaExcel> rows = data.Where(r => r.co_art == u && r.co_alma == a).ToList();

                                int cantidad = rows.Select(r => r.total_art).Sum();
                                string unidad = rows.Select(r => r.co_uni).First();
                                decimal val = rows.Select(r => r.val_unit).First();
                                decimal imp = rows.Select(r => r.monto_imp).First();
                                string tip_imp = imp > 0 ? "1" : "6";
                                decimal porc_imp = imp > 0 ? 16 : 0;
                                Guid rowguid_art = context.saArticulo.AsNoTracking().Single(art => art.co_art.Trim() == u).rowguid;

                                // EN CASO DE NOTA DE ENTREGA VALIDA SI HAY STOCK DISPONIBLE
                                // ADEMAS TAMBIEN EVALUA QUE EL PRECIO DE VENTA NO SEA MENOR QUE EL ULTIMO COSTO
                                if (output)
                                {
                                    var sp_out = context.pConsultarStockArticulo(u, a, "ACT").GetEnumerator();
                                    if (sp_out.MoveNext())
                                    {
                                        decimal stock = sp_out.Current.stock;
                                        if (cantidad > stock)
                                            throw new Exception(string.Format("ARTICULO {0} / ALMACEN {1} / TOTAL SALIDA {2} (STOCK ACTUAL {3})",
                                                u, a, cantidad, stock.ToString("N2")));
                                    }
                                    sp_out.Dispose();

                                    var sp_cost = context.pObtenerUltimoCosto(rowguid_art, a, null, DateTime.Now, null, null, null).GetEnumerator();
                                    if (sp_cost.MoveNext())
                                    {
                                        decimal cost = sp_cost.Current.costo.Value;
                                        if (cost > val)
                                            throw new Exception(string.Format("ARTICULO {0} / ALMACEN {1} / PRECIO VENTA {2} (ULTIMO COSTO {3})",
                                                u, a, val, cost.ToString("N2")));
                                    }
                                    sp_cost.Dispose();
                                }

                                // ACTUALIZAR STOCK
                                var sp_3 = context.pStockActualizar(a, u, unidad, cantidad, "ACT", !output, false);
                                sp_3.Dispose();

                                // INGRESO DE RENGLON AJUSTE
                                Guid rowguid_reng;
                                if (output)
                                {
                                    var sp_4 = context.pInsertarRenglonesNotaEntregaVenta(reng, num_note, u, null, unidad, null, a, "01", tip_imp, null, null, cantidad, 
                                        0, val, null, 0, val * cantidad, cantidad, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, porc_imp, 0, 0, imp * cantidad, 0, 0, 0, 
                                        0, 0, null, null, sucur, user, null, null, "NEXTLOAD");
                                    sp_4.Dispose();

                                    rowguid_reng = context.saNotaEntregaVentaReng.Single(r => r.doc_num == num_note && r.reng_num == reng).rowguid;
                                }
                                else
                                {
                                    var sp_4 = context.pInsertarRenglonesNotaRecepcionCompra(reng, num_note, u, null, unidad, null, a, tip_imp, null, null, null, null, 
                                        null, null, val * cantidad, val, Math.Round(val / tasa, 2), cantidad, 0, 0, porc_imp, 0, 0, imp * cantidad, 0, 0, 0, 0, 0, 0, 
                                        null, false, 0, 0, 0, 0, 0, 0, 0, 0, 0, cantidad, 0, null, sucur, user, null, null, "NEXTLOAD", 0, 0, 0);
                                    sp_4.Dispose();

                                    rowguid_reng = context.saNotaRecepcionCompraReng.Single(r => r.doc_num == num_note && r.reng_num == reng).rowguid;
                                }

                                reng++;

                                int r_lot = 1;
                                foreach (NotaExcel row in rows)
                                {
                                    // INGRESO DE RENGLON LOTE AJUSTE
                                    saLoteEntrada lote = context.saLoteEntrada.SingleOrDefault(r => r.numero_lote == row.num_lote);

                                    if (output)
                                    {
                                        if (lote != null)
                                        {
                                            var sp_8 = context.pActualizarLote(lote.rowguid, lote.numero_lote, row.total_art, false, false);
                                            sp_8.Dispose();

                                            var sp_9 = context.pInsertarRenglonesLoteSalida(rowguid_reng, r_lot, "NENT", u, a, lote.numero_lote, lote.rowguid,
                                                row.total_art, 1, user, "NEXTLOAD", sucur, null, null);
                                            sp_9.Dispose();
                                        }
                                        else
                                        {
                                            // LOTE NO ESTA REGISTRADO
                                            throw new Exception(string.Format("El lote #{0} para el articulo {1} no se encuentra registrado", row.num_lote, u));
                                        }
                                    }
                                    else
                                    {
                                        if (lote != null)
                                        {
                                            // LOTE YA ESTA REGISTRADO
                                            throw new Exception(string.Format("El lote #{0} para el articulo {1} ya se encuentra registrado", row.num_lote, u));
                                        }
                                        else
                                        {
                                            var sp_5 = context.pInsertarRenglonesLote(rowguid_reng, r_lot, "NREC", u, a, row.num_lote, row.fec_elab, row.fec_venc,
                                                row.total_art, 1, user, "NEXTLOAD", sucur, null, null);
                                            sp_5.Dispose();
                                        }
                                    }

                                    r_lot++;
                                }

                                // ASIGNACION DE LOTE
                                if (output)
                                {
                                    saNotaEntregaVentaReng ne_reng = context.saNotaEntregaVentaReng.AsNoTracking()
                                        .Single(r => r.rowguid == rowguid_reng);
                                    ne_reng.lote_asignado = true;

                                    var local = context.Set<saNotaEntregaVentaReng>().Local.FirstOrDefault(r => r.rowguid == rowguid_reng);
                                    if (local != null)
                                        context.Entry(local).State = EntityState.Detached;

                                    context.Entry(ne_reng).State = EntityState.Modified;
                                    context.SaveChanges();
                                }
                                else
                                {
                                    int sp_6 = context.pActualizaCampoLoteAsignado("NREC", true, rowguid_reng);
                                }
                            }
                        }

                        // ACTUALIZACION DE AJUSTE
                        if (output)
                        {
                            var sp_7 = context.pActualizarEncabezadoNotaEntregaVenta(num_note, user, sucur, "NEXTLOAD", null, rowguid);
                            sp_7.Dispose();
                        }
                        else
                        {
                            var sp_7 = context.pActualizarEncabezadoNotaRecepcionCompra(num_note, user, sucur, "NEXTLOAD", null, rowguid);
                            sp_7.Dispose();
                        }

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        throw ex;
                    }
                }
            }

            LogController.CreateLog(user, "NOTA DE " + (output ? "ENTREGA" : "RECEPCION"), num_note, "I", null);
            return num_note.Trim();
        }

		// PROCESOS DOCUMENTOS EXISTENTES
		public List<DocExistExcel> ProcessExcelDocExist(string path, bool output)
		{
			List<DocExistExcel> rows = new List<DocExistExcel>();

			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
			{
				using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
				{
					bool firstRow = true;
					while (reader.Read())
					{
						if (firstRow)
						{
							firstRow = false;
						}
						else
						{
							int r = reader.Depth + 1; // FILA ACTUAL DEL READER
							for (int column = 0; column < reader.FieldCount - 1; column++) // SE EVALUA QUE NINGUNA CELDA ESTE EN BLANCO
							{
								string value = reader.GetValue(column) != null ? reader.GetValue(column).ToString() : "";
								if (string.IsNullOrEmpty(value))
								{
									char c = (char)('A' + column);
									throw new Exception(string.Format("CELDA EN BLANCO: {0}{1}", c, r));
								}
							}

							DocExistExcel row = new DocExistExcel();
							row.doc_num = reader.GetValue(0).ToString();
							row.reng_num = int.Parse(reader.GetValue(1).ToString());
							row.co_art = reader.GetValue(2).ToString();
							row.num_lote = reader.GetValue(3).ToString();
							row.total_art = int.Parse(reader.GetValue(4).ToString());
							if (!output)
							{
								row.fec_elab = Convert.ToDateTime(reader.GetValue(5));
								row.fec_venc = Convert.ToDateTime(reader.GetValue(6));
							}

							rows.Add(row);
						}
					}
				}
			}

			return rows;
		}

		public void ProcessExistDataAdjust(List<DocExistExcel> data, bool output)
		{
			// AGRUPANDO NUMEROS DE RENGLON
			var uniques = data.GroupBy(r => r.reng_num).Select(g => g.First().reng_num).ToList();

			// VALIDANDO QUE EL NUMERO DE DOCUMENTO/AJUSTE SEA IGUAL PARA TODOS LOS RENGLONES
			var doc_nums = data.GroupBy(r => r.doc_num).Select(g => g.First().doc_num).ToList();
			if (doc_nums.Count > 1)
				throw new Exception("Solo se admite un unico Nro. de Ajuste por archivo");

			if (!output)
			{
				foreach (DocExistExcel row in data)
				{
					// ARTICULO CON FECHA DE ELABORACION MAYOR O IGUAL A LA FECHA DE VENCIMIENTO
					if (row.fec_elab >= row.fec_venc)
						throw new Exception(string.Format("Articulo {0}/Lote #{1} (fec_elab >= fec_venc)", row.co_art, row.num_lote));
				}
			}

			using (ProfitAdmEntities context = new ProfitAdmEntities(entity.ToString()))
			{
				// TIPO DE RENGLON
				string tipo = output ? "S01" : "E01";
                string d_tipo = output ? "(SALIDA)" : "(ENTRADA)";

				// BUSCANDO NUMERO DE AJUSTE EN PROFIT
				saAjuste aj = context.saAjuste.AsNoTracking().Include(a => a.saAjusteReng)
					.SingleOrDefault(a => a.ajue_num.Trim() == doc_nums.FirstOrDefault());

				if (aj == null) // AJUSTE NO EXISTE EN PROFIT
					throw new Exception(string.Format("NRO. DE AJUSTE {0} NO ENCONTRADO", doc_nums.FirstOrDefault()));

				foreach (int u in uniques)
				{
					if (!aj.saAjusteReng.Any(r => r.reng_num == u)) // RENGLON NO EXISTE EN PROFIT
						throw new Exception(string.Format("NO EXISTE RENGLON NRO {0} EN EL DOCUMENTO {1}", u, doc_nums.FirstOrDefault()));

					if (aj.saAjusteReng.Single(r => r.reng_num == u).co_tipo.Trim() != tipo) // RENGLON NO ES DE TIPO E01/S01
						throw new Exception(string.Format("EL RENGLON NRO {0} NO ES DE TIPO {1} {2}", u, tipo, d_tipo));

					if (aj.saAjusteReng.Single(r => r.reng_num == u).lote_asignado)
						throw new Exception(string.Format("Renglon {0} ya ha sido asignado previamente", u));

					// VALIDANDO QUE EL CODIGO DE ARTICULO SEA IGUAL PARA CADA RENGLON
					var co_arts = data.Where(r => r.reng_num == u).GroupBy(r => r.co_art).Select(g => g.First().co_art).ToList();
					if (co_arts.Count > 1)
						throw new Exception(string.Format("Renglon {0} con codigos de articulos distintos", u));

					// RENGLON DOCUMENTO NO COINCIDE CON RENGLON ARCHIVO
					if (aj.saAjusteReng.Single(r => r.reng_num == u).co_art.Trim() != co_arts.FirstOrDefault())
						throw new Exception(string.Format("ARTICULOS EN EL RENGLON NRO {0} NO COINCIDEN", u));

					// VERIFICANDO QUE EL TOTAL DEL RENGLON SEA IGUAL A LA SUMATORIA DE CANTIDAD ENTRE LOTES EN EL ARCHIVO
					decimal total_reng = aj.saAjusteReng.Single(r => r.reng_num == u).total_art;
					decimal total_cant = data.Where(r => r.reng_num == u).Select(r => r.total_art).Sum();
					if (total_reng != total_cant)
						throw new Exception(string.Format("TOTAL RENGLON: {0} / TOTAL CANTIDAD ARCHIVO: {1}", total_reng, total_cant));

					// BUSCANDO ARTICULO EN PROFIT
					saArticulo art = context.saArticulo.AsNoTracking().SingleOrDefault(a => a.co_art.Trim() == co_arts.FirstOrDefault());
					if (art == null)
						throw new Exception(string.Format("ARTICULO {0} NO ENCONTRADO", co_arts.FirstOrDefault())); // ARTICULO NO EXISTE EN PROFIT

					if (art.anulado) // ARTICULO INACTIVO
						throw new Exception(string.Format("ARTICULO {0} INACTIVO", co_arts.FirstOrDefault()));

					if (!art.maneja_lote) // ARTICULO NO MANEJA LOTE
						throw new Exception(string.Format("ARTICULO {0} NO MANEJA LOTE", co_arts.FirstOrDefault()));

					if (!art.maneja_lote_venc) // ARTICULO NO MANEJA FECHA DE VENCIMIENTO
						throw new Exception(string.Format("ARTICULO {0} NO MANEJA LOTE CON FECHA DE VENCIMIENTO", u));

					var num_lotes = data.Select(r => r.num_lote).ToList();
					if (num_lotes.Distinct().Count() < num_lotes.Count) // ARTICULO CON NUMEROS DE LOTES REPETIDOS
					{
						string rep = num_lotes.GroupBy(r => r).Where(g => g.Count() > 1).Select(g => g.Key).First();
						throw new Exception(string.Format("El numero de lote #{0} se encuentra repetido", rep));
					}
				}
			}
		}

		public void ProcessExistDataNote(List<DocExistExcel> data, bool output)
		{
			// AGRUPANDO NUMEROS DE RENGLON
			var uniques = data.GroupBy(r => r.reng_num).Select(g => g.First().reng_num).ToList();

			// VALIDANDO QUE EL NUMERO DE DOCUMENTO/AJUSTE SEA IGUAL PARA TODOS LOS RENGLONES
			var doc_nums = data.GroupBy(r => r.doc_num).Select(g => g.First().doc_num).ToList();
			if (doc_nums.Count > 1)
				throw new Exception(string.Format("Solo se admite un unico Nro. de {0} por archivo", output ? "N/E" : "N/R"));

			if (!output)
			{
				foreach (DocExistExcel row in data)
				{
					// ARTICULO CON FECHA DE ELABORACION MAYOR O IGUAL A LA FECHA DE VENCIMIENTO
					if (row.fec_elab >= row.fec_venc)
						throw new Exception(string.Format("Articulo {0}/Lote #{1} (fec_elab >= fec_venc)", row.co_art, row.num_lote));
				}
			}

			using (ProfitAdmEntities context = new ProfitAdmEntities(entity.ToString()))
			{
				foreach (int u in uniques)
				{
					// VALIDANDO QUE EL CODIGO DE ARTICULO SEA IGUAL PARA CADA RENGLON
					var co_arts = data.Where(r => r.reng_num == u).GroupBy(r => r.co_art).Select(g => g.First().co_art).ToList();
					if (co_arts.Count > 1)
						throw new Exception(string.Format("Renglon {0} con codigos de articulos distintos", u));

                    decimal total_reng;
					if (!output)
                    {
						// BUSCANDO NUMERO DE NOTA EN PROFIT
						saNotaRecepcionCompra note = context.saNotaRecepcionCompra.AsNoTracking().Include(a => a.saNotaRecepcionCompraReng)
							.SingleOrDefault(a => a.doc_num.Trim() == doc_nums.FirstOrDefault());

						if (note == null) // DOCUMENTO NO EXISTE EN PROFIT
							throw new Exception(string.Format("NRO. DE DOCUMENTO {0} NO ENCONTRADO", doc_nums.FirstOrDefault()));

						// RENGLON DOCUMENTO NO COINCIDE CON RENGLON ARCHIVO
						if (note.saNotaRecepcionCompraReng.Single(r => r.reng_num == u).co_art.Trim() != co_arts.FirstOrDefault())
							throw new Exception(string.Format("ARTICULOS EN EL RENGLON NRO {0} NO COINCIDEN", u));

						if (!note.saNotaRecepcionCompraReng.Any(r => r.reng_num == u)) // RENGLON NO EXISTE EN PROFIT
							throw new Exception(string.Format("NO EXISTE RENGLON NRO {0} EN EL DOCUMENTO {1}", u, doc_nums.FirstOrDefault()));

						if (note.saNotaRecepcionCompraReng.Single(r => r.reng_num == u).lote_asignado)
							throw new Exception(string.Format("Renglon {0} ya ha sido asignado previamente", u));

						total_reng = note.saNotaRecepcionCompraReng.Single(r => r.reng_num == u).total_art;
					}
                    else
                    {
						// BUSCANDO NUMERO DE NOTA EN PROFIT
						saNotaEntregaVenta note = context.saNotaEntregaVenta.AsNoTracking().Include(a => a.saNotaEntregaVentaReng)
							.SingleOrDefault(a => a.doc_num.Trim() == doc_nums.FirstOrDefault());

						if (note == null) // DOCUMENTO NO EXISTE EN PROFIT
							throw new Exception(string.Format("NRO. DE DOCUMENTO {0} NO ENCONTRADO", doc_nums.FirstOrDefault()));

						// RENGLON DOCUMENTO NO COINCIDE CON RENGLON ARCHIVO
						if (note.saNotaEntregaVentaReng.Single(r => r.reng_num == u).co_art.Trim() != co_arts.FirstOrDefault())
							throw new Exception(string.Format("ARTICULOS EN EL RENGLON NRO {0} NO COINCIDEN", u));

						if (!note.saNotaEntregaVentaReng.Any(r => r.reng_num == u)) // RENGLON NO EXISTE EN PROFIT
							throw new Exception(string.Format("NO EXISTE RENGLON NRO {0} EN EL DOCUMENTO {1}", u, doc_nums.FirstOrDefault()));

						if (note.saNotaEntregaVentaReng.Single(r => r.reng_num == u).lote_asignado)
							throw new Exception(string.Format("Renglon {0} ya ha sido asignado previamente", u));

						total_reng = note.saNotaEntregaVentaReng.Single(r => r.reng_num == u).total_art;
					}

					// VERIFICANDO QUE EL TOTAL DEL RENGLON SEA IGUAL A LA SUMATORIA DE CANTIDAD ENTRE LOTES EN EL ARCHIVO
					decimal total_cant = data.Where(r => r.reng_num == u).Select(r => r.total_art).Sum();
					if (total_reng != total_cant)
						throw new Exception(string.Format("TOTAL RENGLON: {0} / TOTAL CANTIDAD ARCHIVO: {1}", total_reng, total_cant));

					// BUSCANDO ARTICULO EN PROFIT
					saArticulo art = context.saArticulo.AsNoTracking().SingleOrDefault(a => a.co_art.Trim() == co_arts.FirstOrDefault());
					if (art == null)
						throw new Exception(string.Format("ARTICULO {0} NO ENCONTRADO", co_arts.FirstOrDefault())); // ARTICULO NO EXISTE EN PROFIT

					if (art.anulado) // ARTICULO INACTIVO
						throw new Exception(string.Format("ARTICULO {0} INACTIVO", co_arts.FirstOrDefault()));

					if (!art.maneja_lote) // ARTICULO NO MANEJA LOTE
						throw new Exception(string.Format("ARTICULO {0} NO MANEJA LOTE", co_arts.FirstOrDefault()));

					if (!art.maneja_lote_venc) // ARTICULO NO MANEJA FECHA DE VENCIMIENTO
						throw new Exception(string.Format("ARTICULO {0} NO MANEJA LOTE CON FECHA DE VENCIMIENTO", u));

					var num_lotes = data.Select(r => r.num_lote).ToList();
					if (num_lotes.Distinct().Count() < num_lotes.Count) // ARTICULO CON NUMEROS DE LOTES REPETIDOS
					{
						string rep = num_lotes.GroupBy(r => r).Where(g => g.Count() > 1).Select(g => g.Key).First();
						throw new Exception(string.Format("El numero de lote #{0} se encuentra repetido", rep));
					}
				}
			}
		}

		public string InsertExistDataAdjust(List<DocExistExcel> data, string user, bool output)
		{
			string doc_num = "";
			var uniques = data.GroupBy(r => r.reng_num).Select(g => g.First().reng_num).ToList();
			using (ProfitAdmEntities context = new ProfitAdmEntities(entity.ToString()))
			{
				using (DbContextTransaction tran = context.Database.BeginTransaction())
				{
					try
					{
						string sucur = context.saSucursal.First().co_sucur;
						doc_num = data.GroupBy(r => r.doc_num).Select(g => g.First().doc_num).ToList().First();
						saAjuste ajus = context.saAjuste.AsNoTracking().Include(a => a.saAjusteReng).Single(a => a.ajue_num == doc_num);

						foreach (int u in uniques)
						{
							int r_lot = 1;
							saAjusteReng reng = ajus.saAjusteReng.First(r => r.reng_num == u);
							List<DocExistExcel> rows = data.Where(r => r.reng_num == u).ToList();

							foreach (DocExistExcel row in rows)
							{
								// INGRESO DE RENGLON LOTE AJUSTE
								saLoteEntrada lote = context.saLoteEntrada.SingleOrDefault(r => r.numero_lote == row.num_lote);

								if (output)
								{
									if (lote != null)
									{
										if (lote.stock_actual < row.total_art)
											throw new Exception(string.Format("STOCK ACTUAL LOTE #{0}: {1} / CANTIDAD SOLICITADA: {2}",
												row.num_lote, lote.stock_actual, row.total_art));

										var sp_8 = context.pActualizarLote(lote.rowguid, lote.numero_lote, row.total_art, false, false);
										sp_8.Dispose();

										var sp_extract = context.pInsertarRenglonesLoteSalida(reng.rowguid, r_lot, "AJUS", reng.co_art,
											reng.co_alma, lote.numero_lote, lote.rowguid, row.total_art, 1, user, "NEXTLOAD", sucur, null, null);
										sp_extract.Dispose();
									}
									else
									{
										// LOTE NO ESTA REGISTRADO
										throw new Exception(string.Format("El lote #{0} para el articulo {1} no se encuentra registrado", row.num_lote, u));
									}
								}
								else
								{
									if (lote != null)
									{
										// LOTE YA ESTA REGISTRADO
										throw new Exception(string.Format("El lote #{0} para el articulo {1} ya se encuentra registrado", row.num_lote, u));
									}
									else
									{
										var sp_insert = context.pInsertarRenglonesLote(reng.rowguid, r_lot, "AJUS", reng.co_art, reng.co_alma,
											row.num_lote, row.fec_elab, row.fec_venc, row.total_art, 1, user, "NEXTLOAD", sucur, null, null);
										sp_insert.Dispose();
									}
								}

								r_lot++;
							}

							// ASIGNACION DE LOTE
							int sp_lot = context.pActualizaCampoLoteAsignado("AJUS", true, reng.rowguid);
						}

						// ACTUALIZACION DE AJUSTE
						var sp_aj = context.pActualizarEncabezadoAjusteEntradaSalida(ajus.rowguid, user, sucur, "NEXTLOAD", null);
						sp_aj.Dispose();

						tran.Commit();
					}
					catch (Exception ex)
					{
						tran.Rollback();
						throw ex;
					}
				}
			}

			LogController.CreateLog(user, "AJUSTE " + (output ? "SALIDA" : "ENTRADA"), doc_num, "M", "CARGA DE RENGLONES");
			return doc_num;
		}

        public string InsertExistDataNote(List<DocExistExcel> data, string user, bool output)
        {
			string doc_num = "";
			var uniques = data.GroupBy(r => r.reng_num).Select(g => g.First().reng_num).ToList();
			using (ProfitAdmEntities context = new ProfitAdmEntities(entity.ToString()))
			{
				using (DbContextTransaction tran = context.Database.BeginTransaction())
				{
					try
					{
						Guid rowguid = new Guid();
						string co_art = "", co_alma = "", sucur = context.saSucursal.First().co_sucur;
						doc_num = data.GroupBy(r => r.doc_num).Select(g => g.First().doc_num).ToList().First();

						List<DocExistExcel> rows;
						foreach (int u in uniques)
						{
							if (!output)
                            {
								saNotaRecepcionCompra note = context.saNotaRecepcionCompra.AsNoTracking()
                                    .Include(n => n.saNotaRecepcionCompraReng).Single(a => a.doc_num == doc_num);
								saNotaRecepcionCompraReng reng = note.saNotaRecepcionCompraReng.First(r => r.reng_num == u);
                                
                                co_art = reng.co_art;
                                co_alma = reng.co_alma;
                                rowguid = reng.rowguid;
							}
                            else
                            {
								saNotaEntregaVenta note = context.saNotaEntregaVenta.AsNoTracking()
									.Include(n => n.saNotaEntregaVentaReng).Single(a => a.doc_num == doc_num);
								saNotaEntregaVentaReng reng = note.saNotaEntregaVentaReng.First(r => r.reng_num == u);

								co_art = reng.co_art;
								co_alma = reng.co_alma;
								rowguid = reng.rowguid;
							}

							int r_lot = 1;
							rows = data.Where(r => r.reng_num == u).ToList();

							foreach (DocExistExcel row in rows)
							{
								// INGRESO DE RENGLON LOTE AJUSTE
								saLoteEntrada lote = context.saLoteEntrada.SingleOrDefault(r => r.numero_lote == row.num_lote);

								if (output)
								{
									if (lote != null)
									{
										if (lote.stock_actual < row.total_art)
											throw new Exception(string.Format("STOCK ACTUAL LOTE #{0}: {1} / CANTIDAD SOLICITADA: {2}",
												row.num_lote, lote.stock_actual, row.total_art));

										var sp_8 = context.pActualizarLote(lote.rowguid, lote.numero_lote, row.total_art, false, false);
										sp_8.Dispose();

                                        var sp_extract = context.pInsertarRenglonesLoteSalida(rowguid, r_lot, "NENT", co_art, co_alma, 
                                            lote.numero_lote, lote.rowguid, row.total_art, 1, user, "NEXTLOAD", sucur, null, null);
                                        sp_extract.Dispose();
                                    }
									else
									{
										// LOTE NO ESTA REGISTRADO
										throw new Exception(string.Format("El lote #{0} para el articulo {1} no se encuentra registrado", row.num_lote, u));
									}
								}
								else
								{
									if (lote != null)
									{
										// LOTE YA ESTA REGISTRADO
										throw new Exception(string.Format("El lote #{0} para el articulo {1} ya se encuentra registrado", row.num_lote, u));
									}
									else
									{
                                        var sp_insert = context.pInsertarRenglonesLote(rowguid, r_lot, "NREC", co_art, co_alma, row.num_lote, 
                                            row.fec_elab, row.fec_venc, row.total_art, 1, user, "NEXTLOAD", sucur, null, null);
                                        sp_insert.Dispose();
                                    }
								}

								r_lot++;
							}

							// ASIGNACION DE LOTE
							if (output)
							{
								saNotaEntregaVentaReng ne_reng = context.saNotaEntregaVentaReng.AsNoTracking().Single(r => r.rowguid == rowguid);
								ne_reng.lote_asignado = true;

								var local = context.Set<saNotaEntregaVentaReng>().Local.FirstOrDefault(r => r.rowguid == rowguid);
								if (local != null)
									context.Entry(local).State = EntityState.Detached;

								context.Entry(ne_reng).State = EntityState.Modified;
								context.SaveChanges();
							}
							else
							{
								int sp_6 = context.pActualizaCampoLoteAsignado("NREC", true, rowguid);
							}
						}

						// ACTUALIZACION DE AJUSTE
						if (output)
						{
							var sp_7 = context.pActualizarEncabezadoNotaEntregaVenta(doc_num, user, sucur, "NEXTLOAD", null, rowguid);
							sp_7.Dispose();
						}
						else
						{
							var sp_7 = context.pActualizarEncabezadoNotaRecepcionCompra(doc_num, user, sucur, "NEXTLOAD", null, rowguid);
							sp_7.Dispose();
						}

						tran.Commit();
					}
					catch (Exception ex)
					{
						tran.Rollback();
						throw ex;
					}
				}
			}

			LogController.CreateLog(user, "NOTA " + (output ? "DE ENTREGA" : "DE RECEPCION"), doc_num, "M", "CARGA DE RENGLONES");
			return doc_num;
		}

		// GENERALES
		public string GetNextConsec(ProfitAdmEntities context, string sucur, string serie)
        {
            string num = "";

            var sp = context.pConsecutivoProximo(sucur, serie).GetEnumerator();
            if (sp.MoveNext())
                num = sp.Current;
            sp.Dispose();

            return num;
        }
    }
}