
$(document).ready(function () {
    showRecord();
});
function InsertUpdate() {
    if ($("#txtCategoryName").val() == "") {
        alert('Please Enter Category Name');
        $("#txtCategoryName").focus();
    }
  

    else {
        $.post("/Admin/InsertUpdateCategoryMaster",
            {
                Id: $("#hdId").val(),
                CategoryName: $("#txtCategoryName").val(),
                IsActive: $("#chkIsActive").is(':checked') ? 1 : 0,
            },
            function (data) {
                if (data.Result != "") {
                    alert(data.Result);
                    ClearData();
                    showRecord();
                }
            });
    }
}
function showRecord() {
    $.post("/Admin/ShowCategoryList",
        {},
        function (data) {
            if (data.Data != undefined && data.Data != "") {
                var jsonData = JSON.parse(data.Data);
                $('#tblBookList').DataTable().destroy()
                $('#tblBookList').DataTable({
                    data: jsonData,
                    columns: [
                        { data: "CategoryName", title: "Category Name" },
                        { data: "IsActive", title: "Is Active" },
                        {
                            render: function (data, type, row, meta) {
                                return '<button onclick="EditRecord(\'' + row.Id + '\')" class="btn btn-success">Edit</button>';
                            },
                            title: "Edit"
                        },
                        {
                            render: function (data, type, row, meta) {
                                return '<button onclick="DeleteRecord(\'' + row.Id + '\')" class="btn btn-danger">Delete</button>';
                            },
                            title: "Delete"
                        },
                    ]
                });
            }
            else {
                $('#tblBookList').DataTable().clear().destroy();
            }
            if (data.Result != "") {
                alert(data.Result)
            }
        });

}
function EditRecord(Id) {
    $.post("/Admin/EditRecord",
        { Id: Id },
        function (data) {
            if (data.Result == "") {
                var Data = JSON.parse(data.Record);
                $.each(Data, function (index, Value) {
                    $('#hdId').val(Value.Id),
                        $('#txtCategoryName').val(Value.CategoryName),
                    $('#chkIsActive').prop('checked', Value.IsActive == 1 ? true : false);
                })
                $('#btnSave').text('Update');
            }
            else {
                alert(data.Result);
            }
        });
}
function DeleteRecord(Id) {
    if (confirm("Do you want to delete this Book Record ?")) {
        $.post("/Admin/DeleteRecord",
            { Id: Id },
            function (data) {
                if (data.Result != "") {
                    alert(data.Result);
                    showRecord();
                }
            });
    }
}
function ClearData() {
    $("#hdId").val("0");
    $("#txtCategoryName").val("");
    $('#btnSave').text('Save');
}