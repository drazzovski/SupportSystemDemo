$(document).ready(function () {

    usersEditId = sessionStorage.usersEditId;
    SelectRoles(false);
    SelectAcitivity();

    PopulateUsersEdit(usersEditId);

    $("#btnUsersList").on("click", function () {
        location.href = '/Users/Index';
    });


    $("#btnSaveEditedUser").on("click", function () {
        // save suggestion with comments in db
        SaveEditedUser();
    });

});

var usersEditId;

function PopulateUsersEdit(id) {
    $.ajax({
        type: "POST",
        url: "/Users/PopulateUsersEdit",
        data: "id=" + id,
        success: function (response) {
            if (response !== null) {
                

                $("#firstName").val(response.FirstName);
                $("#lastName").val(response.LastName);
                $("#userName").val(response.UserName);
                $("#password").prop('disabled', true);
                $("#passwordConfirm").prop('disabled', true);
                $("#address").val(response.Address);
                $("#city").val(response.City);
                $("#country").val(response.Country);
                $("#phone").val(response.PhoneNumber);
                $("#userRole").val(response.IdRole).trigger("change");
                $("#email").val(response.Email);
                $("#activity").val(`${response.isActive}`).trigger("change");

                $("#sidebar-wrapper").height($("#page-content-wrapper").height());

            } else {
                alert("Error");
            }
        }
    });
}




function SaveEditedUser() {

    UsersValidacije();

    if (boolUsersValidacije) {
        //var suggNo = $("#suggestion").val();
        //var createdBy = $("#createdBy").val();
        //var createdOn = $("#createdOn").dxDateBox("instance").option("value").split('/').reverse().join('-');
         var firstName = $("#firstName").val();
         var lastname = $("#lastName").val();
         var userName = $("#userName").val();
         var address =  $("#address").val();
         var city = $("#city").val();
         var country = $("#country").val();
         var phoneNumber = $("#phone").val();
         var role = $("#userRole").select2("data")[0].text;
         var email = $("#email").val();

        $.ajax({
            type: "POST",
            url: "/Account/SaveEditedUser",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                Id: usersEditId,
                FirstName: firstName,
                LastName: lastname,
                UserName: userName,
                Address: address,
                City: city,
                Country: country,
                PhoneNumber: phoneNumber,
                SelectedRole: role,
                Email: email,
                isActive: $("#activity").val()
            }),
            success: function (response) {
                if (response !== null) {
                    //alert(response);
                    toastr.success("User is edited!");
                } else {
                    alert("Error");
                    toastr.error("Oops! There was an error during editing an user.");
                }
            }
        });

    }

};