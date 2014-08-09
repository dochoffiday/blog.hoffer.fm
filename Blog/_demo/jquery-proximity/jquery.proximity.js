(function($) {
    $.fn.proximity = function(options) {

        var defaults = {
            range: 100,
            fire: function(input, percent) {
                // do nothing
            }
        };
        options = $.extend(defaults, options);

        return this.each(function() {
            var input = $(this);

            function py(a, b) {
                return Math.sqrt(a * a + b * b);
            }

            $(document).mousemove(function(e) {

                var offset = input.offset();

                var top = offset.top;
                var left = offset.left;
                var right = left + input.outerWidth(false);
                var bottom = top + input.outerHeight(false);
                var x = e.pageX;
                var y = e.pageY;

                var dif;

                if (x < left) {
                    if (y < top) {
                        dif = py(top - y, left - x);
                    }
                    else if (y >= top && y <= bottom) {
                        dif = left - x;
                    }
                    else {
                        dif = py(y - bottom, left - x);
                    }
                }
                else if (x >= left && x <= right) {
                    if (y < top) {
                        dif = top - y;
                    }
                    else if (y >= top && y <= bottom) {
                        dif = 0;
                    }
                    else {
                        dif = y - bottom;
                    }
                }
                else {
                    if (y < top) {
                        dif = py(top - y, x - right);
                    }
                    else if (y >= top && y <= bottom) {
                        dif = x - right;
                    }
                    else {
                        dif = py(y - bottom, x - right);
                    }
                }

                if (y - $(window).scrollTop() <= 1) {
                    dif = options.range;
                }

                if (dif <= options.range) {
                    var percent = dif / options.range;

                    options.fire(input, percent);
                }
            });
        });
    };
})(jQuery);