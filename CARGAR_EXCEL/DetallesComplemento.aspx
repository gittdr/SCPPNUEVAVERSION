<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetallesComplemento.aspx.cs" Inherits="CARGAR_EXCEL.DetallesComplemento"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>TDR | Complementos de Pagos V2.0</title>
    <link rel="shortcut icon" href="images/icono-tdr-soluciones-logisticas.ico" />
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
          html{
            scroll-behavior: smooth;
        }
        #divLoading {
    -moz-animation: cssAnimation 0s ease-in 2s forwards;
    /* Firefox */
    -webkit-animation: cssAnimation 0s ease-in 2s forwards;
    /* Safari and Chrome */
    -o-animation: cssAnimation 0s ease-in 2s forwards;
    /* Opera */
    animation: cssAnimation 0s ease-in 2s forwards;
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
        <nav class="navbar navbar-expand-lg navbar-dark bg-dark" style="background:rgba(0, 8, 20, 0.9) !important;">
              <a class="navbar-brand" href="#">
                  <img src="images/logo.png" /></a>
              <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
              </button>
        
              <div class="collapse navbar-collapse" id="navbarNav">
                  <<ul class="navbar-nav mr-auto">
         <li class="nav-item active">
        <asp:HyperLink ID="HyperLink3" CssClass="text-white" Style="text-decoration:none; padding-right: 20px;"  runat="server" NavegateUrl="Listado.aspx" NavigateUrl="~/Listado.aspx"><b><i class="fa fa-check-circle" style="color:#f2c43e" aria-hidden="true"></i> Complementos de pago </b></asp:HyperLink>
      </li>
      <li class="nav-item">
        <asp:HyperLink ID="HyperLink1" CssClass="text-white" Style="text-decoration:none;padding-right: 20px;"  runat="server" NavegateUrl="CSinRfc.aspx" NavigateUrl="~/CSinRfc.aspx"> &nbsp;<i class="fa fa-times-circle" aria-hidden="true"></i> Complementos sin RFC</asp:HyperLink>
      </li>
          <li class="nav-item">
        <asp:HyperLink ID="HyperLink4" CssClass="text-white" Style="text-decoration:none;padding-right: 20px;"  runat="server" NavegateUrl="DownloadTxt.aspx" NavigateUrl="~/DownloadTxt.aspx"> &nbsp;<i class="fa fa-arrow-circle-down" aria-hidden="true"></i> Descargas</asp:HyperLink>
      </li>
         <li class="nav-item">
        <asp:HyperLink ID="HyperLink5" CssClass="text-white" Style="text-decoration:none;"  runat="server" NavegateUrl="QFListado.aspx" NavigateUrl="~/QFListado.aspx"> &nbsp;<i class="fa fa-trash" aria-hidden="true"></i> Papelera</asp:HyperLink>
      </li>
    </ul>
                <%--<ul class="navbar-nav mr-auto ml-auto">
       
                  <li class="nav-item active m-auto">
                    <a class="nav-link" href="#"><h3>Complementos de Pago</h3><span class="sr-only">(current)</span></a>
                  </li>
                </ul>--%>
                  <ul class="navbar-nav">
                        <li class="nav-item active text-white">
                <asp:HyperLink ID="HyperLink2" CssClass="btn btn-outline-warning text-white"  runat="server" NavegateUrl="Listado.aspx" NavigateUrl="~/Listado.aspx"><b><i class="fa fa-chevron-circle-left" aria-hidden="true"></i> Regresar</b></asp:HyperLink>
              
            </li>
                      </ul>
     
      
     
              </div>
             
            </nav>
        
        <div runat="server" id="formularioT" class="container mt-4">
            <div class="row">
                <div class="col-md-12">
                    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="360000" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div style="box-shadow: 1px 1px 82px -2px rgba(0,0,0,0.75);-webkit-box-shadow: 1px 1px 82px -2px rgba(0,0,0,0.75);-moz-box-shadow: 1px 1px 82px -2px rgba(0,0,0,0.75);" class="card">
                                <div class="card-header">
                                      <div class="form-row">
                                
                                            <div class="form-group col-md-6">
                                                <label>Complemento de Pago - </label>
                                                <asp:Label Style="font-weight:700" ID="lblFact" runat="server" Text="Label"></asp:Label>
                                            </div>
                                            <div class="form-form-group col-md-6" style="text-align:right">
                                                <asp:Button ID="btnGenerarTxt" runat="server" CssClass="btn btn-primary" OnClick="btnGenerarTxt_Click" Text="Generar TXT" />
                                            </div>
                                    </div>
                                    <div class="form-row">
                                
                                            <div class="form-group col-md-12 bg-red alert alert-danger text-center" role="alert" runat="server" id="montos">
                                                <h1>¡Atención!</h1>
                                                <table class="table">
                                                    <tr>
                                                        <td>Monto Pagado</td>
                                                        <td>Suma de los UUID relacionados</td>
                                                        <td>Faltan</td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label Style="font-weight:700" ID="Mpagado" runat="server" Text="Label"></asp:Label></td>
                                                        <td><asp:Label Style="font-weight:700" ID="Suuid" runat="server" Text="Label"></asp:Label></td>
                                                        <td><asp:Label Style="font-weight:700" ID="Faltan" runat="server" Text="Label"></asp:Label></td>
                                                    </tr>
                                                </table>
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
    margin-top: 10vh;background:rgba(0, 8, 20, 0.9) !important;">
    <div class="container text-center text-white">
         <a href="#form1" style="font-size:28px;text-decoration:none;color:white"><i class="fa fa-arrow-circle-up" aria-hidden="true"></i></a><br />
      <small>2022 Copyright &copy; TDR Soluciones Logísticas</small>
    </div>
  </footer>
</body>
</html>
