//show other book reivew(s) in bottom of the Page.
$(document).ready(function () {

    try {
        var bookID = $("#BookDetail_Books_BookID").val();

        $.ajax({
            type: "GET",
            url: "/BooksReview/GetAllBookReviewsByBookID",
            data: { 'bookId': bookID },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                $('#divBooksReview').hide();
                alert(response.responseText);
            }
        });

    } catch (e) {
        alert(e.message.toString());
    }
});

function OnSuccess(response) {
    var model = response;
    var row = $(".tblBooksReview tr:last-child").removeAttr("style").clone(true);
    $(".tblBooksReview tr").not($(".tblBooksReview tr:first-child")).remove();
    $.each(model, function () {
        $("td", row).eq(0).html(model[0].BookID);
        $("td", row).eq(1).html(model[0].BookDetail.BookDetailsID);
        $("td", row).eq(2).html(model[0].Header);
        $("td", row).eq(3).html(model[0].UserName);
        var createdDate = parseInt(model[0].CreatedDate.replace("/Date(", "").replace(")/", ""));
        var newDate = new Date(createdDate).toLocaleDateString("en-GB");
        var time = new Date(createdDate).toLocaleTimeString("en-GB");
        $("td", row).eq(4).html(newDate + "  " + time);
        //$("td", row).eq(4).html(model[0].CreatedDate);
        $(".tblBooksReview").append(row);
        row = $(".tblBooksReview tr:last-child").clone(true);
    });
    if (model.length == 0) {
        $('#divBooksReview').hide();
    }
}


function FillUser() {
    var UserID = $("#UserType").val();
    if (UserID == "1" || UserID == "6") {
        $('#magazine').hide();
    }
    else if (UserID == "M") {
        $('#Users').hide();
    }

    $.ajax({
        url: '/User/GetUsersByTypeID',
        type: "GET",
        dataType: "JSON",
        data: { userTypeId: UserID },
        success: function (users) {
            $("#UserID").html(""); // clear before appending new list
            $.each(users, function (i, User) {

                $('#UserID').append(
                    $('<option></option>').val(User.UserID).html(User.UserName));
            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("FillUser Error" + XMLHttpRequest.status);
            alert(XMLHttpRequest.responseText);
        }
    });
}

$(function () {
    var currentYear = (new Date).getFullYear();
    var lastYear = (new Date).getFullYear() - 75;
    $("#SourceDate").datepicker({
        changeMonth: true,
        changeYear: true,
        showOn: "button",
        buttonImage: "/Images/calendar.gif",
        buttonImageOnly: true,
        dateFormat: 'dd-mm-yy',
        yearRange: lastYear + ":" + currentYear
    });
});