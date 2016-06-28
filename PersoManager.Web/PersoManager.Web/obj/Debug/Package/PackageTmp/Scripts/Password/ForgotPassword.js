function retrievePassword() {

    try {
        var formValid = $('#demo-form').parsley().validate();
        if (formValid) {
            var username = $('#username').val();

            $('#addBtn').html('<i class="fa fa-spinner fa-spin"></i>');
            $("#addBtn").attr("disabled", "disabled");

            var data = { Username: username };
            $.ajax({
                url: settingsManager.websiteURL + 'api/UserAPI/ForgotPassword',
                type: 'PUT',
                data: data,
                processData: true,
                async: true,
                cache: false,
                success: function (data) {
                    displayMessage("success", "An email that contains a link to continue with your password reset has been sent to your email: " + data + ". If this email address is not correct, kindly contact your administrator to modify accordingly", "Password Management");
                    $('#username').val("");
                    $("#addBtn").removeAttr("disabled");
                    $('#addBtn').html('Retrieve');
                },
                error: function (xhr) {
                    displayMessage("error", "Error encountered: " + xhr.responseText, "Password Management");
                    $("#addBtn").removeAttr("disabled");
                    $('#addBtn').html('Retrieve');
                }
            });
        } else {
            displayMessage("warning", "Fill the required values", "Password Management");
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err, "Password Management");
        console.log(err);
        $("#addBtn").removeAttr("disabled");
        $('#addBtn').html('Retrieve');
    }
}
