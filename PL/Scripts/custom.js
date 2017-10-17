function onFolderCreateSuccess() {
    $("#createFolderModal").modal("hide");

    $(document).ready(function () {
        var name = "";
        var type = "";

        $("#deleteButton").prop("disabled", true);
        $("#explorerTable tr").click(function () {
            var selected = $(this).hasClass("chosen");
            $("#explorerTable tr").removeClass("chosen");
            name = "";
            type = "";
            $("#deleteButton").prop("disabled", true);
            if (!selected) {
                $("#deleteButton").prop("disabled", false);
                $(this).addClass("chosen");
                var tds = $(this).children("td");
                name = tds[0].innerText.trim();
                type = tds[2].innerText;
            }
        });

        $("#deleteButton").off("click");
        $("#deleteButton").click(function () {
            console.log(name + " " + type);
            var jsonObject = {
                "name": name,
                "type": type,
                "path": $("#uriPath").val()
            };
            console.log(JSON.stringify(jsonObject));
            var isConfirmed = confirm("Are you sure?");
            if (isConfirmed === true) {
                $.ajax({
                    url: "/Home/DeleteFile",
                    type: "POST",
                    dataType: "html",
                    data: JSON.stringify(jsonObject),
                    contentType: "application/json; charset=UTF-8",
                    success: function (data) {

                        var target = $("#explorerTable");
                        target.empty();
                        target.append(data);

                        $("tr.folder").each(function () {
                            var folderName = $(this).children("td")[0].innerText.trim();
                            console.log($("td.folderName"));
                            var dblStr = "location.href = '/Home/Explorer/" + $("#uriPath").val() + folderName + "/'";
                            $(this).attr("ondblclick", dblStr);
                        });
                    },
                    failure: function (errMsg) {
                        alert(errMsg);
                    }
                }).done(function () {

                    $("#deleteButton").prop("disabled", true);
                    $("#explorerTable tr").click(function () {
                        var selected = $(this).hasClass("chosen");
                        $("#explorerTable tr").removeClass("chosen");
                        name = "";
                        type = "";
                        $("#deleteButton").prop("disabled", true);
                        if (!selected) {
                            $("#deleteButton").prop("disabled", false);
                            $(this).addClass("chosen");
                            var tds = $(this).children("td");
                            console.log(tds[0]);
                            name = tds[0].innerText.trim();
                            type = tds[2].innerText;
                        }
                    });
                });
            }
        });

    });


};

function onFileUploadSuccess() {
    $("#uploadFileModal").modal("hide");
};

(function () {

    $(document).ready(function () {
        var name = "";
        var type = "";

        $("#deleteButton").prop("disabled", true);
        $("#explorerTable tr").click(function () {
            var selected = $(this).hasClass("chosen");
            $("#explorerTable tr").removeClass("chosen");
            name = "";
            type = "";
            $("#deleteButton").prop("disabled", true);
            if (!selected) {
                $("#deleteButton").prop("disabled", false);
                $(this).addClass("chosen");
                var tds = $(this).children("td");
                name = tds[0].innerText.trim();
                type = tds[2].innerText;
            }
        });

        $("#deleteButton").click(function () {
            console.log(name + " " + type);
            var jsonObject = {
                "name": name,
                "type": type,
                "path": $("#uriPath").val()
            };
            console.log(JSON.stringify(jsonObject));
            var isConfirmed = confirm("Are you sure?");
            if (isConfirmed === true) {
                $.ajax({
                    url: "/Home/DeleteFile",
                    type: "POST",
                    dataType: "html",
                    data: JSON.stringify(jsonObject),
                    contentType: "application/json; charset=UTF-8",
                    success: function (data) {

                        var target = $("#explorerTable");
                        target.empty();
                        target.append(data);

                        $("tr.folder").each(function () {
                            var folderName = $(this).children("td")[0].innerText.trim();
                            console.log($("td.folderName"));
                            var dblStr = "location.href = '/Home/Explorer/" + $("#uriPath").val() + folderName + "/'";
                            $(this).attr("ondblclick", dblStr);
                        });
                    },
                    failure: function (errMsg) {
                        alert(errMsg);
                    }
                }).done(function () {

                    $("#deleteButton").prop("disabled", true);
                    $("#explorerTable tr").click(function () {
                        var selected = $(this).hasClass("chosen");
                        $("#explorerTable tr").removeClass("chosen");
                        name = "";
                        type = "";
                        $("#deleteButton").prop("disabled", true);
                        if (!selected) {
                            $("#deleteButton").prop("disabled", false);
                            $(this).addClass("chosen");
                            var tds = $(this).children("td");
                            console.log(tds[0]);
                            name = tds[0].innerText.trim();
                            type = tds[2].innerText;
                        }
                    });
                });
            }
        });

    });
}());