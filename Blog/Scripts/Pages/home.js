var header, content;

$(document).ready(function () {
    header = $("#header-img");
    main = $("#content");

    //    setInterval(function () {
    //        if (header.hasClass('on')) {
    //            header.addClass('off').removeClass('on');
    //            header.attr('src', '/content/images/header-off.png');
    //        }
    //        else {
    //            header.addClass('on').removeClass('off');
    //            header.attr('src', '/content/images/header-on.png');
    //        }
    //    }, 300);

    //    $("#header-input").focus(function () {
    //        this.selectionStart = this.selectionEnd = -1;
    //    }).focus();

    $("#header-input").putCursorAtEnd().focus();

    setHeight();
    $(window).resize(function () {
        setHeight();
    });
});

function setHeight() {

    var height = 0;

    $.each($(".container"), function () {
        height += $(this).outerHeight(true);
    });

    var difference = $(window).height() - (height + 15);

    if (difference > 0) {
        $("#push").height(difference);
    }
    else {
        $("#push").height(Math.max($("#push").height() + difference, 0));
    }

}

function footerWidth() {
    if ($("#footer .first div").css('padding-left') == '0px') {
        $("#footer").equalWidths();
    }
}

(function ($) {
    jQuery.fn.putCursorAtEnd = function () {
        return this.each(function () {
            $(this).focus()

            // If this function exists...
            if (this.setSelectionRange) {
                // ... then use it
                // (Doesn't work in IE)

                // Double the length because Opera is inconsistent about whether a carriage return is one character or two. Sigh.
                var len = $(this).val().length * 2;
                this.setSelectionRange(len, len);
            }
            else {
                // ... otherwise replace the contents with itself
                // (Doesn't work in Google Chrome)
                $(this).val($(this).val());
            }

            // Scroll to the bottom, in case we're in a tall textarea
            // (Necessary for Firefox and Google Chrome)
            this.scrollTop = 999999;
        });
    };
})(jQuery);