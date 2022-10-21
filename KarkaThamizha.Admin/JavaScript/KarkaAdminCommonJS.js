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