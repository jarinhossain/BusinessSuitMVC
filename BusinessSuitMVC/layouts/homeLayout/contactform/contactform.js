$(document).ready(function () {
    $("form").submit(function (event) {

        if ($("#FullName").val() == "") {
            event.preventDefault();
            swal("Failed", "FullName is required", "error").then(function () {
                $("#FullName").focus();
            });
        }
        else if ($("#Email").val() == "") {
            event.preventDefault();
            swal("Failed", "Email is required", "error").then(function () {
                $("#Email").focus();
            });
        }
        else if ($("#Phone").val() == "" || $("#Phone").val().length != 11) {
            event.preventDefault();
            swal("Failed", "Phone is required", "error").then(function () {
                $("#Phone").focus();
            });
        }
        else if ($("#Subject").val() == "") {
            event.preventDefault();
            swal("Failed", "Subject is required", "error").then(function () {
                $("#Subject").focus();
            });
        }
        else if ($("#City").val() == "") {
            event.preventDefault();
            swal("Failed", "City is required", "error").then(function () {
                $("#City").focus();
            });
        }
        else if ($("#Message").val() == "") {
            event.preventDefault();
            swal("Failed", "Message is required", "error").then(function () {
                $("#Message").focus();
            });
        }
        else {
            event.preventDefault();
           
            $.ajax({
                url: "/Home/Index",
                type: "POST",
                dataType: "json",
                data: {
                    FullName: $("#FullName").val(),
                    Email: $("#Email").val(),
                    Phone: $("#Phone").val(),
                    Subject: $("#Subject").val(),
                    Job_Title: $("#Job_Title").val(),
                    Company_Name: $("#Company_Name").val(),
                    City: $("#City").val(),
                    postal_code: $("#postal_code").val(),
                    Address: $("#Address").val(),
                    Message: $("#Message").val(),
                },

                success: function (data) {
                    if (data == "true")
                        swal("Success", "Successfully Saved", "success");
                    else
                        swal("Failed", data, "error");
                    resetfunction();
                },
                error: function (data) {
                    alert(data)
                },

            })
        }
  });

});
