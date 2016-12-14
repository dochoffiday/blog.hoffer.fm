(function ($) {

    $.ajaxSetup({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataFilter: function (data) {
            return DataFilter(data);
        }
    });

    /* Tab Index */

    $(function () {
        var tabindex = 1;
        $('input, select').each(function () {
            if (this.type != "hidden") {
                var $input = $(this);
                $input.attr("tabindex", tabindex);
                tabindex++;
            }
        });
    });

})(jQuery);

(function ($) {
    $.fn.autowidth = function () {
        return this.each(function () {
            var item = $(this);

            function setWidth() {
                item.width(item.parent().width() - (item.outerWidth() - item.width()));
            }

            setWidth();

            $(window).resize(function () {
                setWidth();
            });
        });
    };
})(jQuery);

function DataFilter(data) {
    var msg;

    if (typeof (JSON) !== 'undefined' && typeof (JSON.parse) === 'function') {
        msg = JSON.parse(data);
    }
    else {
        msg = eval('(' + data + ')');
    }

    if (msg.hasOwnProperty('d')) {
        return msg.d;
    }
    else {
        return msg;
    }
}