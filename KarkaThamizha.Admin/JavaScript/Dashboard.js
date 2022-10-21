$(document).ready(function () {
    GetAllToDo();
});

function GetAllToDo() {
    var targetControl = "#ToDoList";
    $.ajax({
        cache: false,
        type: "GET",
        url: "/Master/GetAllToDo",
        data: {  },
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (response) {            
            $(targetControl).html(response);
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

$(document).on("click", "#btnAddToDO", function () {
    if ($("#txtToDo").val() == '') {
        alert('Please add To Do List.');
        return false;
    }
    AddToDoList($("#txtToDo").val());
});

function AddToDoList(toDo) {
    try {
        $.ajax({
            type: "POST",
            url: "/Master/AddToDoList",
            data: '{description: "' + toDo + '" }',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                if (res.message > 0) {
                    GetAllToDo();
                    $("#txtToDo").val() = '';
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
}

$('body').on('click', '#editToDo', function () {
    var value = $("#item_ToDoListID").val();
    alert(value);
});
//$(document).on("click", "#editToDo", function () {
//    var value = $("#item_ToDoListID").val();
//    alert(value);
//});

function myFunction() {
    var value = document.getElementById("item_ToDoListID").value;
    alert(value);
}

$(document).on("click", "#deleteToDo", function () {

    try {
        $.ajax({
            type: "POST",
            url: "/Master/Delete",
            data: '{id: "' + $("#ToDoListID").val() + '" }',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (res) {
                if (res.message == "Deleted") {
                    GetAllToDo();
                    $("#txtToDo").val() = '';
                }
                else {
                    alert(res.responseText);
                }
            },
            error: function (res) {
                alert("Error while deleting ToDo data" + res.message);
            }
        });
    }
    catch (e) {
        alert(e.message);
    }
});