/* Author List Page Stared*/
var userID;
$(document).ready(function () {
    var Page = hdnPage;
    if (Page == "AuthorDetails") {
        userID = hdnUserID;
        PopulateAuthorInfoByAuthorID(userID);
        PopulateAuthorBooksDetailsByAuthorID(userID);
        //LoadUserFeedback(masterPageID);
    }
    else if (Page == "AuthorList") {
        GetAllAuthor('', 1);    // Search String, Page
    }
});
var _data = [];
function AddOrUpdateUserRating() {
    if ((typeof onAddOrUpdateUserRating) == "function") {
        onAddOrUpdateUserRating(false, function () {
            PopulateAuthorBooksDetailsByAuthorID(userID)
        })
    }
}
function designAuthorBookGrid(data) {
    _data = data;
    if ($('#tblAuthorBooks').children().length > 0) {
        var gridRows = $('#tblAuthorBooks').find('.sgexpanded').parent()
        $("#tblAuthorBooks").jqGrid({ data: data }).trigger("reloadGrid");
        gridRows.each(function (id) {
            debugger
            $("#tblAuthorBooks").expandSubGridRow($(this)[0].id)

        });
        return;
    }
    //$("#tblAuthorBooks").empty();
    $("#tblAuthorBooks").jqGrid({
        data: data,
        colNames: ['வகைகள் (Category)', 'CategoryID','BookList'],
        colModel: [
            { name: 'CategoryName', index: 'CategoryName', title: 'வகைகள்(Category)' },
            { name: 'CategoryID', hidden: true, index: 'CategoryID' },
            { name: 'BookList', hidden: true, index: 'BookList' }
        ],
        rowNum: 8,
        rowList: [8, 10, 20, 30],
        pager: '#psg2',
        autowidth: true,
        sortname: 'id',
        viewrecords: true,
        sortorder: "desc",
        multiselect: false,
        guiStyle: "bootstrap",
        iconSet: "fontAwesome",
        subGrid: true,
        caption: "",
        // define the icons in subgrid
        subGridOptions: {
            "plusicon": "ui-icon-triangle-1-e",
            "minusicon": "ui-icon-triangle-1-s",
            "openicon": "ui-icon-arrowreturn-1-e",
            //expand all rows on load
            "expandOnLoad": false
        },
        collapseSubGridRow: function (subgrid_id, row_id) {
            debugger
        },
        subGridRowExpanded: function (subgrid_id, row_id) {
            var categoryID = $("#tblAuthorBooks").jqGrid('getRowData', row_id).CategoryID;
            var subGridData = _data.filter(f => f.CategoryID == categoryID)[0].BookList;
            var subgrid_table_id, pager_id;
            subgrid_table_id = subgrid_id + "_t";
            pager_id = "p_" + subgrid_table_id;
            $("#" + subgrid_id).html("<table id='" + subgrid_table_id + "' class='scroll'></table><div id='" + pager_id + "' class='scroll'></div>");
            jQuery("#" + subgrid_table_id).jqGrid({
                data: subGridData,
                colNames: ['புத்தகத்தின் பெயர்', 'முதல் பதிப்பு', 'Global Rating', 'User Rating', 'Add Review', 'Show Review', 'BookID', 'CategoryID'],
                colModel: [
                    { name: "Book", index: "Book", title: 'புத்தகத்தின் பெயர்', width: 45 },
                    { name: 'FirstEdition', index: 'FirstEdition', title: 'முதல் பதிப்பு', width: 15 },
                    { name: 'GlobalRating', index: 'GlobalRating', title: 'Global Rating', width: 10, formatter: globalRatingFormatter },
                    { name: 'UserRating', index: 'UserRating', title: 'User Rating', width: 10, editable: false, formatter: userRatingFormatter },
                    { name: 'BooksReview', index: 'BooksReview', title: 'Add Review', width: 10, editable: false, formatter: addReviewFmatter },
                    { name: 'ShowReview', index: 'ShowReview', title: 'Show Review', width: 10, editable: false, formatter: showReviewFmatter },
                    { name: "CategoryID", index: "CategoryID", hidden: true },
                    { name: "BookID", index: "BookID", key: true, hidden: true },
                ],
                rowNum: 20,
                pager: pager_id,
                sortname: 'num',
                sortorder: "asc",
                autowidth: true,
                height: '100%',
                guiStyle: "bootstrap",
                iconSet: "fontAwesome",
            });
            $("#" + subgrid_table_id).jqGrid('navGrid', "#" + pager_id, { edit: false, add: false, del: false })
        }
    });
    $("#tblAuthorBooks").jqGrid('navGrid', '#psg2', { add: false, edit: false, del: false });
}

function globalRatingFormatter(el, cellval, opts) {
    console.log(cellval, opts);
    var globalRating = '';
    //opts.GlobalRating = 2;
    if (opts.GlobalRating > 0) {
        globalRating = '<label style="font-size: 1.4em;margin-left:5px;">' + opts.GlobalRating + '/10</label>';
    }
    var rating = '<div style="text-align: center;margin-top:5px;"><i id="elmRateUs" data-bookid=' + opts.BookID + " data-categoryid=" + opts.CategoryID + ' class="fas fa-star" style="font-size:1.8em;"></i>' + globalRating + '</div>';
    return rating;
}

function userRatingFormatter(el, cellval, opts) {
    var userRating = '';
    //opts.UserRating = 2;
    if (opts.UserRating > 0) {
        userRating = '<label style="font-size: 1.4em;margin-left:5px;">' + opts.UserRating + '/10</label>';
    }
    console.log(cellval, opts);
    var rating = '<div style="text-align: center;margin-top:5px;"><i id="elmRateUs" onclick="onUserRatingClickHandler(this)" data-userrating="' + opts.UserRating + '" data-booktitle="' + opts.Book + '" data-bookid=' + opts.BookID + " data-categoryid=" + opts.CategoryID + ' class="fas fa-star" style="font-size:1.8em;"></i>' + userRating + '</div>';
    return rating;
}

function onUserRatingClickHandler(e) {
    var $elem = $('#divRateTool');
    $('#hdnBookdDetailID').val(e.dataset.bookid);
    designRating($elem, 10, e.dataset.booktitle, e.dataset.userrating);
}

function addReviewFmatter(el, cellval, opts) {
    return '<div style="text-align:center"><a class="link" href="/BooksReview/BooksReview?booksReviewID=' + opts.BooksReviewID + '" target="_blank">Add Review</a></div>';
}

function showReviewFmatter(el, cellval, opts) {
    if (opts.BooksReviewID > 0) {
        return '<div style="text-align:center"><a class="link" href="/BooksReview/BooksReview?booksReviewID=' + opts.BooksReviewID + '" target="_blank">View</a></div>';
    }
    return "";
}

function LoadUserFeedback(MainPage) {
    if ($('#hdnUserID').val() == 'undefined')
        return;

    var ChildPage = userID;
    var targetControlInfo = "#divUserFeedback";
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Author/LoadUserFeedback",
        data: { masterPageID: MainPage, childPageID: ChildPage },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            if (response != null) {
                $(targetControlInfo).html(response);
            }
        },
        failure: function (response) {
            alert("Failure to loading  UserFeedback");
        }
    });
}

function GetAllAuthor(CharSearch, Page) {
    $.ajax({
        type: "GET",
        url: "/Author/GetAllAuthor",
        data: { search: CharSearch, page: Page },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $("#divAuthorList").html(response);
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

function PopulateAuthorInfoByAuthorID(userid) {
    var targetControlInfo = "#divAuthorInfo";
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Author/GetAuthorInfoByAuthorID",
        data: { userID: userid },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            if (response != null) {
                $(targetControlInfo).html(response);
            }
        },
        failure: function (response) {
            alert("Failure in Populate Author Info...");
        },
        error: function (response) {
            alert("Error in Populate Author Info...");
        }
    });
}

function PopulateAuthorBooksByAuthorID(userid) {
    var targetControlBook = "#divAuthorBooks";

    $.ajax({
        type: "GET",
        url: "/Author/GetAuthorBooksByAuthorID",
        data: { userID: userid },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            if (response != null) {
                $(targetControlBook).html(response);
            }
        },
        failure: function (response) {
            alert(response);
            alert(response.responseText);
            alert("Failure:" + response.responseText);
        },
        error: function (response) {
            alert(response);
            alert(response.responseText);
            alert("Error in PopulateAuthorBooksByAuthorName:" + response.responseText);
        }
    });
}


function PopulateAuthorBooksDetailsByAuthorID(userid) {
    var targetControlBook = "#divAuthorBooks";

    $.ajax({
        type: "GET",
        url: "/Author/GetAuthorBooksDetailsByAuthorID",
        data: { userID: userid },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response != null) {
                designAuthorBookGrid(response);
            }
        },
        failure: function (response) {
            alert(response);
            alert(response.responseText);
            alert("Failure:" + response.responseText);
        },
        error: function (response) {
            alert(response);
            alert(response.responseText);
            alert("Error in PopulateAuthorBooksByAuthorName:" + response.responseText);
        }
    });
}

$(document).on("click", "#contentPager a[href]", function (e) {
    e.preventDefault();
    GetAllAuthor('', $(e.target).text());
});

$(document).on("click", "#searchString a[href]", function (e) {
    e.preventDefault();
    GetAllAuthor($(e.target).text(), '');
});

/* Author List Page Ended*/

/* Insert Login Page Started*/
$("#submit1").click(function () {
    var dataObject = {
        Name: document.getElementById("Name").value,
        Mobile: document.getElementById("Mobile").value,
        EMail: document.getElementById("EMail").value,
        Password: document.getElementById("Password").value,
        Comments: document.getElementById("Comments").value,
        mainPage: masterPageID,
        childPage: $('#UserID').val()
    };
    try {
        $.ajax({
            url: '/User/AddFeedbackUser',
            type: 'POST',
            data: {},
            datatype: "json",
            contentType: "application/json; charset=utf-8",
            processData: false,
            success: function (data) {
                if (data.toString() == "Success") {
                    $("#Name").val('');
                    $("#EMail").val('');
                    $("#Mobile").val('');
                    $("#Comments").val('');
                    $("#content").html("<div class='success'>" + data + "</div>");
                }
                else {
                    $("#content").html("<div class='failed'>" + data + "</div>");
                }
            },
            error: function () {
                alert("Error Occured. Please contact Administrator.");
            }
        });
    }
    catch (e) {
        alert(e.message);
    }
});

/* Insert Login Page Ended*/

/* Author Series Started */

function LoadAuthorSeries(Page) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Author/GetAllAuthorSeries",
        data: { page: Page },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            $("#divAuthorSeries").html(response);
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

/* Author Series Ended */


$("#submit").click(function () {
    var Name = document.getElementById("Name").value;
    var Mobile = document.getElementById("Mobile").value;
    var EMail = document.getElementById("EMail").value;
    var Password = document.getElementById("Password").value;
    var Comments = document.getElementById("Comments").value;
    var MainPage = masterPageID;
    var ChildPage = $('#UserID').val();

    $.ajax({
        cache: false,
        type: "GET",
        url: "/ContactUs/Feedback",
        data: { name: Name, mobile: Mobile, EMail: EMail, password: Password, comments: Comments, MasterPageID: MainPage, ChildPageID: ChildPage },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            ClearFeedbackControls();
            alert('data added succesfully');
            if (response.isRedirect) {
                alert(response.redirectUrl);
                window.location.href = response.redirectUrl;
            }
        },
        failure: function (response) {
            alert(response);
            alert(response.responseText);
        },
        error: function (response) {
            alert(response);
            alert(response.responseText);
        }
    });
});