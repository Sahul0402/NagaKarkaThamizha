var bookID;
$(document).ready(function () {
    bookID = setBookID;
    //show other Publisher books in bottom of the Page.
    ShowOtherPublisherBooks(bookID);
});

function ShowOtherPublisherBooks(bookID) {
    try {        
        $.ajax({
            type: "GET",
            url: "/BooksDetails/GetBookDetailsByBookID",
            data: { 'bookId': bookID },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                $('#divPublishers').hide();
                alert(response.responseText);
            }
        });

    } catch (e) {
        alert(e.message);
    }
}

function OnSuccess(response) {
    var model = response;
    var row = $(".tblPublishers tr:last-child").removeAttr("style").clone(true);
    $(".tblPublishers tr").not($(".tblPublishers tr:first-child")).remove();
    $.each(model, function (index) {
        $("td", row).eq(0).html(model[index].PublisherName);
        $("td", row).eq(1).html(model[index].Price);
        $("td", row).eq(2).html(model[index].NoOfCopy);
        $(".tblPublishers").append(row);
        row = $(".tblPublishers tr:last-child").clone(true);
    });
    if (model.length == 0) {
        $('#divPublishers').hide();
    }
    else if (model.length > 0)
    { $('#divPublishers').show(); }
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

$("#tamilchar").on("click", "a", function (e) {
    //var href = $(this).attr("href");
    var hrefText = $(e.target).text();
    try {
        $.ajax({
            url: '/Publishers/GetPublisherBySearchChar',
            type: "GET",
            dataType: "JSON",
            data: { character: hrefText },
            success: function (publishers) {
                $("#PublisherID").html(""); // clear before appending new list
                $.each(publishers, function (i, Pub) {
                    $('#PublisherID').append(
                            $('<option></option>').val(Pub.PublisherID).html(Pub.Publisher));
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(XMLHttpRequest.status);
                alert(XMLHttpRequest.responseText);
            }
        });
    }
    catch (e) {
        alert(e.message);
    }
});


$(function () {    
    var currentYear = (new Date).getFullYear();
    var lastYear = (new Date).getFullYear() - 155;
    $('#FirstEdition,#CurrentEdition').datepicker({
        //closeOnEscape: true,
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        dateFormat: 'MM yy',
        yearRange: lastYear + ":" + currentYear,
        onClose: function (dateText, inst) {
            var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
            var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
            //$(this).datepicker('setDate', new Date(year, month, 1));
            $(this).val($.datepicker.formatDate('M yy', new Date(year, month, 1)));
        }
    });
});

//Searchable dropdown
$(document).ready(function () {
    $("#PublisherID").searchable({
        maxListSize: 200, // if list size are less than maxListSize, show them all
        maxMultiMatch: 300, // how many matching entries should be displayed
        exactMatch: false, // Exact matching on search
        wildcards: true, // Support for wildcard characters (*, ?)
        ignoreCase: true, // Ignore case sensitivity
        latency: 200, // how many millis to wait until starting search
        warnMultiMatch: 'top {0} matches ...',
        warnNoMatch: 'no matches ...',
        zIndex: 'auto'
    });
});