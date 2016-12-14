tinyMCE.init({
    // General options
    mode: "textareas",
    theme: "advanced",
    theme_advanced_buttons1: "code,cleanup,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,outdent,indent,blockquote,|,bullist,numlist,|,link,unlink,|,styleselect,formatselect,fontselect,fontsizeselect",
    theme_advanced_buttons2: "",
    theme_advanced_buttons3: "",
    theme_advanced_toolbar_location: "top",
    theme_advanced_toolbar_align: "left",
    theme_advanced_statusbar_location: "bottom"
});

$(document).ready(function () {
    category();
    $("input[type=checkbox]").checkbox();

    $("#Title").blur(function () {
        if ($("#Slug").val() == '') {
            slug();
        }
    });

    $("#slug-update").click(function () {
        slug();
    });

    $("#CategoryID").change(function () {
        category();
    });

    $("#Tags")
            .alpha({ nocaps: true, allow: ",-#" })
            .bind("keydown", function (event) {
                if (event.keyCode === $.ui.keyCode.TAB &&
						$(this).data("autocomplete").menu.active) {
                    event.preventDefault();
                }
            })
            .autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: url,
                        data: " { 'q' : '" + extractLast(request.term) + "' , 'limit' : '10' } ",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.Name,
                                    value: item.Name,
                                    id: item.TagID
                                };
                            }));
                        }
                    });
                },
                focus: function () {
                    // prevent value inserted on focus
                    return false;
                },
                select: function (event, ui) {
                    var terms = split(this.value);
                    terms.pop();
                    terms.push(ui.item.value);
                    terms.push("");
                    this.value = terms.join(", ");
                    return false;
                },
                minLength: 1
            });
});

function split(val) {
    return val.split(/,\s*/);
}
function extractLast(term) {
    return split(term).pop();
}

function slug() {
    $.ajax({
        url: surl,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: " { 'title' : '" + escape($("#Title").val()) + "' } ",
        success: function (result) {
            $("#Slug").val(result.Result);
        }
    });
}

function category() {
    var categoryID = $("#CategoryID").val();

    if (categoryID > 0) {
        $("#category").html("/" + $("#CategoryID option:selected").text());
    }
    else {
        $("#category").html("");
    }
}