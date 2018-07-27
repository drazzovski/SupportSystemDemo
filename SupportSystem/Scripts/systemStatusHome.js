$(document).ready(function () {

    PopuniTabelu();

    var toggle = true;
    $("#btnDeletedStatus").on("click", function () {
        if (toggle) {
            OnlyDeleted();
            toggle = false;
            $("#btnDeletedStatus").text('Show Full Table');
        } else {
            PopuniTabelu();
            toggle = true;
            $("#btnDeletedStatus").text('Show Deleted');
        }        
     
    })

    $("#btnNewStatus").on("click", function () {
        location.href = '/SystemStatus/Create';
    })
});


function PopuniTabelu() {


    $.ajax({
        type: "GET",
        url: "/SystemStatus/GetStatusList",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (data) {



            $("#statusGrid").dxDataGrid({
                dataSource: data,
                editing: {
                    allowUpdating: true,
                    allowDeleting: false,
                    useIcons: true
                },
                columns: [
                    {
                        caption: "Name",
                        dataField: "StatusName"
                    },
                    "Description"
                ],

                onContentReady: function (e) {
                    $(".dx-link-save").hide();
                    $(".dx-link-cancel").hide();

                    function moveEditColumnToLeft(dataGrid) {
                        dataGrid.columnOption("command:edit", {
                            visibleIndex: -1
                        });
                    }

                    moveEditColumnToLeft(e.component);
                }

            });

        }
    });
}

function OnlyDeleted() {


    $.ajax({
        type: "GET",
        url: "/SystemStatus/GetDeletedStatusList",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (data) {



            $("#statusGrid").dxDataGrid({
                dataSource: data,
                editing: {
                    allowUpdating: true,
                    allowDeleting: false,
                    useIcons: true
                },
                columns: [
                    {
                        caption: "Name",
                        dataField: "StatusName"
                    },
                    "Description"
                ],

                onContentReady: function (e) {
                    function moveEditColumnToLeft(dataGrid) {
                        dataGrid.columnOption("command:edit", {
                            visibleIndex: -1
                        });
                    }

                    moveEditColumnToLeft(e.component);
                }

            });

        }
    });
}