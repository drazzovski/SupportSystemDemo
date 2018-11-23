$(document).ready(function () {

    SuggestionNumber();
    UserName();
   

    SelectStatuses(true);
    SelectCategories(true);
    SelectPriorities(true);
    SelectSeverities(true);
    SelectSections(true);

    //btn back to list
    $("#btnMainList").on("click", function () {
        location.href = '/Home/Index';

        ClearTempComments();
    });


    $("#btnSaveSugg").on("click", function () {

        // save suggestion with comments in db
        Validacije();
        SaveSuggestionWithComments();
    });

    var todayDate = moment().format("DD/MM/YYYY");
    $("#createdOn").dxDateBox({
        width: 500,
        min: new Date(2000, 0, 1),
        max: new Date(2029, 11, 31),
        value: new Date()
    });

    $("#acceptedOn").dxDateBox({
        width: 500,
        min: new Date(2000, 0, 1),
        max: new Date(2029, 11, 31),
        value: null
    });
    
    $("#dueOn").dxDateBox({
        width: 500,
        min: new Date(2000, 0, 1),
        max: new Date(2029, 11, 31),
        value: null
    });

    $("#resolvedOn").dxDateBox({
        width: 500,
        min: new Date(2000, 0, 1),
        max: new Date(2029, 11, 31),
        value: null
    });
    
    $("#btnVal").on("click", function () {
        Validacije();
    });

    $("#sidebar-wrapper").height($("#page-content-wrapper").height());
});

function SaveSuggestionWithComments() {

    var date = new Date();

    var hh = date.getHours();
    var mm = date.getMinutes();
    var ss = date.getSeconds();
    var ff = date.getMilliseconds()

    var time = " " + hh + ":" + mm + ":" + ss + "." + ff;

    Validacije();

    if (boolValidacije) {
        var suggNo = $("#suggestion").val();
        var createdBy = $("#createdBy").val();
        var datum = moment($("#createdOn").dxDateBox("instance").option("value")).format("YYYY-MM-DD");
       
        var createdOn = new Date(datum + time);
        var statusId = $("#status").val();
        var categoryId = $("#category").val();
        var sectionId = $("#sysSection").val();
        var severityId = $("#severity").val();
        var title = $("#title").val();
        var steps = $("#steps").val();
        var priorityId = $("#priority").val();
        var acceptedOn = moment($("#acceptedOn").dxDateBox("instance").option("value")).format("YYYY-MM-DD");      
        var dueOn = moment($("#dueOn").dxDateBox("instance").option("value")).format("YYYY-MM-DD");
        var resolvedOn = moment($("#resolvedOn").dxDateBox("instance").option("value")).format("YYYY-MM-DD");
        var notes = $("#notes").val();

        $.ajax({
            type: "POST",
            url: "/Home/SaveSuggestion",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                Number: suggNo,
                User: createdBy,
                CreatedOn: createdOn,
                IdStatus: statusId,
                IdKategorija: categoryId,
                IdSystemSection: sectionId,
                IdSeverity: severityId,
                Title: title,
                Steps: steps,
                IdPriority: priorityId,
                AcceptedOn: acceptedOn,
                DueOn: dueOn,
                ResolvedOn: resolvedOn,
                Notes: notes
            }),
            success: function (response) {
                if (response !== null) {
                    //alert(response);
                    toastr.success("Suggestion is saved!");
                    if (commentsForEmial !== "") {
                        SendCommentsMail("arandjic@gmail.com", "Comments from Created Suggestion.", emailHtml)
                        commentsForEmial = "";
                    }   //<--------------------------------------- send mail
                    ClearTempComments();
                    SuggestionNumber();
                    $("#suggestion").val("");
                    $("#status").val("").trigger("change");
                    $("#category").val("").trigger("change");
                    $("#sysSection").val("").trigger("change");
                    $("#severity").val("").trigger("change");
                    $("#title").val("");
                    $("#steps").val("");
                    $("#priority").val("").trigger("change");

                    $("#dueOn").dxDateBox({
                        value: null
                    });
                    $("#resolvedOn").dxDateBox({
                        value: null
                    });
                    $("#acceptedOn").dxDateBox({
                        value: null
                    });
                    $("#notes").val("");

                    $('div[name="cbox"]').remove();
                } else {
                    alert("Error");
                    toastr.error("Oops! There was an error during saving a suggestion.");
                }
            }
        });

   
    }   
}


function SuggestionNumber() {
    $.ajax({
        type: "GET",
        url: "/Home/SuggestionNumber",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: function (data) {
            $("#suggestion").val(data);
        }
    });

    $("#suggestion").prop('disabled', true);
}

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


    $("#createdBy").prop('disabled', true);
}


