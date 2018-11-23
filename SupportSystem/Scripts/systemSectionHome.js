$(document).ready(function () {

    PopuniTabelu(true);

    var toggle = true;
    $("#btnDeletedSection").on("click", function(){
        if (toggle) {
            PopuniTabelu(false);
            toggle = false;
            $("#btnDeletedSection").text('Show Full Table');
        } else {
            PopuniTabelu(true);
            toggle = true;
            $("#btnDeletedSection").text('Show Deleted');
        }     
    })

    $("#btnNewSection").on("click", function () {
        location.href = '/SystemSection/Create';
    });
    
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
        url = "/SystemSection/GetSectionList";
    } else {
        url = "/SystemSection/GetDeletedSectionList";
    }

    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (data) {

            $("#sectionGrid").dxDataGrid({
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
                        title: "Section edit",
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

                               $("<div style='display: flex; align-items: center; margin-bottom: 10px;'><label style='width: 120px;'><strong>Section Name:</strong></label><div style='display: inline-block; width: calc(100% - 120px);' id='secNamePop'></div></div>"),
                               $("<div style='display: flex; align-items: center; margin-bottom: 10px;'><label style='width: 120px;'><strong>Description:</strong></label><div style='display: inline-block; width: calc(100% - 120px);'id='secDescPop'></div>"),
                               $("<div style='display: flex; align-items: center; margin-bottom: 10px;'><label style='width: 120px;'><strong>Deleted:</strong></label>   <div style='display: inline-block; width: calc(100% - 120px);' id='secDelPop'></div>")
                                  
                            );   

                            $("#secNamePop").dxTextBox({
                                value: null
                            });

                            $("#secDescPop").dxTextArea({
                                value: null
                            });

                            $("#secDelPop").dxSelectBox({
                                valueExpr: 'id',
                                displayExpr: 'text',
                                value: null,
                                dataSource: isDeleted,
                            });
                        },
                        onShown: function () {

                            $("#secNamePop").dxTextBox('instance').option('value', dataForPopup.Name);
                            $("#secDescPop").dxTextArea('instance').option('value', dataForPopup.Description);

                            $("#secDelPop").dxSelectBox({
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

                                        var rowIndex = grid.getRowIndexByKey(dataForPopup.Id);

                                        if ($("#secNamePop").dxTextBox('instance').option('value').length < 3) {
                                            $("#secNamePop").parent().siblings("p").remove();
                                            $("#secNamePop").css("border-color", "red");
                                            $("#secNamePop").parent().after("<p style='padding-left:120px; color:red;'>Must be at least 3 characters!</p>");
                                        } else {
                                            $("#secNamePop").css("border-color", "lightgray"); 
                                            grid.cellValue(rowIndex, "Name", $("#secNamePop").dxTextBox('instance').option('value'));
                                            grid.cellValue(rowIndex, "Description", $("#secDescPop").dxTextArea('instance').option('value'));
                                            isDel = $("#secDelPop").dxSelectBox('instance').option('value');
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
                        caption: "Section Name",
                        dataField: "Name"
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
                  
                }              
                ,
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
        url: "/SystemSection/EditSectionRow",
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
