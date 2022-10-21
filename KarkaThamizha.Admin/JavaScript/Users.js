$(document).ready(function () {
    //AutoComplete
});

function FillStates() {
    var countryID = document.getElementById("UserDetail_CountryID").value;

    $.ajax({
        url: '/State/FillStates',
        type: "GET",
        dataType: "JSON",
        data: { countryid: countryID },
        success: function (states) {
            $("#UserDetail_StateID").html(""); // clear before appending new list
            $("#UserDetail_StateID").append($('<option></option>').val(0).html('...Select State...'));
            $.each(states, function (i, State) {
                $('#UserDetail_StateID').append(
                        $('<option></option>').val(State.StateID).html(State.State));
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //$("#grid").html("Error");
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.responseText);
        }
    });
}

function FillCities() {
    var stateID = document.getElementById("UserDetail_StateID").value;
    $.ajax({
        url: '/City/FillCities',
        type: "GET",
        dataType: "JSON",
        data: { stateid: stateID },
        success: function (Cities) {
            $("#UserDetail_CityID").html(""); // clear before appending new list
            $("#UserDetail_CityID").append($('<option></option>').val(0).html('...Select City...'));
            $.each(Cities, function (i, City) {
                $('#UserDetail_CityID').append(
                    $('<option></option>').val(City.CityID).html(City.City));
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //$("#grid").html("Error");
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.responseText);
        }
    });
}

$("#User").blur(function () {
    $("#lblStatus").html("");
    var user = $("#Initial").val() + $("#User").val();
    if (user == null || user == "")
        return;
    $("#lblStatus").html("wait...");
    $.ajax({
        type: "GET",
        url: "/User/CheckUser",
        data: { username: user },// user name value 
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response);
        },
    });
});


//function OnSuccess 
function OnSuccess(response) {
    var targetControlInfo = "#lblStatus";
    if (response) {
        $(targetControlInfo).show();
        $(targetControlInfo).css("color", "red");
        $(targetControlInfo).html("User Name already exists.");
    }
    else {
        $(targetControlInfo).html("");
    }
}

$("#Name").autocomplete({
    source: function (request, response) {
        $.ajax({
            url: "/User/AutoCompleteUser",
            data: "{ 'searchPrefix': '" + request.term + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                response($.map(data, function (item) {
                    return alert('User already exists')
                }))
            },
            error: function (response) {
                alert("error :- " + response.responseText);
            },
            failure: function (response) {
                alert("failure :- " + response.responseText);
            }
        });
    },
    select: function (e, i) {
        var text = this.value.split(/;\s*/);
        text.pop();
        text.push(i.item.value);
        text.push("");
        this.value = text.join("; ");
        return false;
    },
    minLength: 1
});

//$("#txtSearch").autocomplete({
//    source: function (request, response) {
//        $.ajax({
//            url: '@Url.Action("AutoCompleteUser", "User")',
//            data: "{ 'searchPrefix': '" + request.term + "'}",
//            dataType: "json",
//            type: "POST",
//            contentType: "application/json; charset=utf-8",
//            success: function (data) {
//                response($.map(data, function (item) {
//                    return item;
//                }))
//            },
//            error: function (response) {
//                alert("error :- " + response.responseText);
//            },
//            failure: function (response) {
//                alert("failure :- " + response.responseText);
//            }
//        });
//    },
//    select: function (e, i) {
//        var text = this.value.split(/;\s*/);
//        text.pop();
//        text.push(i.item.value);
//        text.push("");
//        this.value = text.join("; ");
//        return false;
//    },
//    minLength: 1
//});

$(function () {
    var currentYear = (new Date).getFullYear() - 15;
    var lastYear = (new Date).getFullYear() - 155;
    $("#UserDetail_DOB").datepicker({
        changeMonth: true,
        changeYear: true,
        showOn: "button",
        buttonImage: "/Images/calendar.gif",
        buttonImageOnly: true,
        dateFormat: 'dd-mm-yy',
        yearRange: lastYear + ":" + currentYear
    });
});
$(function () {
    var currentYear = (new Date).getFullYear();
    var lastYear = (new Date).getFullYear() - 100;
    $("#UserDetail_DOD").datepicker({
        changeMonth: true,
        changeYear: true,
        showOn: "button",
        buttonImage: "/Images/calendar.gif",
        buttonImageOnly: true,
        dateFormat: 'dd-mm-yy',
        yearRange: lastYear + ":" + currentYear
    });
});

//Searching User
$(document).on("click", "input[type='submit']", function () {
    //GetAllUsers(1, $("#txtSearch").val()); need to create new partial control for binding the user like Books/Publisher
});

function GetAllUsers(PageSize, Search) {
    var targetControl = "#divBooks";
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Books/GetAllBooks",
        data: { page: PageSize, txtSearch: Search, ddlAuthorID: AuthorID },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $(targetControl).html(response);
            setTimeout(LoadUserAttributes, 1000);
        },
        failure: function (response) {
            alert(response);
            alert(response.responseText);
            alert("Failure:" + response.responseText);
        },
        error: function (response) {
            alert(response);
            alert(response.responseText);
            alert("Error:" + response.responseText);
        }
    });
}