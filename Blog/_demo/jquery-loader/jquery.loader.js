(function($) {

    $.widget("ui.loader", {
        interval: 0,
        intervalValue: 0,

        options: {
            speed: 25,
            autoStart: false,
            cssClass: 'ui-loader',
            overlayCssClass: 'ui-loader-overlay',
            progressCssClass: 'ui-loader-progress'
        },

        _create: function() {

            if (this.options.autoStart) {
                this.loader();
            }
        },

        destroy: function() {
            this.element.find('.' + this.options.cssClass).remove();

            clearInterval(this.interval);

            $.Widget.prototype.destroy.call(this);
        },

        _setOption: function(key, value) {
            this.options[key] = value;
        },

        _update: function() {
        },

        loader: function(show) {
            if (show) {
                var loader = this;
                loader.intervalValue = 0;

                var wrapper = $("<div>");
                wrapper.appendTo(this.element).addClass(this.options.cssClass);
                wrapper.width(loader.element.outerWidth()).height(loader.element.outerHeight());
                wrapper.position({ of: loader.element, my: 'left top', at: 'left top' });

                var overlay = $("<div>");
                overlay.appendTo(wrapper).addClass(this.options.overlayCssClass).addClass('ui-widget-overlay');

                var progress = $("<div>");
                progress.appendTo(wrapper).addClass(this.options.progressCssClass);
                progress.progressbar({ value: loader.intervalValue });
                progress.css({
                    'margin-top': (-1 * progress.outerHeight()) / 2,
                    'margin-left': (-1 * progress.outerWidth()) / 2
                });

                loader.interval = setInterval(function() {
                    progress.progressbar({ value: loader.intervalValue });
                    loader.intervalValue = loader.intervalValue == 100 ? 0 : loader.intervalValue = loader.intervalValue + 1;
                }, loader.options.speed);
            }
            else {
                this.element.find('.' + this.options.cssClass).remove();
                clearInterval(this.interval);
            }
        }
    });

})(jQuery);