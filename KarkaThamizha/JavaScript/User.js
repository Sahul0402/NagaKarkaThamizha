$(document).ready(function () {
    debugger;
    var pageName = document.location.href.match(/[^\/]+$/)[0];

    var urlParamValue = GetParameterValues('UserID');
    if (urlParamValue > 0) {
        PopulateUserInfoByUserId(urlParamValue);
        BooksReviewUserByID(urlParamValue, 1, "#UserBooksReviewInGrid");
    }
});

//#region Books Review By User ID

function PopulateUserInfoByUserId(id) {
    debugger;
    var targetControlInfo = "#divUserInfo";

    $.ajax({
        cache: false,
        type: "GET",
        url: "/User/UserInfoByUserId",
        data: { userId: id },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            if (response != null) {
                $(targetControlInfo).html(response);
            }
            else if (response == null) {
                alert("Something went wrong. Please logout and login again.");
            }
        },
        failure: function (response) {
            alert("Failure in Populate User Info...");
        },
        error: function (response) {
            alert("Error in Populate User Info...");
        }
    });
}

function BooksReviewUserByID(id, paging, viewIn) {
    var targetControl = viewIn;

    $.ajax({
        cache: false,
        type: "GET",
        url: "/User/UserReviews",
        data: { userId: id, page: paging, view: viewIn },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $(targetControl).html('');
            $(targetControl).html(response);
            $(targetControl).show();

            if (viewIn == "#UserBooksReviewInTable") {
                $("#UserBooksReviewInGrid").hide();
                $("#UserBooksReviewInList").hide();
            }
            if (viewIn == "#UserBooksReviewInList") {
                $("#UserBooksReviewInTable").hide();
                $("#UserBooksReviewInGrid").hide();
            }
            else {
                $("#UserBooksReviewInTable").hide();
                $("#UserBooksReviewInList").hide();
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
            alert("Error:" + response.responseText);
        }
    });
}

function GetParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}

function GetTargetControl(controlName) {
    switch (controlName) {
        case "G":
            $("#UserBooksReviewInGrid").show();
            $("#UserBooksReviewInGrid").css("display:block");
            $("#UserBooksReviewInList").hide();
            $("#UserBooksReviewInTable").hide();
            return "#UserBooksReviewInGrid";
        case "L":
            $("#UserBooksReviewInGrid").hide();
            $("#UserBooksReviewInList").show();
            $("#UserBooksReviewInTable").hide();
            $("#UserBooksReviewInList").css("display:block");
            return "#UserBooksReviewInList";
        case "T":
            $("#UserBooksReviewInGrid").hide();
            $("#UserBooksReviewInList").hide();
            $("#UserBooksReviewInTable").show();
            $("#UserBooksReviewInTable").css("display:block");
            return "#UserBooksReviewInTable";
    }
}

$(document).ajaxStart(function () {
    $("#wait").css("display", "block");
});

$(document).ajaxComplete(function () {
    $("#wait").css("display", "none");
});

$(document).on("click", "#contentPager a[href]", function (e) {
    e.preventDefault();
    BooksReviewUserByID($("#ShowReviewIn").val(), $(e.target).text());
});

//$("#ShowReviewIn").change(function () {
//    alert($("#ShowReviewIn").val())
//});

//$('#ShowReviewIn').trigger('change')
//{
//    BooksReviewUserByID($("#ShowReviewIn").val(), 1);
//}
//#endregion Books Review By User ID

//$("#ShowReviewIn").on("change", function () {
//    var value = $("#ShowReviewIn").val();
//    alert(value);
//});

// List View
function listViewlistView() {
    $("#grid").css('background-color', '#f1f1f1');
    $("#list").css('background-color', '#feb500');

    BooksReviewUserByID("#UserBooksReviewInList", 1);
}


// Grid View
function gridView() {
    $("#grid").css('background-color', '#feb500');
    $("#list").css('background-color', '#f1f1f1');

    BooksReviewUserByID("#UserBooksReviewInGrid", 1);
}

