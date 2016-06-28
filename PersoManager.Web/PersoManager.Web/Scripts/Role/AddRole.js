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
            $('#dynamicfunctions').html('<p style="font-family:Calibri;color:green;"><i class="fa fa-cog fa-spin"></i> Loading functions...</p>');
            $.ajax({
                url: settingsManager.websiteURL + 'api/FunctionAPI/RetrieveFunctions',
                type: 'GET',
                async: true,
                cache: false,
                success: function (response) {
                    var functions = response.data;
                    var html = '';
                    $.each(functions, function (key, value) {
                        html += '<input type="checkbox" name="functions" value="' + value.ID + '"/>' + value.Name + '<br/>';
                    });
                    $('#dynamicfunctions').html('');
                    $('#dynamicfunctions').append(html);
                },
                error: function (xhr) {
                    displayMessage("error", 'Error experienced: ' + xhr.responseText, "Roles Management");
                }
            });
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err, "Roles Management");
    }
});

String.prototype.trimRight = function (charlist) {
    if (charlist === undefined)
        charlist = "\s";

    return this.replace(new RegExp("[" + charlist + "]+$"), "");
};

function testing() {
    var formInstance = $('#demo-form').parsley();
    console.log(formInstance.isValid());
}

function addRole() {
    try {

        var formValid = $('#demo-form').parsley().validate();
        if (formValid) {
            $('#addBtn').html('<i class="fa fa-spinner fa-spin"></i>');
            $("#addBtn").attr("disabled", "disabled");

            var roleName = $('#roleName').val();
            var roleFunctions = [];
            $("input:checkbox[name=functions]:checked").each(function () {
                var roleFunction = {};
                var _function = $(this).val();
                roleFunction = { FunctionID: _function };
                roleFunctions.push(roleFunction);
            });

            var data = { Name: roleName, RoleFunctions: roleFunctions };
            $.ajax({
                url: settingsManager.websiteURL + 'api/RoleAPI/SaveRole',
                type: 'POST',
                data: data,
                processData: true,
                async: true,
                cache: false,
                success: function (response) {
                    displayMessage("success", response, "Roles Management");
                    $('#roleName').val('');
                    $('#dynamicfunctions input[type=checkbox]').removeAttr('checked');
                    $("#addBtn").removeAttr("disabled");
                    $('#addBtn').html('Add');
                },
                error: function (xhr) {
                    displayMessage("error", 'Error experienced: ' + xhr.responseText, "Roles Management");
                    $("#addBtn").removeAttr("disabled");
                    $('#addBtn').html('Add');
                }
            });
        } else {
            displayMessage("warning", "Fill the required values", "Roles Management");
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err, "Roles Management");
        $("#addBtn").removeAttr("disabled");
        $('#addBtn').html('Add');
    }
}