
$(document).ready(function () {
    showRecord();
});
function InsertUpdate() {
    if ($("#txtDiscountName").val() == "") {
        alert('Please Enter Discount Name');
        $("#txtDiscountName").focus();
    }
    else if ($("#txtPercentageAmount").val() == "") {
        alert('Please Enter Percentage Amount');
        $("#txtPercentageAmount").focus();
    }


    else {
        $.post("/Admin/InsertUpdateDiscountMaster",
            {
                Id: $("#hdId").val(),
                DiscountName: $("#txtDiscountName").val(),
                PercentageAmount: $("#txtPercentageAmount").val(),
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
    $.post("/Admin/ShowDiscountList",
        {},
        function (data) {
            if (data.Data != undefined && data.Data != "") {
                var jsonData = JSON.parse(data.Data);
                $('#tblBookList').DataTable().destroy()
                $('#tblBookList').DataTable({
                    data: jsonData,
                    columns: [
                        { data: "DiscountName", title: "Category Name" },
                        { data: "PercentageAmount", title: "Percentage Amount" },
                        { data: "IsActive", title: "Is Active" },
                        {
                            render: function (data, type, row, meta) {
                                return '<a href="#"><button onclick="EditRecord(\'' + row.Id + '\')" class="btn btn-success">Edit</button><a>';
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
    $.post("/Admin/EditDiscount",
        { Id: Id },
        function (data) {
            if (data.Result == "") {
                var Data = JSON.parse(data.Record);
                $.each(Data, function (index, Value) {
                    $('#hdId').val(Value.Id),
                        $('#txtDiscountName').val(Value.DiscountName),
                        $('#txtPercentageAmount').val(Value.PercentageAmount),
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
        $.post("/Admin/DeleteDiscount",
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
    $("#txtDiscountName").val("");
    $("#txtPercentageAmount").val("");
    $('#btnSave').text('Save');
}