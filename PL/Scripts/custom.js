function onFileFindSuccess() {
    $(document).ready(function () {
        $("#deleteButton").off("click");
        var name = "";
        var type = "";

        if ($("#closeButton").length) {
            $("#closeButton").remove();
        }

        var closeBtn = $('<button>Close Search</button>').attr({
            id: "closeButton",
            value: "Close search"
        });
        closeBtn.addClass("btn");
        $("#buttons").append(closeBtn);

        $("#closeButton").click(function () {
            console.log(name + " " + type);
            var jsonObject = {
                "path": $("#uriPath").val(),
                "driveName": $("#uriDrive").val()
            };
            console.log(JSON.stringify(jsonObject));
            console.log($("#uriDrive").val());
            var urlToPass = "/Home/GetDirectories/" + $("#uriDrive").val() + "/" + $("#uriPath").val();
            console.log(urlToPass);
            var isConfirmed = confirm("Are you sure?");
            if (isConfirmed === true) {
                $.ajax({
                    url: urlToPass,
                    type: "GET",
                    dataType: "html",
                    data: {},
                    contentType: "application/json; charset=UTF-8",
                    success: function (data) {

                        var target = $("#explorerTable");
                        target.empty();
                        target.append(data);

                        $("tr.folder").each(function () {
                            var folderName = $(this).children("td")[0].innerText.trim();
                            console.log($("td.folderName"));
                            var dblStr = "location.href = '/Home/Explorer/" + $("#uriDrive").val() + $("#uriPath").val() + folderName + "/'";
                            $(this).attr("ondblclick", dblStr);
                        });
                    },
                    failure: function (errMsg) {
                        alert(errMsg);
                    }
                }).done(function () {
                    $("#closeButton").remove();

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
                            "path": $("#uriPath").val(),
                            "driveName": $("#uriDrive").val()
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
                                        var dblStr = "location.href = '/Home/Explorer/" + $("#uriDrive").val() + $("#uriPath").val() + folderName + "/'";
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
            }
        });
    });
};

function onFolderCreateSuccess() {
    $("#createFolderModal").modal("hide");

    $(document).ready(function () {
        $("#deleteButton").off("click");

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
                "path": $("#uriPath").val(),
                "driveName": $("#uriDrive").val()
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
                            var dblStr = "location.href = '/Home/Explorer/" + $("#uriDrive").val() + $("#uriPath").val() + folderName + "/'";
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
                "path": $("#uriPath").val(),
                "driveName": $("#uriDrive").val()
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
                            var dblStr = "location.href = '/Home/Explorer/" + $("#uriDrive").val() + $("#uriPath").val() + folderName + "/'";
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