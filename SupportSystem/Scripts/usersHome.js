$(document).ready(function () {
    PopuniTabelu(true);

    $("#btnCreateUser").on("click", function () {
        CreateNewUser();
    }); 

    //$("#btnDeletedUsers").on("click", function () {
    //    PopuniTabelu(false);
    //}); 


    var toggle = true;
    $("#btnDeletedUsers").on("click", function () {
        if (toggle) {
            PopuniTabelu(false);
            toggle = false;
            $("#btnDeletedUsers").text('Show All Users');
        } else {
            PopuniTabelu(true);
            toggle = true;
            $("#btnDeletedUsers").text('Show Deleted');
        }
    });  

});

function PopuniTabelu(Main) {

    var url;

    if (Main) {
        url = "/Users/GetUsers";
    } else {
        url = "/Users/GetNonActiveUsers";
    }

    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (data) {

            $("#usersGrid").dxDataGrid({
                dataSource: data,
                editing: {
                    allowUpdating: true,
                    allowDeleting: false,
                    useIcons: true
                },
                columns: [
                    {
                        caption: "Name",
                        dataField: "UserName"
                    },
                    "Address", "City", "Country", 
                    {
                        caption: "Active?",
                        dataField: "isActive"
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
                //onRowUpdating: function (e) {

                //    //console.dir(e.newData);
                //    //console.dir(e);

                //    model.Id = e.key.Id;
                //    model = $.extend(model, e.newData);

                //    EditRowOnUsers(model);
                //},
                onEditingStart: function (e) {

                    sessionStorage.setItem("usersEditId", e.key.Id);

                    if (e.key.Id !== null) {
                        $.ajax({
                            type: "POST",
                            url: "/Users/EditModeSet",
                            data: "id=" + e.key.Id,
                            success: function (response) {
                                if (response !== null) {
                                    location.href = '/Users/Edit/';
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

function CreateNewUser() {
    location.href = '/Users/Create';
}

//function EditRowOnUsers(object) {

//    var obj = JSON.stringify(object);

//    $.ajax({
//        type: "POST",
//        url: "/Users/EditRowOnUsers",
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
