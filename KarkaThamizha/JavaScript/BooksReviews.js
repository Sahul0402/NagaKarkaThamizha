$(document).ready(function () {
    var pageName = document.location.href.match(/[^\/]+$/)[0];
    var reviewBy = hdnPageReview;
    if (reviewBy == "User")
        PopulateBooksReviewByUsers(1);
    else
        PopulateBooksReviewByWriter(1);
});

function PopulateBooksReviewByUsers(page) {
    var targetControl = "#divUserReview";
    $.ajax({
        cache: false,
        type: "GET",
        url: "/BooksReview/UsersReview",
        data: { page: page },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $(targetControl).show();
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

function PopulateBooksReviewByWriter(page) {
    var targetControl = "#divWriterReview";
    $.ajax({
        cache: false,
        type: "GET",
        url: "/BooksReview/WritersReview",
        data: { page: page },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $(targetControl).show();
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

$(document).on("click", "#contentPager a[href]", function (e) {
    e.preventDefault();
    if (hdnPageReview == "User")
        PopulateBooksReviewByUsers($(e.target).text());
    else
        PopulateBooksReviewByWriter($(e.target).text());
});
/* Magazine Page Start*/

function SetMagazineBottomColor() {
    try {
        var totalcount = document.getElementById('articleCount').value;

        for (i = 0; i < totalcount; i++) {
            if (totalcount % i == NaN) { document.getElementById('SetColor').className = 'material-card Red'; }
            else if (totalcount % i == 0) {
                document.getElementById('SetColor').className = 'material-card Blue';
            }
            else if (totalcount % i == 1) {
                document.getElementById('SetColor').className = 'material-card Grey';
            }
            else if (totalcount % i == 2) {
                document.getElementById('SetColor').className = 'material-card Pink';
            }
        }
    }
    catch (e) {
        alert(e.message.toString());
    }
}

$('.material-card > .mc-btn-action').click(function () {
    var card = $(this).parent('.material-card');
    var icon = $(this).children('i');
    icon.addClass('fa-spin-fast');

    if (card.hasClass('mc-active')) {
        card.removeClass('mc-active');

        window.setTimeout(function () {
            icon
                .removeClass('fa-arrow-left')
                .removeClass('fa-spin-fast')
                .addClass('fa-bars');

        }, 800);
    } else {
        card.addClass('mc-active');

        window.setTimeout(function () {
            icon
                .removeClass('fa-bars')
                .removeClass('fa-spin-fast')
                .addClass('fa-arrow-left');

        }, 800);
    }
});
/* Magazine Page End*/

