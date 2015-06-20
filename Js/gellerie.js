$(document).ready(function()
{
    var Css =
        {
            dialog: ".js-photo-dialog",
            closeButton: ".js-close",
            prevButton: ".js-prev",
            nextButton: ".js-next",

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
            items.push(this.href);
        });

        return items;
    };

    function imageItemClick(ev)
    {
        console.debug("imageItemClick " + ev.currentTarget.href);

        showImage(ev.currentTarget.href);

        return false;
    };

    function showImage(url)
    {
        $(Css.dialog).show();
        var img = $("<img src='" + url + "'/>");

        $(Css.previewImage, Css.dialog).empty().append(img);

        img.click(nextClick);

        imageDialog.current = url;

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
    $(Css.closeButton, Css.dialog).click(closeClick);

    $(Css.imageItem).click(imageItemClick);

    window.imageDialog =
        {
            items: findImageItems(),
            current: "",
        };


})