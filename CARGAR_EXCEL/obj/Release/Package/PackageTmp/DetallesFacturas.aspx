<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetallesFacturas.aspx.cs" Inherits="CARGAR_EXCEL.DetallesFacturas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" ></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
    
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
     <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/5.5.2/bootbox.min.js"></script>--%>
    <script type="text/javascript" src='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.js'> </script>
        <link rel="stylesheet" href='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css'
            media="screen" />
    <script src="https://kit.fontawesome.com/789a3ce2b4.js" crossorigin="anonymous"></script>
    <style>
        #divLoading {
    -moz-animation: cssAnimation 0s ease-in 5s forwards;
    /* Firefox */
    -webkit-animation: cssAnimation 0s ease-in 5s forwards;
    /* Safari and Chrome */
    -o-animation: cssAnimation 0s ease-in 5s forwards;
    /* Opera */
    animation: cssAnimation 0s ease-in 5s forwards;
    -webkit-animation-fill-mode: forwards;
    animation-fill-mode: forwards;
}
@keyframes cssAnimation {
    to {
        width:0;
        height:0;
        overflow:hidden;
    }
}
@-webkit-keyframes cssAnimation {
    to {
        width:0;
        height:0;
        visibility:hidden;
    }
}
    </style>
</head>
<body>
     
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
              <a class="navbar-brand" href="#">
                  <img src="images/logo.png" /></a>
              <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
              </button>
        
              <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav mr-auto ml-auto">
       
                  <li class="nav-item active m-auto">
                    <a class="nav-link" href="#"><h3>Complemento Pago</h3><span class="sr-only">(current)</span></a>
                  </li>
                </ul>
                  <ul class="navbar-nav">
                        <li class="nav-item active">
                          <a class="nav-link" href="#"><i style="color:green !important" class="fas fa-user"></i> <asp:Label ID="lblUserDetails" runat="server" Text=""></asp:Label> | </a>
                        </li>
                        <li class="nav-item active text-white">
                          <asp:Label Style="color:white" ID="Label1" runat="server" Text=""></asp:Label> Detalles de la factura
                        </li>
                      </ul>
     
      
     
              </div>
             
            </nav>
        
        <div runat="server" id="formularioT" class="container mt-4">
            <div class="row">
                <div class="col-md-12">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div class="card">
                                <div class="card-header">
                                      <div class="form-row">
                                <label>Complemento Pago - </label>
                                    <div class="form-group col-md-6">
                                        <asp:Label Style="font-weight:700" ID="lblFact" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </div>
                                </div>
                                <div class="card-body">
                                    <div class="form-row">
                                    <div class="form-group col-md-6">
                                      <label for="inputEmail4">Folio</label>
                                        <asp:TextBox ID="txtFolio" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                      <label for="inputPassword4">Fecha Factura</label>
                                      <asp:TextBox ID="txtFechaFactura" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                      <label for="inputCity">Id Cliente</label>
                                      <asp:TextBox ID="txtIdCliente"  runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                      <div class="form-group col-md-6">
                                      <label for="inputCity">Cliente</label>
                                      <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                                      <asp:Image ID="imgCliente" runat="server" ImageUrl="Img/alerta.png" Visible="False" ToolTip="No puede estar vacío" />
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label>Calle</label>
                                        <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                        <asp:Image ID="imgCalle" runat="server" ImageUrl="Img/alerta.png" Visible="False" />
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Colonia</label>
                                        <asp:TextBox ID="txtColonia" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                        <asp:Image ID="imgColonia" runat="server" ImageUrl="Img/alerta.png" Visible="False" ToolTip="No puede estar vacío" />
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label>No. Ext</label>
                                        <asp:TextBox ID="txtNoExt" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                         <asp:Image ID="imgNoExt" runat="server" ImageUrl="~/img/alerta.png" Visible="False" />
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>No. Int</label>
                                        <asp:TextBox ID="txtNoInt" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label>País</label>
                                        <asp:TextBox ID="txtPaís" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                        <asp:Image ID="imgPais" runat="server" ImageUrl="~/img/alerta.png" Visible="False" />
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Municipio</label>
                                        <asp:TextBox ID="txtMunicipio" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                        <asp:Image ID="imgMunicipio" runat="server" ImageUrl="~/img/alerta.png" Visible="False" />
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label>Localidad</label>
                                        <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Referencia</label>
                                        <asp:TextBox ID="txtReferencia" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label>Estado</label>
                                        <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                         <asp:Image ID="imgEstado" runat="server" ImageUrl="~/img/alerta.png" Visible="False" />
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>C.P</label>
                                        <asp:TextBox ID="txtCP" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                        <asp:Image ID="imgCP" runat="server" ImageUrl="Img/alerta.png" Visible="False" />
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <label>RFC</label>
                                        <asp:TextBox ID="txtRFC" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="form-group col">
                                        <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True" Visible="False"></asp:TextBox>&nbsp;
                                        <asp:Image ID="imgFDesde" runat="server" ImageUrl="~/img/alerta.png" Visible="False" />
                                    </div>
                                    <div class="form-group col">
                                        <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True" Visible="False"></asp:TextBox>&nbsp;
                                        <asp:Image ID="imgFHasta" runat="server" ImageUrl="~/img/alerta.png" Visible="False" />
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label>Concepto</label>
                                        <asp:TextBox ID="txtConcepto" runat="server" CssClass="form-control readOnlyTextBox" Height="68px" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Tipo de Cobro</label>
                                        <asp:TextBox ID="txtTipoCobro" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label>Pagado</label>
                                        <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Fecha Pago</label>
                                        <asp:TextBox ID="txtFechaPago" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label>Cuenta Pago</label>
                                        <asp:TextBox ID="txtCuentaPago" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Forma Pago</label>
                                        <asp:TextBox ID="txtFormaPago" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label>Banco Emisor</label>
                                        <asp:TextBox ID="txtBancoEmisor" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Moneda</label>
                                        <asp:TextBox ID="txtMoneda" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label>RFC Banco Emisor</label>
                                        <asp:TextBox ID="txtRFCbancoEmisor" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Unidad de Medida</label>
                                        <asp:TextBox ID="txtUnidadMedida" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label>Método Pago</label>
                                        <asp:TextBox ID="txtMetodoPago" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Cantidad</label>
                                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <label>Id Concepto SAT</label>
                                        <asp:TextBox ID="txtIdConcepto" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>UUID de facturas pagadas</label>
                                        <asp:TextBox ID="txtFechaIniOP" runat="server" CssClass="form-control readOnlyTextBox" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-row">
                                    <div class="form-group col-md-12">
                                        <label>Relación folio UUID</label>
                                        <asp:TextBox ID="FolioUUIDTxt" runat="server" ReadOnly="True" CssClass="form-control" Height="100px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                    
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <asp:Button ID="btnEdit" runat="server" CausesValidation="False" CssClass="btn btn-warning" OnClick="btnEdit_Click" Text="Editar" UseSubmitBehavior="False" />
                                         <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" OnClick="btnGuardar_Click" Text="Facturar CFDi" />
                                         
                                    </div>
                                </div>
                                </div>
                            </div>
                  
                          
                                
                     </ContentTemplate>
                        <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
            </Triggers>
                    </asp:UpdatePanel>
         
                </div>
            </div>
        </div>
        <div runat="server" id="Div1" class="container mt-4" style="min-height:150vh; background-color:white;">
            <div class="d-flex align-items-center justify-content-center vh-100">
            <div class="text-center">
                <h1 class="display-1 fw-bold">404</h1>
                <p class="fs-3"> <span class="text-danger">Opps!</span> Page not found.</p>
                <p class="lead">
                    The page you’re looking for doesn’t exist.
                  </p>
                
            </div>
        </div>
        </div>
         <div runat="server" id="divLoading" style="background-image:url(images/loading.gif);position:absolute;top:0;left:0;width:100%;height:100%;background-repeat:no-repeat;background-position:center;z-index:2000"></div>
    </form>
    <footer id="sticky-footer" class="flex-shrink-0 py-4 bg-dark text-white-50" style="position: relative;
    margin-top: 10vh;">
    <div class="container text-center text-white">
      <small>2022 Copyright &copy; TDR Soluciones Logísticas</small>
    </div>
  </footer>
</body>
</html>
