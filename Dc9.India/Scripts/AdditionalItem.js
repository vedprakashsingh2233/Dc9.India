
$(document).ready(function () {
    showRecord();
});
function InsertUpdate() {
    if ($("#txtItemName").val() == "") {
        alert('Please Enter Item Name');
        $("#txtItemName").focus();
    }
    else if ($("#txtItemePrice").val() == "") {
        alert('Please Enter Item Price');
        $("#txtItemePrice").focus();
    }

    else {
        $.post("/Admin/InsertUpdateAdditionalItem",
            {
                Id: $("#hdId").val(),
                ItemName: $("#txtItemName").val(),
                ItemPrice: $("#txtItemePrice").val(),
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
    $.post("/Admin/ShowAdditionalItem",
        {},
        function (data) {
            if (data.Data != undefined && data.Data != "") {
                var jsonData = JSON.parse(data.Data);
                $('#tblBookList').DataTable().destroy()
                $('#tblBookList').DataTable({
                    data: jsonData,
                    columns: [
                        { data: "ItemName", title: "Item Name" },
                        { data: "ItemPrice", title: "Price" },
                        {
                            render: function (data, type, row, meta) {
                                return '<a href="#"><button onclick="EditRecord(\'' + row.Id + '\')" class="btn btn-success">Edit</button></a>';
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
    $.post("/Admin/EditAdditionalItem",
        { Id: Id },
        function (data) {
            if (data.Result == "") {
                var Data = JSON.parse(data.Record);
                $.each(Data, function (index, Value) {
                    $('#hdId').val(Value.Id),
                        $('#txtItemName').val(Value.ItemName),
                        $('#txtItemePrice').val(Value.ItemPrice),
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
    if (confirm("Do you want to delete this Category ?")) {
        $.post("/Admin/DeleteAdditionalItem",
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
    $("#txtItemName").val("");
    $("#ItemPrice").val("");
    $('#btnSave').text('Save');
}