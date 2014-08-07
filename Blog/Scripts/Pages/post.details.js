$.SyntaxHighlighter.init();

(function ($) {
    $.fn.autowidth = function () {
        return this.each(function () {
            var item = $(this);
            var width = parseInt(item.css('width').substring(0, item.css('width').indexOf("px")));

            item.hover(function () {
                if ($(window).width() >= 768) {
                    item.animate({ 'width': width + 300 + "px" }, { duration: 500, queue: false });
                }
            }, function () {
                if ($(window).width() >= 768) {
                    item.animate({ 'width': width + "px" }, { duration: 500, queue: false });
                }
            });
        });
    };
})(jQuery);

$(document).ready(function () {
    var info = $(".info");
    var title = $(".info-title");

    var top = parseInt(info.offset().top);
    info.css('top', top + 'px');

    function setLeft() {
        info.css('left', 0 + 'px');
        info.css('position', 'relative');

        var left = parseInt(info.offset().left);

        info.css('left', left + 'px');
        info.css('position', 'absolute');
    }

    function setTop() {
        if ($(window).width() <= 1023) {
            title.hide();
            info.css('top', '0px');
            info.css('left', '0px');
            info.css('position', 'relative');
        }
        else {
            var offset = $(document).scrollTop();

            if (offset > 0) {
                title.show();
            }
            else {
                title.hide();
            }

            info.animate({ 'top': (offset + top) + "px" }, { duration: 500, queue: false });
        }
    }

    setLeft();
    setTop();

    $(window).scroll(function () {
        setTop();
    });

    $(window).resize(function () {
        setLeft();
        setTop();
    });

    $('.highlight').autowidth();
});