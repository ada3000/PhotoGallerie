<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PhotoGalerie.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= Title + PhotoGalerie.Config.Title %></title>

    <link type="text/css" rel="stylesheet" href=<%= "\"Css/common.css?v="+PhotoGalerie.Config.Version+"\"" %> />
    <link type="text/css" rel="stylesheet" href=<%= "\"Css/toolbar.css?v="+PhotoGalerie.Config.Version+"\"" %> />
    <link type="text/css" rel="stylesheet" href=<%= "\"Css/folder.css?v="+PhotoGalerie.Config.Version+"\"" %> />
    <link type="text/css" rel="stylesheet" href=<%= "\"Css/galerie.css?v="+PhotoGalerie.Config.Version+"\"" %> />

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/font-awesome.css" rel="stylesheet" />
    <link href="Content/alertifyjs/alertify.min.css" rel="stylesheet" />

    <script type="text/javascript" src="Js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="Js/jquery.event.move.js"></script>
    <script type="text/javascript" src="Js/jquery.event.swipe.js"></script>

    <script type="text/javascript">
        var toolbarPathData = <%= ToolBarData%>;
    </script>
    <script type="text/javascript" <%= "src=\"Js/toolbar.js?v="+PhotoGalerie.Config.Version+"\"" %>></script>

    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/knockout-3.4.2.js"></script>
    <script src="Scripts/alertify.min.js"></script>
    <script src="Scripts/Models/LoginModel.js"></script>

    <style type="text/css">
        .login input
        {
            margin-bottom: 10px;
        }
    </style>
</head>
<body>

    <div class="content" id="form1" runat="server">
    </div>

    <div class="toolbar">
        <ul class="js-toolbar">
            <li class="home"><a href="/">&nbsp;</a></li>

            <!-- tets data -->
            <!--li class="item"><a href="/folder=">2006</a></li>
        <li class="item"><a href="/">(01) Январь 04 - Тест Др.</a></li>
        <li class="item last"><a href="#">Video</a></li-->

            <li class="item last"><a href="#">&nbsp;</a></li>
        </ul>
    </div>
    <div class="login">
        <div class="row">
            <div class="col-xs-12 col-lg-2 col-lg-offset-5">
                &nbsp;
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-lg-2 col-lg-offset-5">
                <input type="text" placeholder="login" data-bind="value: login" class="form-control" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-lg-2 col-lg-offset-5">
                <input type="password" placeholder="password" data-bind="value: pwd" class="form-control" />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-lg-2 col-lg-offset-5">
                <button type="button" data-bind="click: checkLoginPwd" class="btn btn-primary form-control">Login</button>
            </div>
        </div>
    </div>
    <div class="version">v<%= PhotoGalerie.Config.Version %></div>

</body>
</html>
