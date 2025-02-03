$(document).ready(function () {
    BindSubCategory();
    showRecord();
    $('#ddlPriceType').change(function () {
        var selectedOption = $(this).val();
        if (selectedOption == 'Yearly') {
            $("#divYear").show();
            $("#divMonthy").hide();
            $("#txtPrice").val("0");
        } else {
            $('#divYear').hide();
            $('#divMonthy').show();
            $("#txtPriceYear").val("0");
        }
    });
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
   
     if ($("#txtPlanName").val() == "") {
        alert('Please Enter Plan Name');
        $("#txtPlanName").focus();
    }
    else if ($("#txtPrice").val() == "") {
        alert('Please Enter Plan Price');
        $("#txtPrice").focus();
    }
    else if ($("#ddlPriceType").val() == "0") {
        alert('Please Select Price Type');
        $("#ddlPriceType").focus();
    }

    else {
        $.post("/Admin/InsertUpdatePlanMaster",
            {
                Id: $("#hdId").val(),
                PlanName: $("#txtPlanName").val(),             
                Price: $("#txtPrice").val(),
                PriceYearly: $("#txtPriceYear").val(),
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
                PriceType: $("#ddlPriceType").val(),
                IsActive: $("#chkIsActive").is(':checked') ? 1 : 0,
                Isprevention: $("#chkDdosAttact").is(':checked')? $("#DdosAttact").text() : "",
                IsFreeSSL: $("#chkFreeSsl").is(':checked') ? $("#FreeSsl").text() : "",
                IsFirewallMonitory: $("#chkServerFire").is(':checked') ? $("#ServerFire").text() : "",
                Is27Support: $("#chk24Support").is(':checked') ? $("#24Support").text() : "",
                IsUpTimeGuarntee: $("#chkUptimeGurantee").is(':checked') ? $("#UptimeGurantee").text() : "",
                IsFreeMigration: $("#chkFreeMigration").is(':checked') ? $("#FreeMigration").text() : "",
                IsFreeBonuses: $("#chkFreeBonus").is(':checked') ? $("#FreeBonus").text() : "",
                IsOSChoice: $("#chkOSChoice").is(':checked') ? $("#OSChoice").text() : "",
                IsPowerfulControlPanel: $("#chkPowercontrol").is(':checked') ? $("#Powercontrol").text() : "",
                IsFullRootAccess: $("#chkFullRoot").is(':checked') ? $("#FullRoot").text() : "",
                IsOneClickIns: $("#chkOneClick").is(':checked') ? $("#OneClick").text() : "",
                IsCSSJSOptimizers: $("#chkCSSJsOpt").is(':checked') ? $("#CSSJsOpt").text() : "",
                IsWorldwideDataCenters: $("#chkWorldwideData").is(':checked') ? $("#WorldwideData").text() : "",
                IsAPIIntegration: $("#chkAPIIntegrn").is(':checked') ? $("#APIIntegrn").text() : "",
                IsVul_Scanner: $("#chkVulScanner").is(':checked') ? $("#VulScanner").text() : "",
                IsFullStackDevelopment: $("#chkFullStack").is(':checked') ? $("#FullStack").text() : "",
                IsMultiplePHPVersion: $("#chkphpVersion").is(':checked') ? $("#phpVersion").text() : "",
                IsPageSpeed: $("#chkPageSpeed").is(':checked') ? $("#PageSpeed").text() : "",
                IsWebsiteOptimization: $("#chkWebsiteOpt").is(':checked') ? $("#WebsiteOpt").text() : "",
                IsPHPVulCheck: $("#chkphpVulCheck").is(':checked') ? $("#phpVulCheck").text() : "",
                IsProtectiveFirewall: $("#chkProtectiveFir").is(':checked') ? $("#txtProtectiveFir").text() : "",
                IsMalwareScans: $("#chkMalwareScan").is(':checked') ? $("#txtMalwareScan").text() : "",
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
                    $('#ddlPriceType').val(Value.PriceType);
                    $('#chkIsActive').prop('checked', Value.IsActive == 1 ? true : false);
                    $('#chkDdosAttact').prop('checked', Value.Isprevention !== null && Value.Isprevention !==""? true : false);
                    $('#chkFreeSsl').prop('checked', Value.IsFreeSSL !== null && Value.IsFreeSSL !== "" ? true : false);
                    $('#chkServerFire').prop('checked', Value.IsFirewallMonitory !== null && Value.IsFirewallMonitory !== "" ? true : false);
                    $('#chk24Support').prop('checked', Value.Is27Support !== null && Value.Is27Support !== "" ? true : false);
                    $('#chkUptimeGurantee').prop('checked', Value.IsUpTimeGuarntee !== null && Value.IsUpTimeGuarntee !== "" ? true : false);
                    $('#chkFreeMigration').prop('checked', Value.IsFreeMigration !== null && Value.IsFreeMigration !== "" ? true : false);
                    $('#chkFreeBonus').prop('checked', Value.IsFreeBonuses !== null && Value.IsFreeBonuses !== "" ? true : false);
                    $('#chkOSChoice').prop('checked', Value.IsOSChoice !== null && Value.IsOSChoice !== "" ? true : false);
                    $('#chkPowercontrol').prop('checked', Value.IsPowerfulControlPanel !== null && Value.IsPowerfulControlPanel !== "" ? true : false);
                    $('#chkFullRoot').prop('checked', Value.IsFullRootAccess !== null && Value.IsFullRootAccess !== "" ? true : false);
                    $('#chkOneClick').prop('checked', Value.IsOneClickIns !== null && Value.IsOneClickIns !== "" ? true : false);
                    $('#chkCSSJsOpt').prop('checked', Value.IsCSSJSOptimizers !== null && Value.IsCSSJSOptimizers !== "" ? true : false);
                    $('#chkWorldwideData').prop('checked', Value.IsWorldwideDataCenters !== null && Value.IsWorldwideDataCenters !== "" ? true : false);
                    $('#chkAPIIntegrn').prop('checked', Value.IsAPIIntegration !== null && Value.IsAPIIntegration !== "" ? true : false);
                    $('#chkVulScanner').prop('checked', Value.IsVul_Scanner !== null && Value.IsVul_Scanner !== "" ? true : false);
                    $('#chkFullStack').prop('checked', Value.IsFullStackDevelopment !== null && Value.IsFullStackDevelopment !== "" ? true : false);
                    $('#chkphpVersion').prop('checked', Value.IsMultiplePHPVersion !== null && Value.IsMultiplePHPVersion !== "" ? true : false);
                    $('#chkPageSpeed').prop('checked', Value.IsPageSpeed !== null && Value.IsPageSpeed !== "" ? true : false);
                    $('#chkWebsiteOpt').prop('checked', Value.IsWebsiteOptimization !== null && Value.IsWebsiteOptimization !== "" ? true : false);
                    $('#chkphpVulCheck').prop('checked', Value.IsPHPVulCheck !== null && Value.IsPHPVulCheck !== "" ? true : false);
                    $('#chkProtectiveFir').prop('checked', Value.IsProtectiveFirewall !== null && Value.IsProtectiveFirewall !== "" ? true : false);
                    $('#chkMalwareScan').prop('checked', Value.IsMalwareScans !== null && Value.IsMalwareScans !== "" ? true : false);

                    
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
    $("#ddlPriceType").val("0");
    $("#chkIsActive").prop('checked', false);
    $("#chkDdosAttact").prop('checked', false);
    $("#chkFreeSsl").prop('checked', false);
    $("#chkServerFire").prop('checked', false);
    $("#chk24Support").prop('checked', false);
    $("#chkUptimeGurantee").prop('checked', false);
    $("#chkFreeMigration").prop('checked', false);
    $("#chkFreeBonus").prop('checked', false);
    $("#chkOSChoice").prop('checked', false);
    $("#chkPowercontrol").prop('checked', false);
    $("#chkFullRoot").prop('checked', false);
    $("#chkOneClick").prop('checked', false);
    $("#chkCSSJsOpt").prop('checked', false);
    $("#chkWorldwideData").prop('checked', false);
    $("#chkAPIIntegrn").prop('checked', false);
    $("#chkVulScanner").prop('checked', false);
    $("#chkFullStack").prop('checked', false);
    $("#chkphpVersion").prop('checked', false);
    $("#chkPageSpeed").prop('checked', false);
    $("#chkWebsiteOpt").prop('checked', false);
    $("#chkphpVulCheck").prop('checked', false);
    $("#chkProtectiveFir").prop('checked', false);
    $("#chkMalwareScan").prop('checked', false);

    $('#btnSave').text('Save');
}