var sortName = "";
var sortDirection = "ASC";
$(document).ready(function () {
    GetAllBooks("", 1, "", "0", "");
    //setTimeout(LoadUserAttributes, 2000);
});

$("body").on("click", "#pattern-style-b th a", function (e) {
    e.preventDefault();
    sortDirection = sortDirection == "ASC" ? "DESC" : "ASC";
    GetAllBooks($(this).html(), 1, "", "0", sortDirection);
});

function GetAllBooks(sortName, PageSize, Search, AuthorID, sortDirection) {
    var targetControl = "#divBooks";
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Books/GetAllBooks",
        data: { sortOrder: sortName, page: PageSize, txtSearch: Search, ddlAuthorID: AuthorID, directionSort: sortDirection },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $(targetControl).html(response);
            setTimeout(LoadUserAttributes, 1000);
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

function LoadUserAttributes() {
    // Enable Live Search.
    $('#UserID').attr('data-live-search', true);

    //// Enable multiple select.
    $('#UserID').attr('multiple', true);
    $('#UserID').attr('data-selected-text-format', 'count');

    $('.selectAuthor').selectpicker(
    {
        width: '80%',
        title: '- [Choose Multiple Author] -',
        style: 'btn-warning',
        size: 6,
        iconBase: 'fa',
        tickIcon: 'fa-check'
    });
}

var hdnSelectedUserId;
$(document.body).on('change', "#UserID", function (e) {
    if (this.selectedIndex != -1) {
        hdnSelectedUserId = this.options[this.selectedIndex].value;
        //var optVal = $("#UserID option:selected").val();
        GetAllBooks("", 1, $("#txtSearch").val(), hdnSelectedUserId);
    }
    else {
        hdnSelectedUserId = ""; //Global variable, so set empty value for userid.
        GetAllBooks("", 1, $("#txtSearch").val(), hdnSelectedUserId);
    }        
});

$(document).on("click", "#contentPager a[href]", function (e) {
    e.preventDefault();
    var page = $(e.target).text() == ">>" ? -4 : $(e.target).text();
    GetAllBooks("", page, $("#txtSearch").val(), hdnSelectedUserId);
});

//Searching book
$(document).on("click", "input[type='submit']", function () {
    GetAllBooks("", 1, $("#txtSearch").val(), hdnSelectedUserId);
});

//The below funcation not working.
$("#UserID").change(function () {
    document.getElementById("MultiSelectedUserId").value = "";
    document.getElementById("hdnBookListUserID").value = "BookList";

    var hdnSelectedUserId = this.options[this.selectedIndex].value;
    document.getElementById("MultiSelectedUserId").value = this.options[this.selectedIndex].value;
    $("#UserID").val(hdnSelectedUserId);
    document.getElementById("UserID").value = hdnSelectedUserId;
    document.forms["frmBookList"].submit();
});

$(function () {
    var selectedValue = '@(ViewBag.SelectedUserId)';
    if (selectedValue != null) {
        $("[id*=UserID] option").each(function () {
            if ($(this).val() == selectedValue) {
                $(this).attr('selected', 'selected');
            }
        });
    }
});

function TranslateText() {
    var input = $("#txtSearch").val();
    if (input != undefined || input != "") {
        var targetControl = "#txtSearch";

        $.ajax({
            cache: false,
            type: "GET",
            url: "/TextTranslation/TranslateText",
            data: { input: input.trim() },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $(targetControl).val(response);
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
}

function printSelection(node) {
    var content = document.getElementById('divBookPrint').innerHTML;
    var pwin = window.open('', 'print_content', 'width=5,height=5');
    pwin.document.open();
    pwin.document.write('<html><body onload="window.print()">' + content + '</body></html>');
    pwin.document.close();
    setTimeout(function () { pwin.close(); }, 1000);
}

//Searching book with Category(Books > BooksCategory)
//$(document).on("click", "input[type='submit']", function (e) {
//    e.preventDefault();
//    GetAllBooksWithCategory("", 1, $("#txtSearch").val(), hdnSelectedUserId);
//});

function GetAllBooksWithCategory(sortName, PageSize, Search, AuthorID, sortDirection) {
    var targetControl = "#pattern-style-b";
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Books/BooksCategory",
        data: { sortOrder: sortName, page: PageSize, txtSearch: Search, ddlAuthorID: AuthorID, directionSort: sortDirection },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $(targetControl).html(response);//Inner Page created so create partial view like _Books.cshtml
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

function RemoveSpecialChar(source) {
    //This will strip out all non-numeric values
    source.value = source.value.replace(/[^\w\s!]/gi, ' ');
}

function toTitleCase(str) {
    var lcStr = str.toString().toLowerCase();
    return lcStr.replace(/(?:^|\s)\w/g, function (match) {
        return match.toUpperCase();
    });
}