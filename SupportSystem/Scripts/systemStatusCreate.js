$(document).ready(function () {



    $("#btnStatusList").on("click", function () {

        location.href = '/SystemStatus/Index';

    });

    $("#btnSaveStatus").on("click", function () {
        SaveStatus();
    });

});


function SaveStatus() {

    var name = $("#statusName").val();
    var description = $("#statusDescription").val();



    $.ajax({
        type: "POST",
        url: "/SystemStatus/SaveStatus",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            StatusName: name,
            Description: description
        }),
        success: function (response) {
            if (response !== null) {
                alert(response);
            } else {
                alert("Error");
            }
        }
    });

    name = "";
    description = "";
    $("#statusName").val(name);
    $("#statusDescription").val(description);

}
