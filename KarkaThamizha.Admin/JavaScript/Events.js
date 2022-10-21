$(document).ready(function () {
    GetAllEvents(0, 1, ""); //Already we call BindEventsType in GetAllEvents
    // BindEventsType(); //In  udpate , eventstype is cleared.so commented this line

    //In Update mode, raise validation for PublisherID
    if ($("#EventsTypeID option:selected").val() > 0) {
        SetControls();
    }
    else {
        BindEventsType();
    }
    var sortOrder;
    $('#divEvents').on('click', 'thead th', function (e) {
        var sortOrder = $(this).text();
        switch (sortOrder)
        {
            case "EventsDate":
                sortOrder = "EventsDtSort";
                break;
            case "EventsType":
                sortOrder = "EventsTypeSort";
                break;
            default:
                sortOrder = "EventsDtSort_Desc";
        }
        GetAllEvents(0, 1, "", sortOrder);
        e.preventDefault();
    });
});

$("#EventsTypeID").on('change', function () {
    SetControls();
});


$("body").on("change", "#EventTypeID", function (e) {
    var articleType = $(this).children("option:selected").val();
    //var articleType1 = $("#EventTypeID option:selected").text();
    e.preventDefault();
    //SetControls();
    GetAllEvents($(this).val(), 1, "");
    BindEventsType();
    setTimeout(function () { setValue(articleType); }, 1000);
    //$("#EventTypeID").val($(this).val());
    //$("#EventTypeID option[value='articleType']").prop('selected', true);
    //$("#EventTypeID").text(articleType1);
});

function setValue(value) {
    $("#EventTypeID").val(value);
}

//function GetEventsByEventID(eventTypeID) {
//    GetAllEvents(eventTypeID, 1);
//    BindEventsType();
//    $("EventsTypeID").val(eventTypeID);
//}

function SetControls() {
    var articleType = $("#EventsTypeID option:selected").text();
    var article = $("#EventsTypeID").val();
    switch (articleType.trim()) {

        case "அஞ்சலி நிகழ்ச்சி":
            break;

        case "ஆவணப்படம் வெளியீடு":
            $("#PublisherList_PublisherID").rules("remove", "required");
            $("#divPublisherId").hide();
            $("#divEndDate").hide();
            $("#divURL").hide();
            $("#AuthorID").rules("remove", "required")
            $("#divAuthorId").hide();
            
            break;

        case "இலக்கிய விழா":
            break;

        case "கண்டன உரை":
            break;

        case "கருத்தரங்கம்":
            break;

        case "கலந்துரையாடல்":
            $("#PublisherList_PublisherID").rules("remove", "required")
            $("#divEndDate").hide();
            break;

        case "சிறப்புத் தள்ளுபடி":
            $("#divURL").hide();
            break;

        case "செம்பதிப்பு வெளியீட்டு விழா":

            break;

        case "சொற்பொழிவு":
            break;

        case "நினைவேந்தல்":
            $("#PublisherList_PublisherID").rules("remove", "required")
            $("#divEndDate").hide();
            break;

        case "நூல் அறிமுக விழா":
            $("#divEndDate").hide();
            $("#divEndDate").css("display:block");
            $("#divAuthorId").show();
            $("#divAuthorId").css("display:block");
            $("#PublisherList_PublisherID").show();
            $("#divAuthorId").rules("remove", "required");
            $("#PublisherList_PublisherID").css("display:block");
            $("#PublisherList_PublisherID").rules("remove", "required");
            break;

        case "நூல் திறனாய்வுக் கூட்டம்":
            break;

        case "நூல் விவாதம்":
            break;        

        case "ஒலிப்புத்தகம் வெளியீட்டு விழா":
        case "நூல் வெளியீட்டு விழா":
            $("#divEndDate").hide();
            $("#divEndDate").css("display:block");
            $("#divAuthorId").show();
            $("#divAuthorId").css("display:block");
            $("#divAuthorId").rules("remove", "required");
            $("#PublisherList_PublisherID").show();
            $("#PublisherList_PublisherID").css("display:block");
            $("#PublisherList_PublisherID").rules("remove", "required");
            break;

        case "பட்டிமன்றம்":
            break;

        case "பாராட்டு விழா":
            break;

        case "புத்தகக் கண்காட்சி":
            $("#divEndDate").show();
            $("#divEndDate").css("display:block")
            $("#divAuthorId").hide();
            $("#divAuthorId").css("display:none");
            $("#divPublisherId").hide();
            $("#PublisherList_PublisherID").hide();
            $("#PublisherList_PublisherID").css("display:none")
            $("#PublisherList_PublisherID").rules("remove", "required")
            $("#AuthorID").rules("remove", "required")
            break;

        case "முன் வெளியீட்டுத் திட்டம்":
            $("#AuthorID").rules("remove", "required")//sometimes more than 1 other books can be release. we cant add more author.
            $("#divURL").hide();
            break;

        case "விருது வழங்கும் விழா":
            $("#PublisherList_PublisherID").rules("remove", "required");
            $("#AuthorID").rules("remove", "required");
            $("#divEndDate").hide();
            break;

        case "மற்றவை":
            $("#divAuthorId").hide();
            $("#divAuthorId").css("display:block")
            break;
    }
}

$(function () {
    var currentYear = (new Date).getFullYear() + 1;
    var lastYear = (new Date).getFullYear() - 1;
    $("#EventsDate, #EndDate").datepicker({
        changeMonth: true,
        changeYear: true,
        showOn: "button",
        buttonImage: "/Images/calendar.gif",
        buttonImageOnly: true,
        dateFormat: 'dd-mm-yy',
        yearRange: lastYear + ":" + currentYear
    });
});

//Searchable dropdown
$(document).ready(function () {
    $("#AuthorID,#PublisherList.PublisherID").searchable({
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

function BindEventsType() {
    $.ajax({
        type: "GET",
        url: "/Events/BindEventsType",
        data: {},
        //contentType: "application/html; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $('#EventTypeID').empty();
            $('#EventTypeID').append("<option value='0'>Select Events Type</option>");
            for (var i = 0; i < data.length; i++) {
                var opt = new Option(data[i].EventsType, data[i].EventsTypeID);
                $('#EventTypeID').append(opt);
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

function GetAllEvents(typeID, page, search,sort) {
    $.ajax({
        type: "GET",
        //async: false,
        url: "/Events/GetAllEvents",
        data: { eventTypeID: typeID, page: page, txtSearch: search, sortOrder:sort },
        contentType: "application/html; charset=utf-8",
        dataType: "html",
        success: function (response) {
            if (response != null) {
                $('#divEvents').html(response);
            }
            else {
                alert('No Records found.');
            }
            //BindEventsType();  Need to check use of this line
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        },
    });
}

$(document).on("click", "#contentPager a[href]", function (e) {
    e.preventDefault();
    GetAllEvents(0, $(e.target).text(), "");
});

//Searching Events
$(document).on("click", "input[type='submit']", function () {
    GetAllEvents(0, 1, $("#txtSearch").val());
});

$("#btnSubmit").click(function (e) {
    var files = $("#ImgPath").get(0).files;

    if (files != 'undefined' && files.length > 0) {
        var imageName = files[0].name;
    }
    if (imageName.length > 35) {
        alert('Image Name must be 35 characters.');
        return false;
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

$(document).ready(function () {
    
});