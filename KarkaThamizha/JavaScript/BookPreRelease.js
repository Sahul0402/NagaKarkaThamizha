$(document).ready(function () {
    GetAllBookRelease(1);
    //SetGetPageViewCount(masterPageID);
    //LoadComments();
});

//Book Release Page
function GetAllBookRelease(PageSize) {
    var targetControl = "#divBookPreRelease";

    $.ajax({
        cache: false,
        type: "GET",
        url: "/BookPreRelease/GetAllBookRelease",
        data: { page: PageSize },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $(targetControl).html(response);
        },
        failure: function (response) {
            alert("Failure in GetAllBookRelease:" + response.responseText);
        },
        error: function (response) {
            alert("No Records found.");
        }
    });
}

$(document).on("click", "#contentPager a[href]", function (e) {
    e.preventDefault();
    GetAllBookRelease($(e.target).text());
});

$("#submit").click(function () {
    var mainPage;
    var name = document.getElementById("Name").value;
    var mobile = document.getElementById("Mobile").value;
    var mail = document.getElementById("EMail").value;
    mainPage = masterPageID;
    alert(name);
});