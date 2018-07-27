var boolUsersValidacije = true;
function UsersValidacije(Create) {

    boolUsersValidacije = true;

    $("#userRole").next().children(".selection").children(0).css("border-color", "")
    $("#userName").css("border-color", "");
    $("#email").css("border-color", "");
    $("#password").css("border-color", "");

    $("#userRole").siblings("span.redcolor").remove();
    $("#userName").siblings("span.redcolor").remove();
    $("#email").siblings("span.redcolor").remove();
    $("#password").siblings("span.redcolor").remove();

    if ($("#userRole").val() === null) {
        $("<span>Choose user role!</span>").appendTo($("#userRole").parent()).addClass("redcolor");
        $("#userRole").next().children(".selection").children(0).css("border-color", "red");
        boolUsersValidacije = false;
    }

    if ($("#userName").val().length < 3) {
        $("<span>Must have 3 or more characters!</span>").appendTo($("#userName").parent()).addClass("redcolor");
        $("#userName").css("border-color", "red");
        boolUsersValidacije = false;
    }

    if ($("#email").val().length < 1) {
        $("<span>E-Mail is required!</span>").appendTo($("#email").parent()).addClass("redcolor");
        $("#email").css("border-color", "red");
        boolUsersValidacije = false;
    }

    if (Create) {
        if ($("#password").val().length < 6) {
            $("<span>Password must contain 6 or more characters!</span>").appendTo($("#password").parent()).addClass("redcolor");
            $("#password").css("border-color", "red");
            boolUsersValidacije = false;
        }
    }   

}