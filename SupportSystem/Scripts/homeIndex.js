
$(document).ready(function () {

    StatusLookUp();
    CategoryLookUp();
    SeverityLookUp();
    PriorityLookUp();

    PopuniTabelu(true);    

    UserName();

    $("#viewSugg").on("click", function () {
        SuggestionView();
    });

     
    var toggle = true;
    $("#showClosed").on("click", function () {
        if (toggle) {
            PopuniTabelu(false);
            toggle = false;
            $("#showClosed").text('Show Full Table');
        } else {
            PopuniTabelu(true);
            toggle = true;
            $("#showClosed").text('Show Closed');
        }        
    });  

});

var IdMain;

var userName;
function UserName() {
    $.ajax({
        type: "GET",
        url: "/Home/UserName",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (data) {
            $("#createdBy").val(data);
            userName = data;
        }
    });    
}



function PopuniTabelu(main) {

    var url; 
    if (main) {
        url = "/Home/GetList";
    } else {
        url = "/Home/ShowClosed";
    }



    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (data) {

            //console.dir(data);

            $("#dataGrid").dxDataGrid({
                dataSource: data,
                selection: {
                    mode: "single"
                },
                editing: {
                    allowUpdating: true,
                    allowDeleting: false,
                    useIcons: true
                },
                columns: [
                    {
                        caption: "Title No",
                        dataField: "Number",
                        allowEditing: false
                    },
                    "Title",
                    {
                        caption: "Status",
                        dataField: "IdStatus", 
                        lookup: {
                            dataSource: statusLookUp,
                            displayExpr: "value",
                            valueExpr: "key"
                        },

                        cellTemplate: function (element, info) {

                            if (info.text === "Closed") {
                                element.append("<div>" + info.text + "</div>").addClass("cell-green");
                            }
                            else if (info.text === "Submited") {
                                element.append("<div>" + info.text + "</div>").addClass("cell-submited");
                            }
                            else if (info.text === "In Process") {
                                element.append("<div>" + info.text + "</div>").addClass("cell-inprocess");
                            }
                            else {
                                element.append("<div>" + info.text + "</div>");
                            }

                        },
                    },
                    {
                        caption: "Category",
                        dataField: "IdKategorija",
                        lookup: {
                            dataSource: categoryLookUp,
                            displayExpr: "value",
                            valueExpr: "key"
                        },
                    }, {
                        caption: "Severity",
                        dataField: "IdSeverity",
                        lookup: {
                            dataSource: severityLookUp,
                            displayExpr: "value",
                            valueExpr: "key"
                        },
                    }, {
                        caption: "Priority",
                        dataField: "IdPriority",
                        lookup: {
                            dataSource: priorityLookUp,
                            displayExpr: "value",
                            valueExpr: "key"
                        },
                    },
                    {
                        caption: "Rased By",
                        dataField: "User",
                        allowEditing: false
                    },
                    {
                        caption: "Rased On",
                        dataField: "strCreatedOn",
                        allowEditing: false
                    },
                    {
                        caption: "Due On",
                        dataField: "strDueOn",
                        allowEditing: false
                    },
                    {
                        caption: "Resolved On",
                        dataField: "strResolvedOn",
                        allowEditing: false
                    }
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
                },

                onSelectionChanged: function (selectedItems) {
                    var data = selectedItems.selectedRowsData[0];
                    if (data) {
                        IdMain = data.Id;
                        PrikaziKomentare(data.Id, true);
                    }
                },
                //ON EDIT -------> SAVE <------------------------------------------------------------------------
                //onRowUpdating: function (e) {

                //    //console.dir(e.newData);
                //    //console.dir(e);

                //    model.Id = e.key.Id;
                //    model = $.extend(model, e.newData);

                //    EditRowOnMain(model);
                //},

                onEditingStart: function (e) {

                    sessionStorage.setItem("homeEditId", e.key.Id);

                    if (e.key.Id !== null) {
                        $.ajax({
                            type: "POST",
                            url: "/Home/EditModeSet",
                            data: "id=" + e.key.Id,
                            success: function (response) {
                                if (response !== null) {
                                    location.href = '/Home/Edit/';
                                } else {
                                    alert("Error");
                                }
                            }
                        });
                    }

                    
                },
            });
            
        }
    });
}

var model = {};
function SuggestionView() {
    
    location.href = '/Home/Create';
}


var statusLookUp = [];
function StatusLookUp() {

    $.ajax({
        type: "GET",
        url: "/Home/StatusList",
        dataType: "json",
        success: function (data) {

            statusLookUp = data;
        } 
    });
}

var categoryLookUp = [];
function CategoryLookUp() {

    $.ajax({
        type: "GET",
        url: "/Home/CategoriesList",
        dataType: "json",
        success: function (data) {

            categoryLookUp = data;
        }
    });
}

var severityLookUp = [];
function SeverityLookUp() {

    $.ajax({
        type: "GET",
        url: "/Home/SeveritiesList",
        dataType: "json",
        success: function (data) {

            severityLookUp = data;
        }
    });
}

var priorityLookUp = [];
function PriorityLookUp() {

    $.ajax({
        type: "GET",
        url: "/Home/PrioritiesList",
        dataType: "json",
        success: function (data) {

            priorityLookUp = data;
        }
    });
}


//function EditRowOnMain(object) {

//    var obj = JSON.stringify(object);

//    $.ajax({
//        type: "POST",
//        url: "/Home/EditRowOnMain",
//        contentType: "application/json; charset=utf-8",
//        data: obj,
//        success: function (response) {
//            if (response !== null) {
//                //idComment = response;
//                //alert(idComment);

//            } else {
//                alert("Error");
//            }
//        }
//    });
//}
