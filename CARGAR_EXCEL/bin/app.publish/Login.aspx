<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CARGAR_EXCEL.Login" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CARGAR_EXCEL.WebForm1" %>--%>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" ></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
    <link rel="shortcut icon" href="images/icon.png" />
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
     <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/bootbox.js/5.5.2/bootbox.min.js"></script>--%>
    <script type="text/javascript" src='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.js'> </script>
        <link rel="stylesheet" href='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css'
            media="screen" />
    <style>
        body {
  background: linear-gradient(90deg, #4b6cb7 0%, #182848 100%);
}

.login {
  width: 360px;
  padding: 8% 0 0;
  margin: auto;
  font-family: 'Comfortaa', cursive;
}

.form {
  position: relative;
  z-index: 1;
  background: #FFFFFF;
  border-radius: 10px;
  max-width: 360px;
  margin: 0 auto 100px;
  padding: 45px;
  text-align: center;
}

.form input {
  outline: 0;
  background: #f2f2f2;
  width: 100%;
  border: 0;
  border-radius: 5px;
  margin: 0 0 15px;
  padding: 15px;
  box-sizing: border-box;
  font-size: 14px;
  font-family: 'Comfortaa', cursive;
}
.boton {
    background: #007bff !important;
}

.form input:focus {
  background: #dbdbdb;
}


.form span {
  font-size: 75px;
  color: #4b6cb7;
}
    </style>
</head>
<body>
    <form class="login-form" id="form1" runat="server">
        <div class="login">
  <div class="form">
    

      <%--<span class="material-icons">Usuario</span>--%>
        <div class="form-group">
            <img class="img-fluid" src="images/login.png" /><br />
        </div>
        <div class="form-group">
            
            <label for="exampleInputEmail1">Correo</label>
            <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server"></asp:TextBox>
    <%--<input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Enter email">--%>
    <%--<small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>--%>
       </div>
          <div class="form-group">
            <label for="exampleInputEmail1">Contraseña</label>
            <asp:TextBox TextMode="Password" ID="txtPassword" CssClass="form-control" runat="server"></asp:TextBox>
    <%--<input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Enter email">--%>
    <%--<small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>--%>
       </div>
 
       <%-- <label class="material-icons" for="txtUserName">Usuario</label>--%>
        
      <%--<input type="text" placeholder="email" required pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$" required/>
      <input type="password" placeholder="password" required />--%>
        <asp:Button ID="Button1" runat="server" Text="Ingresar" Class="btn btn-primary boton mt-4" OnClick="Button1_Click" />
      <div class="form-group">
            
            <asp:Label ID="lblErrorMessage" Text="Usuario y/o contraseña incorrecta" style="font-size: 15px !important;color: #dc3545 !important;" runat="server"></asp:Label>
    <%--<input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder="Enter email">--%>
    <%--<small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>--%>
       </div>
      <%--<button>login</button>--%>
   
  </div>
</div>
    </form>
        
    </body>
</html>
