$(document).ready(function (e) {
    var pageName = document.location.href.match(/[^\/]+$/)[0];
    if (pageName == "AuthorSeries") {        
        LoadSeries(1, 0);
        BindSeriesType();
    }
    else {
        BindArticleTypes();
        BindArticlesByArticleID(1, 1);
    }    
});

//#region Articles
function BindArticleTypes() {
    $.ajax({
        type: "GET",
        url: '/Article/GetArticleType',
        data: {},
        //contentType: "application/html; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#ArticleType_ArticleTypeID').empty();
            $('#ArticleType_ArticleTypeID').append("<option value='0'>Select Article Type</option>");
            for (var i = 0; i < data.length; i++) {
                var opt = new Option(data[i].ArticleType, data[i].ArticleTypeID);
                $('#ArticleType_ArticleTypeID').append(opt);
            }            
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        },
    });
}

$('#ArticleType_ArticleTypeID').change(function (e) {
    debugger;
    var articleType_ID = $('#ArticleType_ArticleTypeID').val();
    var articleType = $("#ArticleType_ArticleTypeID option:selected").text();
    if (document.location.href.match(/[^\/]+$/)[0] == "AddArticle") {
        ShowHideControls(articleType);
    }
    else if (document.location.href.match(/[^\/]+$/)[0] == "Article") {
        BindArticlesByArticleID(1, articleType_ID);
    }
});

function BindArticlesByArticleID(PageSize, articleTypeID) {
    var targetControl = "#divArticle";
    $.ajax({
        cache: false,
        type: "GET",
        url: '/Article/GetArticlesByArticleID',
        data: { page: PageSize, articleType_ID: articleTypeID },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $(targetControl).html(response);
        },
        failure: function (response) {
            alert("LoadArticles Failure:" + response);
            alert("LoadArticles Failure:" + response.responseText);
        },
        error: function () {
            alert("LoadArticles Error");
        }
    });
}

function ShowHideControls(articleType) {
    debugger;
    switch (articleType) {
        case "அணிந்துரைகள்":
            $("#divReviewBy").hide();
            $("#divReviewBy").css("display:block");
            break;

        case "இலக்கியம்":
            $("#divReviewBy").hide();
            $("#divReviewBy").css("display:block");
            break;

        case "உரைகள்":
            $("#divReviewBy").hide();
            $("#divReviewBy").css("display:block");
            break;

        case "எழுத்தாளர் சுயவிபரம்":
            $("#divAuthor").show();
            $("#divAuthor").css("display:block");
            $("#divMagazine").hide();
            $("#divMagazine").css("display:none");
            $("#divInterviewBy").hide();
            $("#divInterviewBy").css("display:none");
            $("#divWeekDaysID").hide();
            $("#divWeekDaysID").css("display:none");
            $("#divReviewBy").show();
            $("#divReviewBy").css("display:block");
            break;

        case "எழுத்தாளர்கள் தொடர்கள்":
            $("#divInterviewBy").hide();
            $("#divInterviewBy").css("display:none");
            $("#ckEditDesc").hide();
            $("#ckEditDesc").css("display:none");
            $("#divReviewBy").hide();
            $("#divReviewBy").css("display:none");
            $("#divEndDate").show();
            $("#divEndDate").css("display:block");
            $("#divWeekDaysID").show();
            $("#divWeekDaysID").css("display:block");
            $("#WeekDays_WeekDaysID").rules("remove", "required")
            $("#divImage").hide();
            $("#divImage").css("display:none");
            $("#divHeader").show();
            $("#divHeader").css("display:block");
            break;

        case "எழுத்துக்கு அப்பால்":
            $("div[id*='ckEditDesc']").show();
            $("#divImage").hide();
            break;

        case "எனக்குப் பிடித்த நூல்கள்":
            $("div[id*='ckEditDesc']").show();
            $("#divImage").hide();
            break;

        case "கட்டுரைகள்":
            $("#divReviewBy").hide();
            $("#divReviewBy").css("display:block");
            $("#divImage").hide();
            break;

        case "பொங்கல் மலர்":
        case "தீபாவளி மலர்":
            $("div[id*='ckEditDesc']").show();
            $("#divAuthor").hide();
            $("#divAuthor").css("display:none");
            $("#divMagazine").show();
            $("#divMagazine").css("display:block");
            $("#divReviewBy").hide();
            $("#divReviewBy").css("display:none");
            $("#divSeriesType").hide();
            $("#divSeriesType").css("display:none");
            $("#divEndDate").hide();
            $("#divEndDate").css("display:none");
            $("#Author_UserID").rules("remove", "required")
            break;

        case "நம்பிக்கை எழுத்தாளர்":
            $("#divAuthor").show();
            $("#divAuthor").css("display:none");
            $("#divMagazine").show();
            $("#divMagazine").css("display:none");
            $("#divReviewBy").hide();
            $("#divReviewBy").css("display:block");
            $("#divEndDate").hide();
            $("#divEndDate").css("display:block");
            $("#divInterviewBy").hide();
            $("#divInterviewBy").css("display:block");
            $("#divWeekDaysID").hide();
            $("#divWeekDaysID").css("display:block");
            break;

        case "நான் ஏன் எழுதுகிறேன்?":
            break;

        case "புத்தக பரிந்துரை":
            $("#divWeekDaysID").hide();
            $("#divWeekDaysID").css("display:block");
            $("#divInterviewBy").hide();
            $("#divInterviewBy").css("display:block");
            $("#divReviewBy").hide();
            $("#divReviewBy").css("display:block");
            break;

        case "பேட்டிகள்":
            $("#divWeekDaysID").hide();
            $("#divWeekDaysID").css("display:block");
            $("#divReviewBy").hide();
            $("#divReviewBy").css("display:block");
            break;

        case "மதிப்பீடுகள்":
            $("#divReviewBy").hide();
            $("#divReviewBy").css("display:block");
            break;

        //case "முன் வெளியீட்டுத் திட்டம்":
        //    $("#divReviewBy").hide();
        //    $("#divReviewBy").css("display:block");
        //    break;

        case "Select Article Type":
            $("#divAuthor").show();
            $("#divAuthor").css("display:none");
            $("#divMagazine").hide();
            $("#divMagazine").css("display:block");
            $("#divReviewBy").hide();
            $("#divReviewBy").css("display:block");
            break;
    }
}

//#endregion Articles

//#region Author Series
function BindSeriesType() {
    $.ajax({
        type: "GET",
        url: '/Series/GetSeriesType',
        data: {},
        //contentType: "application/html; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#Series_SeriesTypeID').empty();
            $('#Series_SeriesTypeID').append("<option value='0'>Select Series Type</option>");
            for (var i = 0; i < data.length; i++) {
                var opt = new Option(data[i].SeriesType, data[i].SeriesTypeID);
                $('#Series_SeriesTypeID').append(opt);
                //$('#FromJson').append(opt);
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        },
    });
}

function LoadSeries(PageSize,seriesType) {
    var targetControl = "#divAuthorSeries";

    $.ajax({
        cache: false,
        type: "GET",
        url: '/Series/GetAuthorSeries',
        data: { page: PageSize, typeID: seriesType },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $(targetControl).html(response);
        },
        failure: function (response) {
            alert("LoadSeries Failure:" + response);
            alert("LoadSeries Failure:" + response.responseText);
        },
        error: function () {
            alert("LoadSeries Error");
        }
    });
}

function GetSeriesByTypeID(seriesID) {
    LoadSeries(1, seriesID);
    setTimeout(function () { setValue(); }, 3000);
    $("#Series_SeriesTypeID").val(seriesID);
}

function setValue(value) {
    BindSeriesType();
}

$("body").on("change", "#Series_SeriesTypeID1", function (e) {
    var seriesID = $('#Series_SeriesTypeID').val();
    LoadSeries(1, seriesID);
    BindSeriesType();
    $("#Series_SeriesTypeID").val(seriesID);
});

//$('#Series_SeriesTypeID').change(function (e) {
    
//});

$("#btnSave").click(function () {
    var objSeries = {};
    var objAuthor = {};
    var objMagazine = {};
    var objWeekDay = {};
    var objSeriesType = {};

    objAuthor.AuthorID = $("#Author_UserID").val();
    objMagazine.MagazineID = $("#Magazine_MagazineID").val();
    objWeekDay.WeekDaysID = $("#WeekDay_WeekDaysID").val();
    objSeriesType.SeriesTypeID = $("#SeriesType_SeriesTypeID").val();

    objSeries.SeriesID = $("#SeriesID").val();
    objSeries.SeriesName = $("#SeriesName").val();
    objSeries.StartDate = $("#StartDate").val();
    objSeries.EndDate = $("#EndDate").val();

    objSeries.Author = objAuthor;
    objSeries.Magazine = objMagazine;
    objSeries.WeekDay = objWeekDay;
    objSeries.SeriesType = objSeriesType;

    $.ajax({
        type: "POST",
        url: '/Series/AddAuthorSeries',
        data: '{series : ' + JSON.stringify(objSeries) + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response.Message == "Success") {
                alert('Data Inserted Successfully.');
            }
            else if (response.Message == "Updated") {
                alert('Data Updated Successfully.');
            }
            window.location.href = "/Series/AuthorSeries";
        },
        error: function (response) {
            alert(response);
            alert(response.responseText);
            alert("Error:" + response.responseText);
        },
        failure: function (response) {
            alert('Failure');
            alert("failure :- " + response.responseText);
        }
    });
});

//#endregion Author Series

$(function () {
    var currentYear = (new Date).getFullYear();
    var lastYear = (new Date).getFullYear() - 75;
    $("#SourceDate, #EndDate,#StartDate").datepicker({
        changeMonth: true,
        changeYear: true,
        showOn: "button",
        buttonImage: "/Images/calendar.gif",
        buttonImageOnly: true,
        dateFormat: 'dd-mm-yy',
        yearRange: lastYear + ":" + currentYear
    });
});

//#region Unwanted Code
//function GetArticlesByArticleID(articleTypeID) {
//    BindArticlesByArticleID(1, articleTypeID);
//}


//$(document).on("click", "#contentPager a[href]", function (e) {
//    e.preventDefault();
//    BindArticlesByArticleID($(e.target).text(), 0);
//});

//#endregion Unwanted Code

