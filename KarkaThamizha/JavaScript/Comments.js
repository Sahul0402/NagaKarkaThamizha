function ValidateEmail(email) {
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return expr.test(email);
};

$("#EMail").blur(function () {

    var targetControlInfo = $("#lblStatus");
    targetControlInfo.html("");
    targetControlInfo.html("wait...");

    var email = $("#EMail").val();
    if (email == null || email == "")
        return;

    if (ValidateEmail(email) == false) {
        targetControlInfo.html("Please enter valid Email");
        return;
    }

    $.ajax({
        type: "POST",
        url: "/User/IsMailExists",
        data: { mailId: email },
        dataType: "json",
        success: function (response) {
            if (response.message) {
                targetControlInfo.show();
                targetControlInfo.css("color", "red");
                targetControlInfo.html("Email already exists.");
            }
            else {
                targetControlInfo.html("");
            }
        },
        failure: function (response) {
            targetControlInfo.html("Error Occured. Please try again.");
        },
    });
});

$("#submitComments").click(function () {
    var objComments = {};

    //if ($("#Name").val() == '') {
    //    alert('Please enter Name');
    //    return true;
    //}
    //if ($("#Mobile").val() == '') {
    //    alert('Please enter Mobile');
    //    return true;
    //}
    //if ($("#EMail").val() == '') {
    //    alert('Please enter EmailID');
    //    return true;
    //}
    //if ($("#Comments").val() == '') {
    //    alert('Please enter your Comments');
    //    return true;
    //}

    objComments.Name = $("#Name").val();
    objComments.EMail = $("#EMail").val();
    objComments.Password = $("#Password").val();
    objComments.Mobile = $("#Mobile").val();
    objComments.Comments = $("#Comments").val();
    objComments.MasterPageId = 1;
    objComments.ChildPageId = GetParameterValues('booksReviewID');

    $.ajax({
        type: "POST",
        url: '/Comments/AddComments',
        data: '{mdlComments : ' + JSON.stringify(objComments) + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response == "Success") {
                alert('Thanks for your Comments.');
                ClearControls();
            }
            else if (response == "Fail") {
                alert('Something went wrong. Please try again sometime.');
            }
            else if (response == "") {
                alert('Please enter all fields.');
            }
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

function GetParameterValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}

function ClearControls() {
    $("#Name").val('');
    $("#EMail").val('');
    $("#Password").val('');
    $("#Mobile").val('');    
    $("#Comments").val('');
}