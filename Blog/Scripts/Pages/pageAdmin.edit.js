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
    $("input[type=checkbox]").checkbox();

    $("#Title").blur(function () {
        if ($("#Slug").val() == '') {
            slug();
        }
    });

    $("#slug-update").click(function () {
        slug();
    });
});

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