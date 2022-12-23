$(document).ready(function () {
    var pageName = document.location.href.match(/[^\/]+$/)[0];

    var urlParamValue = GetParameterValues('UserID');
    if (urlParamValue > 0) {
        PopulateUserInfoByUserId(urlParamValue);
        BooksReviewUserByID(urlParamValue, 1, "Grid");
    }
});

//#region Books Review By User ID

function PopulateUserInfoByUserId(id) {
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
    var targetControl = "#divUserBooksReview";

    $.ajax({
        cache: false,
        type: "GET",
        url: "/User/UserReviews",
        data: { userId: id, page: paging, viewType: viewIn },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $(targetControl).empty();
            $(targetControl).html(response);
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

// Grid View
function OnGridViewClick() {
    $("#grid").css('background-color', '#feb500');
    $("#list").css('background-color', '#f1f1f1');
    $("#table").css('background-color', '#f1f1f1');

    var urlParamValue = GetParameterValues('UserID');
    BooksReviewUserByID(urlParamValue, 1, "Grid");
}

function OnListViewClick() {
    $("#grid").css('background-color', '#f1f1f1');
    $("#table").css('background-color', '#f1f1f1');
    $("#list").css('background-color', '#feb500');

    var urlParamValue = GetParameterValues('UserID');
    BooksReviewUserByID(urlParamValue, 1, "List");
}

// Table View
function OnTableViewClick() {
    $("#grid").css('background-color', '#f1f1f1');
    $("#list").css('background-color', '#f1f1f1');
    $("#table").css('background-color', '#feb500');

    var urlParamValue = GetParameterValues('UserID');
    BooksReviewUserByID(urlParamValue, 1, "Table");
}

