//const { each } = require("jquery");
//const { Button } = require("../lib/bootstrap/dist/js/bootstrap.bundle");

$(document).ready(function () {
    
  
}
);

//--------------------------save course cat---------------------


$('#form').submit(function (event) {

    event.preventDefault();
    var isValid = $(this).validate().form();
    console.log(isValid);
    if (!isValid) return false;
    else {
        var formData = new FormData(document.getElementById("form"));
        console.log(formData);

        $.ajax({
            url: globalUrlForAPIs+'CourseCategory/AddCourseCategory',

            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            type: 'POST',
            beforeSend: function () {
                //  $("#btnSave").attr('disabled', true);
                //   $("#btnSave").html('Saving&nbsp <i class="fa fa-refresh fa-spin"></i>');
            },
            success: (res) => {
               
                loadTable();
                $('#txtCourseCatName').val('');
                $('#lblAddSuccess').removeAttr('hidden');
                $('#txtCourseCatName').hide();
                $('#btnclick').hide();
                
                $('#courseCategoryName').prop('hidden', true);
              //  $('.courseCategoryAddModal').modal('toggle');
            },
            error: () =>
            {
                alert('something went wrong!!!');
            }

        });
    }
    return false;
});

$(document).ready()
{
    loadTable();
}

//------------------------load Datatable-----------------------

function loadTable()
{

    $.ajax(
        {
            type: 'GET',
            url: globalUrlForAPIs + 'CourseCategory/GetAllCourseCategories',
            success: function (res) {
                console.log(res);

                var table = $('#dataTable').DataTable();
                table.clear().draw();

                //destroy datatable
                table.destroy();

                $('#dataTable').DataTable(
                    {
                        data: res,
                        columns: [
                            { "data": "courseCategoryId" },
                            { "data": "courseCategoryName" },
                            {
                                "data": null, "render": function (data) {

                                    return '<button type="button" courseCategoryId="' + data.courseCategoryId + '" courseCategoryName="' + data.courseCategoryName + '"   class="btn btn-primary btnEdit">Edit</button>'

                                }
                            },
                            {
                                "data": null, "render": function (data) {

                                    return '<button type="button" courseCategoryId="' + data.courseCategoryId + '" courseCategoryName="' + data.courseCategoryName + '"   class="btn btn-danger btnDelete">Delete</button>'

                                }
                            }
                        ],
                    });
            },
            error: function (data) {
                alert(data.responseText);
            } //End of AJAX error function
        });
}

//--------------------btn Add Click -------------------

$(document).on('click', '#btnAdd', function () {

    $('#lblAddSuccess').prop('hidden',true);
    $('#txtCourseCatName').show();
    $('#btnclick').show();
    $('.courseCategoryAddModal').modal('toggle');
    $('#courseCategoryName').prop('hidden', false);
});







//--------------------Delete---------------------------
$(document).on('click', '.btnDelete', function () {
    var id = $(this).attr("courseCategoryId");
   
    $.ajax(
        {
            type: 'POST',
            url: globalUrlForAPIs + 'CourseCategory/DeleteCourseCategory?id=' + id,
            success: function (res)
            {
                console.log(res);
                loadTable();
            }
        })
});


//-----------------------Edit-------------------------------

$(document).on('click', '.btnEdit', function () {
    $('#txtmodalCourseCategory').show();
    $('#lblCourseCategoryName').show();
    $('#lblMessage').prop('hidden', true);
    $('.btnSaveCourse').show();
    var courseCategoryId = $(this).attr('courseCategoryId');
    var courseCategoryName = $(this).attr('courseCategoryName');
   
    $('.courseCategoryEditModal').modal('toggle');
    $('#txtmodalCourseCategory').val(courseCategoryName);
    $('#txtmodalCourseCategoryId').val(courseCategoryId);
});

//----------------------btnSave----------------------------------

$(document).on('click', '.btnSaveCourse', function () {
    $.ajax(
        {
            type: 'POST',
            url: globalUrlForAPIs + 'CourseCategory/UpdateCourseCategory',
            data: { "courseCategoryId": $('#txtmodalCourseCategoryId').val(), "courseCategoryName": $('#txtmodalCourseCategory').val() },
            success: function (res)
            {
                console.log(res);
                $('#txtmodalCourseCategory').hide();
                $('#lblCourseCategoryName').hide();
                $('#lblMessage').removeAttr('hidden');
                $('.btnSaveCourse').hide();
                loadTable();
            },

        })

});

