//const { forEach } = require("../vendor/fontawesome-free/js/v4-shims");

/*const { Doc } = require("../ckeditor/samples/toolbarconfigurator/lib/codemirror/codemirror");*/

/*const { Tab } = require("../lib/bootstrap/dist/js/bootstrap.bundle");*/



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

$('#CoursedrpLesson').change(function () {

    if ($('#CousedrpLesson').find(":selected").val()!=-1)
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
                                var length = data.videos.length;
                                for (var i = 0; i < length; i++) {
                                    videoArray = videoArray + '<video width="120" height="120" controls><source src="' + data.videos[i] + '"></video><span> </span>';
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
                                return `<img id="imgAudio" src="` + data.image + `"  width="50" height="50">`;
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
                                return `<img id="imgText" src="` + data.image + `"  width="50" height="50">`;
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

//aaaaaaaaaaaaaaaaaaaaaa

//-------------------  load mcqs ----------------------------------

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
              <p>Option 1 :`+ mcqsArray[i].option1 + `</p>
              <p>Option 2 :`+ mcqsArray[i].option2 +`</p>
              <p>Option 3 :`+ mcqsArray[i].option3+`</p>
              <p>Option 4 :`+ mcqsArray[i].option4+`</p>
              <h3>Correct Answer :`+ mcqsArray[i].correctAnswer+`</h3>
             
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
        }
    });
});





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


            // ------------  Lesson type ------------------------

           
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
                //  load week
                $.ajax({
                    type: 'GET',
                    url: globalUrlForAPIs + 'Lesson/GetWeekByCourseIdAndWeekNumber?id=' + resLesson[0].courseId + '&weekNumber=' + resLesson[0].weekNumber + '',
                    success: (resWeek) =>
                    {
                        console.log('--resweek--' + resWeek);
                        $('#imgDiv').html(`  <img id='imgWeekModal'  alt="Image" style=" width:50px ; height:50px" src=' ` + resWeek.image + `' />`);
                        $('#weekDescription').val(resWeek.description);

                    }
                });



                //load Day

                $("#dayDrp option[value='" + resLesson[0].dayNumber + "']").attr("selected", "selected");
                $("#drpFormat option[value='" + resLesson[0].activity + "']").attr("selected", "selected");
                $("#drpAlias option[value='" + resLesson[0].activityAlias + "']").attr("selected", "selected");
              //  $('#imgLessonEditDiv').html(`<img id='imgLessonEdit'  alt="Image" style=" width:50px ; height:50px" src=' ` + resLesson.image + `' />`)
              
            }



            $('#imgLessonEditDiv').html(
                `<img id="imgText" src="` + resLesson[0].image[0] + `"  width="75" height="auto">`
            );

            $('#videoLessonEditDiv').html(``);
            for (var i = 0; i < resLesson[0].videos.length; i++) {
              
                $('#videoLessonEditDiv').append(
                    `<video width="150" height="130" controls><source src="` + resLesson[0].videos[i] + `"></video> <span> </span>`
                    
                );

            }


            $('#audioLessonEditDiv').html(``);
            for (var i = 0; i < resLesson[0].audios.length; i++) {
 
                $('#audioLessonEditDiv').append(
                    `<audio controls> <source src= ` + resLesson[0].audios[i] + ` type="audio/ogg"> </audio>`

                );

            }


            if (resLesson[0].activity == 'video')
            {
                $('#videoHideDiv').prop('hidden', false);
                $('#imgHideDiv').prop('hidden', true);
                $('#audioHideDiv').prop('hidden', true);
                $('#divMcsqs').prop('hidden', true);
                
            }

            if (resLesson[0].activity == 'audio') {
                $('#imgHideDiv').prop('hidden', false);
                $('#audioHideDiv').prop('hidden', false);
                $('#divMcsqs').prop('hidden', true);
                $('#videoHideDiv').prop('hidden', true);
            }

            if (resLesson[0].activity == 'read') {
                $('#imgHideDiv').prop('hidden', false);
                $('#audioHideDiv').prop('hidden', false);
                $('#divMcsqs').prop('hidden', true);
                $('#videoHideDiv').prop('hidden', true);
            }

            if (resLesson[0].activity == 'mcqs') {
                $('#imgHideDiv').prop('hidden', true);
                $('#audioHideDiv').prop('hidden', true);
                $('#divMcsqs').prop('hidden', false);
                $('#videoHideDiv').prop('hidden', true);

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

//  ---------------   mcqs calculations --------------------
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
    $('#audioLessonEditDiv').html(``);
    var id = "video";
    for (i = 0; i < this.files.length; i++) {
        id = i;
        $('#audioLessonEditDiv').append(`<audio id=` + id + ` width="100" height="100" controls style="display: none;"></audio>`);
        var media = URL.createObjectURL(this.files[i]);
        var video = document.getElementById(id);
        video.src = media;
        video.style.display = "block";

    }
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
    
    var activity = $('#drpFormat').find(":selected").val();
    var weekNumber = $('#weekNumberDrp').find(":selected").val();

    formData.append("LessonId", lessonid);
    formData.append("courseId", courseiddrp);
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
    formData.append("text", textVal);
    formData.append("weekNumber", weekNumber);

    

  
   
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

                    console.log('update response');
                    console.log(res);

                    loadVideoLesson();
                    loadAudioLesson();
                    loadTextLesson();
                    loadMcqsLesson();
                   



                    ////---------------update mcq------------------
                    if (activity == 'mcqs') {
                    //    var idgenerator = 1;
                    //    var formdataMcqs = new FormData();
                    //    var txtMcqQuestion = "";
                    //    var txtoption1 = 0;
                    //    var txtoption2 = 0;
                    //    var txtoption3 = 0;
                    //    var txtoption4 = 0;
                    //    var txtCorrect = "";

                    //    var mcqsArr = [

                    //    ];


                    //    $('.txtMcqQuestion').each((i, element) => {
                    //        txtMcqQuestion = $('#txtMcqQuestion' + idgenerator + '').val();
                    //        txtoption1 = $('#txtMcqOption1' + idgenerator + '').val();
                    //        txtoption2 = $('#txtMcqOption2' + idgenerator + '').val();
                    //        txtoption3 = $('#txtMcqOption3' + idgenerator + '').val();
                    //        txtoption4 = $('#txtMcqOption4' + idgenerator + '').val();
                    //        txtCorrect = $('#txtCorrectAnswer' + idgenerator + '').val();
                    //        mcqsArr.push({
                    //            question: txtMcqQuestion,
                    //            option1: txtoption1,
                    //            option2: txtoption2,
                    //            option3: txtoption3,
                    //            option4: txtoption4,
                    //            correctAnswer: txtCorrect,
                    //            lessonId: globalLessonIdForEdit,
                    //        });

                    //        idgenerator++;
                    //    });

                        //console.log(mcqsArr);
                        //arrdata = JSON.stringify(mcqsArr);

                        //console.log(arrdata);
                   

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
                    else {
                        $.ajax(
                            {
                                url: globalUrlForAPIs + 'Lesson/deleteMcqs?id=' + globalLessonIdForEdit,
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









