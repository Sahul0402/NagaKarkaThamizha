//#region Change Password
$("#SaveChangePassword").click(function () {
    var pwd = document.getElementById("Password").value;
    var conPwd = document.getElementById("ConfirmPassword").value;
    if (pwd == "") {
        alert("Please enter Password");
        return true;
    }
    if (conPwd == "") {
        alert("Please enter Confirm Password");
        return true;
    }
    if (pwd != conPwd) {
        alert("Password and Confirm Password not match");
        return true;
    }
    try {
        $.ajax({
            type: "Post",
            url: "/Login/ChangePassword",
            data: { password: pwd, confirmpwd: conPwd },
            dataType: "JSON",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                alert(response.message);
                if (response.message == "Success") {
                    alert("Password Changed Successful");
                }
                else if (response.message == "Expired") {
                    alert("Session Expired");
                }
            },
            failure: function (response) {
                alert('Failure' + response);
            },
            error: function (response) {
                alert("Error while User login " + response);
            }
        });
    } catch (ex) {

    }
});
//#endregion Change Password



//#region Add User Books Review
$("#SaveBookReview").click(function () {
    debugger;
    var bookId = document.getElementById("AuthorID").value;
    var header = document.getElementById("Header").value;
    var description = document.getElementById("Description").value;

    if (description == "") {
        alert("Please enter Description");
        return false;
    }

    $.ajax({
        type: "Post",
        url: "/BooksReview/AddBooksReviewByUser",
        data: { password: pwd, confirmpwd: conPwd },
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            alert(response.message);
            if (response.message == "Success") {
                alert("Password Changed Successful");
            }
            else if (response.message == "Expired") {
                alert("Session Expired");
            }
        },
        failure: function (response) {
            alert('Failure' + response);
        },
        error: function (response) {
            alert("Error while User login " + response);
        }
    });

});
//#endregion  Add User Books Review