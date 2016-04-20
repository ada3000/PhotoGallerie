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
        clearDialogContent();

        console.debug("closeClick");
    };

    function nextClick(ev)
    {
        console.debug("nextClick");
        var idx = getCurrentImageIndex();

        if (idx < imageDialog.items.length - 1)
            showImage(imageDialog.items[idx + 1].url, imageDialog.items[idx + 1].downloadUrl);
    };

    function prevClick(ev)
    {
        console.debug("prevClick");

        var idx = getCurrentImageIndex();

        if (idx > 0)
            showImage(imageDialog.items[idx - 1].url, imageDialog.items[idx - 1].downloadUrl);
    };

    function findImageItems(ev)
    {
        var items = [];
        $(Css.imageItem).each(function ()
        {
            items.push({ url: $(this).data("image-url"), downloadUrl: $(this).data("download-url") });
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
        var node = $(ev.currentTarget);

        var downloadUrl = node.attr("download-url");
        var url = node.data("image-url");
        var isVideo = node.parent().hasClass("js-video");

        console.debug("imageItemClick " + url);


        if (isVideo)
            showVideo(url, downloadUrl);
        else
            showImage(url, downloadUrl);

        return false;
    };

    function clearDialogContent()
    {
        $(Css.previewImage, Css.dialog).empty();
    };

    function showVideo(url, downloadUrl)
    {
        var videoNode = $("<video class='video-js vjs-default-skin' controls preload='auto'/>");
        
        clearDialogContent();

        $(Css.dialog).show();

        $(Css.previewImage, Css.dialog).append(videoNode);

        videoNode.attr("src", url);
        videoNode.attr("autoPlay", true);
        
        imageDialog.current = url;
        imageDialog.downloadUrl = downloadUrl;

        updateImageInfo();
    };

    function showImage(url, downloadUrl)
    {
        $(Css.dialog).show();
        var img = $("<img src='" + url + "'/>");

        clearDialogContent();

        $(Css.previewImage, Css.dialog).append(img);

        img.click(nextClick);

        imageDialog.current = url;
        imageDialog.downloadUrl = downloadUrl;

        updateImageInfo();
    };

    function getCurrentImageIndex()
    {
        for (var i = 0; i < imageDialog.items.length; i++)
            if (imageDialog.items[i].url == imageDialog.current)
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

    $(Css.imageItem).each(function ()
    {
        var a = $(this);
        a.data("image-url", this.href);
        a.href = "javascript:void(0);";
    })

    window.imageDialog =
        {
            items: findImageItems(),
            current: "",
            downloadUrl: ""
        };
})