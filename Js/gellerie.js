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

    function showVideo(url, downloadUrl)
    {
        /*
        <video class='video-js vjs-default-skin' controls preload='auto' data-setup='{ "asdf": true }'>
    <source src="http://vjs.zencdn.net/v/oceans.mp4" type='video/mp4'>
  </video>
        */

        var videoNode = $("<video class='video-js vjs-default-skin' controls preload='auto'/>");
        //videoNode.data("setup", '{ "asdf": true }');

        

        $(Css.dialog).show();
        
        $(Css.previewImage, Css.dialog).empty().append(videoNode);

        //$("<source type='video/mp4'/>").attr("src", url).appendTo(videoNode);
        videoNode.attr("src", url);
        videoNode.autoPlay = true;

        //img.click(nextClick);

        imageDialog.current = url;
        imageDialog.downloadUrl = downloadUrl;

        updateImageInfo();
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