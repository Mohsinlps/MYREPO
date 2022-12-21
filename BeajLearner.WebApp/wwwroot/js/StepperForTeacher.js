/*const { event } = require("jquery");*/

//----------------  Global Func n Vars-----------------
function isNullOrUndefined(value) {
    if (value == undefined)
        return false;
    else if (value == null)
        return false;
    else if (value == "")
        return false;
}


//---------------------------------stepper js ---------------------

$(document).ready(function () {

    var current_fs, next_fs, previous_fs; //fieldsets
    var opacity;
    var current = 1;
    var steps = $("fieldset").length;

    setProgressBar(current);

    $(".next").click(function () {

        current_fs = $(this).parent();
        next_fs = $(this).parent().next();

        //Add Class Active
        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

        //show the next fieldset
        next_fs.show();
        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                next_fs.css({ 'opacity': opacity });
            },
            duration: 500
        });
        setProgressBar(++current);
    });

    //////////////////////////////////////    save Lesson //////////////////////////////////

    $(".btnNextLesson1").click(function () {
        var type = $('#drpLessonType').find(":selected").val();
        var format = $('#drpFormat').find(":selected").val();
        var day = $('#dayDrp').find(":selected").val();


        if (type == 'week' && day == '-1') {

        }
        else {
            current_fs = $(this).parent();
            next_fs = $(this).parent().next();

            //Add Class Active
            $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

            //show the next fieldset
            next_fs.show();
            //hide the current fieldset with style
            current_fs.animate({ opacity: 0 }, {
                step: function (now) {
                    // for making fielset appear animation
                    opacity = 1 - now;

                    current_fs.css({
                        'display': 'none',
                        'position': 'relative'
                    });
                    next_fs.css({ 'opacity': opacity });
                },
                duration: 500
            });
            setProgressBar(++current);

        }


    });






    //--------------------------------------------------------------------------------------------------------------

    $(".previous").click(function () {

        current_fs = $(this).parent();
        previous_fs = $(this).parent().prev();

        //Remove class active
        $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

        //show the previous fieldset
        previous_fs.show();

        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                previous_fs.css({ 'opacity': opacity });
            },
            duration: 500
        });
        setProgressBar(--current);
    });

    function setProgressBar(curStep) {
        var percent = parseFloat(100 / steps) * curStep;
        percent = percent.toFixed();
        $(".progress-bar")
            .css("width", percent + "%")
    }

    $(".submit").click(function () {
        return false;
    })

});


//--------------------------------Load CourseCategory Dropdown in Course Section-------------------------------------

$(document).ready
    (

        loadCategoryDropdown()


    );
//------------------------------------------Load Category Drowpdown in Course Sections--------------------
function loadCategoryDropdown() {

    $.ajax(
        {
            type: 'GET',
            url: globalUrlForAPIs + 'CourseCategory/GetAllCourseCategories',
            success: (res) => {
                var response = JSON.stringify(res);
                var i;

                $('#drpCourseCategory').empty();
                $('#drpCourseCategory').append(`<option value="-1">Select Course Category</option>`)
                $.each(res, function (i, item) {
                    $('#drpCourseCategory').append(
                        `<option value="${item.courseCategoryId}">
                                       ${item.courseCategoryName}

                                 </option>`)
                });




            }
        }
    );
}
//-------------Next cat Button Click-------------save course category---------------------


$(document).on('click', '#btnNextCategory', function () {


    var check = isNullOrUndefined($('#txtCourseCategory').val());
    if (check != false) {
        $.ajax({
            url: globalUrlForAPIs + 'CourseCategory/AddCourseCategory',

            data: { "CourseCategoryName": $('#txtCourseCategory').val() },
            dataType: 'json',

            type: 'POST',

            success: (res) => {

                alert('saved');
                loadCategoryDropdown();
            },
            error: () => {
                alert('something went wrong!!!');
            }

        });

    }

    loadCategoryDropdown();

});


//-------------Next Course Button Click-------------save course ---------------------


$(document).on('click', '#btnNextCourse', function () {

    var check = isNullOrUndefined($('#txtCourse').val());
    if (check != false) {

        $.ajax(
            {
                url: globalUrlForAPIs + 'Course/AddCourse',
                data: { "CourseCategoryId": $('#drpCourseCategory').find(":selected").val(), "CourseName": $('#txtCourse').val() },

                dataType: 'JSON',

                type: 'POST',
                success: (res) => {

                    alert("saved successfully");
                    redurl = 'StepperTeacher';
                    window.location.href = redurl;

                },
                error: (res) => { alert('something went wrong') }
            });



    }
    redurl = 'StepperTeacher';
    window.location.href = redurl;
});



//------------------   stepper teacher previous btn click-----------------------

$(document).on('click', '#btnPreStepperTeacher', function () {
    redurl = 'Stepper';
    window.location.href = redurl;

});



//----------------------------------- lesson 1 click-------------

$(document).on('click', '#btnNextLesson1', function () {

    var type = $('#drpLessonType').find(":selected").val();
    var format = $('#drpFormat').find(":selected").val();


});



//----------------------------------  Stepper Teacher  JS-------------------------

$(document).ready
    (


        function () {
            $.ajax(
                {
                    type: 'GET',
                    url: globalUrlForAPIs + 'CourseCategory/GetAllCourseCategories',

                    success: (res) => {

                        $('#CourseCategorydrp').empty();
                        $('#CourseCategorydrp').append(`<option value="-1">Select Course Category</option>`);
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


//-------------------------on category change--------------------
$('#CourseCategorydrp').change(function () {
    var id = $('#CourseCategorydrp').find(":selected").val();

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
);


//--------------------------   on lesson type -----------------------
$('#drpLessonType').change(function () {

    var type = $('#drpLessonType').find(":selected").val();
    var format = $('#drpFormat').find(":selected").val();
    var day = $('#dayDrp').find(":selected").val();


    if (type != '-1' && format != '-1') {
        $('#btnNextLesson1').prop('disabled', false);
    }
    if (type == '-1' && format == '-1') {
        $('#btnNextLesson1').prop('disabled', true);
    }

    if (type == '-1' || format == '-1') {
        $('#btnNextLesson1').prop('disabled', true);
    }


    //if (type == '-1') {
    //    $('#btnNextLesson1').prop('disabled', true);
    //}


    if (type == "-1") {

        $("#dayDrp").val($("#dayDrp option:first").val());
        $('.dayDiv').prop('hidden', true);

    }


    if (type == 'week') {
        $('.dayDiv').prop('hidden', false);

    }
    if (type == 'day') {

        $("#dayDrp").val($("#dayDrp option:first").val());
        $('.dayDiv').prop('hidden', true);

    }



});

//-----------------  lesson type change----------------------
$('#drpFormat').change(function () {

    var type = $('#drpLessonType').find(":selected").val();
    var format = $('#drpFormat').find(":selected").val();
    var day = $('#dayDrp').find(":selected").val();


    if (type != '-1' && format != '-1') {
        $('#btnNextLesson1').prop('disabled', false);
    }
    if (type == '-1' && format == '-1') {
        $('#btnNextLesson1').prop('disabled', true);
    }

    if (type == '-1' || format == '-1') {
        $('#btnNextLesson1').prop('disabled', true);
    }


    //if (format == '-1') {
    //    $('#btnNextLesson1').prop('disabled', true);
    //}




    if (format == 'video') {
        $('.videoHide').prop('hidden', false);
        $('.audioHide').prop('hidden', true);
        $('.textDiv').prop('hidden', true);
    }
    if (format == 'audio') {
        $('.audioHide').prop('hidden', false);
        $('.videoHide').prop('hidden', true);
        $('.textDiv').prop('hidden', true);
    }
    if (format == 'text') {
        $('.textDiv').prop('hidden', false);
        $('.audioHide').prop('hidden', true);
        $('.videoHide').prop('hidden', true);
    }



});


//------------------------------   Save Lesson  ---------------

$(document).on('click', '#btnNextLesson2', function () {



    var lessonType = $('#drpLessonType').find(":selected").val();
    var day = $('#dayDrp').find(":selected").val();

    var courseiddrp = $('#Coursedrp').find(":selected").val();

    var textval = CKEDITOR.instances['text'].getData();

    var formData = new FormData();
    formData.append("CourseId", $('#Coursedrp').find(":selected").val());
    formData.append("lessonType", lessonType);
    formData.append("text", CKEDITOR.instances['text'].getData());
    formData.append("dayNumber", day);

    $($("#videoUpload")[0].files).each((i, element) => {

        formData.append("Videos", $("#videoUpload")[0].files[i]);
    })

    $($("#audioUpload")[0].files).each((i, element) => {

        formData.append("Audios", $("#audioUpload")[0].files[i]);
    })

    $($('#videoUpload')[0].files).each((i, element) => {
        console.log(i);
        console.log(element);
    });

    console.log(formData);
    $.ajax(
        {
            url: globalUrlForAPIs + 'Lesson/AddLesson',
            data: formData,
            dataType: 'JSON',
            processData: false,
            contentType: false,
            enctype: "multipart/form-data",
            type: 'POST',
            success: (res) => {
                alert("saved successfully");
            }
        });


});

//---------------------------------------  validation --------------------

$(document).ready(function () {
    $('#btnNextLesson1').prop('disabled', true);
    $('.dayDiv').prop('hidden', true);
});
