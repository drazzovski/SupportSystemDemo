
var selectStatuses = []
function SelectStatuses(Create) {

    if (Create) {
        $('#status').select2({
            ajax: {
                url: "/Home/StatusList",

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
            url: "/Home/StatusList",
            dataType: "json",
            success: function (data) {

                selectStatuses = data;

                selectStatuses.forEach(function (e) {
                    e.id = e.key;
                    delete e.key;
                    e.text = e.value;
                    delete e.value;
                });

                $("#status").select2({
                    data: selectStatuses
                });            
            }
        });  
    } 
}


var selectCategories = []
function SelectCategories(Create) {

    if (Create) {
        $('#category').select2({
            ajax: {
                url: "/Home/CategoriesList",

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
            url: "/Home/CategoriesList",
            dataType: "json",
            success: function (data) {

                selectCategories = data;

                selectCategories.forEach(function (e) {
                    e.id = e.key;
                    delete e.key;
                    e.text = e.value;
                    delete e.value;
                });

                $("#category").select2({
                    data: selectCategories
                });               
            }
        });  
    }   
}


var selectPriorities = []
function SelectPriorities(Create) {


    if (Create) {
        $('#priority').select2({
            ajax: {
                url: "/Home/PrioritiesList",

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
            url: "/Home/PrioritiesList",
            dataType: "json",
            success: function (data) {

                selectPriorities = data;

                selectPriorities.forEach(function (e) {
                    e.id = e.key;
                    delete e.key;
                    e.text = e.value;
                    delete e.value;
                });

                $("#priority").select2({
                    data: selectPriorities
                });               
            }
        });  
    }

  
}


var selectSeverities = []
function SelectSeverities(Create) {

    if (Create) {
        $('#severity').select2({
            ajax: {
                url: "/Home/SeveritiesList",

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
            url: "/Home/SeveritiesList",
            dataType: "json",
            success: function (data) {

                selectSeverities = data;

                selectSeverities.forEach(function (e) {
                    e.id = e.key;
                    delete e.key;
                    e.text = e.value;
                    delete e.value;
                });

                $("#severity").select2({
                    data: selectSeverities
                });               
            }
        });  
    }  
}


var selectSections = []
function SelectSections(Create) {

    if (Create) {

        $('#sysSection').select2({
            ajax: {
                url: "/Home/SectionList",

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
            url: "/Home/SectionList",
            dataType: "json",
            success: function (data) {

                selectSections = data;

                selectSections.forEach(function (e) {
                    e.id = e.key;
                    delete e.key;
                    e.text = e.value;
                    delete e.value;
                });

                $("#sysSection").select2({
                    data: selectSections
                });               
            }
        });  
    }

}

