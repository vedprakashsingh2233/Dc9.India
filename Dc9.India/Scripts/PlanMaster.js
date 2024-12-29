$(document).ready(function () {
    BindSubCategory();
    showRecord();
});
function BindSubCategory() {
    $.post("/Admin/ShowSubCategoryList",
        {},
        function (data) {
            var jsonData = JSON.parse(data.Data);
            $('#ddlSubCategory').html("").append('<option value="0">Select Category</option>');
            $(jsonData).each(function () {
                $('#ddlSubCategory').append('<option value=' + this.Id + '>' + this.SubCategoryName + '</option>');
            });
        }).fail(function (edata) {
            alert("error while feching record.");
        });
};

function InsertUpdate() {
    if ($("#ddlSubCategory").val() == "0") {
        alert('Please Select Sub Category ');
        $("#ddlSubCategory").focus();
    }
    else if ($("#txtPlanName").val() == "") {
        alert('Please Enter Plan Name');
        $("#txtPlanName").focus();
    }
    else if ($("#txtPrice").val() == "") {
        alert('Please Enter Plan Price');
        $("#txtPrice").focus();
    }


    else {
        $.post("/Admin/InsertUpdatePlanMaster",
            {
                Id: $("#hdId").val(),
                PlanName: $("#txtPlanName").val(),
                Price: $("#txtPrice").val(),
                PlanType: $("#txtPlanType").val(),
                Ram: $("#txtRam").val(),
                vCPU: $("#txtvCPU").val(),
                SSD: $("#txtSSD").val(),
                HDD: $("#txtHDD").val(),
                Memory: $("#txtMemory").val(),
                Bandwidth: $("#txtBandwidth").val(),
                DedicatedIP: $("#txtDedicatedIP").val(),
                OSChoice: $("#txtOSChoice").val(),
                Remark: $("#txtRemark").val(),
                ServerLocation: $("#txtServerLocation").val(),
                NVMe: $("#txtNVMe").val(),
                Bonus: $("#txtBonus").val(),
                Migration: $("#txtMigration").val(),
                SSL: $("#txtSSL").val(),
                Security: $("#txtSecurity").val(),
                Monitoring: $("#txtMonitoring").val(),
                Prevention: $("#txtPrevention").val(),
                Service_Support: $("#txtService_Support").val(),
                Support: $("#txtSupport").val(),
                Guarantee: $("#txtGuarantee").val(),
                Sub_Cat_Id_Fk: $("#ddlSubCategory").val(),
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
    $.post("/Admin/ShowPlanList",
        {},
        function (data) {
            if (data.Data != undefined && data.Data != "") {
                var jsonData = JSON.parse(data.Data);
                $('#tblBookList').DataTable().destroy()
                $('#tblBookList').DataTable({
                    data: jsonData,
                    columns: [
                        { data: "PlanName", title: "Plan Name" },
                        { data: "Price", title: "Price" },
                        { data: "Bandwidth", title: "Bandwidth" },
                        { data: "SubCategoryName", title: "SubCategory Name" },
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
    $.post("/Admin/EditPlan",
        { Id: Id },
        function (data) {
            if (data.Result == "") {
                var Data = JSON.parse(data.Record);
                $.each(Data, function (index, Value) {
                    $('#hdId').val(Value.Id);
                    $('#txtPlanName').val(Value.PlanName);
                    $('#txtPrice').val(Value.Price);
                    $('#txtPlanType').val(Value.PlanType);
                    $('#txtRam').val(Value.Ram);
                    $('#txtvCPU').val(Value.vCPU);
                    $('#txtSSD').val(Value.SSD);
                    $('#txtHDD').val(Value.HDD);
                    $('#txtMemory').val(Value.Memory);
                    $('#txtBandwidth').val(Value.Bandwidth);
                    $('#txtDedicatedIP').val(Value.DedicatedIP);
                    $('#txtOSChoice').val(Value.OSChoice);
                    $('#txtRemark').val(Value.Remark);
                    $('#txtServerLocation').val(Value.ServerLocation);
                    $('#txtNVMe').val(Value.NVMe);
                    $('#txtBonus').val(Value.Bonus);
                    $('#txtMigration').val(Value.Migration);
                    $('#txtSSL').val(Value.SSL);
                    $('#txtSecurity').val(Value.Security);
                    $('#txtMonitoring').val(Value.Monitoring);
                    $('#txtPrevention').val(Value.Prevention);
                    $('#txtService_Support').val(Value.Service_Support);
                    $('#txtSupport').val(Value.Support);
                    $('#txtGuarantee').val(Value.Guarantee);
                    $('#ddlSubCategory').val(Value.Sub_Cat_Id_Fk);
                    $('#chkIsActive').prop('checked', Value.IsActive == 1 ? true : false);
                });

                $('#btnSave').text('Update');
            }
            else {
                alert(data.Result);
            }
        });
}
function DeleteRecord(Id) {
    if (confirm("Do you want to delete this ?")) {
        $.post("/Admin/DeletePlan",
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
    $("#txtPlanName").val("");
    $("#txtPrice").val("");
    $("#txtPlanType").val("");
    $("#txtRam").val("");
    $("#txtvCPU").val("");
    $("#txtSSD").val("");
    $("#txtHDD").val("");
    $("#txtMemory").val("");
    $("#txtBandwidth").val("");
    $("#txtDedicatedIP").val("");
    $("#txtOSChoice").val("");
    $("#txtRemark").val("");
    $("#txtServerLocation").val("");
    $("#txtNVMe").val("");
    $("#txtBonus").val("");
    $("#txtMigration").val("");
    $("#txtSSL").val("");
    $("#txtSecurity").val("");
    $("#txtMonitoring").val("");
    $("#txtPrevention").val("");
    $("#txtService_Support").val("");
    $("#txtSupport").val("");
    $("#txtGuarantee").val("");
    $("#ddlSubCategory").val("0");
    $("#chkIsActive").prop('checked', false);
    $('#btnSave').text('Save');
}