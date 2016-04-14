$(document).ready(function ()
{
    var Css =
        {
            container: ".js-toolbar",
            item: ".item"
        };

    
    var container = $(Css.container);
    container.find(Css.item).remove();

    for(var i=0; i< toolbarPathData.length-1; i++)
    {
        var itemData = toolbarPathData[i];
        $("<li class=\"item\"><a href=\"/?folder=" + itemData.Id + "\">" + itemData.Title + "</a></li>").appendTo(container);
    }

    var currentFolderName = "&nbsp;";
    if (toolbarPathData.length > 0)
        currentFolderName = toolbarPathData[toolbarPathData.length - 1].Title;

    $("<li class=\"item last\"><a href=\"#\">" + currentFolderName + "</a></li>").appendTo(container);
})