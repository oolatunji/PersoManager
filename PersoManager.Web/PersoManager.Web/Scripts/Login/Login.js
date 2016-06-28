function LoginIn() {

    try {
        var formValid = $('#demo-form').parsley().validate();
        if (formValid) {
            var username = $('#username').val();
            var password = $('#password').val();

            $('#addBtn').val('processing...');

            var data = { Username: username, Password: password };
            $.ajax({
                url: settingsManager.websiteURL + 'api/UserAPI/AuthenticateUser',
                type: 'POST',
                data: data,
                processData: true,
                async: true,
                cache: false,
                success: function (data) {
                    //Remove local storages if they exist before adding new ones
                    if (window.sessionStorage.getItem("loggedInUser") != null)
                        window.sessionStorage.removeItem("loggedInUser");

                    //Add new local storages
                    var user = JSON.stringify(data);

                    window.sessionStorage.setItem("loggedInUser", user);

                    window.location = ("Home/Dashboard");

                },
                error: function (xhr) {
                    if (xhr.status == 404)
                        displayMessage("error", 'Error experienced: Incorrect System Application Url.', "User Login");
                    else
                        displayMessage("error", 'Error experienced: ' + xhr.responseText, "User Login");
                    console.log(xhr);
                    $('#addBtn').val('Login');
                }
            });
        } else {
            displayMessage("warning", "Fill the required values", "User Login");
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err, "User Login");
        console.log(err);
        $('#addBtn').val('Login');
    }
}