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
        if (window.sessionStorage.getItem('branch') === null) {
            window.location.href = '../Branch/ViewBranch';
        } else {
            var data = JSON.parse(window.sessionStorage.getItem('branch'));
            $('#branchName').val(data.Name);
            $('#branchCode').val(data.Code);
            $('#branchAddress').val(data.Address);
        }
    }
});

String.prototype.trimRight = function (charlist) {
    if (charlist === undefined)
        charlist = "\s";

    return this.replace(new RegExp("[" + charlist + "]+$"), "");
};

function updateBranch() {
    try {
        var formValid = $('#demo-form').parsley().validate();
        if (formValid) {
            $('#updateBtn').html('<i class="fa fa-spinner fa-spin"></i>');
            $("#updateBtn").attr("disabled", "disabled");

            var branchName = $('#branchName').val();
            var branchCode = $('#branchCode').val();
            var branchAddress = $('#branchAddress').val();

            var branch = JSON.parse(window.sessionStorage.getItem('branch'));
            var id = branch.ID;

            var data = { Name: branchName, Code: branchCode, Address: branchAddress, ID: id };
            $.ajax({
                url: settingsManager.websiteURL + 'api/BranchAPI/UpdateBranch',
                type: 'PUT',
                data: data,
                processData: true,
                async: true,
                cache: false,
                success: function (response) {
                    displayMessage("success", response, "Branch Management");
                    $("#updateBtn").removeAttr("disabled");
                    $('#updateBtn').html('Update');
                },
                error: function (xhr) {
                    displayMessage("error", 'Error experienced: ' + xhr.responseText, "Branch Management");
                    $("#updateBtn").removeAttr("disabled");
                    $('#updateBtn').html('Update');
                }
            });
        } else {
            displayMessage("warning", "Fill the required values", "Branch Management");
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err, "Branch Management");
        $("#updateBtn").removeAttr("disabled");
        $('#updateBtn').html('Update');
    }
}
