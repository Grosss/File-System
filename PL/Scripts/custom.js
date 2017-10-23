function onFileFindSuccess() {
    $(document).ready(function () {
        $("#deleteButton").off("click");
        var name = "";
        var type = "";

        if ($("#closeButton").length) {
            $("#closeButton").remove();
        }

        var closeBtn = $("<button>Close Search</button>").attr({
            id: "closeButton",
            value: "Close search"
        });
        closeBtn.addClass("btn");
        $("#buttons").append(closeBtn);

        $("#closeButton").click(function () {
            var urlToPass = "/Home/GetDirectories/" + $("#uriDrive").val() + "/" + $("#uriPath").val();

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
                        var dblStr = "location.href = '/Home/Explorer/" +
                            $("#uriDrive").val() +
                            $("#uriPath").val() +
                            folderName +
                            "/'";
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
                    var jsonObject = {
                        "name": name,
                        "type": type,
                        "path": $("#uriPath").val(),
                        "driveName": $("#uriDrive").val()
                    };
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
                                    var dblStr = "location.href = '/Home/Explorer/" +
                                        $("#uriDrive").val() +
                                        $("#uriPath").val() +
                                        folderName +
                                        "/'";
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
                                    name = tds[0].innerText.trim();
                                    type = tds[2].innerText;
                                }
                            });
                        });
                    }
                });
            });

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
            var jsonObject = {
                "name": name,
                "type": type,
                "path": $("#uriPath").val(),
                "driveName": $("#uriDrive").val()
            };
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
                            var dblStr = "location.href = '/Home/Explorer/" +
                                $("#uriDrive").val() +
                                $("#uriPath").val() +
                                folderName +
                                "/'";
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
        function orderBy(a, b, isNumb) {
            if (isNumb) return parseInt(a) - parseInt(b);
            if (a < b) return -1;
            if (a > b) return 1;
            return 0;
        }

        function sortTable(columnHeader, isDesc) {
            var isDate = columnHeader.hasClass("dateModified");
            var column = columnHeader.index();
            var table = columnHeader.closest("table");
            var isNum = columnHeader.hasClass("fileSize");
            var rows;
            if (!isDesc) {
                rows = table.find("tbody > tr.folder").get();
                rows.sort(function (rowA, rowB) {
                    var keyA;
                    var keyB;
                    if (isDate) {
                        keyA = new Date($(rowA).children("td").eq(column).text().toUpperCase());
                        keyB = new Date($(rowB).children("td").eq(column).text().toUpperCase());
                    } else {
                        keyA = $(rowA).children("td").eq(column).text().toUpperCase();
                        keyB = $(rowB).children("td").eq(column).text().toUpperCase();
                    }
                    return orderBy(keyA, keyB, isNum);
                });
                $.each(rows,
                    function (index, row) {
                        table.children("tbody").append(row);
                    });

                rows = table.find("tbody > tr.file").get();
                rows.sort(function (rowA, rowB) {
                    var keyA;
                    var keyB;
                    if (isDate) {
                        keyA = new Date($(rowA).children("td").eq(column).text().toUpperCase());
                        keyB = new Date($(rowB).children("td").eq(column).text().toUpperCase());
                    } else {
                        keyA = $(rowA).children("td").eq(column).text().toUpperCase();
                        keyB = $(rowB).children("td").eq(column).text().toUpperCase();
                    }
                    return orderBy(keyA, keyB, isNum);
                });
                $.each(rows,
                    function (index, row) {
                        table.children("tbody").append(row);
                    });
            } else {
                rows = table.find("tbody > tr.file").get();
                rows.sort(function (rowA, rowB) {
                    var keyA;
                    var keyB;
                    if (isDate) {
                        keyA = new Date($(rowA).children("td").eq(column).text().toUpperCase());
                        keyB = new Date($(rowB).children("td").eq(column).text().toUpperCase());
                    } else {
                        keyA = $(rowA).children("td").eq(column).text().toUpperCase();
                        keyB = $(rowB).children("td").eq(column).text().toUpperCase();
                    }
                    return orderBy(keyB, keyA, isNum);
                });
                $.each(rows,
                    function (index, row) {
                        table.children("tbody").append(row);
                    });

                rows = table.find("tbody > tr.folder").get();
                rows.sort(function (rowA, rowB) {
                    var keyA;
                    var keyB;
                    if (isDate) {
                        keyA = new Date($(rowA).children("td").eq(column).text().toUpperCase());
                        keyB = new Date($(rowB).children("td").eq(column).text().toUpperCase());
                    } else {
                        keyA = $(rowA).children("td").eq(column).text().toUpperCase();
                        keyB = $(rowB).children("td").eq(column).text().toUpperCase();
                    }
                    return orderBy(keyB, keyA, isNum);
                });
                $.each(rows,
                    function (index, row) {
                        table.children("tbody").append(row);
                    });
            }
        }

        $("th").click(function () {
            var selectedHeader = $(this).closest("th");
            selectedHeader.toggleClass("desc");
            var isDesc = selectedHeader.hasClass("desc");
            sortTable(selectedHeader, isDesc);
        });

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
            var jsonObject = {
                "name": name,
                "type": type,
                "path": $("#uriPath").val(),
                "driveName": $("#uriDrive").val()
            };
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
                            var dblStr = "location.href = '/Home/Explorer/" +
                                $("#uriDrive").val() +
                                $("#uriPath").val() +
                                folderName +
                                "/'";
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
                            name = tds[0].innerText.trim();
                            type = tds[2].innerText;
                        }
                    });
                });
            }
        });
    });
}());