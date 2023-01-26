//const { forEach } = require("../vendor/fontawesome-free/js/v4-shims");

/*const { Doc } = require("../ckeditor/samples/toolbarconfigurator/lib/codemirror/codemirror");*/

/*const { Tab } = require("../lib/bootstrap/dist/js/bootstrap.bundle");*/



//-------------------Add Lesson------------------------
var mcqsArray = [];
var speakActivityQuestionsArray = [];
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
                            $('#CourseCategorydrpLesson').append(`<option value="${item.courseCategoryId}">
                                       ${item.courseCategoryName}
                                  </option>`)
                        });


                    }
                }
            )
           

        }


    )

$('#CourseCategorydrpLesson').change(function () {
    var id = $('#CourseCategorydrpLesson').find(":selected").val();

    var jsondata = JSON.stringify({ "categoryId": id });


    $.ajax(
        {
            type: 'GET',
            url: globalUrlForAPIs +'Course/GetAllCourseByCategoryId?categoryId=' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (res) => {

                $('#CoursedrpLesson').empty();
                $('#CoursedrpLesson').append(`<option value="-1">
                                       Select Course
                                  </option>`);

                $.each(res, function (i, item) {
                    $('#CoursedrpLesson').append(`<option value="${item.courseId}">
                                       ${item.courseName}
                                  </option>`);
                });
            }
        }
    )
}
);


$('#CoursedrpLesson').change(function () {

    if ($('#CousedrpLesson').find(":selected").val()!=-1)
    {
        loadVideoLesson();
        loadAudioLesson();
        loadTextLesson();
        loadListenAndSpeak();
        loadWatchAndSpeak();
        loadMcqsLesson();
    }
}
);


function loadVideoLesson()
{
    var categoryId = $('#CourseCategorydrpLesson').find(":selected").val();
    var CourseId = $('#CoursedrpLesson').find(":selected").val();
   
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
                var table = $('#videoDataTable').DataTable();

                //clear datatable
                table.clear().draw();

                //destroy datatable
                table.destroy();

                $('#videoDataTable').dataTable({
                    data: res,
                    columns: [
                       
                        { "data": "lessonType" },
                        { "data": "weekNumber" },
                        { "data": "dayNumber" },

                        {
                            "data": null, "render": function (data) {

                                

                                var videoArray = "";
                                var length = data.documentFiles.length;
                                for (var i = 0; i < length; i++) {
                                    videoArray = videoArray + '<video width="120" height="120" controls><source src="' + data.documentFiles[i].video + '"></video><span> </span>';
                                   /* videoArray = videoArray + ' ';*/
                                }

                                return videoArray;
                             
                                
                               
                                
                            }
                        },
                        {
                            "data": null, "render": function (data)
                            {
                                return `<button class="btn btn-primary btnEditLesson" lessonId=` + data.lessonId + `>Edit</button>`
                            }
                        },
                        {
                            "data": null, "render": function (data)
                            {
                                return `<button class="btn btn-danger btnDeleteLesson" lessonId=` + data.lessonId + `>Delete</button>`
                            }
                        },
                     
                    ]

                });



            }
        }
    )
};

function loadAudioLesson() {
    var categoryId = $('#CourseCategorydrpLesson').find(":selected").val();
    var CourseId = $('#CoursedrpLesson').find(":selected").val();

    var jsondata = JSON.stringify({ activity: "audio", courseId: CourseId });
    $.ajax(
        {
            type: 'POST',
            url: globalUrlForAPIs + 'GetLessons/getLessonByCourseIdAndActivity',
            dataType: 'JSON',
            contentType: "application/json; charset=utf-8",
            data: jsondata,
            success: (res) => {
               

                var table = $('#audioDataTable').DataTable();

                //clear datatable
                table.clear().draw();

                //destroy datatable
                table.destroy();

                $('#audioDataTable').dataTable({
                    data: res,
                    columns: [

                        { "data": "lessonType" },
                        { "data": "weekNumber" },
                        { "data": "dayNumber" },

                        {
                            "data": null, "render": function (data) {


                                var audioArray = "";
                                var length = data.documentFiles.length;
                                for (var i = 0; i < length; i++) {

                                  

                                   
                                    if (data.documentFiles[i].mediaType == 'audio') {

                                        audioArray = audioArray + `  <audio controls> <source src= ` + data.documentFiles[i].audio + ` type="audio/ogg"> </audio>`;
                                        audioArray = audioArray + ' ';
                                    }
                                }

                                return audioArray;

                            }
                        },
                        
                        {
                            "data": null, "render": function(data)
                            {
                                var imgArray = "";
                                var length = data.documentFiles.length;
                                for (var i = 0; i < length; i++) {
                                    if (data.documentFiles[i].mediaType == 'image') {


                                        imgArray = imgArray + `<img id="imgAudio" src="` + data.documentFiles[i].image + `"  width="50" height="50">`;
                                        imgArray = imgArray + ' ';
                                    }
                                }
                                return imgArray;
                            }
                        },
                        {
                            "data": null, "render": function (data) {
                                return `<button class="btn btn-primary btnEditLesson" lessonId=` + data.lessonId + `>Edit</button>`
                            }
                        },
                        {
                            "data": null, "render": function (data) {
                                return `<button class="btn btn-danger btnDeleteLesson" lessonId=` + data.lessonId + `>Delete</button>`
                            }
                        },
                       


                    ]

                });



            }
        }
    )
};


$(document).on('click', '#imgAudio', function () {

    var src = $(this).attr('src');

    $('.audioImgModalBody').html(
        `<img  src="` + src + `"  width="465" height="auto">`
    );

    $('.audioImgModal').modal('toggle');
});


function loadTextLesson() {
    var categoryId = $('#CourseCategorydrpLesson').find(":selected").val();
    var CourseId = $('#CoursedrpLesson').find(":selected").val();

    var jsondata = JSON.stringify({ activity: "read", courseId: CourseId });
    $.ajax(
        {
            type: 'POST',
            url: globalUrlForAPIs + 'GetLessons/getLessonByCourseIdAndActivity',
            dataType: 'JSON',
            contentType: "application/json; charset=utf-8",
            data: jsondata,
            success: (res) => {


                var table = $('#textDataTable').DataTable();

                //clear datatable
                table.clear().draw();

                //destroy datatable
                table.destroy();


                $('#textDataTable').dataTable({
                    data: res,
                    columns: [

                        { "data": "lessonType" },
                        { "data": "weekNumber" },
                        { "data": "dayNumber" },

                        {
                            "data": null, "render": function (data) {


                                var audioArray = "";
                                var length = data.documentFiles.length;
                                for (var i = 0; i < length; i++) {




                                    if (data.documentFiles[i].mediaType == 'audio') {

                                        audioArray = audioArray + `  <audio controls> <source src= ` + data.documentFiles[i].audio + ` type="audio/ogg"> </audio>`;
                                        audioArray = audioArray + ' ';
                                    }
                                }

                                return audioArray;

                            }
                        },
                        {
                            "data": null, "render": function (data) {
                                var imgArray = "";
                                var length = data.documentFiles.length;
                                for (var i = 0; i < length; i++) {
                                    if (data.documentFiles[i].mediaType == 'image') {


                                        imgArray = imgArray + `<img id="imgAudio" src="` + data.documentFiles[i].image + `"  width="50" height="50">`;
                                        imgArray = imgArray + ' ';
                                    }
                                }
                                return imgArray;
                            }
                        },
                        {
                           
                            "data": null, "render": function (data) {
                               // globalLessonText = data.text;
                                return '<button type="button" lessonId="' + data.lessonId + '" text="' + data.text + '"  class="btn btn-success btnShowLessonText">Show text</button>'

                            }

                        },
                        {
                            "data": null, "render": function (data) {
                                return `<button class="btn btn-primary btnEditLesson" lessonId=` + data.lessonId + `>Edit</button>`
                            }
                        },
                        {
                            "data": null, "render": function (data) {
                                return `<button class="btn btn-danger btnDeleteLesson" lessonId=` + data.lessonId + `>Delete</button>`
                            }
                        },

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

$(document).on('click', '#imgText', function () {

    var src = $(this).attr('src');
   
    $('.textImgModalBody').html(
        `<img id="imgText" src="` + src + `"  width="465" height="auto">`
    );
  

    

    $('.textImgModal').modal('toggle');
});



//----------------  show speak  ------------------------------------

$(document).on('click', '.btnShowSpeakQuestions ', function () {
   
    var questionsArray = [];
    var speakModalBody = '';
    var lessonId = $(this).attr('lessonid');
    $.ajax({

        url: globalUrlForAPIs + 'GetLessons/getLessonById?id=' + lessonId,
        type: 'GET',
        dataType: 'JSON',
        success: (response) =>
        {
            questionsArray = response[0].speakactivity;

            if (response.activity == 'listenAndSpeak')
            {
                $('.showSpeakModalBody').html('');
                for (var i = 0; i < questionsArray.length; i++) {
                    speakModalBody = speakModalBody + `  <div style="border: 1px solid black; border-radius:5px" class="">
<br>
                                      <audio class="form-control"  controls><source src="`+ questionsArray[i].mediaFile + `" ></audio>
<br>
<input class="form-control listenAndSpeakAnswer" value="` + questionsArray[i].question + `"  /><br>

`;

                    for (var j = 0; j < questionsArray[i].answer.length; j++) {
                        var array = questionsArray[i].answer[j].split(',');
                        for (var k = 0; k < array.length; k++) {
                            speakModalBody = speakModalBody + `<input class="form-control " value="` + array[k] + `"  /><br>`;
                        }



                    }
                    speakModalBody = speakModalBody + `</div> <br>`;
                }


                $('.showSpeakModalBody').append(speakModalBody);
                $('.speakModal').modal('toggle');

            }

            //-----------------   watch and speak ----------------------------

            else
            {
                $('.showSpeakModalBody').html('');
                for (var i = 0; i < questionsArray.length; i++) {
                    speakModalBody = speakModalBody + `  <div style="border: 1px solid black; border-radius:5px" class="">
<br>
                                      <video style="margin-top:10px;height:250px;width:250px" class="form-control"  controls><source src="`+ questionsArray[i].mediaFile + `" ></video>
<br>
<input class="form-control listenAndSpeakAnswer" value="` + questionsArray[i].question + `"  /><br>

`;

                    for (var j = 0; j < questionsArray[i].answer.length; j++) {
                        var array = questionsArray[i].answer[j].split(',');
                        for (var k = 0; k < array.length; k++) {
                            speakModalBody = speakModalBody + `<input class="form-control " value="` + array[k] + `"  /><br>`;
                        }



                    }
                    speakModalBody = speakModalBody + `</div> <br>`;
                }


                $('.showSpeakModalBody').append(speakModalBody);
                $('.speakModal').modal('toggle');

            }

          
        }

    })

   

   


});

//-------------------  load listen and speak ----------------------------------

function loadListenAndSpeak() {
    var categoryId = $('#CourseCategorydrpLesson').find(":selected").val();
    var CourseId = $('#CoursedrpLesson').find(":selected").val();

    var jsondata = JSON.stringify({ activity: "listenAndSpeak", courseId: CourseId });
    $.ajax(
        {
            type: 'POST',
            url: globalUrlForAPIs + 'GetLessons/getLessonByCourseIdAndActivity',
            dataType: 'JSON',
            contentType: "application/json; charset=utf-8",
            data: jsondata,
            success: (res) => {

                var table = $('#listenAndSpeakDataTable').DataTable();

                //clear datatable
                table.clear().draw();

                //destroy datatable
                table.destroy();


                $('#listenAndSpeakDataTable').dataTable({
                    data: res,
                    columns: [

                        { "data": "lessonType" },
                        { "data": "weekNumber" },
                        { "data": "dayNumber" },

                        {

                            "data": null, "render": function (data) {

                               // var length = data.mcqquestion.length;

                                //for (var i = 0; i < length; i++) {

                                //    mcqsArray[i] = data.mcqquestion[i];

                                //}

                                return '<button type="button"  lessonId="' + data.lessonId + '"  class="btn btn-success btnShowSpeakQuestions ">Show Speak Lesson</button>'

                            }


                        },
                        {
                            "data": null, "render": function (data) {
                                return `<button class="btn btn-primary btnEditLesson" lessonId=` + data.lessonId + `>Edit</button>`
                            }
                        },
                        {
                            "data": null, "render": function (data) {
                                return `<button class="btn btn-danger btnDeleteLesson" lessonId=` + data.lessonId + `>Delete</button>`
                            }
                        },

                    ]

                });



            }
        }
    )
};

//-------------------  load watch and speak ------------------------------------
function loadWatchAndSpeak() {
    var categoryId = $('#CourseCategorydrpLesson').find(":selected").val();
    var CourseId = $('#CoursedrpLesson').find(":selected").val();

    var jsondata = JSON.stringify({ activity: "watchAndSpeak", courseId: CourseId });
    $.ajax(
        {
            type: 'POST',
            url: globalUrlForAPIs + 'GetLessons/getLessonByCourseIdAndActivity',
            dataType: 'JSON',
            contentType: "application/json; charset=utf-8",
            data: jsondata,
            success: (res) => {

                var table = $('#watchAndSpeakDataTable').DataTable();

                //clear datatable
                table.clear().draw();

                //destroy datatable
                table.destroy();


                $('#watchAndSpeakDataTable').dataTable({
                    data: res,
                    columns: [

                        { "data": "lessonType" },
                        { "data": "weekNumber" },
                        { "data": "dayNumber" },

                        {

                            "data": null, "render": function (data) {

                                //var length = data.mcqquestion.length;

                                //for (var i = 0; i < length; i++) {

                                //    mcqsArray[i] = data.mcqquestion[i];

                                //}

                                return '<button type="button"  lessonId="' + data.lessonId + '"  class="btn btn-success btnShowSpeakQuestions ">Show Speak Lesson</button>'

                            }


                        },
                        {
                            "data": null, "render": function (data) {
                                return `<button class="btn btn-primary btnEditLesson" lessonId=` + data.lessonId + `>Edit</button>`
                            }
                        },
                        {
                            "data": null, "render": function (data) {
                                return `<button class="btn btn-danger btnDeleteLesson" lessonId=` + data.lessonId + `>Delete</button>`
                            }
                        },

                    ]

                });



            }
        }
    )
};


//------------------   Load mcqs  ---------------------------------------------
function loadMcqsLesson() {
    var categoryId = $('#CourseCategorydrpLesson').find(":selected").val();
    var CourseId = $('#CoursedrpLesson').find(":selected").val();

    var jsondata = JSON.stringify({ activity: "mcqs", courseId: CourseId });
    $.ajax(
        {
            type: 'POST',
            url: globalUrlForAPIs + 'GetLessons/getLessonByCourseIdAndActivity',
            dataType: 'JSON',
            contentType: "application/json; charset=utf-8",
            data: jsondata,
            success: (res) => {

                var table = $('#mcqsDataTable').DataTable();

                //clear datatable
                table.clear().draw();

                //destroy datatable
                table.destroy();


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
                              
                                return '<button type="button"  lessonId="' + data.lessonId + '"  class="btn btn-success btnShowMcq ">Show Mcqs</button>'
                              
                            }
                                 

                        },
                        {
                            "data": null, "render": function (data) {
                                return `<button class="btn btn-primary btnEditLesson" lessonId=` + data.lessonId + `>Edit</button>`
                            }
                        },
                        {
                            "data": null, "render": function (data) {
                                return `<button class="btn btn-danger btnDeleteLesson" lessonId=` + data.lessonId + `>Delete</button>`
                            }
                        },

                    ]

                });



            }
        }
    )
};
$(document).on('click', '.btnShowMcq', function () {
    console.log("-----btn click-----------");
    console.log(mcqsArray);

    console.log('-----question  =' + mcqsArray[0].question);
    $('.mcqsModalBody').html(``);
    for (var i = 0; i < mcqsArray.length; i++)
    {

       

      
        $('.mcqsModalBody').append(`
       <div class="question">
              <h2>Question : `+ mcqsArray[i].question + `</h2>
              <p><b>Option  :&nbsp;&nbsp;</b>`+ mcqsArray[i].option1 + `</p>
              <p><b>Option  :&nbsp;&nbsp;</b>`+ mcqsArray[i].option2 +`</p>
              <p><b>Option  :&nbsp;&nbsp;</b>`+ mcqsArray[i].option3+`</p>
              <p><b>Option  :&nbsp;&nbsp;</b>`+ mcqsArray[i].option4+`</p>
              <h5>Correct Answer :`+ mcqsArray[i].correctAnswer+`</h3>
             
          </div>
<br>

`);
      


    }

    $('.mcqsModal').modal('toggle');

   
    
});
$(document).on('click', '.btnDeleteLesson', function () {

    var lessonId = $(this).attr('lessonId');
   
    $.ajax({

        type: 'GET',
        url: globalUrlForAPIs + 'Lesson/DeleteLesson?id=' + lessonId,
        success: (res) =>
        {
            loadVideoLesson();
            loadAudioLesson();
            loadTextLesson();
            loadMcqsLesson();
            loadListenAndSpeak();
        }
    });
});


var toBeRemovedQuestions = [];
var globalLessonIdForEdit = 0;
var globalMcqLength = 0;

//        ---------------          edit -----------------------------

$(document).on('click', '.btnEditLesson', function () {

    var lessonId = $(this).attr('lessonId');
  
    globalLessonIdForEdit = lessonId;
    

    $.ajax({
        type: 'GET',
        url: globalUrlForAPIs + 'GetLessons/GetLessonWithCourseAndCategoryById?id=' + lessonId,
      
        success: (resLesson) =>
        {
            console.log(resLesson);
            globalMcqLength = resLesson[0].mcqquestion.length;

            //load category
            $.ajax(
                {
                    type: 'GET',
                    url: globalUrlForAPIs + 'CourseCategory/GetAllCourseCategories',

                    success: (res) => {
                        $('#CourseCategorydrpEdit').empty();
                        $.each(res, function (i, item) {
                            $('#CourseCategorydrpEdit').append(`<option ${item.courseCategoryId === resLesson[0].courseCategoryId ? 'selected' : ''} value="${item.courseCategoryId}">
                                       ${item.courseCategoryName}
                                  </option>`)
                        });
                     
                      
                          }
                }
            )
                                     //   load course drp

            $.ajax({
                type: 'GET',
                url: globalUrlForAPIs + 'Course/GetAllCourseByCategoryId?categoryId=' + resLesson[0].courseCategoryId,
                success: (resCourse) =>
                {

                    $('#CoursedrpEdit').empty();
                    $.each(resCourse, function (i, item) {
                        $('#CoursedrpEdit').append(`<option ${item.courseId === resLesson[0].courseId ? 'selected' : ''} value="${item.courseId}">
                                       ${item.courseName}
                                  </option>`);
                    });
   }
            });




            //                                Make dropDowns as selected


      
            $("#drpAlias option[value='" + resLesson[0].activityAlias + "']").attr("selected", "selected");
           // $("#dayDrp option[value='" + resLesson[0].dayNumber + "']").attr("selected", "selected");

            $("#dayDrp").val(resLesson[0].dayNumber );

            //                                load week Dropdown
          
            if (resLesson[0].lessonType == 'week') {
                $.ajax({
                    type: 'POST',
                    url: globalUrlForAPIs + 'Course/GetCourseById?id=' + $('#CoursedrpEdit').find(':selected').val(),
                    success: (courseGetForWeek) => {
                        if (courseGetForWeek != undefined)
                        {
                            $('#weekNumberDrp').empty();
                            $('#weekNumberDrp').append(`<option  value="${-1}">
                                      Select week Number
                                  </option>`)
                            for (var i = 1; i <= courseGetForWeek.courseWeeks; i++) {

                                $('#weekNumberDrp').append(`<option ${i == resLesson[0].weekNumber ? 'selected' : ''}  value="${i}">
                                       ${i}
                                  </option>`)
                            }
                        }
                       
                    }
                });
            }

            // ------------  Lesson type ------------------changed------

            $(document).on('change', '#drpLessonType', function ()
            {

                var lessonType = $('#drpLessonType').find(':selected').val();
                if (lessonType == 'day') {
                    $('.weekHide').prop('hidden', true);
                }
                else
                {
                    $('.weekHide').prop('hidden', false);
             var courseId=       $('#CoursedrpEdit').find(':selected').val();
                    $.ajax({
                        type: 'POST',
                        url: globalUrlForAPIs + 'Course/GetCourseById?id=' + courseId,
                        success: (courseGetForWeek) => {
                            $('#weekNumberDrp').empty();
                            $('#weekNumberDrp').append(`<option  value="${-1}">
                                      Select week Number
                                  </option>`)
                            for (var i = 1; i <= courseGetForWeek.courseWeeks; i++) {

                                $('#weekNumberDrp').append(`<option ${i == resLesson[0].weekNumber ? 'selected' : ''}  value="${i}">
                                       ${i}
                                  </option>`)
                            }

                            if (resLesson[0].lessonType == 'day') {
                                 $("#dayDrp option[value='" + -1 + "']").attr("selected", "selected");
                            }
                        }
                    });


                  
                }
            });




           
            if (resLesson[0].lessonType != 'week') {
                
            
               // $("#drpLessonType").val(week);
              $('.weekNumberDiv').prop('hidden', true);
                $('.dayDiv').prop('hidden', true);

            

                $('#weekNumberDrp').empty();
                $('#drpLessonType').html(` 
                                          
                   <option value="day">Topic Based</option>
                  <option value="week">Weekly</option>
                                          `);
            }
            else
            {
                $('.weekNumberDiv').prop('hidden', false);
                $('.dayDiv').prop('hidden', false);


                $("#drpLessonType").empty();

                $('#drpLessonType').html(` <option value="week">Weekly</option>
                             <option value="day">Topic Based</option>
                                          `);



               
                $.ajax({
                    type: 'POST',
                    url: globalUrlForAPIs + 'Course/GetCourseById?id=' + resLesson[0].courseId,
                    success: (courseGetForWeek) =>
                    {
                        $('#weekNumberDrp').empty();

                        for (var i = 1; i <= courseGetForWeek.courseWeeks; i++) {

                            $('#weekNumberDrp').append(`<option ${i === resLesson[0].weekNumber ? 'selected' : ''} value="${i}">
                                       ${i}
                                  </option>`)
                        }
                    }
                });

                // ---------- load week------------------------------
                if (resLesson[0].weekNumber != -1) {
                    $.ajax({
                        type: 'GET',
                        url: globalUrlForAPIs + 'Lesson/GetWeekByCourseIdAndWeekNumber?id=' + resLesson[0].courseId + '&weekNumber=' + resLesson[0].weekNumber + '',


                        success: (resWeek) => {


                            $('#imgDiv').html(`  <img id='imgWeekModal'  alt="Image" style=" width:50px ; height:50px" src=' ` + resWeek.image + `' />`);

                            $('#weekDescription').val(resWeek.description);

                        }
                    });
                }


                //load Day

            
            }

          
            //---------------------------    Load Alias  -----------------------
            $(document).ready(function () {
                $.ajax(
                    {
                        type: 'GET',
                        url: globalUrlForAPIs + 'Lesson/getActivityAlias',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: (res) => {
                            console.log(res);
                            $('#drpAlias').empty();
                            $('#drpAlias').append(`<option value="-1">Select Alias </option>`);
                            for (var i = 0; i < res.length; i++) {

                                $('#drpAlias').append(`<option ${res[i].alias == resLesson[0].activityAlias?'selected':''} value="${res[i].alias}">
                                       ${res[i].alias}
                                  </option>`)
                            }
                        }
                    }
                )

            });

            $("#drpFormat option[value='" + resLesson[0].activity + "']").attr("selected", "selected");
            //$('#txtActivity').val(resLesson[0].activity);
            //---------------------------  activity Changed --------------------------

            //$(document).on('change', '#drpFormat', function () {

            //    alert('activity changed');
            //    var selectedActivity = $('#drpFormat').find(':selected').val();
            //    if (selectedActivity == 'audio')
            //    {
            //        $('#imgHideDiv').prop('hidden', false);
            //        $('#audioHideDiv').prop('hidden', false);
            //        $('#divMcsqs').prop('hidden', true);
            //        $('#videoHideDiv').prop('hidden', true);
            //        $('.listenAndSpeak').prop('hidden', true);
            //        $('#audioHideDiv2').prop('hidden', true);
            //    }
            //    if (selectedActivity == 'video')
            //    {
            //        $('#videoHideDiv').prop('hidden', false);
            //        $('#imgHideDiv').prop('hidden', true);
            //        $('#audioHideDiv').prop('hidden', true);
            //        $('#divMcsqs').prop('hidden', true);
            //        $('.listenAndSpeak').prop('hidden', true);
            //        $('#audioHideDiv2').prop('hidden', true);
            //    }
            //    if (selectedActivity == 'read')
            //    {
            //        $('#imgHideDiv').prop('hidden', false);
            //        $('#audioHideDiv').prop('hidden', false);
            //        $('#divMcsqs').prop('hidden', true);
            //        $('#videoHideDiv').prop('hidden', true);
            //        $('.listenAndSpeak').prop('hidden', true);
            //        $('#audioHideDiv2').prop('hidden', false);
            //    }
            //    if (selectedActivity == 'listenAndSpeak')
            //    {
            //        $('#imgHideDiv').prop('hidden', true);
            //        $('#audioHideDiv').prop('hidden', false);
            //        $('#divMcsqs').prop('hidden', true);
            //        $('#videoHideDiv').prop('hidden', true);
            //        $('.listenAndSpeak').prop('hidden', false);
            //        $('#audioHideDiv2').prop('hidden', true);

                      
            //    }




            //    if (selectedActivity == 'mcqs')
            //    {
            //        $('#imgHideDiv').prop('hidden', true);
            //        $('#audioHideDiv').prop('hidden', true);
            //        $('#divMcsqs').prop('hidden', false);
            //        $('#videoHideDiv').prop('hidden', true);
            //        $('.listenAndSpeak').prop('hidden', true);

                    

            //    }
            //    if (selectedActivity == 'watchAndSpeak') { }
                
                
            //});


            //------------------------   Format change ended -----------------------------


            var imgArray = "";
            var length = resLesson[0].documentFiles.length;
            for (var i = 0; i < length; i++) {
                if (resLesson[0].documentFiles[i].mediaType == 'image') {


                    imgArray = imgArray + `<img id="imgAudio" src="` + resLesson[0].documentFiles[i].image + `"  width="50" height="50">`;
                    imgArray = imgArray + ' ';
                }
            }

                $('#imgLessonEditDiv').html(
                    imgArray
                );

            var videoArray = "";
            $('#videoLessonEditDiv').html(``);
            for (var i = 0;i< length; i++) {
                if (resLesson[0].documentFiles[i].mediaType == 'video')
                {

                    videoArray = videoArray + `<video width="150" height="130" controls><source src="` + resLesson[0].documentFiles[i].video + `"></video> <span> </span>`;
                }
                   
            }
            $('#videoLessonEditDiv').append(

                videoArray
            );


            //----------------- Audios Edit -----------------
         

            var audioArray = "";
            $('#audioLessonEditDiv').html('' );
            for (var i = 0; i < length; i++) {
                if (resLesson[0].documentFiles[i].mediaType == 'audio' && resLesson[0].documentFiles[i].language == 'English') {

                    audioArray = audioArray + `<audio width="150" height="130" controls><source src="` + resLesson[0].documentFiles[i].audio + `"></video> <span> </span>`;
                }

            }
            $('#audioLessonEditDiv').append(
                audioArray

             );



            var audioArray = "";
            $('#audioLessonEditDiv2').html('');
            for (var i = 0; i < length; i++) {
                if (resLesson[0].documentFiles[i].mediaType == 'audio' && resLesson[0].documentFiles[i].language == 'Urdu') {

                    audioArray = audioArray + `<audio width="150" height="130" controls><source src="` + resLesson[0].documentFiles[i].audio + `"></audio> <span> </span>`;
                }

            }
            $('#audioLessonEditDiv2').append(
                audioArray

            );
            

            if (resLesson[0].activity == 'video')
            {
                $('#videoHideDiv').prop('hidden', false);
                $('#imgHideDiv').prop('hidden', true);
                $('#audioHideDiv').prop('hidden', true);
                $('#divMcsqs').prop('hidden', true);
                $('.listenAndSpeak').prop('hidden', true);
                $('#audioHideDiv2').prop('hidden', true);
                
                
                
            }

            if (resLesson[0].activity == 'audio') {
                $('#imgHideDiv').prop('hidden', false);
                $('#audioHideDiv').prop('hidden', false);
                $('#divMcsqs').prop('hidden', true);
                $('#videoHideDiv').prop('hidden', true);
                $('.listenAndSpeak').prop('hidden', true);
                $('#audioHideDiv2').prop('hidden', true);
            }

            if (resLesson[0].activity == 'read') {
                $('#imgHideDiv').prop('hidden', false);
                $('#audioHideDiv').prop('hidden', false);
                $('#divMcsqs').prop('hidden', true);
                $('#videoHideDiv').prop('hidden', true);
                $('.listenAndSpeak').prop('hidden', true);
                $('#audioHideDiv2').prop('hidden', false);
            }

            if (resLesson[0].activity == 'listenAndSpeak') {
                $('#imgHideDiv').prop('hidden', true);
                $('#audioHideDiv').prop('hidden', false);
                $('#divMcsqs').prop('hidden', true);
                $('#videoHideDiv').prop('hidden', true);
                $('.listenAndSpeak').prop('hidden', false);
                $('#audioHideDiv2').prop('hidden', true);
                $('#audioHideDiv').prop('hidden', true);

                var speakLength = resLesson[0].speakactivity.length;
                var speakModalBody = "";
                var questionsArray = resLesson[0].speakactivity;
                
              

                for (var i = 0; i < speakLength; i++)
                {
                  
                  
                    speakModalBody = speakModalBody + ` <br/> <div style="border: 1px solid black; border-radius:5px" class="listenAndSpeakQuestionDiv">
 <button id="btnAddAnswerSpeak" type="button" style="width:50%;background-color:#0080ff;color:white" class="form-control btn btn ">Add Answer</button>
<button id="btnRemoveQuestion" speakId=`+ resLesson[0].speakactivity[i].id +`  type="button" style="width:50%;background-color: 	#d92626;color:white; float:right" class="form-control btn ">Remove Question</button>
 <br>
<span class="oldCheck" old="1" speakId=`+ resLesson[0].speakactivity[i].id +`></span>
                        <label class="control-label  "> Audio Question</label>
<input type="file" class="form-control speakAudio" style="width:50%"  />
                                      <audio class="form-control " controls><source class="savedAudio" src="`+ questionsArray[i].mediaFile + `" ></audio>
                <input class="form-control listenAndSpeakQuestion" value="` + questionsArray[i].question + `"  /><br>

`;

                    for (var j = 0; j < questionsArray[i].answer.length; j++) {
                        var array = questionsArray[i].answer[0].split(',');

                        speakModalBody = speakModalBody + `<input class="form-control listenAndSpeakAnswer" value="` + array[0] + `"  /><br>`;

                        for (var k = 1; k < array.length; k++) {
                            speakModalBody = speakModalBody + `<input class="form-control listenAndSpeakAnswer" value="` + array[k] + `"  /><br>
<button id="btnRemoveAnswer" type="button" style="background-color:#d92626;color:white; float:right;margin-top: -57px;" class=" btn ">X</button>`;
                            }

                        }
                    speakModalBody = speakModalBody + ` 

</div> <br>`;
             
                }

                $('#listenSpeakAppendQuestions').html(speakModalBody);
             
            }

            //----------------  watch and speak edit  --------------------------
            if (resLesson[0].activity == 'watchAndSpeak') {
                $('#imgHideDiv').prop('hidden', true);
                $('#audioHideDiv').prop('hidden', false);
                $('#divMcsqs').prop('hidden', true);
                $('#videoHideDiv').prop('hidden', true);
                $('.listenAndSpeak').prop('hidden', false);
                $('#audioHideDiv2').prop('hidden', true);
                $('#audioHideDiv').prop('hidden', true);

                var speakLength = resLesson[0].speakactivity.length;
                var speakModalBody = "";
                var questionsArray = resLesson[0].speakactivity;



                for (var i = 0; i < speakLength; i++) {


                    speakModalBody = speakModalBody + ` <br/> <div style="border: 1px solid black; border-radius:5px" class="listenAndSpeakQuestionDiv">
 <button id="btnAddAnswerSpeak" type="button" style="width:50%;background-color:#0080ff;color:white" class="form-control btn btn ">Add Answer</button>
<button id="btnRemoveQuestion" speakId=`+ resLesson[0].speakactivity[i].id + `  type="button" style="width:50%;background-color: #d92626;color:white; float:right" class="form-control btn ">Remove Question</button>
 <br>
<span class="oldCheck" old="1" speakId=`+ resLesson[0].speakactivity[i].id + `></span>
                        <label class="control-label  "> Audio Question</label>
<input type="file" class="form-control speakAudio" style="width:50%"  />
                                      <video style="width:80px;height:80px"  controls><source class="savedAudio" src="`+ questionsArray[i].mediaFile + `" ></video>
                <input class="form-control listenAndSpeakQuestion" value="` + questionsArray[i].question + `"  /><br>

`;

                    for (var j = 0; j < questionsArray[i].answer.length; j++) {
                        var array = questionsArray[i].answer[0].split(',');

                        speakModalBody = speakModalBody + `<input class="form-control listenAndSpeakAnswer" value="` + array[0] + `"  /><br>`;

                        for (var k = 1; k < array.length; k++) {
                            speakModalBody = speakModalBody + `<input class="form-control listenAndSpeakAnswer" value="` + array[k] + `"  /><br>
<button id="btnRemoveAnswer" type="button" style="background-color:#d92626;color:white; float:right;margin-top: -57px;" class=" btn ">X</button>`;
                        }

                    }
                    speakModalBody = speakModalBody + ` 

</div> <br>`;

                }

                $('#listenSpeakAppendQuestions').html(speakModalBody);

            }




            if (resLesson[0].activity == 'mcqs') {
                $('#imgHideDiv').prop('hidden', true);
                $('#audioHideDiv').prop('hidden', true);
                $('#divMcsqs').prop('hidden', false);
                $('#videoHideDiv').prop('hidden', true);
                $('.listenAndSpeak').prop('hidden', true);

                let mcqQuestionCount = 1;
                let idCount = 1;
                alert(resLesson[0].mcqquestion.length);
                $('#questionDiv').html(``);
                for (var i = 0; i < resLesson[0].mcqquestion.length; i++)
                {
                    console.log(resLesson[0].mcqquestion[i].question);
                   
                    $('#questionDiv').append(`<div id="removeQuestion` + idCount + `" class="iterateElements">

                                    <h3 class="regenerateCount"><p>Question No : `+ mcqQuestionCount + `</p></h3>
                                    <button type="button" class="btn btn-danger " id="`+ idCount + `" onclick="removeQuestionFunc(this.id)">Remove</button>
<br>
<label>Question</label>
<input type="text" value="`+ resLesson[0].mcqquestion[i].question + `"    id="txtMcqQuestion` + idCount + `" class="form-control txtMcqQuestion iterateInputElements question" style="border-radius:5px" placeholder="Write question" />
<br>
<label>Option 1</label>
                                     <input type="text" value="`+ resLesson[0].mcqquestion[i].option1 + `" id="txtMcqOption1` + idCount + `"  class="form-control iterateInputElements option"  placeholder="provide option" />
<br>
<label>Option 2</label>
<input type="text" value="`+ resLesson[0].mcqquestion[i].option2 + `" id="txtMcqOption2` + idCount + `"  class="form-control iterateInputElements option" placeholder="provide option" />
<br>
<label>Option 3</label>
<input type="text" value="`+ resLesson[0].mcqquestion[i].option3 + `" id="txtMcqOption3` + idCount + `"  class="form-control iterateInputElements option" placeholder="provide option" />
<br>
<label>Option 4</label>
<input type="text" value="`+ resLesson[0].mcqquestion[i].option4 + `" id="txtMcqOption4` + idCount + `"  class="form-control iterateInputElements option" placeholder="provide option" />
                                    <br>
<label>Correct Answer</label>
                                   <input type="text"  value="`+ resLesson[0].mcqquestion[i].correctAnswer + `"   id="txtCorrectAnswer` + idCount + `" class="form-control iterateInputElements" placeholder="provide Correct answer" />
                                   
                                    </div>
                                     `);


                    mcqQuestionCount++;
                    idCount++;
                    regenerateQuestionAfterAdd();
                }
               



            }


            CKEDITOR.instances['ckContent'].setData(resLesson[0].text);

            $('.lessonEditModal').modal('toggle');
            
            //$(".lessonEditModal").on("hidden.bs.modal", function () {
            //    $(".EditModalBody").html("");
            //});




        }
        
    });

});


//                      add listen and speak question
$(document).on('click', '#btnAddQuestion', function () {

    $('#listenSpeakAppendQuestions').append(` <br/> <div style="border: 1px solid black; border-radius:5px" class="listenAndSpeakQuestionDiv">
 <button id="btnAddAnswerSpeak" type="button" style="width:50%;background-color:#0080ff;color:white" class="form-control btn btn ">Add Answer</button>
<button id="btnRemoveQuestion" type="button" style="width:50%;background-color: 	#d92626;color:white; float:right" class="form-control btn ">Remove Question</button>

<br/>
        <label class="control-label  ">Upload Question</label>
        <input type="file"  id="questionAudio"  name="files" class="form-control fileUpload speakAudio "  multiple />
<br>
                                          <input class="form-control listenAndSpeakQuestion" placeholder="Write Question" />
<br>
                                         <input class="form-control listenAndSpeakAnswer" placeholder="Provide answer" />
                                      
                  </div>
                                     `);

});
$(document).on('click', '#btnAddAnswerSpeak', function () {

    /* $('.listenAndSpeakQuestionDiv').append(`<input class="form-control listenAndSpeakAnswer" placeholder="Provide answer" />`);*/
    $(this).parent().append(`<input class="form-control listenAndSpeakAnswer" style="width:100% ;border-radius:5px" placeholder="Provide answer" />
<br>
<button id="btnRemoveAnswer" type="button" style="background-color:#d92626;color:white; float:right;margin-top: -57px;" class=" btn ">X</button>


`);

});


$(document).on('click', '#btnRemoveQuestion', function () {

    var questionId = parseInt($(this).attr('speakid'));
    if (isNaN(questionId))
    {
     
    }
    else
    {
        toBeRemovedQuestions.push(questionId);
    }
    
    $(this).parent().remove();

});

$(document).on('click', '#btnRemoveAnswer', function () {

    /* $('.listenAndSpeakQuestionDiv').append(`<input class="form-control listenAndSpeakAnswer" placeholder="Provide answer" />`);*/
    $(this).prev().remove();
    $(this).prev().remove();
    $(this).remove();

});



//-------------  End Edit Modal event--------------------------------------------------

// load week image and description on course change in edit
$(document).on('change', '#CoursedrpEdit', function () {
    $.ajax({
        type: 'GET',
        url: globalUrlForAPIs + 'Lesson/GetWeekByCourseIdAndWeekNumber?id=' + $('#CoursedrpEdit').find(':selected').val() + '&weekNumber=' + $('#weekNumberDrp').find(':selected').val() + '',


        success: (resWeek) => {


            $('#imgDiv').html(`  <img id='imgWeekModal'  alt="Image" style=" width:50px ; height:50px" src=' ` + resWeek.image + `' />`);

            $('#weekDescription').val(resWeek.description);

        }
    });
});

$(document).on('change', '#weekNumberDrp', function () {
    $.ajax({
        type: 'GET',
        url: globalUrlForAPIs + 'Lesson/GetWeekByCourseIdAndWeekNumber?id=' + $('#CoursedrpEdit').find(':selected').val() + '&weekNumber=' + $('#weekNumberDrp').find(':selected').val() + '',


        success: (resWeek) => {


            $('#imgDiv').html(`  <img id='imgWeekModal'  alt="Image" style=" width:50px ; height:50px" src=' ` + resWeek.image + `' />`);

            $('#weekDescription').val(resWeek.description);

        }
    });
});



// ------------  empty modal on click down -----------------

$('.lessonEditModal').on('hidden.bs.modal', function () {
    $('#videoUpload').val('');
    $('#imageUpload').val('');
    $('#audioUpload').val('');
    $('#audioUpload2').val('');
});


//   Adding new questions in listen And Speak activity edit





//  ---------------   mcqs calculations --------------------
function removeQuestionFunc(value) {

    $('#removeQuestion' + value + '').remove();
    regenerateQuestionAfterAdd();
   
}

function regenerateQuestionAfterAdd() {
    var j = 1;
    $('.regenerateCount').each((i, element) => {

        $(element).children("p").text('Question No : ' + j);

        j++;
      
    });
}


  //   load video in video div

document.getElementById("videoUpload").addEventListener("change", function () {
    $('#videoLessonEditDiv').html(``);
   
    var id = "video";
    for (i = 0; i < this.files.length; i++) {
        id = i;
        $('#videoLessonEditDiv').append(`<video id=` + id + ` width="100" height="100" controls style="display: none;"></video>`);
        var media = URL.createObjectURL(this.files[i]);
        var video = document.getElementById(id);
        video.src = media;
        video.style.display = "block";

    }


    
});



//---------------------------/   load audio in audio div---------------------

document.getElementById("audioUpload").addEventListener("change", function () {
    
    var id = "video_1";
    for (i = 0; i < this.files.length; i++) {
      //  id = i;
        $('#audioLessonEditDiv').html(``);
        $('#audioLessonEditDiv').append(`<div><audio id=` + id + ` width="100" height="100" controls style="display: none;"></audio></div>`);
        var media = URL.createObjectURL(this.files[i]);
        var video = document.getElementById(id);
        video.src = media;
        video.style.display = "block";

    }
});

document.getElementById("audioUpload2").addEventListener("change", function () {
   
    var id = "video_2";
    var media = "";
    for (i = 0; i < this.files.length; i++) {
        id = i;
      
        var media = URL.createObjectURL(this.files[i]);
      //  video = document.getElementById(id);
        //video.src = media;
        //srcAudio2 = media;
        //video.style.display = "block";

    }

    $('#audioLessonEditDiv2').html(`<div><audio  width="150" height="130" controls><source src="` + media + `"></audio> <span> </span></div>`);
});


//


//----------------------------  image load in table---------------------


var fileModalpic = "";

$("#imageUpload").on("change", function (e) {
    const [file] = e.target.files;

    $('#imgLessonEditDiv').html('<div class="imageContainer"> <img id="blah" src="#" alt="your image" style="width:100px;height:100px"  /></div>');

    if (file) {
        blah.src = URL.createObjectURL(file);

        fileModalpic = URL.createObjectURL(file);
        console.log(fileModalpic);
    }
});


//   edit modal category change


$(document).on('change', '#CourseCategorydrpEdit', function () {

    var id = $('#CourseCategorydrpEdit').find(':selected').val();
    var jsondata = JSON.stringify({ "categoryId": id });


    $.ajax(
        {
            type: 'GET',
            url: globalUrlForAPIs + 'Course/GetAllCourseByCategoryId?categoryId=' + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: (res) => {

                $('#CoursedrpEdit').empty();
                $('#CoursedrpEdit').append(`<option value="-1">
                                       Select Course
                                  </option>`);

                $.each(res, function (i, item) {
                    $('#CoursedrpEdit').append(`<option value="${item.courseId}">
                                       ${item.courseName}
                                  </option>`);
                });
            }
        }
    )

});

// **************************   UPDATING ****************************
//*******************************************************************


$(document).on('click', '#btnUpdateLesson', function () {


    var textVal = CKEDITOR.instances['ckContent'].getData();
   
    var formData = new FormData();
    //alert('inserted lesson id---' + insertedLessonId);

    var courseiddrp = $('#CoursedrpEdit').find(":selected").val();
   /* var courseCatdrp = $('#CourseCategorydrp').find(":selected").val();*/
    var lessonid = globalLessonIdForEdit;

    var lessonType = $('#drpLessonType').find(":selected").val();
    var day = $('#dayDrp').find(":selected").val();
   // var textval = CKEDITOR.instances['ckContent'].getData();

    var activity =$('#drpFormat').find(":selected").val();;
    var weekNumber = $('#weekNumberDrp').find(":selected").val();
    var alias = $('#drpAlias').find(":selected").val();

    formData.append("LessonId", lessonid);
    formData.append("courseId", courseiddrp);
    formData.append("lessonType", lessonType);


    if (lessonType == 'week') {
        formData.append("weekNumber", weekNumber);
        formData.append("dayNumber", day);
    }
    else
    {
        formData.append("weekNumber", 0);
        formData.append("dayNumber", 0);
    }


   
    formData.append("activity", activity);
    formData.append("activityAlias", alias);

    

    formData.append("text", textVal);
   

    console.log();

        $.ajax(
            {
                url: globalUrlForAPIs+'Lesson/updateLesson',
                data: formData,
                dataType: 'JSON',
                processData: false,
                contentType: false,
                enctype: "multipart/form-data",
                type: 'POST',
                success: (res) => {

                    loadVideoLesson();
                    loadAudioLesson();
                    loadTextLesson();
                    loadMcqsLesson();

                 
                   

                    // -----------------update video --------------------
                    //-----------------  video -----------------------------

                    if (activity == 'video') {
                        var videoObject = {};
                        var documentFilesArray = [];
                        var formDataVideo = new FormData();

                        $($("#videoUpload")[0].files).each((i, element) => {


                            videoObject.video = $("#videoUpload")[0].files[i];
                        });

                        if (videoObject.video != null)
                        {
                          
                            videoObject.language = "-";
                            videoObject.lessonId = lessonid;
                            videoObject.mediaType = "video";

                            formDataVideo.append("video", videoObject.video);
                            formDataVideo.append("language", videoObject.language);
                            formDataVideo.append("lessonId", videoObject.lessonId);
                            formDataVideo.append("mediaType", videoObject.mediaType);



                            $.ajax(
                                {
                                    url: globalUrlForAPIs + 'Lesson/UpdateDocumentFilesForLesson',
                                    data: formDataVideo,
                                    //dataType: 'JSON',
                                    processData: false,
                                    contentType: false,
                                    enctype: "multipart/form-data",
                                    type: 'POST',
                                    success: (res) => {


                                        loadVideoLesson();
                                        loadAudioLesson();
                                        loadTextLesson();
                                        loadMcqsLesson();

                                    }
                                });
                        }
                       





                    }
                    if (activity == 'audio') {


                        //--------------  updating image -------------------
                        var imageObject = {};
                        var documentFilesArray = [];
                        var formDataImage = new FormData();

                        $($("#imageUpload")[0].files).each((i, element) => {


                            imageObject.image = $("#imageUpload")[0].files[i];
                        });
                        if (imageObject.image != null) {

                            imageObject.language = "-";
                            imageObject.lessonId = lessonid;
                            imageObject.mediaType = "image";

                            formDataImage.append("image", imageObject.image);
                            formDataImage.append("language", imageObject.language);
                            formDataImage.append("lessonId", imageObject.lessonId);
                            formDataImage.append("mediaType", imageObject.mediaType);



                            $.ajax(
                                {
                                    url: globalUrlForAPIs + 'Lesson/UpdateDocumentFilesForLesson',
                                    data: formDataImage,
                                    //dataType: 'JSON',
                                    processData: false,
                                    contentType: false,
                                    enctype: "multipart/form-data",
                                    type: 'POST',
                                    success: (res) => {


                                        loadVideoLesson();
                                        loadAudioLesson();
                                        loadTextLesson();
                                        loadMcqsLesson();

                                    }
                                });
                        }



                        //---------------  updating audio ------------------
                        var audioObject = {};
                        var formDataAudio = new FormData();

                        $($("#audioUpload")[0].files).each((i, element) => {


                            audioObject.audio = $("#audioUpload")[0].files[i];
                        });

                        if (audioObject.audio != null) {

                            audioObject.language = "-";
                            audioObject.lessonId = lessonid;
                            audioObject.mediaType = "audio";

                            formDataAudio.append("audio", audioObject.audio);
                            formDataAudio.append("language", audioObject.language);
                            formDataAudio.append("lessonId", audioObject.lessonId);
                            formDataAudio.append("mediaType", audioObject.mediaType);



                            $.ajax(
                                {
                                    url: globalUrlForAPIs + 'Lesson/UpdateDocumentFilesForLesson',
                                    data: formDataAudio,
                                    //dataType: 'JSON',
                                    processData: false,
                                    contentType: false,
                                    enctype: "multipart/form-data",
                                    type: 'POST',
                                    success: (res) => {


                                        loadVideoLesson();
                                        loadAudioLesson();
                                        loadTextLesson();
                                        loadMcqsLesson();

                                    }
                                });
                        }






                    }

                    // ------------- update Listen and speak -------------
                    if (activity == 'listenAndSpeak' || activity == 'watchAndSpeak')
                    {
                        // deleting questions
                        if (toBeRemovedQuestions.length > 0) {

                            $.ajax(
                                {
                                    type: 'POST',
                                    url: globalUrlForAPIs + 'Lesson/deleteSpeakActivityQuestions',
                                    data: JSON.stringify( toBeRemovedQuestions),

                                    contentType: 'application/json; charset=utf-8',
                                    datatype: 'json',
                                    success: function (result) {
                                        alert('Success ');
                                        toBeRemovedQuestions = [];
                                    },
                                });
                        }
                    }

                    if (activity == 'listenAndSpeak' || activity == 'watchAndSpeak') {

                      
                       

                        var questionsArray = [];

                        //------------------------

                        $('.listenAndSpeakQuestionDiv').each((i, element) => {

                            //      var questionsObject = new Object;
                            var objectListen =
                            {
                                old: "",
                                haveNewMediaFile: "",
                                savedSpeakId:"",
                                audio: "",
                                question: "",
                                answer: []
                            }

                            var oldCheck = $(element).find('.oldCheck').attr('old');

                            if (oldCheck == 1) {

                                var newAudioCheck = $(element).find('.speakAudio')[0].files[0];
                                if (newAudioCheck != undefined) {
                                    objectListen.audio = $(element).find('.speakAudio')[0].files[0];
                                    objectListen.question = $(element).find('.listenAndSpeakQuestion').val();
                                    objectListen.lessonId = lessonid;
                                    objectListen.savedSpeakId = $(element).find('.oldCheck').attr('speakId');

                                    $(element).find('.listenAndSpeakAnswer').each((j, text) => {
                                        objectListen.answer.push($(text).val());
                                    });
                                    objectListen.old = 1;
                                    objectListen.haveNewMediaFile = 1;

                                    questionsArray.push(objectListen);
                                }
                                else
                                {
                                    objectListen.audio = $(element).find('.savedAudio').attr('src');;
                                    objectListen.question = $(element).find('.listenAndSpeakQuestion').val();
                                    objectListen.lessonId = lessonid;
                                    objectListen.savedSpeakId = $(element).find('.oldCheck').attr('speakId');

                                    $(element).find('.listenAndSpeakAnswer').each((j, text) => {
                                        objectListen.answer.push($(text).val());
                                    });
                                    objectListen.old = 1;
                                    objectListen.haveNewMediaFile = 0;

                                    questionsArray.push(objectListen);
                                }
                               
                            }
                            else
                            {
                               
                                    objectListen.audio = $(element).find('.speakAudio')[0].files[0];
                                   objectListen.question = $(element).find('.listenAndSpeakQuestion').val();
                                objectListen.lessonId = lessonid;
                                objectListen.savedSpeakId = $(element).find('.oldCheck').attr('speakId');

                                $(element).find('.listenAndSpeakAnswer').each((j, text) => {
                                    objectListen.answer.push($(text).val());
                                });
                                objectListen.old = 0;
                                questionsArray.push(objectListen);
                            }
                           
                         

                           

                            

                        });

                        for (var i = 0; i < questionsArray.length; i++) {

                            if (questionsArray[i].old == 0) {

                                var formDataSave = new FormData();
                                formDataSave.append('question', questionsArray[i].question);
                                formDataSave.append('answer', questionsArray[i].answer);
                                formDataSave.append('audio', questionsArray[i].audio);
                                formDataSave.append('lessonId', lessonid);

                                $.ajax(
                                    {
                                        url: globalUrlForAPIs + 'Lesson/AddSpeakActivityQuestions',
                                        data: formDataSave,
                                        async: false,
                                        processData: false,
                                        contentType: false,
                                        enctype: "multipart/form-data",
                                        type: 'POST',
                                        success: (res) => { }
                                    });

                            }
                            if (questionsArray[i].old == 1)
                            {
                               
                                if (questionsArray[i].haveNewMediaFile == 0)
                                {

                                    var formDataUpdateWithoudAudio = new FormData();
                                    formDataUpdateWithoudAudio.append('question', questionsArray[i].question);
                                    formDataUpdateWithoudAudio.append('answer', questionsArray[i].answer);
                                    formDataUpdateWithoudAudio.append('audio', questionsArray[i].audio);
                                    formDataUpdateWithoudAudio.append('lessonId', lessonid);
                                    formDataUpdateWithoudAudio.append('savedSpeakId', questionsArray[i].savedSpeakId);


                                    $.ajax(
                                        {
                                            url: globalUrlForAPIs + 'Lesson/UpdateSpeakActivityQuestionsWithoutAudio',
                                            data: formDataUpdateWithoudAudio,
                                            async: false,
                                            processData: false,
                                            contentType: false,
                                            enctype: "multipart/form-data",
                                            type: 'POST',
                                            success: (res) => { }
                                        });
                                }
                                else
                                {

                                    var formDataUpdate = new FormData();
                                    formDataUpdate.append('question', questionsArray[i].question);
                                    formDataUpdate.append('answer', questionsArray[i].answer);
                                    formDataUpdate.append('audio', questionsArray[i].audio);
                                    formDataUpdate.append('lessonId', lessonid);
                                    formDataUpdate.append('savedSpeakId', questionsArray[i].savedSpeakId);


                                    $.ajax(
                                        {
                                            url: globalUrlForAPIs + 'Lesson/UpdateSpeakActivityQuestions',
                                            data: formDataUpdate,
                                            async: false,
                                            processData: false,
                                            contentType: false,
                                            enctype: "multipart/form-data",
                                            type: 'POST',
                                            success: (res) => { }
                                        });

                                }
                               
                            }
                        }

                        alert('Updated');

                    }


                    ////---------------update mcq------------------
                    if (activity == 'mcqs') {
          
                   

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
                        questionsObject.lessonId = globalLessonIdForEdit;
                        questionsArray.push(questionsObject)
                    });

                        mcqsArr = JSON.stringify(questionsArray);
                        $.ajax(
                            {
                                url: globalUrlForAPIs + 'Lesson/updateMcqs',
                                data: mcqsArr,
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
    

  
  

});
//--------------------------  MCqs Designing ------------------------------------------

//-----------------------------  btn append mcq question click -------------------------
let mcqQuestionCount = 2;
var idCount = globalMcqLength + 1;
$(document).on('click', '.btnAddMcq', function () {

   // let idCount = globalMcqLength + 1;
   // alert(globalMcqLength);

    alert('mcqlength   ' + globalMcqLength);


    $('#questionDiv').append(`<div id="removeQuestion` + idCount + `" class="iterateElements">

                                    <h3 class="regenerateCount"><p>Question No : `+ mcqQuestionCount + `</p></h3>
                                    <button type="button" class="btn btn-danger " id="`+ idCount + `" onclick="removeQuestionFunc(this.id)">Remove</button>
                                     <input type="text" id="txtMcqQuestion`+ idCount + `" class="form-control txtMcqQuestion iterateInputElements question" style="border-radius:5px" placeholder="Write question" />
<br>
<input type="text" id="txtMcqOption1`+ idCount + `"  class="form-control iterateInputElements option"  placeholder="provide option" />
<br>
<input type="text" id="txtMcqOption2`+ idCount + `"  class="form-control iterateInputElements option" placeholder="provide option" />
<br>
<input type="text" id="txtMcqOption3`+ idCount + `"  class="form-control iterateInputElements option" placeholder="provide option" />
<br>
<input type="text" id="txtMcqOption4`+ idCount + `"  class="form-control iterateInputElements option" placeholder="provide option" />
         <br>                         
                                   <input type="text" id="txtCorrectAnswer`+ idCount + `" class="form-control iterateInputElements" placeholder="provide Correct answer" />
                                   
                                    </div>
                                     `);

    mcqQuestionCount++;
    idCount++;
    regenerateQuestionAfterAdd();

});

function removeQuestionFunc(value) {

    $('#removeQuestion' + value + '').remove();
    regenerateQuestionAfterAdd();
    //var j = 1;

    //$('.regenerateCount').each((i, element) => {

    //    $(element).children("p").text('Question No : '+j);

    //    j++;

    //});
}

function regenerateQuestionAfterAdd() {
    var j = 1;
    $('.regenerateCount').each((i, element) => {

        $(element).children("p").text('Question No : ' + j);

        j++;
        //$('.regenerateCount').text(j);
        //j++
    });
}

//----------------------------------------------------------------------------------------









