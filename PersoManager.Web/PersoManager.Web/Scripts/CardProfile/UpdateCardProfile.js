$(document).ready(function () {

    var currentUrl = window.location.href;
    var user = JSON.parse(window.sessionStorage.getItem("loggedInUser"));
    var userFunctions = user.Function;

    var authorized = false;
    $.each(userFunctions, function (key, userfunction) {
        var link = settingsManager.websiteURL.trimRight('/') + userfunction.PageLink;
        if (currentUrl == link) {
            authorized = true;
        }
    });

    if (!authorized) {
        window.location.href = '../System/UnAuthorized';
    } else {
        if (window.sessionStorage.getItem('cardprofile') === null) {
            window.location.href = '../CardProfile/ViewCardProfile';
        } else {
            var data = JSON.parse(window.sessionStorage.getItem('cardprofile'));
            $('#name').val(data.Name);
            $('#bin').val(data.CardBin);
            $('#ceduration').val(data.CEDuration);

            var cardTypeHtml = '';
            cardTypeHtml += '<option selected="selected" value="VISA">VISA</option>';
            $('#cardType').append(cardTypeHtml);
        }
    }
});

String.prototype.trimRight = function (charlist) {
    if (charlist === undefined)
        charlist = "\s";

    return this.replace(new RegExp("[" + charlist + "]+$"), "");
};

function updateCardProfile() {
    try {
        var formValid = $('#demo-form').parsley().validate();
        if (formValid) {
            $('#updateBtn').html('<i class="fa fa-spinner fa-spin"></i>');
            $("#updateBtn").attr("disabled", "disabled");

            var name = $('#name').val();
            var cardType = $('#cardType').val();
            var cardBin = $('#bin').val();
            var ceDuration = $('#ceduration').val();

            var cardprofile = JSON.parse(window.sessionStorage.getItem('cardprofile'));
            var id = cardprofile.ID;

            var data = { Name: name, CardType: cardType, CardBin: cardBin, CEDuration: ceDuration, ID: id };
            $.ajax({
                url: settingsManager.websiteURL + 'api/CardProfileAPI/UpdateCardProfile',
                type: 'PUT',
                data: data,
                processData: true,
                async: true,
                cache: false,
                success: function (response) {
                    displayMessage("success", response, "Card Profile Management");
                    $("#updateBtn").removeAttr("disabled");
                    $('#updateBtn').html('Update');
                },
                error: function (xhr) {
                    displayMessage("error", 'Error experienced: ' + xhr.responseText, "Card Profile Management");
                    $("#updateBtn").removeAttr("disabled");
                    $('#updateBtn').html('Update');
                }
            });
        } else {
            displayMessage("warning", "Fill the required values", "Card Profile Management");
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err, "Card Profile Management");
        $("#updateBtn").removeAttr("disabled");
        $('#updateBtn').html('Update');
    }
}
