<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PhotoGalerie.IndexPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ADA Photo galerie</title>    
    <link type="text/css" rel="stylesheet" <%= "href=\"styles.css?v="+PhotoGalerie.Config.Version+"\"" %> />
    <script type="text/javascript" src="Js/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" <%= "src=\"Js/gellerie.jsv="+PhotoGalerie.Config.Version+"\"" %> ></script>
</head>
<body>
    <div class="content" id="form1" runat="server">

    </div>

    <div class="toolbar">
    <ul>
        <li class="home"><a href="/">&nbsp;</a></li>
        <li><a href="/">2006</a></li>
        <li><a href="/">(01) Январь 04 - Тест Др.</a></li>
        <li class="last"><a href="#">Video</a></li>
    </ul>
    </div>

    <div class="version">v<%= PhotoGalerie.Config.Version %></div>

    <div class="photo-dialog js-photo-dialog" style="display: none;">
        <div class="overlay">&nbsp;</div>
        <div class="image-info js-image-info">15/30</div>
        <div class="preview-image js-preview-image">
            &nbsp;
        </div>

        <div class="close-button js-close">&nbsp;</div>
        <div class="prev-button js-prev">&nbsp;</div>
        <div class="next-button js-next">&nbsp;</div>

        <div class="download-button js-download">Download</div>
    </div>
</body>
</html>
