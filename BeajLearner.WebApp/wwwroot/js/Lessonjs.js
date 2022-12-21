


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



$('#formlesson').submit(function (event) {

    // var courseiddrp = $('#Coursedrp').find(":selected").val();
    // var week = $('#week').val();
    //var textval = CKEDITOR.instances['text'].getData();



    
    event.preventDefault();
    var isValid = $(this).validate().form();
    console.log(isValid);
    if (!isValid) return false;
    else {

        var courseiddrp = $('#Coursedrp').find(":selected").val();
        var week = $('#week').val();
        var textval = CKEDITOR.instances['text'].getData();

        var formData = new FormData();
        formData.append("CourseId", $('#Coursedrp').find(":selected").val());
        formData.append("week", $('#week').val());
        formData.append("text", CKEDITOR.instances['text'].getData());


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
                url: globalUrlForAPIs +'Lesson/AddLesson',
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
    }
});



//------------------Select Lesson type------------------------

function lessonType(type)
{
    alert(type);
    if (type == 'Video')
    {
        $('.videoDiv').prop('hidden', false);
        $('.audioDiv').prop('hidden', true);
        $('.mcqDiv').prop('hidden', true);
    }
    if (type == 'Audio')
    {
        $('.audioDiv').prop('hidden', false);
        $('.videoDiv').prop('hidden', true);
        $('.mcqDiv').prop('hidden', true);
    }

    if (type == 'MCQs') {
        $('.mcqDiv').prop('hidden', false);
        $('.videoDiv').prop('hidden', true);
        $('.audioDiv').prop('hidden', true);
    }
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




