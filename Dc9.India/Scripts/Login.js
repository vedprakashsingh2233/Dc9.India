$(document).ready(function () {
    ClearData();
    $("#btnLogin").click(function () {
        Login();
        return false;
    });
    $("#btnLogin").keydown(function (e) {
        if (e.which == 13) {
            Login();
            return false;
        }
    });
    $("#txtPassword").keydown(function (e) {
        if (e.which == 13) {
            Login();
            return false;
        }
    });
});


function Login() {
    try {
        if ($("#txtUserCode").val() == "") {
            alert("Please enter User Code.!");
            $("#txtUserCode").focus();
        }
        else if ($("#txtPassword").val() == "") {
            alert("Please enter PASSWORD.!");
            $("#txtPassword").focus();
        }
        else {
            $.post("/Admin/LoginDetail",
                {
                    UserCode: $("#txtUserCode").val(),
                    Password: $("#txtPassword").val()
                }, function (data) {
                    if (data.Result != "") {
                        $("#txtPassword").val("");
                        alert(data.Result);
                    }
                    else {
                        $(window.location).attr("href", "/Admin/CategoryMaster");
                    }
                }).fail(function (xhr, err) {
                    var responseTitle = $(xhr.responseText).filter('title').get(0);
                    alert($(responseTitle).text() + "\n" + AjaxError(xhr, err));
                });
        }
    }
    catch (e) {
        alert("Error In Login: " + e.message);
    }
}


function ClearData() {
    $("#txtUserCode").val("");
    $("#txtPassword").val("");
}



function myFunction() {
    var x = document.getElementById("txtPassword");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}