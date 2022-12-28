function ValidateEmail(email) {
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return expr.test(email);
};

$(document).ready(function () {
    $('#linklogin').on('click', function () {
        LoadLoginControl();
    });
});

function LoginUser(mailid, pwd, isRating, userRating) {
    $.ajax({
        type: "GET",
        url: "/Account/UserLogin",
        data: { email: mailid, password: pwd, isRating: isRating },
        dataType: "JSON",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response == null || response.message == "") {
                alert("Please check EMail or Password");
            }
            else if (response.message) {
                if (isRating) {
                    onAddOrUpdateUserRating(true);
                }
                else {
                    alert("User Login Successful");
                    location.reload();
                }
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
}



//function onAddOrUpdateUserRating1(e) {
//    var userID = $("#hdnSession").val();
//    var bookID = $("#hdnBookdDetailID").val();
//    var userRating = $('#hdnUserRating').val();
//    $.ajax({
//        cache: false,
//        type: 'GET',
//        url: "/UserRating/AddOrUpdateUserRating",
//        data: { userId: userID, bookId: bookID, userRating: userRating },
//        contentType: "application/json; charset=utf-8",
//        datatype: "json",
//        success: function (response, status, xhr) {
//            location.reload();
//        },
//        error: function (jqXHR, exception) {

//        }
//    });
//}

function LoadLoginControl(isRating, userRating) {
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Account/LoginPartial",
        /*data: { masterPageID: MainPage, childPageID: ChildPage },*/
        /* contentType: "application/json; charset=utf-8",*/
        dataType: "html",
        success: function (response) {

            $('#divLoginContainer').html(response);
            /*   $('#divLoginContainer').modal('show');*/
            $("#divLoginContainer").dialog({
                modal: true,
                autoOpen: true,
                maxWidth: 400,
                maxHeight: 500,
                minWidth: 400,
                minHeight: 500,
                width: 400,
                height: 500,
                open: function () {
                    $(this).closest(".ui-dialog")
                        .find(".ui-dialog-titlebar-close")
                        .removeClass("ui-dialog-titlebar-close")
                        .html("<span class='fa fa-times'></span>");
                }
            });

            //#region Login & Register Popup
            $("#btnLogin").on('click', function () {
                var mailid = document.getElementById("loginemail").value;
                var pwd = document.getElementById("loginPassword").value;
                if (mailid == "" || pwd == "") {
                    alert("Please check EMail or Password");
                    return false;
                }
                LoginUser(mailid, pwd, isRating, userRating);
            });

            $('#btnCancel').on('click', function () {
                $("#divLoginContainer").dialog("close");
            });

            $('#btnRegister').on('click', function () {
                RegisgterUser();
            });
        },
        failure: function (response) {
            //alert("Failure: " + response.responseText);
        },
        // error: function (response) {
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
            alert(XMLHttpRequest);
            alert("Error:" + textStatus);
        }
    });
}

function RegisgterUser() {
    debugger;
    var objUser = {};
    objUser.Name = $("#RegName").val();
    objUser.Mobile = $("#RegMobile").val();
    objUser.EMail = $("#RegEMail").val();
    objUser.Password = $("#RegPassword").val();
    
    try {
        $.ajax({
            type: "POST",
            url: "/UserAccount/UserRegister",
            data: '{mdlUsers : ' + JSON.stringify(objUser) + '}',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                if (res.message > 0) {
                    alert("Login Successful");
                    location.reload(true);
                }
                if (res == null || res.message == "") {
                    alert("Error Occuured. Please try after some time.");
                }
                else {
                    //alert(res.responseText);
                }
            },
            error: function (res) {
                debugger;
                //alert("Error while Register the User " + res.responseText);
            }
        });
    }
    catch (e) {
        alert(e.message);
    }
}

//#region Login & Register Popup
//$("#btnLogin").click(function () {
//    var mailid = document.getElementById("loginemail").value;
//    var pwd = document.getElementById("loginPassword").value;
//    if (mailid == "" || pwd == "") {
//        alert("Please check EMail or Password");
//        return false;
//    }
//    $.ajax({
//        type: "GET",
//        url: "/Account/Login",
//        data: { email: mailid, password: pwd },
//        dataType: "JSON",
//        contentType: "application/json; charset=utf-8",
//        success: function (response) {
//            console.log(response);
//            if (!response.IsSuccess) {
//                alert("Please check EMail or Password");
//            }
//            else {
//                alert("User Login Successful");
//                location.reload();
//            }
//        },
//        error: function (response) {
//            alert("Error while User login " + response);
//        },
//        faillure: function (response) {
//            alert("Faillure while User login " + response);
//        }
//    });
//});

$("#loginPassword").keypress(function (event) {
    if (event.keyCode === 13) {
        $("#btnLogin").click();
    }
});

//#endregion Login