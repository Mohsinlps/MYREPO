/*const { type } = require("jquery");*/

function loadAliases()
{
    $.ajax({
        dataType: 'JSON',
        url: globalUrlForAPIs + 'Lesson/getActivityAlias',
        type: 'GET',
        success: (res) => {


            var table = $('#tblAlias').DataTable();

            //clear datatable
            table.clear().draw();

            //destroy datatable
            table.destroy();

            $('#tblAlias').dataTable({
                data: res,
                columns: [

                    { "data": "alias" },

                    {
                        "data": null, "render": function (data) {
                            return `<button class="btn btn-primary btnEditAlias" id=` + data.id + `>Edit</button>`
                        }
                    },
                    {
                        "data": null, "render": function (data) {
                            return `<button class="btn btn-danger btnDeleteAlias" id=` + data.id + `>Delete</button>`
                        }
                    },

                ]

            });
        }

    })
}


$(document).ready(function () {

    loadAliases();

});


$(document).on('click', '#btnAddAlias', function () {

    var alias = $('#txtAlias').val();
    if (alias != null)
    {
        $.ajax({

            type: 'GET',
            url: globalUrlForAPIs + 'Lesson/addActivityAlias?alias=' + alias,
            dataType: 'JSON',
            success: (res) =>
            {
              
                loadAliases();
            }

        });
    }
});


//---------------------           Delete ------------------------
$(document).on('click', '.btnDeleteAlias', function () {
    var id = $(this).attr('id');
   
    $.ajax({
        type: 'GET',
        dataType: 'JSON',
        url: globalUrlForAPIs+'Lesson/deleteActivityAlias?id='+id,
        success: (res) =>
        {
          
            loadAliases();
        }
    });
});

$(document).on('click', '.btnEditAlias', function () {
  
});