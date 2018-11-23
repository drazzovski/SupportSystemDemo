$(document).ready(function () {

    SelectRoles(true);

    $("#btnUsersList").on("click", function () {
        location.href = '/Users/Index';
    });

    $("#btnSaveUser").on("click", function () {
        SaveUser();
    });

    $('#password, #passwordConfirm').on('keyup', function () {


        if ($('#password').val() === $('#passwordConfirm').val()) {
            $('#message').html('Matching').css('color', 'green');
            boolMatching = true;
        } else {
            $('#message').html('Not Matching').css('color', 'red');

            boolMatching = false;

        }
            
            
    });


    $("#btnSaveUser").on("click", function () {
        SaveUser();
    });

    $("#sidebar-wrapper").height($("#page-content-wrapper").height());

});

var boolMatching;

function SaveUser() {

    UsersValidacije(true);

    if (boolUsersValidacije && boolMatching) {

        var firstName = $("#firstName").val();
        var lastName = $("#lastName").val();
        var userName = $("#userName").val();
        var email = $("#email").val();
        var address = $("#address").val();
        var city = $("#city").val();
        var country = $("#country").val();
        var roleId = $("#userRole").select2("data")[0].text;
        var password = $("#password").val();

        $.ajax({
             type: "POST",
             url: "/Account/SaveUser",
             contentType: "application/json; charset=utf-8",
             data: JSON.stringify({
                 FirstName: firstName,
                 LastName: lastName,
                 UserName: userName,
                 Email: email,
                 Address : address,
                 City: city,
                 Country: country,
                 SelectedRole: roleId,
                 Password: password
             }),
             success: function (response) {
                 if (response !== null) {
                     alert(response);
                 } else {
                     alert("Error");
                 }  
             }
        });
    }
}