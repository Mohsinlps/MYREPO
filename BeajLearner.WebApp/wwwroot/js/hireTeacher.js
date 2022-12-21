$(document).on('click', '#btnHire', function () {

    alert('hire');
    var fname = $('#first_name').val();
    var lname = $('#last_name').val();
    var email = $('#email').val();
    var password = $('#password').val();
    var confirmPassword = $('#password_confirmation').val();
    var role = 'Teachers';

    $.ajax({

        type: 'POST',
        dataType: 'JSON',
        data: {"CellNumber":"", "FirstName": fname, "LastName": lname, "Email": email, "Password": password, "ConfirmPassword": confirmPassword, "Role": role },
        url: globalUrlForAPIs + 'v1.0/Account/register',
        success: (res) =>
        {
            alert('Registered successfully');
           
        }
    })
});


//-----------------------------------------------  Loading All Teachers---------------------------

//$(document).ready(function () {
   
   
//    var jwttoken = sessionStorage.getItem("jwt");
    
//    $.ajax({
//        url: globalUrlForAPIs +'v1/User/AllUsers',
//        dataType:'JSON',
//        type: 'GET',
//        // Fetch the stored token from localStorage and set in the header
//        headers: {
//            "Authorization": 'Bearer ' + jwttoken
//        },
//        success: (res) =>
//        {
            
            
//            var role;
//            var i;
//            var lName;
//            var fName;
//            for (i = 0; i < res.data.length; ++i) {
//                role = res.data[i].role;
//                id = res.data[i].id;
//                fName = res.data[i].firstName;
//                lName = res.data[i].lastName;
//                var fullname = fName +" "+ lName;
//                if (role == 'Teachers')
//                {

//                    $('#drpTeachers').append(`<option value="${id}">
//                                       ${fullname}
//                                  </option>`);
//                }
//            }
//        },
//        error: (res) =>
//        {
//            alert('something went wrong');
//        }


//    });
//});




//-------------------------------- Load Course Categories------------------------

       
//$(document).ready
//    (


//        function () {
//            $.ajax(
//                {
//                    type: 'GET',
//                    url: globalUrlForAPIs + 'CourseCategory/GetAllCourseCategories',

//                    success: (res) => {


//                        $.each(res, function (i, item) {
//                            $('#drpCoursesCateg').append(`<option value="${item.courseCategoryId}">
//                                       ${item.courseCategoryName}
//                                  </option>`)
//                        });


//                    }
//                }
//            )

//        }


//    )






$(document).ready
    (


        function () {
            $.ajax(
                {
                    type: 'GET',
                    url: globalUrlForAPIs + 'CourseCategory/GetAllCourseCategories',

                    success: (res) => {


                        $.each(res, function (i, item) {
                            $('#container')
                                .append(`<input type="checkbox" class="chkCourse" name="courses" value="${item.courseCategoryId}">`)
                                .append(`<label for="chkCourse">${item.courseCategoryName}</label></div>`)
                                .append(`<br>`);
                        });


                        //for (var value of res) {
                        //    $('#container')
                        //        .append(`<input type="checkbox" id="${value}" name="interest" value="${value}">`)
                        //        .append(`<label for="${value}">${value}</label></div>`)
                        //        .append(`<br>`);
                        //}
                    }
                }
            )

        }


    )

//----------------------------------------  btnAssign click---------------------------




$(document).on('click', '#btnAssign', function () {
   
    var formData = new FormData();
    var courses = [];
    var teacherId = $('#drpTeachers').find(":selected").val();

    console.log(formData);
    formData.append('teacherId', teacherId);
    var i = 0;
    $('input[name="courses"]:checked').each(function () {
       
        formData.append("courseId", this.value);
        courses[i] = this.value;
        i++;
    });

    console.log(courses);
    console.log(formData);
   
    if (courses.length != 0  && $('#drpTeachers').find(":selected").val() != 'Select Teacher') {
        $.ajax({
            type: 'POST',
            dataType: 'JSON',
            data: formData,
            processData: false,
            contentType: false,

            url: globalUrlForAPIs + 'TeachersManage/AssignCourse',
            success: (res) => {
                alert('success');
            }
        })

    }
    else {
        alert('please select teacher and Courses')
    }

});


//----------------------------------------------------------------------