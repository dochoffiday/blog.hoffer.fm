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