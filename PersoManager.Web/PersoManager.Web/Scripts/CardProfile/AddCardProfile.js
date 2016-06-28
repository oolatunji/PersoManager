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

    if (!authorized)
        window.location.href = '../System/UnAuthorized';
});

String.prototype.trimRight = function (charlist) {
    if (charlist === undefined)
        charlist = "\s";

    return this.replace(new RegExp("[" + charlist + "]+$"), "");
};

function addCardProfile() {
    try {

        var formValid = $('#demo-form').parsley().validate();
        if (formValid) {
            $('#addBtn').html('<i class="fa fa-spinner fa-spin"></i>');
            $("#addBtn").attr("disabled", "disabled");

            var name = $('#name').val();
            var cardType = $('#cardType').val();
            var cardBin = $('#bin').val();
            var ceDuration = $('#ceduration').val();

            var data = { Name: name, CardType: cardType, CardBin: cardBin, CEDuration: ceDuration };
            $.ajax({
                url: settingsManager.websiteURL + 'api/CardProfileAPI/SaveCardProfile',
                type: 'POST',
                data: data,
                processData: true,
                async: true,
                cache: false,
                success: function (response) {
                    displayMessage("success", response, "Card Profile Management");
                    $('#name').val('');
                    $('#cardType').val('');
                    $('#bin').val('');
                    $('#ceduration').val('');
                    $("#addBtn").removeAttr("disabled");
                    $('#addBtn').html('Add');
                },
                error: function (xhr) {
                    displayMessage("error", 'Error experienced: ' + xhr.responseText, "Card Profile Management");
                    $("#addBtn").removeAttr("disabled");
                    $('#addBtn').html('Add');
                }
            });
        } else {
            displayMessage("warning", "Fill the required values", "Card Profile Management");
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err, "Card Profile Management");
        $("#addBtn").removeAttr("disabled");
        $('#addBtn').html('Add');
    }
}