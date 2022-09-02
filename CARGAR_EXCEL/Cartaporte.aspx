<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cartaporte.aspx.cs" Inherits="CARGAR_EXCEL.CartaPorte"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>TDR | Carta Porte</title>
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
     
    <ul class="navbar-nav mr-auto ml-auto">
       
      <li class="nav-item active m-auto">
        <a class="nav-link" href="#"><h3>Carta Porte</h3><span class="sr-only">(current)</span></a>
      </li>
    </ul>
   
     
      
     
  </div>
             
             
            </nav>
        
        <div runat="server" id="formularioT" class="container mt-4">
            <div class="row">
                <div class="col-md-12" >
                    <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="360000" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div style="box-shadow: 1px 1px 82px -2px rgba(0,0,0,0.75);-webkit-box-shadow: 1px 1px 82px -2px rgba(0,0,0,0.75);-moz-box-shadow: 1px 1px 82px -2px rgba(0,0,0,0.75);" class="card">
                                <div class="card-header">
                                      <div class="form-row">
                                
                                    <div class="form-group col-md-6">
                                        <label>Carta Porte - </label>
                                        <asp:Label Style="font-weight:700" ID="lblFact" runat="server" Text="Label"></asp:Label>
                                    </div>
                                   
                                </div>
                                </div>
                                <div class="card-body" style="height:70vh">
                                    <div class="form-row" style="display: flex;align-items: center;">
                                    
                                    <div class="form-group col-md-12" style="line-height: 200px;">
                                        <table class="table text-center">
                                            <tr>
                                                <td>
                                                    <h1>Descargar Carta Porte</h1>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Hyperlink ID="txtDesc" runat="server" CssClass="form-coontrol"><h1 style="color:green;margin-top:25%"><i class="fa fa-cloud-download" style="font-size:160px" aria-hidden="true"></i></h1></asp:Hyperlink>
                                                </td>
                                            </tr>
                                        </table>
                                      
                                    </div>
                                </div>
                                
                    
                               
                                </div>
                            </div>
                  
                          
                                
                     </ContentTemplate>
                        
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
