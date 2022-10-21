/* Author List Page Stared*/
var userID;
$(document).ready(function () {
    var Page = hdnPage;
    if (Page == "AuthorDetails") {
        userID = hdnUserID;
        PopulateAuthorInfoByAuthorID(userID);
        PopulateAuthorBooksByAuthorID(userID);
        //LoadUserFeedback(masterPageID);
    }
    else if (Page == "AuthorList") {
        GetAllAuthor('', 1);    // Search String, Page
    }
});

function LoadUserFeedback(MainPage) {
    if ($('#hdnUserID').val() == 'undefined')
        return;

    var ChildPage = userID;
    var targetControlInfo = "#divUserFeedback";
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Author/LoadUserFeedback",
        data: { masterPageID: MainPage, childPageID: ChildPage },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            if (response != null) {
                $(targetControlInfo).html(response);
            }
        },
        failure: function (response) {
            alert("Failure to loading  UserFeedback");
        }
    });
}

function GetAllAuthor(CharSearch, Page) {
    $.ajax({
        type: "GET",
        url: "/Author/GetAllAuthor",
        data: { search: CharSearch, page: Page },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $("#divAuthorList").html(response);
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

function PopulateAuthorInfoByAuthorID(userid) {
    var targetControlInfo = "#divAuthorInfo";
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Author/GetAuthorInfoByAuthorID",
        data: { userID: userid },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            if (response != null) {
                $(targetControlInfo).html(response);
            }
        },
        failure: function (response) {
            alert("Failure in Populate Author Info...");
        },
        error: function (response) {
            alert("Error in Populate Author Info...");
        }
    });
}

function PopulateAuthorBooksByAuthorID(userid) {
    var targetControlBook = "#divAuthorBooks";

    $.ajax({
        type: "GET",
        url: "/Author/GetAuthorBooksByAuthorID",
        data: { userID: userid },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            if (response != null) {
                $(targetControlBook).html(response);
            }
        },
        failure: function (response) {
            alert(response);
            alert(response.responseText);
            alert("Failure:" + response.responseText);
        },
        error: function (response) {
            alert(response);
            alert(response.responseText);
            alert("Error in PopulateAuthorBooksByAuthorName:" + response.responseText);
        }
    });
}

$(document).on("click", "#contentPager a[href]", function (e) {
    e.preventDefault();
    GetAllAuthor('', $(e.target).text());
});

$(document).on("click", "#searchString a[href]", function (e) {
    e.preventDefault();
    GetAllAuthor($(e.target).text(), '');
});

/* Author List Page Ended*/

/* Insert Login Page Started*/
$("#submit1").click(function () {
    var dataObject = {
        Name: document.getElementById("Name").value,
        Mobile: document.getElementById("Mobile").value,
        EMail: document.getElementById("EMail").value,
        Password: document.getElementById("Password").value,
        Comments: document.getElementById("Comments").value,
        mainPage: masterPageID,
        childPage: $('#UserID').val()
    };
    try {
        $.ajax({
            url: '/User/AddFeedbackUser',
            type: 'POST',
            data: {},
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            processData: false,
            success: function (data) {
                if (data.toString() == "Success") {
                    $("#Name").val('');
                    $("#EMail").val('');
                    $("#Mobile").val('');
                    $("#Comments").val('');
                    $("#content").html("<div class='success'>" + data + "</div>");
                }
                else {
                    $("#content").html("<div class='failed'>" + data + "</div>");
                }
            },
            error: function () {
                alert("Error Occured. Please contact Administrator.");
            }
        });
    }
    catch (e) {
        alert(e.message);
    }
});

/* Insert Login Page Ended*/

/* Author Series Started */

function LoadAuthorSeries(Page) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Author/GetAllAuthorSeries",
        data: { page: Page },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $("#divAuthorSeries").html(response);
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

/* Author Series Ended */


$("#submit").click(function () {
    var Name = document.getElementById("Name").value;
    var Mobile = document.getElementById("Mobile").value;
    var EMail = document.getElementById("EMail").value;
    var Password = document.getElementById("Password").value;
    var Comments = document.getElementById("Comments").value;
    var MainPage = masterPageID;
    var ChildPage = $('#UserID').val();

    $.ajax({
        cache: false,
        type: "GET",
        url: "/ContactUs/Feedback",
        data: { name: Name, mobile: Mobile, EMail: EMail, password: Password, comments: Comments, MasterPageID: MainPage, ChildPageID: ChildPage },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            ClearFeedbackControls();
            alert('data added succesfully');
            if (response.isRedirect) {
                alert(response.redirectUrl);
                window.location.href = response.redirectUrl;
            }
        },
        failure: function (response) {
            alert(response);
            alert(response.responseText);
        },
        error: function (response) {
            alert(response);
            alert(response.responseText);
        }
    });
});