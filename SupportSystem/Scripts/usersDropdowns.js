
var roles = [];
function SelectRoles(isCreate) {


    if (isCreate) {
        $('#userRole').select2({
            ajax: {
                url: "/Users/RolesList",

                processResults: function (data) {

                    return {

                        results: $.map(data, function (item) {

                            return {
                                text: item.value,
                                id: item.key
                            };
                        })

                    };
                }
            }
        });
    } else {
        $.ajax({
            type: "GET",
            url: "/Users/RolesList",
            dataType: "json",
            success: function (data) {

                roles = data;

                roles.forEach(function (e) {
                    e.id = e.key;
                    delete e.key;
                    e.text = e.value;
                    delete e.value;
                });

                $("#userRole").select2({
                    data: roles
                });


            }
        });
    }   
}

var activity = [
    {
        id: true,
        text: "Active"
    },
    {
        id: false,
        text: "Not Active"
    }
];
function SelectAcitivity() {
    $("#activity").select2({
        data: activity
    });
}