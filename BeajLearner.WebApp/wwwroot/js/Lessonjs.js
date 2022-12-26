//const { forEach } = require("../vendor/fontawesome-free/js/v4-shims");



//-------------------Add Lesson------------------------
var mcqsArray = [];
$(document).ready
    (
       

        function () {
            var globalLessonText = "";
           

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

//function loadCoursesDrp() {
//    var id = $('#CourseCategorydrp').find(":selected").val();
//    var text = $("#CourseCategorydrp option:selected").text();
//    text = $.trim(text);
//    $('#txtCategory').val(text);
//    $.ajax(
//        {
//            type: 'POST',
//            url: globalUrlForAPIs + 'Course/GetAllCourseByCategoryId?categoryId=' + id,
//            contentType: "application/json; charset=utf-8",
//            dataType: "json",
//            success: (res) => {
//                $('#Coursedrp').empty();
//                $('#Coursedrp').append(`<option value="-1">Select Course </option>`);
//                $.each(res, function (i, item) {
//                    $('#Coursedrp').append(`<option value="${item.courseId}">
//                                       ${item.courseName}
//                                  </option>`)
//                });
//            }
//        }
//    )
//}

$('#Coursedrp').change(function () {

    if ($('#Cousedrp').find(":selected").val()!=-1)
    {
        loadVideoLesson();
        loadAudioLesson();
        loadTextLesson();
        loadMcqsLesson();
    }
}
);




function loadVideoLesson()
{
    var categoryId = $('#CourseCategorydrp').find(":selected").val();
    var CourseId = $('#Coursedrp').find(":selected").val();
   
    var jsondata = JSON.stringify({ activity: "video", courseId: CourseId });
    $.ajax(
        {
            type: 'POST',
            url: globalUrlForAPIs + 'GetLessons/getLessonByCourseIdAndActivity',
            dataType: 'JSON',
            contentType: "application/json; charset=utf-8",
            data: jsondata,
            success: (res) =>
            {
                console.log(res);
                $('#videoDataTable').dataTable({
                    data: res,
                    columns: [
                       
                        { "data": "lessonType" },
                        { "data": "weekNumber" },
                        { "data": "dayNumber" },

                        {
                            "data": null, "render": function (data) {

                                

                                var videoArray = "";
                                var length = data.videos.length;
                                for (var i = 0; i < length; i++) {
                                    videoArray = videoArray + '<video width="50" height="50" controls><source src="' + data.videos[i] + '"></video>';
                                    videoArray = videoArray + ' ';
                                }

                                return videoArray;
                                //data.each((item, index) => {
                                //    return '<video width="50" height="50" controls><source src="' + data.videos + '"></video>';
                                //});

                                
                               
                                
                            }
                        },
                     
                    ]

                });



            }
        }
    )
};



function loadAudioLesson() {
    var categoryId = $('#CourseCategorydrp').find(":selected").val();
    var CourseId = $('#Coursedrp').find(":selected").val();

    var jsondata = JSON.stringify({ activity: "audio", courseId: CourseId });
    $.ajax(
        {
            type: 'POST',
            url: globalUrlForAPIs + 'GetLessons/getLessonByCourseIdAndActivity',
            dataType: 'JSON',
            contentType: "application/json; charset=utf-8",
            data: jsondata,
            success: (res) => {
                console.log(res);
                $('#audioDataTable').dataTable({
                    data: res,
                    columns: [

                        { "data": "lessonType" },
                        { "data": "weekNumber" },
                        { "data": "dayNumber" },

                        {
                            "data": null, "render": function (data) {


                                var audioArray = "";
                                var length = data.audios.length;
                                for (var i = 0; i < length; i++) {

                                  

                                   


                                    audioArray = audioArray + `  <audio controls> <source src= ` + data.audios[i] + ` type="audio/ogg"> </audio>`;
                                    audioArray = audioArray + ' ';
                                }

                                return audioArray;

                            }
                        },
                        {
                            "data": null, "render": function(data)
                            {
                                return `<img src="` + data.image + `"  width="50" height="50">`;
                            }
                        },
                       


                    ]

                });



            }
        }
    )
};



function loadTextLesson() {
    var categoryId = $('#CourseCategorydrp').find(":selected").val();
    var CourseId = $('#Coursedrp').find(":selected").val();

    var jsondata = JSON.stringify({ activity: "text", courseId: CourseId });
    $.ajax(
        {
            type: 'POST',
            url: globalUrlForAPIs + 'GetLessons/getLessonByCourseIdAndActivity',
            dataType: 'JSON',
            contentType: "application/json; charset=utf-8",
            data: jsondata,
            success: (res) => {
               
                $('#textDataTable').dataTable({
                    data: res,
                    columns: [

                        { "data": "lessonType" },
                        { "data": "weekNumber" },
                        { "data": "dayNumber" },

                        {
                            "data": null, "render": function (data) {


                                var audioArray = "";
                                var length = data.audios.length;
                                for (var i = 0; i < length; i++) {


                                    audioArray = audioArray + `  <audio controls> <source src= ` + data.audios[i] + ` type="audio/ogg"> </audio>`;
                                    audioArray = audioArray + ' ';
                                }

                                return audioArray;

                            }
                        },
                        {
                            "data": null, "render": function (data) {
                                return `<img src="` + data.image + `"  width="50" height="50">`;
                            }
                        },
                        {
                           
                            "data": null, "render": function (data) {
                               // globalLessonText = data.text;
                                return '<button type="button" lessonId="' + data.lessonId + '" text="' + data.text + '"  class="btn btn-danger btnShowLessonText">Show text</button>'

                            }

                        }

                    ]

                });



            }
        }
    )
};




$(document).on('click', '.btnShowLessonText', function () {

    var text = $(this).attr('text');

    $('.textModal').modal('toggle');
    $('.textModalBody').html(text);
});




//-------------------  load mcqs ----------------------------------

function loadMcqsLesson() {
    var categoryId = $('#CourseCategorydrp').find(":selected").val();
    var CourseId = $('#Coursedrp').find(":selected").val();

    var jsondata = JSON.stringify({ activity: "mcqs", courseId: CourseId });
    $.ajax(
        {
            type: 'POST',
            url: globalUrlForAPIs + 'GetLessons/getLessonByCourseIdAndActivity',
            dataType: 'JSON',
            contentType: "application/json; charset=utf-8",
            data: jsondata,
            success: (res) => {
                console.log(res);
                $('#mcqsDataTable').dataTable({
                    data: res, 
                    columns: [  

                        { "data": "lessonType" },
                        { "data": "weekNumber" },
                        { "data": "dayNumber" },

                        {

                            "data": null, "render": function (data) {
                              
                                var length = data.mcqquestion.length;
                              
                                for (var i = 0; i < length; i++) {
                                   
                                    mcqsArray[i]=data.mcqquestion[i];
                                 
                                }
                                console.log("--------array--------");

                                console.log(mcqsArray);
                                return '<button type="button" id="btnShowMcq" lessonId="' + data.lessonId + '"  class="btn btn-danger ">Show Mcqs</button>'
                              
                            }
                                 

                        }

                    ]

                });



            }
        }
    )
};


$(document).on('click', '#btnShowMcqs', function () {
    console.log("-----btn click-----------");
    console.log(mcqsArray);
    alert('clicked');
    
});




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









