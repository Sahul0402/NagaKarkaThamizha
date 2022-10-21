/* Interview Page Started */
function LoadInterviews(Page) {
    $.ajax
        ({
            url: "/Article/GetAllInterivews",
            data: { page: Page },
            contentType: "application/html; charset=utf-8",
            type: "GET",
            cache: !0,
            datatype: "html",
            success: function (response) {
                if (response != null) {
                    $("#Interviews").html(response)
                }
                else {
                    $("#Interviews").html("No Interviews List");
                }
            },
            error: function () {
                $("#Interviews").html("No Interviews List");
            }
        })
}
/* Interview Page Ended */

/* Articles Page Started*/
function LoadArticles(Page) {
    var targetControl = "#divArticles";
    $.ajax({
        url: "/Author/GetAllArticles",
        data: { page: Page },
        contentType: "application/html; charset=utf-8",
        type: "GET",
        cache: !0,
        datatype: "html",
        success: function (response) {
            if (response != null) {
                $(targetControl).html(response);
            }
            else {
                $(targetControl).html("No Article List.")
            }
        },
        error: function () {
            $(targetControl).html("Error in LoadArticles.")
        }
    })
}
/* Articles Page Ended*/

/* User Details - Social Icon Started*/

function GetUserDetailswithSocialMedia(userName) {
    var targetControl = "#divUserDetails";
    $.ajax
        ({
            url: "/Author/GetUserDetailswithSocialMedia",
            data: { Name: userName },
            contentType: "application/html; charset=utf-8",
            type: "GET",
            cache: !0,
            datatype: "html",
            success: function (response) {
                $(targetControl).html(response)
            },
            error: function () {
                $(targetControl).html("Error. Please try after some time.")
            }
        })
}

/* User Details - Social Icon End*/