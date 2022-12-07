//Global Variable and used in this page.
var gMainPageID;
var gChildPageID;
$(document).ready(function () {

    ////GetLatestNews();
    ///*gMainPageID = masterPageID;*/
    //var key = GetParameterValues('BooksReview');
    //var gChildPageID = GetParameterValues('booksReviewID');

    ////if (childPageID != 'undefined' && childPageID > 0)
    ////    gChildPageID = childPageID; // Data from ShowEvents.cshtml js file.
    //if (document.location.href.match(/[^\/]+$/) != null)
    //    var pageName = document.location.href.match(/[^\/]+$/)[0];

    //if (pageName) {
    //    //LoadUserFeedback(gViewPageID);        Need to check 

    //    //ClearControls();
    //    //document.getElementById("Name").maxLength = 35;
    //    //document.getElementById("EMail").maxLength = 40;
    //    //document.getElementById("Mobile").maxLength = 20;
    //    //document.getElementById("Password").maxLength = 12;
    //}
});
//#region Common Function
function ValidateEmail(email) {
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return expr.test(email);
};

function GetParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}
//#End
function GetLatestNews() {
    var targetControl = "#newsDescription";
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Master/GetLatestNews",
        data: {},
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {
            const obj = JSON.parse(response);
            var text = JSON.stringify(obj[0].Description);
            $("#newsDescription").html(text);
        },
        failure: function (response) {
            alert("Failure1:" + response);
            alert("Failure2:" + response.responseText);
            alert("Failure3:" + response.responseText);
        },
        error: function (response) {
            var r = jQuery.parseJSON(response.responseText);
            alert("Message: " + r.Message);
            alert("StackTrace: " + r.StackTrace);
            alert("ExceptionType: " + r.ExceptionType);
            var errorMessage = response.status + ': ' + response.statusText
            alert('Error - ' + errorMessage);
        }
    });
}

//#region Login & Register Popup
$("#btnLogin").click(function () {
    var mailid = document.getElementById("loginemail").value;
    var pwd = document.getElementById("loginPassword").value;
    if (mailid == "" || pwd == "") {
        alert("Please check EMail or Password");
        return false;
    }
    $.ajax({
        type: "GET",
        url: "/Login/UserLogin",
        data: { email: mailid, password: pwd },
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response == null) {
                alert("Please check EMail or Password");
            }
            else if (response > 0) {
                alert("User Login Successful");
                location.reload();
            }
            console.log('${response} is the return value');
        },
        error: function (response) {
            alert("Error while User login " + response);
        },
        faillure: function (response) {
            alert("Faillure while User login " + response);
        }
    });
});

$("#Register").click(function () {

    try {
        $.ajax({
            type: "Get",
            url: "/Login/UserLogin",
            //data: '{email: "' + $("#loginemail").val() + '", password: "' + $("#loginPassword").val() + '" }',
            data: '{emailID: "test@gmai.com", password: "' + $("#loginPassword").val() + '" }',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                if (res.message > 0) {
                    alert("Login Successful");
                    location.reload(true);
                }
                else {
                    alert(res.responseText);
                }
            },
            error: function (res) {
                //alert(res.responseText);
                alert("Error while inserting ToDo data" + res.message);
            }
        });
    }
    catch (e) {
        alert(e.message);
        //alert(e.stack);
    }
});
//#endregion Login

//#region User Feedback
function LoadUserFeedback(MainPage) {
    var ChildPage;

    if (gChildPageID) {
        ChildPage = childPageID;
    }
    else if ($('#hdnUserID').val()) {
        ChildPage = $('#hdnUserID').val();
    }

    if (!ChildPage && !MainPage)
        return;

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
            else {
                alert('No Records found.');
            }
        },
        failure: function (response) {
            alert(response);
            alert(response.responseText);
            alert("Failure:" + response.responseText);
        },
        // error: function (response) {
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
            alert(XMLHttpRequest);
            alert("Error:" + textStatus);
        }
    });
}

$("#submitComments11").click(function (e) {
    debugger;
    e.preventDefault();

    var targetControl = "#msgPageFeedback";
    var comments = {};
    comments.ParentID = 0;
    if (hdnSessionId == 'undefined' || hdnSessionId==0 || hdnSessionId=='') {
        if ($("#Name").val() == '') {
            alert('Please enter Name');
        }
        if ($("#EMail").val() == '') {
            alert('Please enter EMail');
        }
        if ($("#Mobile").val() == '') {
            alert('Please enter Mobile');
        }
        if ($("#Password").val() == '') {
            alert('Please enter Password');
        }
        if ($("#Comments").val() == '') {
            alert('Please enter Comments');
        }
    }

    comments.Name = $.trim($("#Name").val());
    comments.Email = $.trim($("#EMail").val());
    comments.Mobile = $.trim($("#Mobile").val());
    comments.Password = $.trim($("#Password").val());
    comments.Comments = $.trim($("#Comments").val());
    comments.MasterPageID = gMainPageID;
    comments.ChildPageID = GetParameterValues('booksReviewID');

    $(targetControl).html("");
    $.ajax({
        url: '/Comments/AddComments',
        type: 'POST',
        //data: $("#PageFeedback").serialize(),
        data: '{mdlContact: ' + JSON.stringify(comments) + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.Message == "Success") {
                ClearControls11();
                $(targetControl).html("<div class='success'>" + "Thanks for your Feedback" + "</div>");
            }
            else {
                $(targetControl).html(response.Message);
            }
        },
        error: function () {
            $(targetControl).html("<div class='failed'>" + "Error Occured. Please contact Administrator." + "</div>");
        }
    });

});

function ClearControls11() {
    $("#Name").val('');
    $("#EMail").val('');
    $("#Mobile").val('');
    $("#Password").val('');
    $("#Comments").val('');
}

$("#submit").click(function () {
    var ChildPage;
    var feedback = {};
    if (masterPageID == 12 || masterPageID == 13 || masterPageID == 14)
        ChildPage = gChildPageID;
    else
        ChildPage = $('#UserID').val(); //For AuthorDetails Page

    feedback.Name = document.getElementById("Name").value;
    feedback.Mobile = document.getElementById("Mobile").value;
    feedback.EMail = document.getElementById("EMail").value;
    feedback.Password = document.getElementById("Password").value;
    feedback.Comments = document.getElementById("Comments").value;
    feedback.MasterPageID = gMainPageID;
    feedback.ChildPageID = GetParameterValues('booksReviewID');

    $.ajax({
        cache: false,
        type: "POST",
        url: "/Feedback/Add",
        //data: { name: Name, mobile: Mobile, EMail: EMail, password: Password, comments: Comments, MasterPageID: MainPage, ChildPageID: ChildPage },
        data: '{mdlFeedback: ' + JSON.stringify(feedback) + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            ClearFeedbackControls();
            if (response.Redirect) {
                $('#loginModal').modal('hide');
                alert('data added succesfully');
                window.location.href = response.PageURL;
            }
            else {
                $('#loginModal').modal('hide');
                window.location.href = "/Events/Event?eventId" + ChildPage;
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

function LoadComments() {
    var targetControl = "#divComments";
    $.ajax
        ({
            url: "/Article/GetAllComments",
            contentType: "application/html; charset=utf-8",
            type: "GET",
            cache: !0,
            datatype: "html",
            success: function (response) {
                $(targetControl).html(response)
            },
            error: function () {
                $(targetControl).html("Not Found")
            }
        })
}
//#endregion User Feedback

$('img').each(function () {
    $(this)[0].oncontextmenu = function () {
        return false;
    };
});

//#region Page View Count
function SetGetPageViewCount(viewPage) {
    var targetControl = "#lblPageViewCount";

    $.ajax({
        type: "GET",
        url: "/UserTracking/SetGetPageViewCount",
        contentType: "application/json; charset=utf-8",
        data: { viewPageID: viewPage, articleID: 105 },
        dataType: "json",
        asyn: false,
        success: function (response) {
            if (response.count >= 0) {
                document.getElementById("lblPageViewCount").innerHTML = response.count;
            }
        },
        failure: function (response) {
            alert(response);
            alert(response.responseText);
            alert("Failure:" + response.responseText);
            alert("Failure:" + response.failure);
            alert("Failure:" + response.text);
        },
        error: function (response) {
            alert(response);
            alert(response.responseText);
            alert("Error:" + response.responseText);
            alert("Failure:" + response.failure);
            alert("Failure:" + response.text);
        }
    });
}

function SetGetPageLikeCount(viewPage) {
    var targetControl = "#lblPageLikeCount";

    $.ajax({
        cache: false,
        type: "GET",
        url: "/UserTracking/SetGetPageLikeCount",
        data: { viewPageID: viewPage, articleID: 105 },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        asyn: false,
        success: function (response) {
            if (response.count >= 0) {
                var id = document.getElementById("lblPageLikeCount");
                id.innerHTML = response.count;
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
            alert("Error:" + response.responseText);
        }
    });
}
//#endregion Page View Count

function LoadTaggedWith() {
    $.ajax
        ({
            url: "/Article/GetAllTaggedWith",
            contentType: "application/html; charset=utf-8",
            type: "GET",
            cache: !0,
            datatype: "html",
            success: function (t) {
                $("#TaggedWith").html(t)
            },
            error: function () {
                $("#TaggedWith").html("Not Found")
            }
        })
}

//#region Social Page sharing
function LoadShareOn() {
    $.ajax
        ({
            url: "/Article/GetAllShareOn",
            contentType: "application/html; charset=utf-8",
            type: "GET",
            cache: !0,
            datatype: "html",
            success: function (t) {
                $("#ShareOn").html(t)
            },
            error: function () {
                $("#ShareOn").html("Not Found")
            }
        })
}
//#endregion Social Page sharing


$('#Name').keydown(function (e) {
    if (e.ctrlKey || e.altKey) {    //e.shiftKey removed for Name -first letter caps
        e.preventDefault();
    } else {
        var key = e.keyCode;
        if (!((key == 8) || (key == 9) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
            e.preventDefault();
        }
    }
});

//KeyCode Key
//8       backspace/delete
//9       Tab
//14/15   Shift In/out
//46      delete

//$("#EMail").blur(function () {
//    if (!ValidateEmail($("#EMail").val())) {
//        alert("Invalid email address.");
//    }
//    else {
//        alert("Valid email address123.");
//    }
//});

$("#Email").blur(function () {
    $("#lblStatus").html("");
    var mailId = $("#Email").val();
    if (mailId == null || mailId == "")
        return;
    $("#lblStatus").html("wait...");
    $.ajax({
        type: "POST",
        url: "/User/IsMailExists",
        data: { Email: mailId },
        /*contentType: "application/json; charset=utf-8",*/ /*will get error()when using this line*/
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response);
        },
    });
});

//function OnSuccess 
function OnSuccess(response) {
    var targetControlInfo = "#lblStatus";
    if (response) {
        $("#Email").val('');
        $(targetControlInfo).show();
        $(targetControlInfo).css("color", "red");
        $(targetControlInfo).html("EMail already exists.");
    }
    else {
        $(targetControlInfo).html("");
    }
}



//called when key is pressed in textbox
$("#Mobile").keypress(function (e) {
    //if the letter is not digit then display error and don't type anything
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        return false;
    }
});

//$('#Comments').bind("cut copy paste", function (event) {
//    event.preventDefault();
//});

function Password_Show_Hide(controlId, eyeOpenId, eyeCloseId) {
    var x = document.getElementById(controlId);
    var show_eye = document.getElementById(eyeOpenId);
    var hide_eye = document.getElementById(eyeCloseId);
    hide_eye.classList.remove("d-none");
    if (x.type === "password") {
        x.type = "text";
        show_eye.style.display = "none";
        hide_eye.style.display = "block";
    } else {
        x.type = "password";
        show_eye.style.display = "block";
        hide_eye.style.display = "none";
    }
}