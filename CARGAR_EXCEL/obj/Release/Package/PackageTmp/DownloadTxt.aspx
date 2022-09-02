<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownloadTxt.aspx.cs" Inherits="CARGAR_EXCEL.DownloadTxt" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
     <title>TDR | Complementos de Pago</title>
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
        table{
            border:hidden!important;
        }
        .mitabla {
            width :100%
        }
        body {
  height: 100%;
  
}

#page-content {
  flex: 1 0 auto;
}
        /*body {margin: 0; background: #181824; font-family: Arial; }
nav {
  position: fixed;
  width: 100%;
  max-width: 300px;
  bottom: 0; top: 0;
  display: block;
  min-height: 300px;
  height: 100%;
  color: #fff;
  opacity: 0.8;
  transition: all 300ms;
  -moz-transition: all 300ms;
  -webkit-transition: all 300ms;
}
nav .vertical-menu hr{opacity: 0.1; border-width: 0.5px;}
nav ul{width: 90%; padding-inline-start: 0; margin: 10px; height: calc(100% - 20px); }
nav .vertical-menu-logo{padding: 20px; font-size: 1.3em; position: relative}
nav .vertical-menu-logo .open-menu-btn{width: 30px; height: max-content; position: absolute; display: block; right: 20px; top: 0; bottom: 0; margin: auto; cursor: pointer;}
nav .vertical-menu-logo .open-menu-btn hr{margin: 5px 0}
nav li{list-style: none; padding: 10px 10px; cursor: pointer;}
nav li:hover{ color: rgba(75, 105, 176,1); }
nav li#user-info{position: absolute; bottom: 0; width: 80%;}
nav li#user-info > span{display: block; float: right; font-size: 0.9em; position: relative; opacity: 0.6;}
nav li#user-info > span:after{
  content: '';
  width: 12px;
  height: 12px;
  display: block;
  position: absolute;
  background: green;
  left: -20px; top: 0; bottom: 0;
  margin: auto;
  border-radius: 50%;
}
.content-wrapper{
  width: calc(100% - 300px);
  height: 100%;
  position: fixed;
  background: #fff;
  left: 300px;
  padding: 20px;
}
.closed-menu .content-wrapper{
  width: 100%;
  left: 50px;
}
.content-wrapper{
    transition: all 300ms;
}
.vertical-menu-wrapper .vertical-menu-logo div{transition: all 100ms;}
.closed-menu .vertical-menu-wrapper .vertical-menu-logo div{
  margin-left: -100px;
}
.vertical-menu-wrapper .vertical-menu-logo .open-menu-btn{transition: all 300ms;}
.closed-menu .vertical-menu-wrapper .vertical-menu-logo .open-menu-btn{
  left: 7px;
  right: 100%;
}

.closed-menu .vertical-menu-wrapper ul,.closed-menu .vertical-menu-wrapper hr{margin-left: -300px;}
.vertical-menu-wrapper ul, .vertical-menu-wrapper hr{transition: all 100ms;}*/
.content-wrapper{background: #ebebeb;}
.content{
  width: 90%;
  min-height: 90%;
  background: #fff;
  border-radius: 10px;
  padding: 30px;

}
#divLoading {
    -moz-animation: cssAnimation 0s ease-in 3s forwards;
    /* Firefox */
    -webkit-animation: cssAnimation 0s ease-in 3s forwards;
    /* Safari and Chrome */
    -o-animation: cssAnimation 0s ease-in 3s forwards;
    /* Opera */
    animation: cssAnimation 0s ease-in 3s forwards;
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
<body class="bg-muted">
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg navbar-dark bg-dark" style="background:rgba(0, 25, 61, 0.9) !important;">
  <a class="navbar-brand" href="#">
      <img src="images/logo.png" /></a>

  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
  </button>
        
  <div class="collapse navbar-collapse" id="navbarNav">
     <ul class="navbar-nav mr-auto">
         <li class="nav-item active">
        <asp:HyperLink ID="HyperLink3" CssClass="text-white" Style="text-decoration:none; padding-right: 20px;"  runat="server" NavegateUrl="Listado.aspx" NavigateUrl="~/Listado.aspx"><i class="fa fa-check-circle" aria-hidden="true"></i> Complementos de pago</asp:HyperLink>
      </li>
      <li class="nav-item">
        <asp:HyperLink ID="HyperLink1" CssClass="text-white" Style="text-decoration:none;padding-right: 20px;"  runat="server" NavegateUrl="CSinRfc.aspx" NavigateUrl="~/CSinRfc.aspx"> &nbsp;<i class="fa fa-times-circle" aria-hidden="true"></i> Complementos sin RFC</asp:HyperLink>
      </li>
          <li class="nav-item">
        <asp:HyperLink ID="HyperLink4" CssClass="text-white" Style="text-decoration:none;"  runat="server" NavegateUrl="DownloadTxt.aspx" NavigateUrl="~/DownloadTxt.aspx"> <b>&nbsp;<i class="fa fa-arrow-circle-down" style="color:#f2c43e" aria-hidden="true"></i> Descargas</b></asp:HyperLink>
      </li>
    </ul>
    <%--<ul class="navbar-nav mr-auto ml-auto">
       
      <li class="nav-item active m-auto">
        <a class="nav-link" href="#"><h3>Complementos de Pago</h3><span class="sr-only">(current)</span></a>
      </li>
    </ul>--%>
      <ul class="navbar-nav">
            
            <li class="nav-item active text-white">
                <asp:HyperLink ID="HyperLink2" CssClass="btn btn-outline-warning text-white"  runat="server" NavegateUrl="Inicial.aspx" NavigateUrl="~/Inicial.aspx"><b><i class="fa fa-chevron-circle-left" aria-hidden="true"></i> Regresar</b></asp:HyperLink>
              
            </li>
          </ul>
     
      
     
  </div>
             
</nav>
        <div class="container-fluid mt-4">
             <div class="card" style="box-shadow: 1px 1px 82px -2px rgba(0,0,0,0.75);-webkit-box-shadow: 1px 1px 82px -2px rgba(0,0,0,0.75);-moz-box-shadow: 1px 1px 82px -2px rgba(0,0,0,0.75);">
                  <div class="card-header">
                    <b>Descargar TXT de Complementos de Pago</b> 
                  </div>
                  <div class="card-body">
                    <div class="row">
                        <%--<div class="col-sm-12">
                            <figure class="figure">
                              <img src="https://media-exp1.licdn.com/dms/image/C4E1BAQGA1cWuVr4JTw/company-background_10000/0/1612830472883?e=2147483647&v=beta&t=nYmnTbV2bKdoFsLYrmN-3SjNtlA7rH96uyBEnN7VY8M" class="figure-img img-fluid rounded" alt="A generic square placeholder image with rounded corners in a figure.">
                              <figcaption class="figure-caption text-right">A caption for the above image.</figcaption>
                            </figure>
                        </div>--%>
                        
                        <div class="col-sm-12" style="height:80vh; overflow-y:auto">
                            <div class="form-row">
                                <%--<div class="form-group col-sm-10">
                                  <label for="txtName">Numero de orden</label>
                                    <asp:TextBox ID="txtName" CssClass="form-control" runat="server" Width="140" />
                                    
                                </div>--%>
                                 <div class="form-group col-sm-12">
                                  <asp:GridView GridLines="None" ID="GridView1" runat="server" Border="0" CssClass="table table-striped" AutoGenerateColumns="false">
                                      <Columns>
                                          <asp:TemplateField  HeaderText="Folio">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%#Eval("Folio") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Archivo">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%#Eval("Archivo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descargar">
                                            <ItemTemplate>
                                                <a href="<%#Eval("Descargar") %>" class="btn btn-success" download="<%#Eval("Descargar") %>"><i class="fa fa-arrow-circle-down" aria-hidden="true"></i> Descargar</a>
                                              <%-- <asp:LinkButton CssClass="btn btn-success" ID="test" runat="server" CommandArgument='<%#Eval("Archivo")%>' OnClick="descargar">Descargar</asp:LinkButton> 
                                              --%>   
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                  </asp:GridView>

                                </div>
                              
                            </div>
                        </div>

                    </div>
                      <hr />
                  
                  </div>
                 
                </div>
            <div runat="server" id="divLoading" style="background-image:url(images/loading.gif);position:absolute;top:0;left:0;width:100%;height:100%;background-repeat:no-repeat;background-position:center;z-index:2000"></div>
             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnFiltrar" />
                
            </Triggers>
        </div>
    </form>
    
     <footer id="sticky-footer" class="flex-shrink-0 py-4 bg-dark text-white-50" style="position: relative;
    margin-top: 10vh;background:rgba(0, 25, 61, 0.9) !important;">
    <div class="container text-center text-white">
        <a href="#form1" style="font-size:28px;text-decoration:none;color:white"><i class="fa fa-arrow-circle-up" aria-hidden="true"></i></a><br />
      <small>2022 Copyright &copy; TDR Soluciones Logísticas</small>
    </div>
  </footer>
</body>
</html>
