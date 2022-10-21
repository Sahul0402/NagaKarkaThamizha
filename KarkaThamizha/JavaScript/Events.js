var gViewPageID;
$(document).ready(function () {
    debugger;
    gViewPageID = masterPageID;

    if (gViewPageID) {
        SetGetPageViewCount(gViewPageID);
        SetGetPageLikeCount(gViewPageID);
        GetAllEvents(1, gViewPageID); //Initial load,viewpage,Viewcount=1 on page load.
    }
});

$("#PageLike").click(function () {
    SetGetPageLikeCount(gViewPageID);
});

function GetAllEvents(PageSize, gViewPageID) {
    debugger;
    var targetControl = "#divEvents";
    $.ajax({
        type: "GET",
        url: "/Events/ShowEventsType",
        cache: false,
        data: { pagesize: PageSize, viewpage: gViewPageID },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $(targetControl).html(response);
        },
        failure: function (response) {
            alert("GetAllEvents Failure:" + response);
            alert("GetAllEvents Failure:" + response.responseText);
        },
        //error: function (xhr,opt,exe) {
        error: function (response) {
            //alert(opt.url + ":" + xhr.status + " " + xhr.statusText);
            alert("No Records found.");
        }
    });
}

$(document).on("click", "#contentPager a[href]", function (e) {
    e.preventDefault();
    GetAllEvents($(e.target).text(), gViewPageID);
});

function OnError(jqXHR, errorType, exception) {
    responseText = jQuery.parseJSON(jqXHR.responseText);

    if (jqXHR.status === 0) {
        msg = 'Not connect. Verify Network.';
    } else if (jqXHR.status == 404) {
        msg = 'Requested page not found. [404]';
    } else if (jqXHR.status == 500) {
        msg = 'Internal Server Error [500].';
    }
    else if (jqXHR.status == 401) {
        location.href = "/";
        return;
    }
    else if (textStatus === 'parsererror') {
        msg = 'Requested JSON parse failed.';
    } else if (textStatus === 'timeout') {
        msg = 'Time out error.';
    } else if (textStatus === 'abort') {
        msg = 'Ajax request aborted.';
    }
    else {
        try {
            msg = JSON.parse(jqXHR.responseText);
        }
        catch (ers) {
            msg = jqXHR.responseText;
        }
    }
    alert("Error Type: " + errorType + ";" + "Exception: " + exception + msg);
}

$(document).ajaxStart(function () {
    $("#wait").css("display", "block");
});

$(document).ajaxComplete(function () {
    $("#wait").css("display", "none");
});