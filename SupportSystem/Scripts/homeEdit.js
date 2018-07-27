$(document).ready(function () {
    
    homeEditId = sessionStorage.homeEditId;

    SelectStatuses(false);
    SelectCategories(false);
    SelectPriorities(false);
    SelectSeverities(false);
    SelectSections(false);

    userName = $(".username").attr("id");

    PrikaziKomentare(homeEditId);

    PopulateHomeEdit(homeEditId);
   

    $("#btnMainList").on("click", function () {
        location.href = '/Home/Index';
        ClearTempComments();
    });


    $("#btnSaveEditedSugg").on("click", function () {

        // save suggestion with comments in db
        Validacije();
        SaveEditedSuggestionWithComments();
    });




});

var userName;

var homeEditId;

function PopulateHomeEdit(id){
    $.ajax({
        type: "POST",
        url: "/Home/PopulateHomeEdit",
        data: "id=" + id,
        success: function (response) {
            if (response !== null) {


                $("#suggestion").val(response.Number);
                $("#suggestion").prop('disabled', true);
                $("#createdBy").val(response.User);
                $("#createdBy").prop('disabled', true);

                $("#status").val(response.IdStatus).trigger("change");
                $("#category").val(response.IdKategorija).trigger("change");
                $("#priority").val(response.IdPriority).trigger("change");
                $("#sysSection").val(response.IdSystemSection).trigger("change");
                $("#severity").val(response.IdSeverity).trigger("change");
                $("#createdOn").dxDateBox({
                    value: response.strCreatedOn === "" ? null : new Date(response.strCreatedOn),
                    width: 500,
                    readOnly: true,
                    displayFormat: "dd.MM.yyyy"
                });
                $("#title").val(response.Title);
                $("#steps").val(response.Steps);
                $("#notes").val(response.Notes);
                $("#acceptedOn").dxDateBox({
                    value: response.strAcceptedOn === "" ? null : new Date(response.strAcceptedOn),
                    width: 500,
                    displayFormat: "dd.MM.yyyy"
                });

                $("#dueOn").dxDateBox({
                    value: response.strDueOn === "" ? null : new Date(response.strDueOn),
                    width: 500,
                    displayFormat: "dd.MM.yyyy"
                });

                $("#resolvedOn").dxDateBox({
                    value: response.strResolvedOn === "" ? null : new Date(response.strResolvedOn),
                    width: 500,
                    displayFormat: "dd.MM.yyyy"
                });
            

            } else {
                alert("Error");
            }
        }
    });
}


function SaveEditedSuggestionWithComments() {

    Validacije(false);

    if (boolValidacije) {
        //var suggNo = $("#suggestion").val();
        //var createdBy = $("#createdBy").val();
        //var createdOn = $("#createdOn").dxDateBox("instance").option("value").split('/').reverse().join('-');
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
            url: "/Home/SaveEditedSuggestion",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                Id: homeEditId,
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
                    toastr["success"]("Suggestion is edited!");

                    if (commentsForEmial !== "") {
                        SendCommentsMail("arandjic@gmail.com", "New Comments from Edited Suggestion.", commentsForEmial);
                        commentsForEmial = "";
                    }                     // <----------------------------------------------- send email
                    ClearTempComments();
                } else {
                    alert("Error");
                    toastr.error("Oops! There was an error during editing a suggestion.");
                }
            }
        });

    }

};
