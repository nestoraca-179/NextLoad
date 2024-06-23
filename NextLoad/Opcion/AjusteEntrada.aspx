<%@ Page Title="Ajuste de Entrada | NextLoad" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AjusteEntrada.aspx.cs" Inherits="NextLoad.Opcion.AjusteEntrada" %>

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
        <h2>Ajuste de Entrada</h2>
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
    </div>
    <hr />
    <div>
        <h3>Condiciones</h3>
        <ul style="color: #555; font-size: 14px; line-height: 25px;">
            <li>El articulo debe estar activo.</li>
            <li>El articulo debe manejar lote.</li>
            <li>El articulo debe manejar lotes con fecha de vencimiento.</li>
            <li>La fecha de elaboracion del lote no debe ser mayor igual a la fecha de vencimiento.</li>
            <li>Los numeros de lote no pueden repetirse.</li>
            <li>El numero de lote no debe estar registrado anteriormente en el sistema.</li>
            <li>Todos los items deben tener el mismo codigo de sucursal.</li>
            <li>Todos los items deben tener el mismo codigo de moneda.</li>
            <li>Todos los items deben tener el mismo valor de tasa de cambio.</li>
            <li>Los codigos de unidades de los articulos deben ser de las unidades principales.</li>
            <li>Todos los codigos de unidades por articulo deben ser iguales.</li>
            <li>Todos los valores de costo por articulo y por almacen deben ser iguales.</li>
        </ul>
    </div>
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