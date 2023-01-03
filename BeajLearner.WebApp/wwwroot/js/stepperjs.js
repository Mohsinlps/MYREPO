/*const { event } = require("jquery");*/

//----------------  Global Func n Vars-----------------
function isNullOrUndefined(value) {
    if (value == undefined)
        return false;
    else if (value == null)
        return false;
    else if (value == "")
        return false;
    else
        return true;
}

   //    Global variables  ---------
let lessonOperation = '';
let insertedLessonId = 0;
let updateLessonId = 0;

//--------------  setting Edit operation ---------------------
$(document).ready(function () {
    /*   sessionStorage.setItem('savingOperation', 'insert');*/
    lessonOperation='insert'
});

$(document).on('click', '.btnEdit', function () {
    //alert('in update session');
    /* sessionStorage.setItem('savingOperation', 'update');*/
    lessonOperation = 'update';
});




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


    //-----------------------------category  next  click  ---------------------------

    $(document).on('click', '#btnNextCategory', function () {

        var i = $('#CourseCategorydrp').find(":selected").val();
       
        if (i != "-1") {


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
        else
        {
            alert('select category');
        }
    });


    //-----------------------------course  next  click  ---------------------------

    $(document).on('click', '#btnNextCourse', function () {

        var i = $('#Coursedrp').find(":selected").val();

        if (i != "-1") {


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
        else {
            alert('select category');
        }
    });


    function isNumber(evt) {
        alert('pressed');
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }





    //////////////////////////////////////    save Lesson 1 //////////////////////////////////

    $(".btnNextLesson1").click(function () {
        var type = $('#drpLessonType').find(":selected").val();
        var format = $('#drpFormat').find(":selected").val();
        var day = $('#dayDrp').find(":selected").val();
        var weekNumber = $('#weekNumberDrp').find(":selected").val();
        

        if (type == 'week' && day != '-1' && format!='-1')
        {
            if (weekNumber == '-1')
            {
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

        }
        if (type == 'day' && format!='-1')
        {

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



    //------------------------------   -----SAVE LESSON   2  -----     ---------------
    //********************************************************************************

    $(document).on('click', '.btnNextLesson2', function () {

        //alert(sessionStorage.getItem('savingOperation'));
        //let op = sessionStorage.getItem('savingOperation');
        if (lessonOperation == 'insert') {
            var formData = new FormData();


            var courseiddrp = $('#Coursedrp').find(":selected").val();
            var courseCatdrp = $('#CourseCategorydrp').find(":selected").val();

           
            if (courseiddrp != -1 && courseCatdrp != -1) {


                //-----------------next page--------------------

              /*  loadCategoryDropdown();*/


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
                //---------------------------------------------------------------------------




                var lessonType = $('#drpLessonType').find(":selected").val();
                var day = $('#dayDrp').find(":selected").val();
                var weekNumber = $('#weekNumberDrp').find(":selected").val();
               
                console.log(weekNumber);
                var textval = CKEDITOR.instances['text'].getData();
                var activity = $('#drpFormat').find(":selected").val();
                var activityAlias = $('#drpAlias').find(":selected").val();

                formData.append("courseId", $('#Coursedrp').find(":selected").val());
                formData.append("lessonType", lessonType);
                formData.append("dayNumber", day);
                formData.append("activity", activity);
                formData.append("activityAlias", activityAlias);
                                
                formData.append("weekNumber", weekNumber);
               
                $($("#videoUpload")[0].files).each((i, element) => {

                    formData.append("videos", $("#videoUpload")[0].files[i]);
                });

                $($("#audioUpload")[0].files).each((i, element) => {

                    formData.append("Audios", $("#audioUpload")[0].files[i]);
                });

                $($("#imageUpload")[0].files).each((i, element) => {

                    formData.append("image", $("#imageUpload")[0].files[i]);
                });
                formData.append("text", CKEDITOR.instances['text'].getData());
                




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
                            console.log(res);

                            saveWeekInfo();
                            // alert("saved successfully");
                           
                            insertedLessonId = res.data.lessonId;

                            ////---------------save mcq------------------
                            if (activity == 'mcqs') {
                                var idgenerator = 0;
                                var formdataMcqs = new FormData();
                                var txtMcqQuestion = "";
                                var txtoption1 = 0;
                                var txtoption2 = 0;
                                var txtoption3 = 0;
                                var txtoption4 = 0;
                                var txtCorrect = "";

                                var mcqsArr = [

                                ];
                                var iterate = 0;
                                
                                var questionsArray = [];
                                //------------------------
                                $('.iterateElements').each((i, element) => {
                              //      var questionsObject = new Object;
                                    var questionsObject = {
                                        question: "",
                                        option1: "",
                                        option2: "",
                                        option3: "",
                                        option4: "",
                                        correctAnswer: "",
                                    }
                                    questionsObject.question = $(element).find('.question').val();
                                    
                                    $(element).find('.option').each((j, text) => {
                                        Object.defineProperty(questionsObject, `option${j + 1}`, { value: $(text).val() })
                                    });
                                    questionsObject.correctAnswer = $(element).find('input').last().val();
                                    questionsObject.lessonId = res.data.lessonId;
                                    questionsArray.push(questionsObject)
                                });

                                
                                
                           
                              
                             var   arrdata = JSON.stringify(questionsArray);

                                console.log(arrdata);

                                $.ajax(
                                    {
                                        url: globalUrlForAPIs + 'Lesson/AddMcqs',
                                        data: arrdata,
                                        //  dataType: 'JSON',
                                        contentType: 'application/json',

                                        type: 'POST',
                                        success: (res) => {
                                            console.log(res);
                                            //alert("saved successfully");
                                          

                                        }
                                    });
                            }





                        }
                    });


                /* $('.tbldynamic').html(` `);*/
            }


        }

        //---------------------------         Else ------------------------------------
        else
        {
            var formData = new FormData();
            //alert('inserted lesson id---' + insertedLessonId);

            var courseiddrp = $('#Coursedrp').find(":selected").val();
            var courseCatdrp = $('#CourseCategorydrp').find(":selected").val();


            if (courseiddrp != -1 && courseCatdrp != -1) {


                //-----------------next page--------------------

             /*   loadCategoryDropdown();*/


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
                //---------------------------------------------------------------------------




                var lessonType = $('#drpLessonType').find(":selected").val();
                var day = $('#dayDrp').find(":selected").val();
                var textval = CKEDITOR.instances['text'].getData();
                var activity = $('#drpFormat').find(":selected").val();

                formData.append("LessonId", insertedLessonId);
                formData.append("courseId", $('#Coursedrp').find(":selected").val());
                formData.append("lessonType", lessonType);
                formData.append("dayNumber", day);
                formData.append("activity", activity);

                $($("#videoUpload")[0].files).each((i, element) => {

                    formData.append("videos", $("#videoUpload")[0].files[i]);
                });

                $($("#audioUpload")[0].files).each((i, element) => {

                    formData.append("Audios", $("#audioUpload")[0].files[i]);
                });

                $($("#imageUpload")[0].files).each((i, element) => {

                    formData.append("image", $("#imageUpload")[0].files[i]);
                });
                formData.append("text", CKEDITOR.instances['text'].getData());





                $.ajax(
                    {
                        url: globalUrlForAPIs + 'Lesson/updateLesson',
                        data: formData,
                        dataType: 'JSON',
                        processData: false,
                        contentType: false,
                        enctype: "multipart/form-data",
                        type: 'POST',
                        success: (res) => {

                            console.log('update response');
                            console.log(res);

                            updateLessonId = res.lessonId;
                            //alert(updateLessonId);
                            // alert("Updated successfully");



                            ////---------------update mcq------------------
                            if (activity == 'mcqs') {
                                var idgenerator = 0;
                                var formdataMcqs = new FormData();
                                var txtMcqQuestion = "";
                                var txtoption1 = 0;
                                var txtoption2 = 0;
                                var txtoption3 = 0;
                                var txtoption4 = 0;
                                var txtCorrect = "";

                                var mcqsArr = [

                                ];

                                //var arr = [];
                                //for (var i = 0; i < 32; i++) {
                                //    arr.push({ val: i, text: i });
                                //}
                                $('.txtMcqQuestion').each((i, element) => {
                                    txtMcqQuestion = $('#txtMcqQuestion' + idgenerator + '').val();
                                    txtoption1 = $('#txtMcqOption1' + idgenerator + '').val();
                                    txtoption2 = $('#txtMcqOption2' + idgenerator + '').val();
                                    txtoption3 = $('#txtMcqOption3' + idgenerator + '').val();
                                    txtoption4 = $('#txtMcqOption4' + idgenerator + '').val();
                                    txtCorrect = $('#txtCorrectAnswer' + idgenerator + '').val();
                                    mcqsArr.push({
                                        question: txtMcqQuestion,
                                        option1: txtoption1,
                                        option2: txtoption2,
                                        option3: txtoption3,
                                        option4: txtoption4,
                                        correctAnswer: txtCorrect,
                                        lessonId: updateLessonId,
                                    });

                                    idgenerator++;
                                });

                                console.log(mcqsArr);
                                arrdata = JSON.stringify(mcqsArr);

                                console.log(arrdata);

                                $.ajax(
                                    {
                                        url: globalUrlForAPIs + 'Lesson/updateMcqs',
                                        data: arrdata,
                                        //  dataType: 'JSON',
                                        contentType: 'application/json',

                                        type: 'POST',
                                        success: (res) => {
                                            console.log(res);
                                            //alert("saved successfully");


                                        }
                                    });
                            }
                            else
                            {
                                $.ajax(
                                    {
                                        url: globalUrlForAPIs + 'Lesson/deleteMcqs?id=' + updateLessonId,
                                      /*  data: updateLessonId,*/
                                        //  dataType: 'JSON',
                                        contentType: 'application/json',

                                        type: 'POST',
                                        success: (res) => {
                                            console.log(res);
                                            //alert("deleted successfully");


                                        }
                                    });

                            }





                        }
                    });


                /* $('.tbldynamic').html(` `);*/
            }

        }
       

    });
//------------------------------------------------------------------------


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

                $('.categoryDrp').empty();
                $('.categoryDrp').append(`<option value="-1">Select Course Category</option>`)
                $.each(res, function (i, item) {
                    $('.categoryDrp').append(
                        `<option value="${item.courseCategoryId}">
                                       ${item.courseCategoryName}

                                 </option>`)
                });




            }
        }
    );
}
//-------------Next cat Button Click-------------save course category---------------------
//--------------------btn Add Category Click -------------------

$(document).on('click', '#btnAddCategoryModal', function () {

    $('.CategoryAddModal').modal('toggle');
    $('#txtCourseCategory').prop('hidden',false);
    $('#txtCourseCategory').val('');
    //$('#lblCourseCategoryName').show();
    $('#lblCategoryAddSuccess').attr('hidden', true);
    $('#categoryImg').prop('hidden', false);
    $('#imageUploadCat').val('');
    $('#btnAddCategory').prop('hidden', false);
    //$('#lblCategoryAddSuccess').attr('hidden', true);
    
   
});

$(document).on('click', '#btnAddCourseModal', function () {
   
      $('#lblCourseAddedMessage').prop('hidden', true);
      $('#txtCourse').show();
      $('#btnCourseAddclick').show();
    $('.courseAddModal').modal('toggle');
    $('#coursecategoryid').prop('hidden', false);
    $('#courseStatusDrp').prop('hidden', false);
    $("#courseStatusDrp").val($("#courseStatusDrp option:first").val());
    $('#txtCoursePrice').prop('disabled', true);
    $('#txtCourse').val('');
    $('#txtCoursePrice').val('');
    $('#txtCourseWeeks').val('');

    $('#txtCoursePrice').show();
    $('#txtCourseWeeks').show();
});






//----------------------         Save Category  ---------------------------


$(document).on('click','#btnAddCategory', function () {

    var formdata = new FormData();
    formdata.append('CourseCategoryName', $('#txtCourseCategory').val());
    $($("#imageUploadCat")[0].files).each((i, element) => {

        formdata.append("image", $("#imageUploadCat")[0].files[i]);
    });
    JSON.stringify(formdata);
  
    var check = isNullOrUndefined($('#txtCourseCategory').val());
    if (check != false) {
      
        $.ajax({
            url: globalUrlForAPIs + 'CourseCategory/AddCourseCategory',
            data: formdata,
           
            dataType: 'JSON',
            processData: false,
            contentType: false,
            enctype: "multipart/form-data",
            type: 'POST',

            success: (res) => {

                //$('#txtmodalCourseCategory').hide();
                $('#lblCategoryAddSuccess').prop('hidden',false);
                //$('#lblMessage').removeAttr('hidden');
                $('#txtCourseCategory').prop('hidden', true);
                $('#btnAddCategory').prop('hidden', true);
                $('#categoryImg').prop('hidden', true);
               
                
                loadCategoryDropdown();
            },
            error: () => {
                alert('something went wrong!!!');
            }

        });
       
    }
   
    loadCategoryDropdown();
  
});


//--------------------------save course ---------------------


$(document).on('click', '#btnCourseAddclick', function () {

    var check = isNullOrUndefined($('#txtCourse').val());
   
    if (check != false)
    {
       
                $.ajax(
                    {
                        
                        url: globalUrlForAPIs + 'Course/AddCourse',
                        data: { "CourseCategoryId": $('#CourseCategorydrp').find(":selected").val(), "CourseName": $('#txtCourse').val(), "CoursePrice": $('#txtCoursePrice').val(), "CourseWeeks": $('#txtCourseWeeks').val(), "status": $('#courseStatusDrp').find(':selected').val() },

                        dataType: 'JSON',
                  
                        type: 'POST',
                        success: (res) => {

                            
                            $('#lblCourseAddedMessage').prop('hidden', false);
                            $('#txtCourse').hide();
                            $('#btnCourseAddclick').hide();
                            $('#txtCoursePrice').hide();
                            $('#txtCourseWeeks').hide();
                            $('#coursecategoryid').prop('hidden', true);
                            $('#courseStatusDrp').prop('hidden', true);
                            loadCoursesDrp();
                        },
                        error: (res) => { alert('something went wrong') }
                    });
            
        loadCoursesDrp();

    }
    //redurl = 'StepperTeacher';
    //window.location.href = redurl;
});


$(document).on('change', '#courseStatusDrp', function () {

    var value = $('#courseStatusDrp').find(':selected').val();
    if (value == 'free') {
        $('#txtCoursePrice').val('0');
        $('#txtCoursePrice').attr('disabled', true);
    }
    else
    {
        $('#txtCoursePrice').val('');
        $('#txtCoursePrice').attr('disabled', false);
    }
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


//--------------Get all courses on category change -----------on category change--------------------
$('#CourseCategorydrp').change(function () {
    loadCoursesDrp();
}
);
function loadCoursesDrp () {
    var id = $('#CourseCategorydrp').find(":selected").val();
    var text = $("#CourseCategorydrp option:selected").text();
    text = $.trim(text);
    $('#txtCategory').val(text);
    $.ajax(
        {
            type: 'GET',
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


//  ----------------------        Course Drp change  -------------------------

$('#Coursedrp').change(function () {
    drpLessonType
    $('#drpLessonType').val($('#drpLessonType option:first').val());
    $("#dayDrp").val($("#dayDrp option:first").val());
    $('.dayDiv').prop('hidden', true);
    $("#weekNumberDrp").val($("#weekNumberDrp option:first").val());
    $('.weekNumberDiv').prop('hidden', true);
  
   
});



// ---------------------------   Load weeks of selected course ----------------------
    function loadWeeks() {
        var id = $('#Coursedrp').find(":selected").val();
        var text = $("#CourseCategorydrp option:selected").text();
        text = $.trim(text);
        $('#txtCategory').val(text);
        $.ajax(
            {
                type: 'POST',
                url: globalUrlForAPIs + 'Course/GetCourseById?id='+ id,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: (res) => {
                    console.log(res);
                    $('#weekNumberDrp').empty();
                    $('#weekNumberDrp').append(`<option value="-1">Select Course Week </option>`);
                    for (var i = 1; i <= res.courseWeeks; i++)
                    {

                        $('#weekNumberDrp').append(`<option value="${i}">
                                       ${i}
                                  </option>`)
                    }
                }
            }
        )
    }




//------------------------  Course drp change----------------


$('#Coursedrp').change(function () {

    var text = $("#Coursedrp option:selected").text();
    text = $.trim(text);
    $('#txtCoursetbl').val(text);
  
}
);
//--------------------------   on lesson type -----------------------
$('#drpLessonType').change(function () {
   
    var type = $('#drpLessonType').find(":selected").val();
    var format = $('#drpFormat').find(":selected").val();
    var day = $('#dayDrp').find(":selected").val();
    var weekNumber = $('#weekNumberDrp').find(":selected").val();

    //----------------
    var text = $("#drpLessonType option:selected").text();
    text = $.trim(text);
    $('#txtType').val(text);


    if (type != '-1' && format != '-1' )
    {
        $('#btnNextLesson1').prop('disabled', false);
    }
    if (type == '-1' && format == '-1' ) {
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
        loadWeeks();
        $('.dayDiv').prop('hidden', false);
        $('.weekNumberDiv').prop('hidden', false);
        $("#weekNumberDrp").val($("#weekNumberDrp option:first").val());
    }
     if (type == 'day')
     {
         $('.weekNumberDiv').prop('hidden', true);
         $("#dayDrp").val($("#dayDrp option:first").val());
         $('.dayDiv').prop('hidden', true);
         $('#txtDay').val('-');
    }

  

});


//---------------------------------------   Day drp change---------------------
$('#dayDrp').change(function () {

    var text = $("#dayDrp option:selected").text();
    text = $.trim(text);
    $('#txtDay').val(text);


});


//--------------------  week drp change --------------




$('#weekNumberDrp').change(function () {

    var courseId = $('#Coursedrp option:selected').val();
   
    var weekNum = $("#weekNumberDrp option:selected").text();
    weekNum = $.trim(weekNum);
    //assigning value to table below stepper
    $('#txtWeekNum').val(weekNum);
    $('#weekImgUpload').val('');
    // week image load procedure

    $.ajax({

        type: 'GET',
        url: globalUrlForAPIs + 'Lesson/GetWeekByCourseIdAndWeekNumber?id=' + courseId + '&weekNumber=' + weekNum + '',
        success: (res) =>
        {
            if (res != null) {
                $('#imgDiv').html(`  <img id='imgWeekModal' alt="Image" style=" width:50px ; height:50px" src=' ` + res.image + `' />`);
                $('#weekDescription').val(res.description);
            }
            else
            {
                $('#imgDiv').html(``);
            }
        }
    });


});


// -------------------------------------  Save Week Information ----------------------------

function saveWeekInfo()
{
    var formData = new FormData();
    var courseId = $('#Coursedrp option:selected').val();
    var weekNum = $("#weekNumberDrp option:selected").val();
    var description = $('#weekDescription').val();

    formData.append('weekNumber', weekNum);
    formData.append('courseId', courseId);
    formData.append('description', description);
    formData.append('savingPort', "");
    $($("#weekImgUpload")[0].files).each((i, element) => {

        formData.append("image", $("#weekImgUpload")[0].files[i]);
    });
    
    
   
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: globalUrlForAPIs + 'Lesson/AddCourseWeekInfo',
        data: formData,
        processData: false,
        contentType: false,
        enctype: "multipart/form-data",
        success: (res) =>
        {

        }
    });
}


//---------------------  show week Info  -------------------------



$("#weekImgUpload").on("change", function (e) {
    var fileModalpic = "";
    const [file] = e.target.files;

    $('#imgDiv').html(` <img id='imgWeekModal' alt="Image" style=" width:50px ; height:50px" src='#' />`);

    if (file) {
        imgWeekModal.src = URL.createObjectURL(file);

        fileModalpic = URL.createObjectURL(file);
        console.log(fileModalpic);
    }
});











                 //-----------------  Activity Format change----------------------
$('#drpFormat').change(function () {
    //$("#CourseCategorydrp").val($("#CourseCategorydrp option:first").val());
    //$("#Coursedrp").val($("#CourseCategorydrp option:first").val());

    var category = $('#CourseCategorydrp').find(":selected").text();
    var course = $('#Coursedrp').find(':selected').text();


    var text = $("#drpFormat option:selected").text();
    text = $.trim(text);
    $('#txtActivity').val(text);

 




    var type = $('#drpLessonType').find(":selected").val();
    var format = $('#drpFormat').find(":selected").val();
    var day = $('#dayDrp').find(":selected").val();


    if (type != '-1' && format != '-1' ) {
        $('#btnNextLesson1').prop('disabled', false);
    }
    if (type == '-1' && format == '-1') {
        $('#btnNextLesson1').prop('disabled', true);
    }

    if (type == '-1' || format == '-1') {
        $('#btnNextLesson1').prop('disabled', true);
    }


   
    

    if (format == 'video')
    {
        $('.videoHide').prop('hidden', false);
        $('.audioHide').prop('hidden', true);
        $('.textDiv').prop('hidden', false);
        $('.imageHide').prop('hidden', true);
        $('#btnTextSave').prop('hidden', true);
        $('#divMcsqs').prop('hidden', true);
        

        $('.tbldynamic').html(` <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                <thead>
                    <tr>
                        <th>CourseCategory Name</th>
                        <th>CourseName</th>
                        <th>Lesson Type</th>
                        <th>Day</th>
                        <th>Week Number</th>
                        <th>Activity</th>
                        <th>Video</th>
                      
                       
                    </tr>
                </thead>
                  <tbody >
                          <tr><td id="tdCategory"><input readonly type="text" id="txtCategory" /></td>
                             <td id="tdCourse"><input readonly type="text" id="txtCoursetbl" />
                              <td id="tdType"><input readonly type="text" id="txtType" /></td>
                              <td id="tdDay"><input readonly type="text" id="txtDay" /></td>
                              <td id="tdWeekNum"><input readonly type="text" id="txtWeekNum" /></td>
                              <td id="tdActivity"><input readonly type="text" id="txtActivity" /></td>
                              <td id="tdVideo"></td>
                          
                              

                          </tr>
                      
                  </tbody>
            </table>`);
        //<video id="video" width="100" height="100" controls style="display: none;"></video>
       
    }
     if (format == 'audio') {
         $('.audioHide').prop('hidden', false);
         $('.imageHide').prop('hidden', false);
        $('.videoHide').prop('hidden', true);
         $('.textDiv').prop('hidden', false);
         $('#btnTextSave').prop('hidden', true);
         $('#divMcsqs').prop('hidden', true);


         $('.tbldynamic').html(` <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                <thead>
                    <tr>
                        <th>CourseCategory Name</th>
                        <th>CourseName</th>
                        <th>Lesson Type</th>
                        <th>Day</th>
  <th>Week Number</th>
                        <th>Activity</th>
                        <th>Audio</th>
                        <th>Image</th>
                      
                       
                    </tr>
                </thead>
                  <tbody >
                          <tr><td id="tdCategory"><input readonly type="text" id="txtCategory" /></td>
                             <td id="tdCourse"><input readonly type="text" id="txtCoursetbl" />
                              <td id="tdType"><input readonly type="text" id="txtType" /></td>
                              <td id="tdDay"><input readonly type="text" id="txtDay" /></td>
                              <td id="tdWeekNum"><input readonly type="text" id="txtWeekNum" /></td>
                              <td id="tdActivity"><input readonly type="text" id="txtActivity" /></td>
                               <td id="tdAudio">
                               <td id="tdImage" ></td>
                                 
                                  </td>
                             
                              

                          </tr>
                      
                  </tbody>
            </table> `);
    }
    if (format == 'read') {
        $('#btnTextSave').prop('hidden', false);
        $('.imageHide').prop('hidden', false);
        $('.textDiv').prop('hidden', false);
         $('.audioHide').prop('hidden', false);
        $('.videoHide').prop('hidden', true);
        $('#divMcsqs').prop('hidden', true);


        $('.tbldynamic').html(`  <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                <thead>
                    <tr>
                        <th>CourseCategory Name</th>
                        <th>CourseName</th>
                        <th>Lesson Type</th>
                        <th>Day</th>
  <th>Week Number</th>
                        <th>Activity</th>
                     
                        <th>Audio</th>
                        <th>Text</th>
                        <th>Image</th>
                       
                       
                    </tr>
                </thead>
                  <tbody >
                          <tr><td id="tdCategory"><input readonly type="text" id="txtCategory" /></td>
                             <td id="tdCourse"><input readonly type="text" id="txtCoursetbl" />
                              <td id="tdType"><input readonly type="text" id="txtType" /></td>
                              <td id="tdDay"><input readonly type="text" id="txtDay" /></td>
 <td id="tdWeekNum"><input readonly type="text" id="txtWeekNum" /></td>
                              <td id="tdActivity"><input readonly type="text" id="txtActivity" /></td>
                              <td id="tdAudio">
                                  <audio id="audio" width="100" height="100" controls style="display: none;">
                                      
                                       
                                  </audio>


                              </td>
                              <td id="tdText"><input readonly hidden type="text" id="txtText" /><button class="btn-primary btn-primary" id="btnShowText">Show Text</button></td>
                              <td id="tdImage" ></td>
                             
                              

                          </tr>
                      
                  </tbody>
            </table> `);
    }


    if (format == 'mcqs') {
        $('#divMcsqs').prop('hidden', false);
        $('#btnTextSave').prop('hidden', true);
        $('.imageHide').prop('hidden', true);
        $('.textDiv').prop('hidden', false);
        $('.audioHide').prop('hidden', true);
        $('.videoHide').prop('hidden', true);
       


        $('.tbldynamic').html(`  <table class="table table-bordered" id="dataTable" width="100%" cellspacing="0">
                <thead>
                    <tr>
                        <th>CourseCategory Name</th>
                        <th>CourseName</th>
                        <th>Lesson Type</th>
                        <th>Day</th>
  <th>Week Number</th>
                        <th>Activity</th>
                        <th>mcqs</th>
                        
                       
                       
                    </tr>
                </thead>
                  <tbody >
                          <tr><td id="tdCategory"><input readonly type="text" id="txtCategory" /></td>
                             <td id="tdCourse"><input readonly type="text" id="txtCoursetbl" />
                              <td id="tdType"><input readonly type="text" id="txtType" /></td>
                              <td id="tdDay"><input readonly type="text" id="txtDay" /></td>
 <td id="tdWeekNum"><input readonly type="text" id="txtWeekNum" /></td>
                              <td id="tdActivity"><input readonly type="text" id="txtActivity" /></td>
                            
                              <td id="tdText"><input readonly hidden type="text" id="txtMcqs" /><button class="btn-primary btn-primary" id="btnShowText">Show Text</button></td>
                           
                             
                              

                          </tr>
                      
                  </tbody>
            </table> `);





    }


    //changing bottom table values on dropdown change  -------------- type  +   activity
    $('#txtCategory').val(category.trim());
    $('#txtCoursetbl').val(course.trim());
   
     text = $("#drpFormat option:selected").text();
    text = $.trim(text);
    $('#txtActivity').val(text);

    var typeText = $("#drpLessonType option:selected").text();
    if (typeText == 'Weekly') {
        $('#txtDay').val( $('#dayDrp').find(":selected").val());
    }

    var weekNum = $("#weekNumberDrp option:selected").text();
    weekNum = $.trim(weekNum);

    $('#txtWeekNum').val(weekNum);

    $('#txtType').val(typeText);
    $('#imageUpload').val('');
    $('#videoUpload').val('');
    $('#audioUpload').val('');
    
});





//---------------------------------------  validation --------------------

$(document).ready(function () {
    $('#btnNextLesson1').prop('disabled', true);
    $('.dayDiv').prop('hidden', true);
    $('.weekNumberDiv').prop('hidden', true);
});




//----------------------------  image load in table---------------------


var fileModalpic = "";

$(".js-file-upload").on("change", function (e) {
    const [file] = e.target.files;
   
    $('#tdImage').html('<div class="imageContainer"> <img id="blah" src="#" alt="your image" style="width:30px;height:30px"  /></div>');

    if (file) {
        blah.src = URL.createObjectURL(file);

        fileModalpic = URL.createObjectURL(file);
        console.log(fileModalpic);
    }
});

//----------------------------  video load in table---------------------

document.getElementById("videoUpload").addEventListener("change", function () {
   
    $('#tdVideo').html(``);
    var id = "video";
    for (i = 0; i < this.files.length;i++)
    {
        id=i;
        $('#tdVideo').append(`<video id=`+id+` width="100" height="100" controls style="display: none;"></video>`);
        var media = URL.createObjectURL(this.files[i]);
        var video = document.getElementById(id);
        video.src = media;
        video.style.display = "block";

    }
   
   
   // video.play();
});

//---------------------------  Audio load in table ---------------------

document.getElementById("audioUpload").addEventListener("change", function () {
    $('#tdAudio').html(``);
    var id = "video";
    for (i = 0; i < this.files.length; i++) {
        id = i;
        $('#tdAudio').append(`<audio id=` + id +` width="100" height="100" controls style="display: none;"></audio>`);
        var media = URL.createObjectURL(this.files[i]);
        var video = document.getElementById(id);
        video.src = media;
        video.style.display = "block";

    }
});




//---------------------------- save ck text in td----------------------
$(document).on('click', '#btnTextSave', function () {
    $('#txtText').val(CKEDITOR.instances['text'].getData());
});


//----------------------------- Show Text Modal-----------------------------------

$(document).on('click', '#btnShowText', function () {
    $('.textModal').modal('toggle');
    var text = $('#txtText').val();
   
    $('.modal-body').html(text);



});






//----------------------------- Show Image Modal-----------------------------------




$(document).on('click', '#blah', function () {
    $('.picModal').modal('toggle');
    $('.modal-body').html('<div class="imageModal"></div>');

    $('.modal-body').append('<div class="imageModal"> <img id="modalImg" src="' + fileModalpic +'" alt="your image" style="width:465px;height:auto"  /></div>');

   
      
});


//-----------------------------  btn append mcq question click -------------------------
let mcqQuestionCount = 2;
let idCount = 1;

$(document).on('click', '.btnAddMcq', function () {
   
   
   

                                    $('#questionDiv').append(`<div id="removeQuestion` + idCount +`" class="iterateElements">

                                    <h3 class="regenerateCount"><p>Question No : `+ mcqQuestionCount +`</p></h3>
                                    <button type="button" class="btn btn-danger " id="`+ idCount + `" onclick="removeQuestionFunc(this.id)">Remove</button>
                                     <input type="text" id="txtMcqQuestion`+ idCount +`" class="form-control txtMcqQuestion iterateInputElements question" style="border-radius:5px" placeholder="Write question" />
                                     <input type="text" id="txtMcqOption1`+ idCount +`"  class=" iterateInputElements option"  placeholder="provide option" />
                                     <input type="text" id="txtMcqOption2`+ idCount +`"  class=" iterateInputElements option" placeholder="provide option" />
                                     <input type="text" id="txtMcqOption3`+ idCount +`"  class=" iterateInputElements option" placeholder="provide option" />
                                     <input type="text" id="txtMcqOption4`+ idCount +`"  class=" iterateInputElements option" placeholder="provide option" />
                                    <hr style="color:red" />
                                   <input type="text" id="txtCorrectAnswer`+ idCount +`" class=" iterateInputElements" placeholder="provide Correct answer" />
                                   
                                    </div>
                                     `);

    mcqQuestionCount++;
    idCount++;
    regenerateQuestionAfterAdd();

});

function removeQuestionFunc(value)
{
   
    $('#removeQuestion' + value + '').remove();
    regenerateQuestionAfterAdd();
    //var j = 1;
 
    //$('.regenerateCount').each((i, element) => {
        
    //    $(element).children("p").text('Question No : '+j);
        
    //    j++;
       
    //});
}

function regenerateQuestionAfterAdd()
{
    var j = 1;
    $('.regenerateCount').each((i, element) => {

        $(element).children("p").text('Question No : ' + j);

        j++;
        //$('.regenerateCount').text(j);
        //j++
    });
}

