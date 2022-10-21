$(document).ready(function () {
    GetAllBookOffer(1);
    //SetGetPageViewCount(masterPageID);
    //LoadComments();
});

//Book Release Page
function GetAllBookOffer(PageSize) {
    var targetControl = "#divOffer";

    $.ajax({
        cache: false,
        type: "GET",
        url: "/BookPreRelease/GetAllBookOffer",
        data: { page: PageSize },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $(targetControl).html(response);
        },
        failure: function (response) {
            alert("Failure in GetAllBookOffer:" + response.responseText);
        },
        error: function (response) {
            alert("No Records found.");
        }
    });
}

$(document).on("click", "#contentPager a[href]", function (e) {
    e.preventDefault();
    GetAllBookOffer($(e.target).text());
});