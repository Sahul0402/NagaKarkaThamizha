function ValidateEmail(email) {
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return expr.test(email);
};

$("#submitFeedback").click(function () {
    var objFeedback = {};

    //if ($("#Name").val() == '') {
    //    alert('Please enter Name');
    //    return true;
    //}
    //if ($("#Mobile").val() == '') {
    //    alert('Please enter Mobile');
    //    return true;
    //}
    //if ($("#EmailID").val() == '') {
    //    alert('Please enter EmailID');
    //    return true;
    //}
    //if ($("#Feedback").val() == '') {
    //    alert('Please enter your Comments');
    //    return true;
    //}

    objFeedback.Name = $("#Name").val();
    objFeedback.Mobile = $("#Mobile").val();
    objFeedback.EmailID = $("#EmailID").val();
    objFeedback.Feedback = $("#Feedback").val();

    $.ajax({
        type: "POST",
        url: '/Feedback/AddFeedback',
        data: '{mdlFeedback : ' + JSON.stringify(objFeedback) + '}',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response == "Success") {
                alert('Thanks for your feedback.');
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

