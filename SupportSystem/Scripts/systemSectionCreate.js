$(document).ready(function () {



    $("#btnSectionList").on("click", function () {

        location.href = '/SystemSection/Index';

    });
    
    $("#btnSaveSection").on("click", function () {
        SaveSection();
    });

});


function SaveSection() {

    var name = $("#sectionName").val();
    var description = $("#description").val();
   


    $.ajax({
        type: "POST",
        url: "/SystemSection/SaveSection",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            Name: name,
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
    $("#sectionName").val(name);
    $("#description").val(description);

}
