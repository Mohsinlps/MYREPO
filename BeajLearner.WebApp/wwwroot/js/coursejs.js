////const { Button } = require("../lib/bootstrap/dist/js/bootstrap");

/*const { Tab } = require("../lib/bootstrap/dist/js/bootstrap.esm");*/

//const { type } = require("jquery");
let courseCategoryIdGlobal;

$(document).ready
    (
        function () {
          //  $('#myModal').modal('toggle'),
           // $('#myModal').modal('show')
            // $('#myModal').modal('hide')


           
        }
)


//--------------------btn Add Click -------------------

$(document).on('click', '#btnAdd', function () {

    /* $('#lblAddSuccess').prop('hidden', true);*/
    $('#txtCourse').val('');
    $('.courseAddModal').modal('toggle');
    $('#coursecategoryid').prop('hidden', false);
    $('#lblSelect').prop('hidden', false);
    $('#lblAddedMessage').prop('hidden', true);
    $('#txtCourse').prop('hidden', false);
    $('#btnclick').prop('hidden', false);
});





//---------------------------load dropDown values----------------------------------
$(document).ready
    (
        function () {

            $.ajax(
                {
                    type: 'GET',
                    url: globalUrlForAPIs+'CourseCategory/GetAllCourseCategories',
                    success: (res) => {
                        var response = JSON.stringify(res);
                        var i;

                        $.each(res, function (i, item) {
                            $('#coursecategoryid').append(
                                `<option value="${item.courseCategoryId}">
                                       ${item.courseCategoryName}

                                 </option>`)
                        });




                    }
                }
            );
        }
    );

//-----------------------------Save Course Category----------------------------
$('#form').submit(function (event) {
    event.preventDefault();
    var isValid = $(this).validate().form();
    console.log(isValid);
    if (!isValid) return false;
    else {
        var formData = new FormData(document.getElementById("form"));
        console.log(formData);
        $.ajax(
            {
                url: globalUrlForAPIs + 'Course/AddCourse',
                data: formData,
                dataType: 'JSON',
                processData: false,
                contentType: false,
                type: 'POST',
                success: (res) => {

                    /*  alert("saved successfully");*/
                    coursecategoryid
                    $('#coursecategoryid').prop('hidden', true);
                    $('#lblSelect').prop('hidden', true);
                    
                    $('#txtCourse').val('');
                    $('#lblAddedMessage').prop('hidden', false);
                    $('#txtCourse').prop('hidden', true);
                    $('#btnclick').prop('hidden', true);
                    
                    loadCourses();
                },
                error: (res) => { alert('something went wrong') }
            });
    }
});



//--------------------------2nd DropDown-----------------------------------------
$(document).ready(function () {

    loadCourses();
    $.ajax({

        type: "GET",
        url: globalUrlForAPIs + "CourseCategory/GetAllCourseCategories",
        success: (res) => {

           
            var i;

            $.each(res, function (i, item) {
                $('#drpCategory').append(
                   
                    `<option value="${item.courseCategoryId}">
                                       ${item.courseCategoryName}

                                 </option>`)
            });
        }


    });


    //   $.ajax({
    //     type:"GET",
    //     url: globalUrlForAPIs + "Course/GetAllCourseModel",
    //      success: function (rec) {  
    //          alert('loaded');
    //          console.log(rec);

    //     }
    //});
});

//----------------------------------Load dataTable---------------------------
function loadCourses() {

    $.ajax({
        type: "GET",
        url: globalUrlForAPIs + "Course/GetAllCourseWithCategoryModel",
        success: function (rec) {
            console.log('GetAllCourseModel');
            console.log(rec);
            //debugger;
            var table = $('#dataTable').DataTable();

            //clear datatable
            table.clear().draw();

            //destroy datatable
            table.destroy();



            $("#dataTable").DataTable({
                data: rec,
               
                columns: [
                    { "data": "courseId", "visible": false },
                    { "data": "courseName" },
                    { "data": "courseCategoryName" },
                  
                    {
                        "data": null, "render": function (data) {

                            return '<button type="button" courseName="' + data.courseName + '" courseCategoryId="' + data.courseCategoryId + '" courseCategoryName="' + data.courseCategoryName + '" courseid="' + data.courseId + '"  class="btn btn-primary btnedit">Edit</button>'

                        }
                    },
                    {
                        "data": "courseId", "render": function (data) {

                            return '<button type="button" courseid="' + data + '"  class="btn btn-danger btndelete">Delete</button>'

                        }
                    }
                ],


            });
            
        }, //End of AJAX Success function

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function
    });

}

//-------------------------------Load Specific Courses----------------------

$(document).on('change', '#drpCategory', function () {

    var val = $(this).val();
   
    if (val == -1) {
        loadCourses();
    }
    else {
        console.log(val);
        $.ajax({

            type: 'POST',
            url: globalUrlForAPIs + 'Course/GetAllCourseWithCategoryModelById?id=' + val,

            dataType: 'JSON',
            success: function (res) {

                console.log(res);
                var table = $('#dataTable').DataTable();
                table.clear().draw();
                table.destroy();


                $("#dataTable").DataTable({
                    data: res,

                    columns: [
                        { "data": "courseId", "visible": false },
                        { "data": "courseName" },
                        { "data": "courseCategoryName" },

                        {
                            "data": null, "render": function (data) {

                                return '<button type="button" courseName="' + data.courseName + '" courseCategoryId="' + data.courseCategoryId + '" courseCategoryName="' + data.courseCategoryName + '" courseid="' + data.courseId + '"  class="btn btn-primary btnedit">Edit</button>'

                            }
                        },
                        {
                            "data": "courseId", "render": function (data) {

                                return '<button type="button" courseid="' + data + '"  class="btn btn-danger btndelete">Delete</button>'

                            }
                        }
                    ],


                });
            },
            error: function (data) {
                alert();
                var table = $('#dataTable').DataTable();
                table.clear().draw();
                table.destroy();



            } //End of AJAX error function

        });
    }
   
    
});


//--------------------btn Delete------------------------------

$(document).on("click",".btndelete" ,function ()
{
    
    var id = $(this).attr("courseId");
   
   // var jsonid = JSON.stringify('{"id": ' + id + '}');
   // alert(jsonid);
    $.ajax(
        {

            type: 'POST',
            url: globalUrlForAPIs + 'Course/DeleteCourse?id=' + id,

            dataType: 'JSON',
            success: function (res) {
               
                loadCourses();
            },
            error: function () { }

        });
});


//-------------------Edit-----------------------------------
$(document).on("click", ".btnedit", function () {

    $('#drpModalUpdate').show();
    $('#txtmodalCourse').show();
    $('.btnSaveCourse').show();
    $('#lblCourse').show();
    $('#lblCourseName').show();
   
    $('#lblMessage').prop('hidden', true);

  

    $('.courseEditModal').modal('toggle');
    var courseid = $(this).attr("courseId");
    var ccategoryId = $(this).attr("courseCategoryId");
    var courseName = $(this).attr("courseName");
    var courseCategoryName = $(this).attr("courseCategoryName");

  

    $('#txtmodalCourseId').val(courseid);
    $('#txtmodalCourseCategory').val(ccategoryId);
    $('#txtmodalCourse').val(courseName);
    
   
    $.ajax({
        type: "GET",
        url: globalUrlForAPIs + "CourseCategory/GetAllCourseCategories",
        success: function (rec) {
            console.log('check drop down data');
            console.log(rec)


            $('#drpModalUpdate').empty();
            var i;
            $('#drpModalUpdate').append
                (
                    `<option value="${ccategoryId}"> ${courseCategoryName} </option>`
                );
          
            $.each(rec, function (i, item) {
              
                $('#drpModalUpdate').append
                    (
                       
                 `<option value="${item.courseCategoryId}"> ${item.courseCategoryName} </option>`
                   
                    );
               

            });
           
           

        }, //End of AJAX Success function

        failure: function (data) {
            alert(data.responseText);
        }, //End of AJAX failure function
        error: function (data) {
            alert(data.responseText);
        } //End of AJAX error function
    });

});

//-----------------------Save------------------------------
$(document).on('click', '.btnSaveCourse', function () {
   
    $.ajax(
        {
            type: "POST",
            //dataType: 'JSON',
            url: globalUrlForAPIs + 'Course/UpdateCourse',
            data: {
                CourseId: $('#txtmodalCourseId').val(),
                CourseName: $('#txtmodalCourse').val(),
                CourseCategoryId: $('#drpModalUpdate').val()   
            },
            success: function (updateResponse) {
                /* alert('successfully Updated');*/
                $('#drpModalUpdate').hide();
                $('#txtmodalCourse').hide();
                $('.btnSaveCourse').hide();
                $('#lblCourse').hide();
                $('#lblCourseName').hide();


                $('#lblMessage').removeAttr('hidden');
                loadCourses();
            }
        });

});



//-------------------btn Close----------------------------
$(document).on("click", ".btn-close", function () {
   
    $("#courseEditModal").modal('hide');
 
});




