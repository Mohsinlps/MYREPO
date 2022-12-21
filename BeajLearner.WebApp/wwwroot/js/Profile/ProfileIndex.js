$(document).ready(function () {
    var id = $('#Id').val();
    debugger;
    $.ajax({
        type: "Get",
        url: `https://localhost:7099/profile/GetUserDetail?Id=${id}`,
        success: function (userObj) {
            debugger;
            console.log(userObj);
            $('input#email').val(userObj.data.email);
            $('input#firstName').val(userObj.data.firstName);
            $('input#lastName').val(userObj.data.lastName);
            $('input#role').val(userObj.data.role);
            $('.author p').append(userObj.data.email);
            $('.author h6').append(userObj.data.firstName+" "+userObj.data.lastName);
        }, //End of AJAX Success function

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function

    });

    $(function () {
       
        $("#formProfile").validate({
            rules: {
                lastName: {
                    required: true
                },
                firstName: {
                    required: true
                }
            },
            messages: {
                firstName: {
                    required: "First Name is a required field."
                },
                lastName: {
                    required: "Last Name is a required field."
                }
            }
        })
    });
    $(function () {
        $.validator.addMethod('strongPassword', function (value, element) {
            return this.optional(element)
                || value.length >= 7
                && /[a-z]/i.test(value)
                && /[A-Z]/i.test(value)
                && /[0-9]/i.test(value);
        }, 'Passwords must have at least one non alphanumeric character.\r\nPasswords must have at least one digit (\'0\'-\'9\').\r\nPasswords must have at least one uppercase.')
        $("#formChangePassword").validate({
            rules: {
                newPassword: {
                    required: true,
                    strongPassword: true
                },
                confirmedPassword: {
                    required: true,
                    equalTo: "#newPassword"
                }
            },
            messages: {
                newPassword: {
                    required: "Password is required"
                },
                confirmedPassword: {
                    required: "Confirmed Password is required.",
                    equalTo: "Password doesn't match with new password"
                }
            }
        })
    });
    
});
FormProfile = $('#formProfile');
$("#formProfile").submit(function (event) {
    event.preventDefault();

    if (!FormProfile.valid())
        return;
    event.preventDefault();
    console.log("formsubmit");
        debugger;
        var id = $('#Id').val();
        var firstName = $('#firstName').val();
        var lastName = $('#lastName').val();
        var email = $('#email').val();
        var role = $('#role').val();
        debugger;
        $.ajax({
            type: "Post",
            url: `https://localhost:7099/profile/UpdateUser?Id=${id}&&lastName=${lastName}&&firstName=${firstName}&&role=${role}&&email=${email}`,
            success: function (userObj) {
                debugger;
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
FormPassword = $('#formChangePassword');
$("#formChangePassword").submit(function (event) {
    event.preventDefault();
    if (!FormPassword.valid())
        return;
        var id = $('#Id').val();
        var newPassword = $('#newPassword').val();
        var confirmedPassword = $('#confirmedPassword').val();
        debugger;
        $.ajax({
            type: "Post",
            url: `https://localhost:7099/profile/UpdateUserPassword?Id=${id}&&newPassword=${newPassword}&&confirmedPassword=${confirmedPassword}`,
            success: function (userObj) {
                debugger;
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
