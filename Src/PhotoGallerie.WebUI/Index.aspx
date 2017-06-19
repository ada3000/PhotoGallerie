<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PhotoGalerie.IndexPage" %>

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
        <div class="download-folder js-download-folder" id="DownloadFolderButton" title="Download folder" runat="server"><div class="icon">&nbsp;</div></div>
    </div>

    <div class="version">v<%= PhotoGalerie.Config.Version %></div>

    <div class="photo-dialog js-photo-dialog" style="display: none;">
        <div class="overlay">&nbsp;</div>
        
        <div class="preview-image js-preview-image">
            &nbsp;
        </div>

        <div class="image-header">
            <div class="galerie-button download-button button js-download">&nbsp;</div>
            <div class="galerie-button image-info js-image-info">15/30</div>            
        </div>
        

        <div class="closer js-close"><span class="icon icon_pseudo icon_cross-small">
            <svg xmlns="http://www.w3.org/2000/svg" class="svg-icon" width="100%" height="100%" viewBox="0 0 14 14">
                <path fill="currentColor" d="M14.5,0.2l-0.7-0.7L7.5,5.799L1.2-0.5L0.5,0.2l6.3,6.3 l-6.3,6.299L1.2,13.5l6.3-6.3l6.3,6.3l0.7-0.701L8.2,6.5L14.5,0.2z"></path>
            </svg></span>
        </div>

        <div class="prev-button js-prev"><span>&nbsp;</span></div>
        <div class="next-button js-next"><span>&nbsp;</span></div>
    </div>
</body>
</html>
