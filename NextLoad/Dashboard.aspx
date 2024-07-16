<%@ Page Title="Dashboard | NextLoad" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="NextLoad.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .container-dash {
		height: 100%;
		display: flex;
		justify-content: center;
		align-items: center;
		position: relative;
	}
	.container-dash h2 {
		position: absolute;
		top: 0;
		left: 10px;
	}
</style>
<div class="container-fluid container-dash">
    <h2 class="m-2">Bienvenido!</h2>
	<img src="/images/profit.png" alt="profit" width="250" />
</div>
</asp:Content>