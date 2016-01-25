if (!window.console)
    window.console = { debug: function () { }, error: function () { } };

if (!window.console.debug)
    window.console.debug = function () { };

$(document).ready(function ()
{
    var Css =
        {
            dialog: ".js-photo-dialog",
            closeButton: ".js-close",
            prevButton: ".js-prev",
            nextButton: ".js-next",
            downloadButton: ".js-download",

            imageItem: ".js-image a",

            previewImage: ".js-preview-image",
            imageInfo: ".js-image-info",
        };

    function closeClick(ev)
    {
        $(Css.dialog).hide();
        console.debug("closeClick");
    };

    function nextClick(ev)
    {
        console.debug("nextClick");
        var idx = getCurrentImageIndex();
        
        if(idx< imageDialog.items.length-1)
            showImage(imageDialog.items[idx + 1]);
    };

    function prevClick(ev)
    {
        console.debug("prevClick");

        var idx = getCurrentImageIndex();

        if (idx > 0)
            showImage(imageDialog.items[idx - 1]);
    };

    function findImageItems(ev)
    {
        var items = [];
        $(Css.imageItem).each(function ()
        {
            items.push($(this).data("image-url"));
        });

        return items;
    };

    function downloadClick(ev)
    {
        location.href = imageDialog.downloadUrl;
        //window.open(imageDialog.downloadUrl);        
    };

    function imageItemClick(ev)
    {
        var downloadUrl = $(ev.currentTarget).attr("download-url");
        var imageUrl = $(ev.currentTarget).data("image-url");

        console.debug("imageItemClick " + imageUrl);


        showImage(imageUrl, downloadUrl);

        return false;
    };

    function showImage(url, downloadUrl)
    {
        $(Css.dialog).show();
        var img = $("<img src='" + url + "'/>");

        $(Css.previewImage, Css.dialog).empty().append(img);

        img.click(nextClick);

        imageDialog.current = url;
        imageDialog.downloadUrl = downloadUrl;

        updateImageInfo();
    };

    function getCurrentImageIndex()
    {
        for (var i = 0; i < imageDialog.items.length; i++)
            if (imageDialog.items[i] == imageDialog.current)
                return i;

        return -1;
    };

    function updateImageInfo()
    {
        $(Css.imageInfo).html((getCurrentImageIndex() + 1) + "/" + imageDialog.items.length);
    };

    $(Css.nextButton, Css.dialog).click(nextClick);
    $(Css.prevButton, Css.dialog).click(prevClick);
    $(Css.downloadButton, Css.dialog).click(downloadClick);
    $(Css.closeButton, Css.dialog).click(closeClick);

    $(Css.imageItem).click(imageItemClick);

    $(Css.imageItem).each(function()
    {
        var a = $(this);
        a.data("image-url", this.href);
        a.href = "javascript:void(0);";
    })

    window.imageDialog =
        {
            items: findImageItems(),
            current: "",
            downloadUrl:""
        };


})