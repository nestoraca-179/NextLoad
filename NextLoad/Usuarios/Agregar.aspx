<%@ Page Title="Agregar Usuario" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Agregar.aspx.cs" Inherits="NextLoad.Usuarios.Agregar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:Panel ID="PN_Error" runat="server" Width="100%" CssClass="mt-2" Visible="false">
    <div class="alert alert-danger m-0">
        <dx:ASPxLabel ID="LBL_Error" runat="server" Width="100%" Font-Size="14px" CssClass="m-0"></dx:ASPxLabel>
    </div>
</asp:Panel>
<form id="Form1" runat="server" class="container">
    <asp:Panel ID="PN_ContainerForm" runat="server" CssClass="form-header">
        <div class="row my-5">
            <div class="col d-flex align-items-center">
                <asp:LinkButton ID="BTN_Volver" runat="server" CssClass="btn btn-primary" OnClick="BTN_Volver_Click" style="padding: 8px 15px;">
                    <i class="fas fa-arrow-left"></i> Regresar
                </asp:LinkButton>
                <dx:ASPxButton ID="BTN_Guardar" runat="server" CssClass="btn btn-success mx-2" Theme="Metropolis" Text="Guardar" ValidationGroup="Usuario" OnClick="BTN_Guardar_Click" style="text-transform: none;" />
            </div>
            <div class="col">
                <dx:ASPxLabel ID="LBL_IDUsuario" runat="server" Text="Agregar Usuario" Width="100%" Font-Size="24px" CssClass="title-screen text-center"></dx:ASPxLabel>
            </div>
            <div class="col"></div>
        </div>
        <div class="row">
            <div class="col">
                <div class="controls">
                    <label>Usuario</label>
                    <dx:ASPxTextBox ID="TB_NewUsername" runat="server" Theme="Material" Width="100%" CssClass="form-control" Paddings-Padding="2px" AutoCompleteType="None">
                        <ValidationSettings ValidationGroup="Usuario" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                            <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="col">
                <div class="controls">
                    <label>Nombre</label>
                    <dx:ASPxTextBox ID="TB_Descrip" runat="server" Theme="Material" Width="100%" CssClass="form-control" Paddings-Padding="2px" AutoCompleteType="None">
                        <ValidationSettings ValidationGroup="Usuario" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                            <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <div class="controls">
                    <label>Clave</label>
                    <dx:ASPxTextBox ID="TB_NewPassword" runat="server" Theme="Material" Width="100%" CssClass="form-control" Password="true" Paddings-Padding="2px" AutoCompleteType="None">
                        <ValidationSettings ValidationGroup="Usuario" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                            <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
            </div>
            <div class="col">
                <div class="controls">
                    <label>Email</label>
                    <dx:ASPxTextBox ID="TB_Email" runat="server" Theme="Material" Width="100%" CssClass="form-control" Paddings-Padding="2px" AutoCompleteType="None">
                        <ValidationSettings ValidationGroup="Usuario" ErrorText="" ValidateOnLeave="false" ErrorTextPosition="Bottom">
                            <RequiredField IsRequired="True" ErrorText="Campo Obligatorio" />
                            <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorText="Correo Inválido" />
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <div class="controls">
                    <div class="controls">
                        <dx:ASPxCheckBox ID="CK_Admin" runat="server" Theme="Material" Width="100%" Text="Admin"></dx:ASPxCheckBox>
                    </div>
                </div>
            </div>
            <div class="col d-flex align-items-center">
                <div class="controls">
                    <dx:ASPxCheckBox ID="CK_Activo" runat="server" Theme="Material" Width="100%" Text="Activo"></dx:ASPxCheckBox>
                </div>
            </div>
        </div>
    </asp:Panel>
</form>
<script>
    $(document).ready(function () {
        setTimeout(function () {
            $("input[type^=text], input[type^=password]").val("");
        }, 100);
    });
</script>
</asp:Content>