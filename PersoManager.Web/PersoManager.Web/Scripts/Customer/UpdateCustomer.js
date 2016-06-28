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
        if (window.sessionStorage.getItem('customer') === null) {
            window.location.href = '../Customer/ViewCustomer';
        } else {
            var data = JSON.parse(window.sessionStorage.getItem('customer'));
            $('#lastname').val(data.Surname);
            $('#othernames').val(data.Othernames);
        }
    }
});

String.prototype.trimRight = function (charlist) {
    if (charlist === undefined)
        charlist = "\s";

    return this.replace(new RegExp("[" + charlist + "]+$"), "");
};

function updateCustomer() {
    try {
        var formValid = $('#demo-form').parsley().validate();
        if (formValid) {
            $('#updateBtn').html('<i class="fa fa-spinner fa-spin"></i>');
            $("#updateBtn").attr("disabled", "disabled");

            var lastname = $('#lastname').val();
            var othernames = $('#othernames').val();

            var customer = JSON.parse(window.sessionStorage.getItem('customer'));
            var id = customer.ID;

            var data = { Surname: lastname, Othernames: othernames, ID: id };
            $.ajax({
                url: settingsManager.websiteURL + 'api/CustomerAPI/UpdateCustomer',
                type: 'PUT',
                data: data,
                processData: true,
                async: true,
                cache: false,
                success: function (response) {
                    displayMessage("success", response, "Customer Management");
                    $("#updateBtn").removeAttr("disabled");
                    $('#updateBtn').html('Update');
                },
                error: function (xhr) {
                    displayMessage("error", 'Error experienced: ' + xhr.responseText, "Customer Management");
                    $("#updateBtn").removeAttr("disabled");
                    $('#updateBtn').html('Update');
                }
            });
        } else {
            displayMessage("warning", "Fill the required values", "Customer Management");
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err, "Customer Management");
        $("#updateBtn").removeAttr("disabled");
        $('#updateBtn').html('Update');
    }
}
