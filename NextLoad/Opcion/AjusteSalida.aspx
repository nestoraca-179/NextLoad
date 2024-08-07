﻿<%@ Page Title="Ajuste de Salida | NextLoad" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AjusteSalida.aspx.cs" Inherits="NextLoad.Opcion.AjusteSalida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script>
    function openModal() {
        $("#modal-waiting").modal("show");
    }
</script>
<form id="Form1" runat="server" class="container-fluid">
    <asp:Panel ID="PN_Success" runat="server" Width="100%" CssClass="mt-2" Visible="false">
        <div class="alert alert-success m-0">
            <dx:ASPxLabel ID="LBL_Success" runat="server" Width="100%" Font-Size="14px" CssClass="m-0"></dx:ASPxLabel>
        </div>
    </asp:Panel>
    <asp:Panel ID="PN_Error" runat="server" Width="100%" CssClass="mt-2" Visible="false">
        <div class="alert alert-danger m-0">
            <dx:ASPxLabel ID="LBL_Error" runat="server" Width="100%" Font-Size="14px" CssClass="m-0"></dx:ASPxLabel>
        </div>
    </asp:Panel>
    <div class="d-flex justify-content-between align-items-center mt-3">
        <h2>Ajuste de Salida <span id="SP_Exist" runat="server" visible="false" style="font-size: 20px;">(Doc. Existente)</span></h2>
        <div>
            <asp:LinkButton ID="BTN_DownloadTemplate" runat="server" CssClass="btn btn-primary m-0" OnClick="BTN_DownloadTemplate_Click">
                <i class="fas fa-download"></i> Descargar Plantilla
            </asp:LinkButton>
            <asp:LinkButton ID="BTN_DownloadExample" runat="server" CssClass="btn btn-secondary m-0" OnClick="BTN_DownloadExample_Click">
                <i class="fas fa-download"></i> Descargar Ejemplo
            </asp:LinkButton>
        </div>
    </div>
    <hr />
    <asp:FileUpload ID="FU_UploadFile" runat="server" CssClass="form-control" BorderWidth="0" accept=".xls, .xlsx" />
    <div class="w-100 d-flex">
        <asp:LinkButton ID="BTN_UploadFileExcel" runat="server" CssClass="btn btn-success disabled mt-3" OnClick="BTN_UploadFileExcel_Click" OnClientClick="openModal()">
            <i class="fas fa-file-excel"></i> Subir Archivo Excel
        </asp:LinkButton>
        <div class="d-flex mt-3 mx-3">
            <dx:ASPxCheckBox ID="CK_Exist" runat="server" AutoPostBack="true" Text="Cargar lotes a Doc. Existente" OnCheckedChanged="CK_Exist_CheckedChanged">
            </dx:ASPxCheckBox>
        </div>
    </div>
    <hr />
    <asp:Panel ID="PN_CondsNormal" runat="server" Visible="true" CssClass="row">
        <div class="col">
            <h3>Condiciones <span style="font-size: 16px;">- Sistema Profit Plus</span></h3>
            <ul style="color: #555; font-size: 14px; line-height: 25px;">
                <li>El Articulo debe existir.</li>
                <li>El Articulo debe estar activo.</li>
                <li>El Articulo debe manejar lotes.</li>
                <li>El Articulo debe manejar fecha de vencimiento.</li>
                <li>El Nro. de Lote YA debe estar registrado previamente.</li>
                <li>Los Codigos de Unidades de los Articulos deben ser de las Unidades Principales.</li>
                <li>El Articulo debe tener Stock Disponible.</li>
            </ul>
        </div>
        <div class="col">
            <h3>Condiciones <span style="font-size: 16px;">- Archivo Excel</span></h3>
            <ul style="color: #555; font-size: 14px; line-height: 25px;">
                <li>El Nro. de Lote no debe estar repetido entre las filas del archivo.</li>
                <li>Todas las filas deben tener el mismo Codigo de Sucursal.</li>
                <li>Todas las filas deben tener el mismo Codigo de Moneda.</li>
                <li>Todas las filas deben tener el mismo Valor de Tasa de Cambio.</li>
                <li>Todas las filas deben tener el mismo Codigo de Unidad por Articulo.</li>
                <li>Todas las filas deben tener el mismo Valor de Precio por Articulo/Almacen.</li>
            </ul>
        </div>
    </asp:Panel>
    <asp:Panel ID="PN_CondsExists" runat="server" Visible="false" CssClass="row">
        <div class="col">
            <h3>Condiciones (Documento Existente) <span style="font-size: 16px;">- Sistema Profit Plus</span></h3>
            <ul style="color: #555; font-size: 14px; line-height: 25px;">
                <li>El Articulo debe existir.</li>
                <li>El Articulo debe estar activo.</li>
                <li>El Articulo debe manejar lotes.</li>
                <li>El Articulo debe manejar fecha de vencimiento.</li>
                <li>El Nro. de Lote YA debe estar registrado previamente.</li>
                <li>El Nro. del Ajuste debe existir.</li>
                <li>El Nro. de Renglon debe existir.</li>
                <li>El Nro. de Renglon NO debe estar asignado previamente.</li>
                <li>Cada Renglon del Ajuste debe ser de tipo S01 (Salida).</li>
            </ul>
        </div>
        <div class="col">
            <h3>Condiciones (Documento Existente) <span style="font-size: 16px;">- Archivo Excel</span></h3>
            <ul style="color: #555; font-size: 14px; line-height: 25px;">
                <li>El Nro. de Lote no debe estar repetido entre las filas del archivo.</li>
                <li>Todas las filas deben tener el mismo Nro. de Ajuste.</li>
                <li>El Articulo debe ser unico por cada Nro. de Renglon.</li>
                <li>Los Codigos de Articulo en las filas del archivo y los renglones del documento en sistema deben coincidir.</li>
                <li>La Sumatoria de Cantidad por Nro. de Renglon no debe exceder la cantidad del renglon en sistema.</li>
                <li>La Sumatoria de Cantidad del Nro. de Lote no debe exceder stock actual del lote en sistema.</li>
            </ul>
        </div>
    </asp:Panel>
    <%-- MODAL WAITING --%>
    <div class="modal fade" id="modal-waiting" tabindex="-1" role="dialog" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-body py-5">
                    <i class="fas fa-spinner fa-spin"></i>
                    <h2>Procesando archivo...</h2>
                </div>
            </div>
        </div>
    </div>
</form>
<script>
    $("#FU_UploadFile").change(function () {
        $("#BTN_UploadFileExcel").removeClass("disabled");
    });
</script>
</asp:Content>