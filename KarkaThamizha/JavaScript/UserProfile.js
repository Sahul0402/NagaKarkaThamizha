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
                    alert("Password Changed Successfully");
                }
                else if (response.message == "Expired") {
                    alert("Session Expired");
                }
            },
            failure: function (response) {
                alert('Failure' + response);
            },
            error: function (response) {
                alert("Error while CHnage Password " + response);
            }
        });
    } catch (ex) {

    }
});
//#endregion Change Password

//#region User Profile
$("#ProfileRegister").click(function () {
    debugger;
    $.ajax({
        type: "Post",
        url: "/UserAccount/UserProfile",
        data: {  },
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            alert(response.message);
            if (response.message == "Success") {
                alert("Profile updated successfully");
            }
            else if (response.message == "Expired") {
                alert("Session Expired");
            }
            else if (response.message == null) {
                alert("");
            }
        },
        failure: function (response) {
            alert('Failure' + response);
        },
        error: function (response) {
            alert("Error while CHnage Password " + response);
        }
    });
});
//#endregion Update User Profile