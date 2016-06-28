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
            if (window.sessionStorage.getItem('user') !== null) {
                window.sessionStorage.removeItem('user');
            }
            getUsers();
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

function getUsers() {

    $('#example tfoot th').each(function () {
        var title = $('#example tfoot th').eq($(this).index()).text();
        if (title != "")
            $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    });

    var table = $('#example').DataTable({

        "processing": true,

        "ajax": settingsManager.websiteURL + 'api/UserAPI/RetrieveUsers',

        "columns": [
            {
                "className": 'details-control',
                "orderable": false,
                "data": null,
                "defaultContent": ''
            },
            {
                "className": 'edit-control',
                "orderable": false,
                "data": null,
                "defaultContent": ''
            },
            { "data": "Lastname" },
            { "data": "Othernames" },
            { "data": "Username" },
            { "data": "Role.Name" },
            { "data": "Branch.Name" },
            { "data": "Gender" },
            {
                "data": "PhoneNumber",
                "visible": false
            },
            {
                "data": "Email",
                "visible": false
            },
            {
                "data": "CreatedOn",
                "visible": false
            },
            {
                "data": "Role.ID",
                "visible": false
            },
            {
                "data": "Branch.ID",
                "visible": false
            },
            {
                "data": "ID",
                "visible": false
            }
        ],

        "order": [[2, "asc"]],

        dom: 'Bfrtip',

        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [2, 3, 4, 5, 6, 7, 8, 9, 10]
                }
            },
            {
                extend: 'csvHtml5',
                exportOptions: {
                    columns: [2, 3, 4, 5, 6, 7, 8, 9, 10]
                }
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [2, 3, 4, 5, 6, 7, 8, 9, 10]
                },
                orientation: 'landscape',
                pageSize: 'A3'
            }
        ]
    });

    $('#example tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        function closeAll() {
            var e = $('#example tbody tr.shown');
            var rows = table.row(e);
            if (tr != e) {
                e.removeClass('shown');
                rows.child.hide();
            }
        }

        if (row.child.isShown()) {
            closeAll();
        }
        else {
            closeAll();

            row.child(formatDetails(row.data())).show();
            tr.addClass('shown');
        }
    });

    $('#example tbody').on('click', 'td.edit-control', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);
        var data = row.data();
        var update = confirm("Are you sure you want to update user: " + data.Username + "?");
        if (update == true) {
            window.sessionStorage.setItem('user', JSON.stringify(data));
            window.location.href = '../User/UpdateUser';
        } else {
            return;
        }
    });

    $("#example tfoot input").on('keyup change', function () {
        table
            .column($(this).parent().index() + ':visible')
            .search(this.value)
            .draw();
    });
};

function refreshResult() {
    try {
        var table = $('#example').DataTable();
        table.ajax.reload();
    } catch (err) {
        displayMessage("error", "Error encountered: " + err, "Functions Management");
    }
}

$(document).ready(function () {
    $('#dataTables-example').DataTable({
        responsive: true
    });
});

function format(d, roles, branches) {
    var table = '<table width="100%" class="cell-border" cellpadding="5" cellspacing="0" border="2" style="padding-left:50px;">';
    table += '<tr>';
    table += '<td style="color:navy;width:20%;font-family:Arial;">Lastname:</td>';
    table += '<td><input class="form-control" placeholder="Enter Lastname" id="lastname" value="' + d.Lastname + '"/></td>';
    table += '</tr>';
    table += '<tr>';
    table += '<td style="color:navy;width:20%;font-family:Arial;">Othernames:</td>';
    table += '<td><input class="form-control" placeholder="Enter Othernames" id="othernames" value="' + d.Othernames + '"/></td>';
    table += '</tr>';
    table += '<tr>';
    table += '<td style="color:navy;width:20%;font-family:Arial;">Gender:</td>';
    table += '<td><select class="form-control" name="gender" id="gender">';
    table += '<option value="">Select Gender</option>';
    if (d.Gender == "Male") {
        table += '<option selected="selected" value="Male">Male</option>';
        table += '<option value="Female">Female</option>';
    }
    else if (d.Gender == "Female") {
        table += '<option value="Male">Male</option>';
        table += '<option selected="selected" value="Female">Female</option>';
    }
    table += '</select></td></tr>';
    table += '<tr>';
    table += '<td style="color:navy;width:20%;font-family:Arial;">Phone Number:</td>';
    table += '<td><input class="form-control" placeholder="Enter Phone Number" id="phonenumber" value="' + d.PhoneNumber + '"/></td>';
    table += '</tr>';
    table += '<tr>';
    table += '<td style="color:navy;width:20%;font-family:Arial;">Email:</td>';
    table += '<td><input class="form-control" placeholder="Enter Email" id="email" value="' + d.Email + '"/></td>';
    table += '</tr>';
    table += '<tr>';
    table += '<td style="color:navy;width:20%;font-family:Arial;">Role:</td>';
    table += '<td><select class="form-control" name="role" id="role">';
    table += '<option value="">Select Role</option>';
    $.each(roles, function (key, value) {
        if (d.Role.ID == value.ID)
            table += '<option selected="selected" value="' + value.ID + '">' + value.Name + '</option>';
        else
            table += '<option value="' + value.ID + '">' + value.Name + '</option>';
    });
    table += '</select></td></tr>';
    table += '<tr>';
    table += '<td style="color:navy;width:20%;font-family:Arial;">Branch:</td>';
    table += '<td><select class="form-control" name="branch" id="branch">';
    table += '<option value="">Select Branch</option>';
    $.each(branches, function (key, value) {
        if (d.Branch.ID == value.ID)
            table += '<option selected="selected" value="' + value.ID + '">' + value.Name + '</option>';
        else
            table += '<option value="' + value.ID + '">' + value.Name + '</option>';
    });
    table += '</select></td></tr>';
    table += '<tr>';
    table += '<td style="display:none">Username:</td>';
    table += '<td style="display:none"><input class="form-control" id="username" value="' + d.Username + '"/></td>';
    table += '</tr>';
    table += '<tr>';
    table += '<td style="display:none">ID:</td>';
    table += '<td style="display:none"><input class="form-control" id="id" value="' + d.ID + '"/></td>';
    table += '</tr>';
    table += '<tr>';
    table += '<td style="color:navy;width:20%;font-family:Calibri;"></td>';
    table += '<td><button type="button"  id="updateBtn" class="btn btn-green" style="float:right;" onclick="update();"><i class="fa fa-cog"></i> Update</button></td>';
    table += '</tr>';
    table += '</table>';

    return table;
}

function formatDetails(d) {
    var table = '<table width="100%" class="cell-border" cellpadding="5" cellspacing="0" border="2" style="padding-left:50px;">';
    table += '<tr>';
    table += '<td style="color:navy;width:20%;font-family:Arial;">Gender</td>';
    table += '<td>' + d.Gender + '</td>';
    table += '</tr>';
    table += '<tr>';
    table += '<td style="color:navy;width:20%;font-family:Arial;">Phone Number</td>';
    table += '<td>' + d.PhoneNumber + '</td>';
    table += '</tr>';
    table += '<tr>';
    table += '<td style="color:navy;width:20%;font-family:Arial;">Email</td>';
    table += '<td>' + d.Email + '</td>';
    table += '</tr>';
    table += '<tr>';
    table += '<td style="color:navy;width:20%;font-family:Arial;">Created On</td>';
    table += '<td>' + d.CreatedOn + '</td>';
    table += '</tr>';
    table += '</table>';
    return table;
}