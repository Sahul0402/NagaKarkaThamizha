

$("#fileUpload").click(function () {
    debugger;
    if ($("#xlfile").val() != "") {
        var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xlsx|.xls)$/;
        /*Checks whether the file is a valid excel file*/
        if (!regex.test($("#xlfile").val().toLowerCase())) {
            alert("Please upload a valid Excel file!");
            return false;
        }
        else {
            BindDataInGrid(1);
        }
    }
    else {
        alert("Please upload a Excel file!");
        return false;
    }
});

function BindDataInGrid(PageSize) {
    debugger;
    var targetControl = "#divBooksDetails";
    var selectedValue = $('input:radio:checked').val();
    var xlFileName;

    var formData = new FormData();
    if (PageSize == "1") {
        var files = $("#xlfile").get(0).files;

        if (files != 'undefined' && files.length > 0) {
            formData.append("ExcelFile", files[0]);
            xlFileName = files[0].name;
        }
        else { xlFileName = ""; }
    }
    formData.append("FileType", selectedValue);
    formData.append("Page", PageSize);

    $.ajax({
        type: "Get",
        url: '/FileUpload/FileUpload',
        data: formData,
        cache: false,
        //contentType: false,
        //processData: false,
        //dataType: 'json',
        //processData: false,         // Add this line without processing parameters
        success: function (response) {
            debugger;
            $("#file").val('');
            //alert("Success: " + response);
            $(targetControl).html(response);
        },
        error: OnError,
        failure: function (response) {
            alert('Failure');
            alert("failure :- " + response.responseText);
        }
        //error: function (xhr, status, p3, p4) {
        //    if (xhr.responseText && xhr.responseText[0] == "{")
        //        err = JSON.parse(xhr.responseText).Message;
        //    else if (xhr.responseText) {
        //        err = "xhrResponse: " + xhr.responseText;
        //    }
        //    else {
        //        var err = "Error " + " " + status + " " + p3 + " " + p4;
        //    }
        //    alert(err);
        //    return false;
        //}
    });
}

function OnError(xhr, errorType, exception) {
    debugger;
    var responseText;
    $("#dialog").html("");
    try {
        responseText = jQuery.parseJSON(xhr.responseText);
        $("#dialog").append("<div><b>" + errorType + " " + exception + "</b></div>");
        $("#dialog").append("<div><u>Exception</u>:<br /><br />" + responseText.ExceptionType + "</div>");
        $("#dialog").append("<div><u>StackTrace</u>:<br /><br />" + responseText.StackTrace + "</div>");
        $("#dialog").append("<div><u>Message</u>:<br /><br />" + responseText.Message + "</div>");
        $("#dialog").show();
    } catch (e) {
        responseText = xhr.responseText;
        $("#dialog").html(responseText);
    }

    $("#dialog").html(responseText);
}

$(document).on("click", "#contentPager a[href]", function (e) {
    debugger;
    e.preventDefault();
    var targetControl = "#divBooksDetails";
    var formData = new FormData();
    var selectedValue = $('input:radio:checked').val();
    formData.append("FileType", selectedValue);
    formData.append("Page", $(e.target).text());

    $.ajax({
        type: "Get",
        url: '/FileUpload/FileUpload?page=' + $(e.target).text(),
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        //dataType: 'json',
        //processData: false,         // Add this line without processing parameters
        success: function (response) {
            debugger;
            $("#file").val('');
            $(targetControl).html(response);
        },
        error: OnError,
        failure: function (response) {
            alert('Failure');
            alert("failure :- " + response.responseText);
        }
    });
});

function BindExcelInGrid(PageSize) {
    debugger;
    var targetControl = "#divBooksDetails";
    var selectedValue = $('input:radio:checked').val();

    $.ajax({
        type: "Get",
        url: '/FileUpload/FileUpload',
        data: { page: PageSize },
        cache: false,
        success: function (response) {
            debugger;
            $("#file").val('');
            $(targetControl).html(response);
        },
        error: OnError,
        failure: function (response) {
            alert('Failure');
            alert("failure :- " + response.responseText);
        }
    });
}

//document.getElementById("bulkInsert").addEventListener('click', function () {
//    window.location.href = "/FileUplad/BulkInsert";
//});