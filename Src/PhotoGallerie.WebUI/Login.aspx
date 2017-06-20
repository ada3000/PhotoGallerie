<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PhotoGalerie.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= Title + PhotoGalerie.Config.Title %></title>

    <link type="text/css" rel="stylesheet" href=<%= "\"Css/common.css?v="+PhotoGalerie.Config.Version+"\"" %> />
    <link type="text/css" rel="stylesheet" href=<%= "\"Css/toolbar.css?v="+PhotoGalerie.Config.Version+"\"" %> />
    <link type="text/css" rel="stylesheet" href=<%= "\"Css/folder.css?v="+PhotoGalerie.Config.Version+"\"" %> />
    <link type="text/css" rel="stylesheet" href=<%= "\"Css/galerie.css?v="+PhotoGalerie.Config.Version+"\"" %> />
        
    <script type="text/javascript" src="Js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="Js/jquery.event.move.js"></script>
    <script type="text/javascript" src="Js/jquery.event.swipe.js"></script>
    
    <script type="text/javascript" <%= "src=\"Js/gellerie.js?v="+PhotoGalerie.Config.Version+"\"" %>></script>
    <script type="text/javascript">
        var toolbarPathData = <%= ToolBarData%>;
    </script>
    <script type="text/javascript" <%= "src=\"Js/toolbar.js?v="+PhotoGalerie.Config.Version+"\"" %>></script>

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

    <div class="version">v<%= PhotoGalerie.Config.Version %></div>

</body>
</html>
