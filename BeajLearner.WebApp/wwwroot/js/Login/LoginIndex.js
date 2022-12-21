//const { error } = require("jquery");
//const { Doc } = require("../../ckeditor/samples/toolbarconfigurator/lib/codemirror/codemirror");

$(document).ready(function () {

    $(".loading-icon").addClass("hide");
    $(".button").attr("disabled", false);
  
});

$(document).ready(function () {

 
});





function loginclick() {
  
    $("#loginspinner").removeAttr('hidden');

    $(".loading-icon").removeClass("hide");
    $(".button").attr("disabled", true);



    var email = $('#Email').val();
    var password = $('#Password').val();


    if (email == "") {
        $('#lblEmail').text("Please Enter Email");
        $("#lblEmail").css("color", "red");

    }
    if (email != "") {
        $('#lblEmail').text("");

    }
    if (password == "") {
        $('#lblPassword').text("Please Enter Password");
        $("#lblPassword").css("color", "red");

    }
    if (password != "") {
        $('#lblPassword').text("");


    }

    if (email != "" && password != "") {


      
        var jsonData = JSON.stringify({ "Email": email, "Password": password });
        $.ajax(
            {

                type: 'POST',

             
              //  url: "https://lpsapps.com:6043/api/v1.0/Account/authenticate",
                 url: globalUrlForAPIs+"v1.0/Account/authenticate",
            


                data: jsonData,

                contentType: "application/json; charset=utf-8",
                dataType: "json",



                success: (res) => {

                    var jwt = res.data.jwToken;
                    sessionStorage.setItem("jwt", jwt);
                   
                    if (res.data.role == 'Teachers') {
                        redurl = 'Dashboard/teacherDash';
                        window.location.href = redurl;
                    }
                    else {
                        redurl = 'Dashboard/Index';
                        window.location.href = redurl;
                    }
                   
                    //if (res != 0 && res.role == "SuperAdmin") {
                    //    redurl = 'CourseCategoryWeb/AddCourseCategory';
                    //    window.location.href = redurl;

                    //}

                    //if (res.succeeded == false) {


                    //    redurl = 'Login/Index';
                    //    window.location.href = redurl;
                    //    alert("Wrong Email or Password");

                    //}

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    $(".loading-icon").addClass("hide");
                    $(".button").attr("disabled", false);
                    alert("Wrong Email or Password");
                    redurl = 'Login/Index';
                    location.reload();
                       
                }
                
               

            });


    }
}
