$(document).ready(function () {

    PopuniTabelu(true);

    var toggle = true;
    $("#btnDeletedStatus").on("click", function () {
        if (toggle) {
            PopuniTabelu(false);
            toggle = false;
            $("#btnDeletedStatus").text('Show Full Table');
        } else {
            PopuniTabelu(true);
            toggle = true;
            $("#btnDeletedStatus").text('Show Deleted');
        }        
     
    })

    $("#btnNewStatus").on("click", function () {
        location.href = '/SystemStatus/Create';
    })
});


function PopuniTabelu(main) {

    var url;
    var dataForPopup;

    var isDeleted = [
        {
            id: true,
            text: 'Deleted'
        },
        {
            id: false,
            text: 'Active'
        }
    ];

    var isDel;
    var grid;
    var popup;

    if (main) {
        url = "/SystemStatus/GetStatusList";
    } else {
        url = "/SystemStatus/GetDeletedStatusList";
    }

    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (data) {



            $("#statusGrid").dxDataGrid({
                dataSource: {
                    store: {
                        type: 'array',
                        key: 'Id',
                        data: data
                    }
                },
                editing: {
                    mode: "popup",
                    allowUpdating: true,
                    form: {
                        colCount: 1
                    },
                    popup: {
                        title: "Status edit",
                        closeOnOutsideClick: true,
                        showTitle: true,
                        width: 700,
                        height: 345,
                        position: {
                            my: "top",
                            at: "top",
                            of: window
                        }
                        ,
                        onContentReady: function (e) {
                            popup = this;
                        }
                        ,
                        contentTemplate: function (contentElement) {
                            contentElement.append(

                                $("<div style='display: flex; align-items: center; margin-bottom: 10px;'><label style='width: 120px;'><strong>Status Name:</strong></label><div style='display: inline-block; width: calc(100% - 120px);' id='stNamePop'></div></div>"),
                                $("<div style='display: flex; align-items: center; margin-bottom: 10px;'><label style='width: 120px;'><strong>Description:</strong></label><div style='display: inline-block; width: calc(100% - 120px);'id='stDescPop'></div>"),
                                $("<div style='display: flex; align-items: center; margin-bottom: 10px;'><label style='width: 120px;'><strong>Deleted:</strong></label>   <div style='display: inline-block; width: calc(100% - 120px);' id='stDelPop'></div>")

                            );

                            $("#stNamePop").dxTextBox({
                                value: null
                            });

                            $("#stDescPop").dxTextArea({
                                value: null
                            });

                            $("#stDelPop").dxSelectBox({
                                valueExpr: 'id',
                                displayExpr: 'text',
                                value: null,
                                dataSource: isDeleted,
                            });
                        },
                        onShown: function () {

                            $("#stNamePop").dxTextBox('instance').option('value', dataForPopup.StatusName);
                            $("#stDescPop").dxTextArea('instance').option('value', dataForPopup.Description);

                            $("#stDelPop").dxSelectBox({
                                value: dataForPopup.isDeleted
                            });

                        },
                        toolbarItems: [
                            {
                                toolbar: 'bottom',
                                location: 'after',
                                widget: 'dxButton',
                                options: {
                                    onClick: function (e) {

                                        if ($("#stNamePop").dxTextBox('instance').option('value').length < 3) {
                                            $("#stNamePop").parent().siblings("p").remove();
                                            $("#stNamePop").css("border-color", "red");
                                            $("#stNamePop").parent().after("<p style='padding-left:120px; color:red;'>Must be at least 3 characters!</p>");
                                        } else {
                                            $("#stNamePop").css("border-color", "lightgray");
                                            $("#stNamePop").parent().siblings("p").remove();

                                            var rowIndex = grid.getRowIndexByKey(dataForPopup.Id);
                                            grid.cellValue(rowIndex, "StatusName", $("#stNamePop").dxTextBox('instance').option('value'));
                                            grid.cellValue(rowIndex, "Description", $("#stDescPop").dxTextArea('instance').option('value'));
                                            isDel = $("#stDelPop").dxSelectBox('instance').option('value');
                                            grid.saveEditData();
                                            popup.hide();
                                        }   
                                    },
                                    text: 'Save'
                                }
                            },
                            {
                                toolbar: 'bottom',
                                location: 'after',
                                widget: 'dxButton',
                                options: {
                                    onClick: function (e) {
                                        grid.cancelEditData();
                                        popup.hide();
                                    },
                                    text: 'Cancel'
                                }
                            }
                        ]
                    }
                },
                columns: [
                    {
                        caption: "Name",
                        dataField: "StatusName"
                    },
                    "Description"
                ],
                onEditingStart: function (e) {
                    dataForPopup = e.data;
                },  
                onContentReady: function (e) {
                    $(".dx-link-save").hide();
                    $(".dx-link-cancel").hide();

                    function moveEditColumnToLeft(dataGrid) {
                        dataGrid.columnOption("command:edit", {
                            visibleIndex: -1
                        });
                    }

                    grid = this;
                    moveEditColumnToLeft(e.component);
                },
                onRowUpdating: function (e) {

                    //console.dir(e.newData);
                    //console.dir(e);
                    var model = {};
                    model.Id = e.key;
                    model.isDeleted = isDel;
                    model = $.extend(model, e.newData);

                    EditRowOnMain(model);
                }

            });

        }
    });
}

function EditRowOnMain(object) {

    $.ajax({
        type: "POST",
        url: "/SystemStatus/EditSectionRow",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(object),
        success: function (response) {
            if (response !== null) {
                //alert(idComment);

            } else {
                alert("Error");
            }
        }
    });
}
