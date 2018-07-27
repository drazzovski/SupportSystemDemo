$(document).ready(function () {

    PopuniTabelu();

    var toggle = true;
    $("#btnDeletedSection").on("click", function(){
        if (toggle) {
            OnlyDeleted();
            toggle = false;
            $("#btnDeletedSection").text('Show Full Table');
        } else {
            PopuniTabelu();
            toggle = true;
            $("#btnDeletedSection").text('Show Deleted');
        }     
    })

    $("#btnNewSection").on("click", function () {
        location.href = '/SystemSection/Create';
    })
});


function PopuniTabelu() {


    $.ajax({
        type: "GET",
        url: "/SystemSection/GetSectionList",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (data) {



            $("#sectionGrid").dxDataGrid({
                dataSource: data,
                editing: {
                    allowUpdating: true,
                    allowDeleting: false,
                    useIcons: true
                },
                columns: [
                    {
                        caption: "Section Name",
                        dataField: "Name"
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
        url: "/SystemSection/GetDeletedSectionList",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (data) {



            $("#sectionGrid").dxDataGrid({
                dataSource: data,
                editing: {
                    allowUpdating: true,
                    allowDeleting: false,
                    useIcons: true
                },
                columns: [
                    {
                        caption: "Section Name",
                        dataField: "Name"
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