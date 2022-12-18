var _userStars = 0, _userRating = { Rating: 0 };
function designRating($elem, maxRating, bookTitle, rating) {
    $elem.starRating(
        {
            starIconEmpty: 'far fa-star',
            starIconFull: 'fas fa-star',
            starColorEmpty: 'lightgray',
            starColorFull: 'rgb(87 153 239)',
            starsSize: 1.8, // em
            showInfo: false,
            stars: maxRating,
            defaultStars: rating || _userRating['Rating']
        });

    $('#lblRateTitle').text(bookTitle);
    $(document).on('change', '.ratingrating',
        function (e, stars, index) {
            _userStars = stars;
            $('#hdnUserRating').val(stars);
            $('#btnRate').addClass('remove-rate').removeClass('add-rate');
            if (stars > 0) {
                $('#btnRate').removeClass('remove-rate').addClass('add-rate').prop('disabled', false);
            }
        });

    $('#btnRemoveRating').hide();
    if (_userRating['Rating'] > 0) {
        $('#btnRemoveRating').show();
    }
    $("#dialog-message").dialog("open");
}
$(function () {
    var _maxRating = $('#hdnMaxRating').val();
    var _rating = $('#hdnRating').val();
    _userRating['Rating'] = _rating;
    GetUserRatingByUserAndBookID();
    $("#dialog-message").dialog({
        modal: true,
        autoOpen: false,
        maxWidth: 450,
        maxHeight: 250,
        minWidth: 450,
        minHeight: 250,
        width: 450,
        height: 250,
        open: function () {
            $(this).closest(".ui-dialog")
                .find(".ui-dialog-titlebar-close")
                .removeClass("ui-dialog-titlebar-close")
                .html("<span class='fa fa-times'></span>");
        }
    });
    var $ratings = null;
    $('#elmRateUs').on('click', function () {
        designRating($('.ratingrating'), _maxRating);
        /*
        $('.ratingrating').starRating(
            {
                starIconEmpty: 'far fa-star',
                starIconFull: 'fas fa-star',
                starColorEmpty: 'lightgray',
                starColorFull: 'rgb(87 153 239)',
                starsSize: 1.8, // em
                showInfo: false,
                stars: _maxRating,
                defaultStars: _userRating['Rating']
            });

        $(document).on('change', '.ratingrating',
            function (e, stars, index) {
                _userStars = stars;
                $('#hdnUserRating').val(stars);
                $('#btnRate').addClass('remove-rate').removeClass('add-rate');
                if (stars > 0) {
                    $('#btnRate').removeClass('remove-rate').addClass('add-rate').prop('disabled', false);
                }
            });

        $('#btnRemoveRating').hide();
        if (_userRating['Rating'] > 0) {
            $('#btnRemoveRating').show();
        }
        $("#dialog-message").dialog("open");
        */
    });

    $("#dialog-message").on("dialogclose", function (event, ui) {
        $('#divRateTool').removeClass('js-wc-star-rating');
        $('#divRateTool').empty();
    });
});

function desingRatingTool(ratingControlId,) {
}

function onAddOrUpdateUserRating(isLogin, userRatingCallback) {
    var userID = $("#hdnSession").val();
    var bookID = $("#hdnBookdDetailID").val();

    $.ajax({
        cache: false,
        type: 'GET',
        url: "/UserRating/AddOrUpdateUserRating",
        data: { bookId: bookID, userRating: _userStars },
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (response, status, xhr) {
            //if ((typeof userRatingCallback) == "function") {
            //    userRatingCallback();
            //}
            //else { 
            $("#dialog-message").dialog("close");
            debugger
            if (isLogin) {
                location.reload();
            }

            if (!response) {
                LoadLoginControl(true);
                //$('#linklogin').trigger('click');
            }
            else {
                if ((typeof userRatingCallback) == "function") {
                    userRatingCallback();
                    return;
                }
                GetUserRatingByUserAndBookID();
            }
            //}
        },
        error: function (jqXHR, exception) {

        }
    });
}

function onRemoveUserRating(e) {
    _userStars = 0;
    onAddOrUpdateUserRating();
}
function GetUserRatingByUserAndBookID() {
    var userID = $("#hdnSession").val();
    var bookID = $("#hdnBookdDetailID").val();
    if (bookID > 0) {
        $.ajax({
            cache: false,
            type: 'GET',
            url: "/UserRating/GetUserRatingByUserAndBookID",
            data: { userId: userID, bookId: bookID },
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (response, status, xhr) {
                debugger
                _userRating = response;
                $('#divRating').show();
                $('#btnRate').removeClass('remove-rate').addClass('add-rate').prop('disabled', false);
                if (_userRating['Rating'] == 0) {
                    $('#divRating').hide();
                    $('#btnRate').addClass('remove-rate').removeClass('add-rate').prop('disabled', true);
                }
                $('#lblAvgUserRating').text(_userRating['AvgUserRating'])
                $('#lblRateTitle').text(_userRating['Book']);
                $('#lblUserRating').text(_userRating['Rating']);
                $('#lblRating, #lblAvgRating').text("/" + _userRating['MaxRating']);
                /*$('#lblAvgRating').text(_userRating['MaxRating']);*/
            },
            error: function (jqXHR, exception) {

            }
        });
    }
} 