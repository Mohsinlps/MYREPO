


//-------------------Add Lesson------------------------
$(document).ready
    (


        function () {
            $.ajax(
                {
                    type: 'GET',
                    url: globalUrlForAPIs+'CourseCategory/GetAllCourseCategories',

                    success: (res) => {


                        $.each(res, function (i, item) {
                            $('#CourseCategorydrp').append(`<option value="${item.courseCategoryId}">
                                       ${item.courseCategoryName}
                                  </option>`)
                        });


                    }
                }
            )
           

        }


    )

$('#CourseCategorydrp').change(function () {
    var id = $('#CourseCategorydrp').find(":selected").val();

    var jsondata = JSON.stringify({ "categoryId": id });


    $.ajax(
        {
            type: 'POST',
            url: globalUrlForAPIs +'Course/GetAllCourseByCategoryId?categoryId=' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (res) => {

                $.each(res, function (i, item) {
                    $('#Coursedrp').append(`<option value="${item.courseId}">
                                       ${item.courseName}
                                  </option>`)
                });
            }
        }
    )
}
);

function loadCoursesDrp() {
    var id = $('#CourseCategorydrp').find(":selected").val();
    var text = $("#CourseCategorydrp option:selected").text();
    text = $.trim(text);
    $('#txtCategory').val(text);
    $.ajax(
        {
            type: 'POST',
            url: globalUrlForAPIs + 'Course/GetAllCourseByCategoryId?categoryId=' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (res) => {
                $('#Coursedrp').empty();
                $('#Coursedrp').append(`<option value="-1">Select Course </option>`);
                $.each(res, function (i, item) {
                    $('#Coursedrp').append(`<option value="${item.courseId}">
                                       ${item.courseName}
                                  </option>`)
                });
            }
        }
    )
}

//$('#formlesson').submit(function (event) {

//    // var courseiddrp = $('#Coursedrp').find(":selected").val();
//    // var week = $('#week').val();
//    //var textval = CKEDITOR.instances['text'].getData();



    
//    event.preventDefault();
//    var isValid = $(this).validate().form();
//    console.log(isValid);
//    if (!isValid) return false;
//    else {

//        var courseiddrp = $('#Coursedrp').find(":selected").val();
//        var week = $('#week').val();
//        var textval = CKEDITOR.instances['text'].getData();

//        var formData = new FormData();
//        formData.append("CourseId", $('#Coursedrp').find(":selected").val());
//        formData.append("week", $('#week').val());
//        formData.append("text", CKEDITOR.instances['text'].getData());


//        $($("#videoUpload")[0].files).each((i, element) => {

//            formData.append("Videos", $("#videoUpload")[0].files[i]);
//        })

//        $($("#audioUpload")[0].files).each((i, element) => {

//            formData.append("Audios", $("#audioUpload")[0].files[i]);
//        })

//        $($('#videoUpload')[0].files).each((i, element) => {
//            console.log(i);
//            console.log(element);
//        });
        
//        console.log(formData);
//        $.ajax(
//            {
//                url: globalUrlForAPIs +'Lesson/AddLesson',
//                data: formData,
//                dataType: 'JSON',
//                processData: false,
//                contentType: false,
//                enctype: "multipart/form-data",
//                type: 'POST',
//                success: (res) => {
//                    alert("saved successfully");
//                }
//            });
//    }
//});



//------------------Select Lesson type------------------------









