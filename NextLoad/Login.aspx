<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NextLoad.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login | NextLoad</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-gH2yIJqKdNHPEq0n4Mqa/HGKIhSkIHeL5AyhkYV8i59U5AR6csBvApHHNl/vI1Bx" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.2/css/all.min.css" integrity="sha512-1sCRPdkRXhBV2PBLUdRb4tMg1w2YPf37qatUFeS7zlBy7jJI8Lf4VHwWfZZfpXtYSLy85pkm9GaYVYMfw5BC1A==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link href="/Content/Site.css" rel="stylesheet" />
<style>
    section.container-fluid {
        width: 100%;
        height: 100vh;
        background: rgb(15,12,41) !important;
        background: linear-gradient(90deg, rgba(15,12,41,1) 0%, rgba(48,43,99,1) 100%) !important;
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .container { 
        width: 847px; 
        height: 100vh; /* Permalink - use to edit and share this gradient: https://colorzilla.com/gradient-editor/#0f0c29+0,302b63+100 */ 
        background: #0f0c29; /* Old browsers */ 
        background: -moz-linear-gradient(left, #0f0c29 0%, #302b63 100%); /* FF3.6-15 */ 
        background: -webkit-linear-gradient(left, #0f0c29 0%,#302b63 100%); /* Chrome10-25,Safari5.1-6 */ 
        background: linear-gradient(to right, #0f0c29 0%,#302b63 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */ 
    }
    form {
        width: 100%;
        height: 450px;
        max-width: 600px;
        min-height: auto;
        background: rgba(0, 0, 0, 0.5);
        padding: 60px 0 0;
        display: flex;
        flex-direction: column;
        justify-content: center;
        color: #f0f0f0;
    }
    .title-login {
        flex: 1;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }
    .title-login p {
        font-size: 14px;
    }
    .credentials-login {
        flex: 4;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }
    .form-group {
        width: 80%;
        display: flex;
        flex-wrap: wrap;
    }
    .form-group i {
        flex: 1;
        color: #f0f0f0;
        display: flex;
        justify-content: center;
        align-items: center;
        margin-right: 10px;
    }
    .form-group input[type=text], .form-group input[type=password] {
        flex: 10;
        background: none;
        color: #f0f0f0;
        border-bottom: solid 1px #333;
    }
    .form-group input::placeholder {
        color: #f0f0f0;
    }
    .form-group a {
        text-decoration: none;
    }
    .form-control-sm {
        margin: 5px 0;
        padding: 10px 0;
        border: 0;
        border-bottom: solid 1px #c1c1c1;
        border-radius: 0;
    }
    .form-label-error {
        width: 100%;
        font-size: 12px;
        color: #ca1d1d;
        display: none;
    }
    #LBL_Error.form-label-error {
        display: block;
    }
    .checks {
        width: 100%;
        margin: 15px 0 0 0;
        font-size: 14px;
        display: none;
        justify-content: space-between;
    }
</style>
</head>
<body>
    <section class="container-fluid">
        <form id="form1" runat="server">
            <div class="title-login">
                <h1 class="text-center">NextLoad</h1>
                <p class="text-center" style="font-size: 16px;">Sistema de Carga Masiva</p>
            </div>
            <div class="credentials-login">
                <div class="form-group">
                    <i class="fas fa-user"></i>
                    <asp:TextBox ID="TB_Username" runat="server" Width="100%" CssClass="form-control-sm" placeholder="Usuario" AutoCompleteType="None" ClientIDMode="Static" onkeypress="$('.form-label-error').css('display', 'none');"></asp:TextBox>
                    <asp:Label Text="* Debes ingresar usuario" runat="server" CssClass="form-label-error" />
                </div>
                <div class="form-group">
                    <i class="fas fa-lock"></i>
                    <asp:TextBox ID="TB_Password" runat="server" Width="100%" CssClass="form-control-sm" placeholder="Clave" AutoCompleteType="None" ClientIDMode="Static" onkeypress="$('.form-label-error').css('display', 'none');" MaxLength="15" TextMode="Password"></asp:TextBox>
                    <asp:Label Text="* Debes ingresar clave" runat="server" CssClass="form-label-error" />
                    <div class="checks">
                        <dx:ASPxCheckBox ID="CK_Recordar" runat="server" Text="Recordarme" Font-Size="12px" ForeColor="#f0f0f0"></dx:ASPxCheckBox>
                        <a href="#">Contraseña olvidada</a>
                    </div>
                </div>
                <asp:Button ID="BTN_Login" runat="server" Text="Iniciar Sesión" CssClass="btn btn-info mt-3" Width="80%" OnClick="BTN_Login_Click" OnClientClick="return checkForm()"></asp:Button>
                <asp:Label ID="LBL_Error" runat="server" Visible="false" CssClass="form-label-error mt-2 text-center" Width="80%" ClientIDMode="Static"></asp:Label>
            </div>
        </form>
    </section>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-A3rJD856KowSb7dwlZdYEkO39Gagi7vIsF0jrRAoQmDKKtQBHUuLZ9AsSv4jD4Xa" crossorigin="anonymous"></script>
    <script>
        function checkForm() {

            var send = true;

            if ($("#TB_Username").val() == "") {
                $(".form-label-error").first().css("display", "block");
                send = false;
            }

            if ($("#TB_Password").val() == "") {
                $(".form-label-error").last().css("display", "block");
                send = false;
            }

            return send;
        }
    </script>
</body>
</html>