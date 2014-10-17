(function ($) {

    $.fn.styleTable = function (options) {
        var defaults = {
            css: 'ui-table'
        };
        options = $.extend(defaults, options);

        return this.each(function () {

            var input = $(this);
            var thead = $(this);
            var tbody = $(this);

            tbody = $(tbody.children("tbody"));
            thead = $(thead.children("thead"));

            input.addClass(options.css).addClass('ui-widget');

            function SetupRow() {

                thead.children("tr:not(.styled)").each(function () {
                    $(this).children("th").addClass("ui-state-default");
                    $(this).children("th:not(:first)").addClass("noleft");
                    $(this).addClass("styled");
                });

                tbody.children("tr:not(.styled)").each(function () {
                    $(this).children("td").addClass("ui-widget-content");
                    $(this).children("td:not(:first)").addClass("noleft");
                    $(this).addClass("styled");

                    $(this).hover(function () {
                        $(this).children("td").addClass("ui-state-hover");
                    }, function () {
                        $(this).children("td").removeClass("ui-state-hover");
                    });
                });
            }

            setInterval(function () {
                SetupRow();
            }, 1);
        });
    };
})(jQuery);

(function($) {
    $(document).ready(function() {
        $('table').styleTable();
    });
})(jQuery);