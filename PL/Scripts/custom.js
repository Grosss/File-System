$(document).ready(function () {
    $("#deleteFile").bind("click", false);

    $("#explorerTable tr").click(function () {
        $("#deleteFile").bind("click", false);
        var selected = $(this).hasClass("chosen");
        $("#explorerTable tr").removeClass("chosen");
        if (!selected) {
            $(this).addClass("chosen");
            $("#deleteFile").unbind("click", false);
        }
            
    });

    $("#deleteFile").click(function () {
        alert("ASASA");
    });
});