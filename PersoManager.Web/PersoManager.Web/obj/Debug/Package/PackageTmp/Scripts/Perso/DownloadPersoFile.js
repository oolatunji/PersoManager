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
        displayMessage("error", "Error encountered: " + err, "EMV Instant Card Issuance");
    }
});

String.prototype.trimRight = function (charlist) {
    if (charlist === undefined)
        charlist = "\s";

    return this.replace(new RegExp("[" + charlist + "]+$"), "");
};

var table;

function download() {
    // Iterate over all checkboxes in the table
    var customers = [];
    $("input:checkbox[name=customers]:checked").each(function () {
        var _customer = $(this).val();
        customers.push(_customer);
    });
    if (customers.length == 0) {
        displayMessage("warning", "Please select the records you want to download", "Perso File");
    } else {
        try {
            $('#addBtn').html('<i class="fa fa-spinner fa-spin"></i>');
            $("#addBtn").attr("disabled", "disabled");

            var data = { CustomerIDs: customers };
            $.ajax({
                url: settingsManager.websiteURL + 'api/CustomerAPI/DownloadPersoFile',
                type: 'POST',
                data: data,
                processData: true,
                async: true,
                cache: false,
                success: function (response) {
                    displayMessage("success", response, "EMV Instant Card Issuance");
                    refreshResult();
                    $("#addBtn").removeAttr("disabled");
                    $('#addBtn').html('Download Perso File');
                },
                error: function (xhr) {
                    displayMessage("error", 'Error experienced: ' + xhr.responseText, "EMV Instant Card Issuance");
                    $("#addBtn").removeAttr("disabled");
                    $('#addBtn').html('Download Perso File');
                }
            });

        } catch (e) {
            displayMessage("warning", "Error encountered: " + e, "EMV Instant Card Issuance");
            $("#addBtn").removeAttr("disabled");
            $('#addBtn').html('Download Perso File');
        }
    }
}

function getCustomers() {

    $('#example tfoot th').each(function () {
        var title = $('#example thead th').eq($(this).index()).text();
        if (title != "")
            $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    });

    table = $('#example').DataTable({

        "processing": true,

        "ajax": settingsManager.websiteURL + 'api/CustomerAPI/RetrieveCustomerPersoData',

        'columnDefs': [{
            'targets': 0,
            'searchable': false,
            'orderable': false,
            'className': 'dt-body-center',
            'render': function (data, type, full, meta) {
                if (full.Downloaded === true) {
                    return '';
                } else {
                    return '<input type="checkbox" name="customers" value="' + data + '">';
                }
            },
        }],

        "columns": [
            {"data": "ID"},
            { "data": "Surname" },
            { "data": "Othernames" },
            { "data": "AccountNumber" },
            { "data": "CardPan" },
            { "data": "CardExpiryDate" },
        ],

        "order": [[0, "asc"]],
    });

    // Handle click on "Select all" control
    $('#example-select-all').on('click', function () {
        // Get all rows with search applied
        var rows = table.rows({ 'search': 'applied' }).nodes();
        // Check/uncheck checkboxes for all rows in the table
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });

    // Handle click on checkbox to set state of "Select all" control
    $('#example tbody').on('change', 'input[type="checkbox"]', function () {
        // If checkbox is not checked
        if (!this.checked) {
            var el = $('#example-select-all').get(0);
            // If "Select all" control is checked and has 'indeterminate' property
            if (el && el.checked && ('indeterminate' in el)) {
                // Set visual state of "Select all" control 
                // as 'indeterminate'
                el.indeterminate = true;
            }
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
        displayMessage("error", "Error encountered: " + err, "EMV Instant Card Issuance");
    }
}

$(document).ready(function () {
    $('#dataTables-example').DataTable({
        responsive: true
    });
});

