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
            if (window.sessionStorage.getItem('customer') !== null) {
                window.sessionStorage.removeItem('customer');
            }
            getCustomers();
        }
    } catch (err) {
        displayMessage("error", "Error encountered: " + err, "Customer Management");
    }
});

String.prototype.trimRight = function (charlist) {
    if (charlist === undefined)
        charlist = "\s";

    return this.replace(new RegExp("[" + charlist + "]+$"), "");
};


function getCustomers() {

    $('#example tfoot th').each(function () {
        var title = $('#example thead th').eq($(this).index()).text();
        if (title != "")
            $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    });

    var table = $('#example').DataTable({

        "processing": true,

        "ajax": settingsManager.websiteURL + 'api/CustomerAPI/RetrieveCustomers',

        "columns": [
            { "data": "Surname" },
            { "data": "Othernames" },
            { "data": "AccountNumber" },
            {
                "className": 'edit-control',
                "orderable": false,
                "data": null,
                "defaultContent": ''
            },
            {
                "data": "ID",
                "visible": false
            },
        ],

        "order": [[0, "asc"]],

        dom: 'Bfrtip',

        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'csvHtml5',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: ':visible'
                }
            }
        ]
    });

    $('#example tbody').on('click', 'td.edit-control', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);
        var data = row.data();
        var update = confirm("Are you sure you want to update customer: " + data.Surname + "?");
        if (update == true) {
            window.sessionStorage.setItem('customer', JSON.stringify(data));
            window.location.href = '../Customer/UpdateCustomer';
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
}

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

