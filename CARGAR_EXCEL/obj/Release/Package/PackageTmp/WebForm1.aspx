<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CARGAR_EXCEL.WebForm1" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Carga Masiva</title>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" ></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
    <link rel="shortcut icon" href="images/icon.png" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
     <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/5.5.2/bootbox.min.js"></script>--%>
    <script type="text/javascript" src='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.js'> </script>
        <link rel="stylesheet" href='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css'
            media="screen" />
    <script src="https://kit.fontawesome.com/789a3ce2b4.js" crossorigin="anonymous"></script>
    <style>
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
    </style>
   
    
</head>
<body class="bg-muted">
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
        <a class="nav-link" href="#"><h3>Carga Masiva</h3><span class="sr-only">(current)</span></a>
      </li>
    </ul>
      <ul class="navbar-nav">
            <li class="nav-item active">
              <a class="nav-link" href="#"><i style="color:green !important" class="fas fa-user"></i> <asp:Label ID="lblUserDetails" runat="server" Text=""></asp:Label> | </a>
            </li>
            <li class="nav-item active">
              <asp:HyperLink ID="HyperLink1" CssClass="nav-link" runat="server" NavegateUrl="Salir.aspx" NavigateUrl="~/Salir.aspx">Cerrar Sesión</asp:HyperLink>
            </li>
          </ul>
     
      
     
  </div>
             
</nav>
    
         <div class="container-fluid mt-4">
                 <div class="card">
                  <div class="card-header">
                    Cargar Archivo
                  </div>
                  <div class="card-body">
                    <div class="row">
                        
                        <div class="col-sm-12">
                            <div class="form-row">
                                <%--<div class="form-group col-sm-10">
                                  <label for="txtName">Numero de orden</label>
                                    <asp:TextBox ID="txtName" CssClass="form-control" runat="server" Width="140" />
                                    
                                </div>--%>
                                <div class="form-group col-sm-10">
                                  <label for="FileUpload1">Archivo</label>
                                    <asp:FileUpload ID="FileUpload1" CssClass="form-control-file" runat="server" required="true"/>
                                </div>
                                <div class="form-group col-sm-2">
                                  <asp:Button ID="Button1" runat="server" Text="Cargar" CssClass="btn btn-block btn-success mt-4" OnClick="Button1_Click" />
                                    <asp:Button ID="Button2" runat="server" Text="Cargar" CssClass="btn btn-block btn-success mt-4" OnClick="Button2_Click" />
                                </div>
                                <div class="form-group col-sm-12">
                                    <asp:RequiredFieldValidator CssClass="alert alert-danger w-100" ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ErrorMessage="¡Debe seleccionar un archivo!" ControlToValidate="FileUpload1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                    </div>
                      <hr />
                   <div class="row">
                       <div class="col-sm-12">
                           <div class="card">
                          <div class="card-header p-1">
                            Registros Cargados
                          </div>
                          <div class="card-body">
                              <asp:GridView ID="GridView1" style="font-size:10px" runat="server" CssClass="table table-bordered mitabla table-hover"></asp:GridView>
                          </div>
                        </div>
                     </div>
                       

                   </div>
                  </div>
                </div>
        </div>

        
        <%--<div>
            
            <br />
            <br />
           
        </div>
        <br />
        <div>
            <asp:Label ID="lblrespuesta" runat="server"></asp:Label>
        </div>--%>
        
    </form>
  
    
   <footer id="sticky-footer" class="flex-shrink-0 py-4 bg-dark text-white-50" style="position: relative;
    margin-top: 100vh;">
    <div class="container text-center text-white">
      <small>2019 Copyright &copy; TDR Soluciones Logísiticas</small>
    </div>
  </footer>
</body>
</html>
