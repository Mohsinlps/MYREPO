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
                                    videoArray = videoArray + '<video width="150" height="150" controls><source src="' + data.videos[i] + '"></video>';
                                    videoArray = videoArray + ' ';
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

    var jsondata = JSON.stringify({ activity: "text", courseId: CourseId });
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







//        ---------------          edit -----------------------------

$(document).on('click', '.btnEditLesson', function () {

    var lessonId = $(this).attr('lessonId');

   
   

    $.ajax({
        type: 'GET',
        url: globalUrlForAPIs + 'GetLessons/GetLessonWithCourseAndCategoryById?id=' + lessonId,
      
        success: (resLesson) =>
        {
            console.log(resLesson);

            //load category
            $.ajax(
                {
                    type: 'GET',
                    url: globalUrlForAPIs + 'CourseCategory/GetAllCourseCategories',

                    success: (res) => {

                        alert('cat' + resLesson[0].courseCategoryId);
                        $('#CourseCategorydrpEdit').empty();

                        //$('#CourseCategorydrpEdit').append(`<option value="` + resLesson[0].courseCategoryId + `">
                        //               `+ resLesson[0].courseCategoryName + ` </option>`);
                        $.each(res, function (i, item) {
                            $('#CourseCategorydrpEdit').append(`<option value="${item.courseCategoryId}">
                                       ${item.courseCategoryName}
                                  </option>`)
                        });


                        $("#CourseCategorydrpEdit select").val(resLesson[0].courseCategoryId).change();
                        console.log(res[0].courseCategoryId);
                      
                    


                    }
                }
            )

            //   load course drp

            //$.ajax({
            //    type: 'GET',
            //    url: globalUrlForAPIs + 'Course/GetAllCourseByCategoryId?categoryId=' + res[0].courseCategoryId,
            //    success: (res) =>
            //    {
            //        $('#Coursedrp').empty();
            //        $('#Coursedrp').append(`<option value="-1">
            //                           Select Course
            //                      </option>`);

            //        $.each(res, function (i, item) {
            //            $('#Coursedrp').append(`<option value="${item.courseId}">
            //                           ${item.courseName}
            //                      </option>`);
            //        });

            //        $('#Coursedrp').val(res[0].courseId).change();


            //    }
            //});

            //$('#drpLessonType').val(res[0].lessonType).change();
            //if (res[0].lessonType != 'week') {
            //    $('.weekNumberDiv').prop('hidden', true);
            //    $('.dayDiv').prop('hidden', true);
                
            //}
            //else
            //{
            //    $('.weekNumberDiv').prop('hidden', false);
            //    $('.dayDiv').prop('hidden', false);

            //    $('#weekNumberDrp').val(res[0].weekNumber).change();


            //    //  load week
            //    $.ajax({
            //        type: 'GET',
            //        url: globalUrlForAPIs + 'Lesson/GetWeekByCourseIdAndWeekNumber?id=' + res[0].courseId + '&weekNumber=' + res[0].weekNumber + '',
            //        success: (resWeek) =>
            //        {
            //            console.log('--resweek--' + resWeek);
            //            $('#imgDiv').html(`  <img id='imgWeekModal' alt="Image" style=" width:50px ; height:50px" src=' ` + resWeek.image + `' />`);
            //        }
            //    });
               

            //}





            $('.lessonEditModal').modal('toggle');




        }
        
    });

});


//--------------------------  MCqs Designing ------------------------------------------

//-----------------------------  btn append mcq question click -------------------------
let mcqQuestionCount = 2;
let idCount = 1;

$(document).on('click', '.btnAddMcq', function () {




    $('#questionDiv').append(`<div id="removeQuestion` + idCount + `" class="iterateElements">

                                    <h3 class="regenerateCount"><p>Question No : `+ mcqQuestionCount + `</p></h3>
                                    <button type="button" class="btn btn-danger " id="`+ idCount + `" onclick="removeQuestionFunc(this.id)">Remove</button>
                                     <input type="text" id="txtMcqQuestion`+ idCount + `" class="form-control txtMcqQuestion iterateInputElements question" style="border-radius:5px" placeholder="Write question" />
                                     <input type="text" id="txtMcqOption1`+ idCount + `"  class=" iterateInputElements option"  placeholder="provide option" />
                                     <input type="text" id="txtMcqOption2`+ idCount + `"  class=" iterateInputElements option" placeholder="provide option" />
                                     <input type="text" id="txtMcqOption3`+ idCount + `"  class=" iterateInputElements option" placeholder="provide option" />
                                     <input type="text" id="txtMcqOption4`+ idCount + `"  class=" iterateInputElements option" placeholder="provide option" />
                                    <hr style="color:red" />
                                   <input type="text" id="txtCorrectAnswer`+ idCount + `" class=" iterateInputElements" placeholder="provide Correct answer" />
                                   
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









