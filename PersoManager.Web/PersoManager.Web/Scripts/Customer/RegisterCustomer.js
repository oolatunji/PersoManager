$(document).ready(function () {
    try {

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
        else {
            $('#cardProfile').html('<option>Loading Card Profiles...</option>');
            $('#cardProfile').prop('disabled', 'disabled');

            //Get Card Profiles
            $.ajax({
                url: settingsManager.websiteURL + 'api/CardProfileAPI/RetrieveCardProfiles',
                type: 'GET',
                async: true,
                cache: false,
                success: function (response) {
                    $('#cardProfile').html('');
                    $('#cardProfile').prop('disabled', false);
                    $('#cardProfile').append('<option value="">Select Card Profile</option>');
                    var roles = response.data;
                    var html = '';
                    $.each(roles, function (key, value) {
                        $('#cardProfile').append('<option value="' + value.ID + '">' + value.Name + '</option>');
                    });
                },
                error: function (xhr) {
                    displayMessage("error", 'Error experienced: ' + xhr.responseText, "Customer Management");
                }
            });
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err, "User Management");
    }
});

String.prototype.trimRight = function (charlist) {
    if (charlist === undefined)
        charlist = "\s";

    return this.replace(new RegExp("[" + charlist + "]+$"), "");
};

function register() {
    try {

        var formValid = $('#demo-form').parsley().validate();
        if (formValid) {
            $('#addBtn').html('<i class="fa fa-spinner fa-spin"></i>');
            $("#addBtn").attr("disabled", "disabled");

            var lastname = $('#lastname').val();
            var othernames = $('#othernames').val();
            var accountNumber = $('#accountNumber').val();
            var cardProfileID = $('#cardProfile').val();

            var user = JSON.parse(window.sessionStorage.getItem("loggedInUser"));
            var branch = user.Branch;

            var customerBranch = branch.ID;

            var data = { Surname: lastname, Othernames: othernames, AccountNumber: accountNumber, CardProfileID: cardProfileID, CustomerBranch: customerBranch };
            $.ajax({
                url: settingsManager.websiteURL + 'api/CustomerAPI/SaveCustomer',
                type: 'POST',
                data: data,
                processData: true,
                async: true,
                cache: false,
                success: function (response) {
                    displayMessage("success", response, "Customer Management");
                    $('#lastname').val('');
                    $('#othernames').val('');
                    $('#accountNumber').val('');
                    $('#cardProfile').val('');

                    $("#addBtn").removeAttr("disabled");
                    $('#addBtn').html('Add');
                },
                error: function (xhr) {
                    displayMessage("error", 'Error experienced: ' + xhr.responseText, "Customer Management");
                    $("#addBtn").removeAttr("disabled");
                    $('#addBtn').html('Add');
                }
            });
        } else {
            displayMessage("warning", "Fill the required values", "Customer Management");
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err, "Customer Management");
        $("#addBtn").removeAttr("disabled");
        $('#addBtn').html('Add');
    }
}