$(document).ready(function () {

    debugger;
    $.ajax({
        type: "Get",
        url: "https://localhost:7099/user/GetUser",
        success: function (userObj) {
            debugger;
            $("#dataTable").DataTable({
                data: userObj.data,
                columns: [
                    { 'data': 'firstName' },
                    { 'data': 'lastName' },
                    { 'data': 'email' },
                    { 'data': 'role' },
                ]
            });
            console.log(userObj);
        }, //End of AJAX Success function

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function

    });
});