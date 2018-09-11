$(document).ready(function () {
    $("form").submit(function (event) {

        if ($("#FullName").val() == "") {
            event.preventDefault();
            $("#name").text("Full Name is required");
            $("#name").show(500);
            $("#FullName").focus();
        }
        else
            $("#name").hide(500);

        if ($("#Email").val() == "") {
            event.preventDefault();
            $("#email").text("Email is required");
            $("#email").show(500);
                $("#Email").focus();
           
        }
        else if ($("#Phone").val() == "" || $("#Phone").val().length != 11) {
            event.preventDefault();
            $("#phone").text("Phone is required");
            $("#phone").show(500);
                $("#Phone").focus();
            
        }
        else if ($("#Address").val() == "") {
            event.preventDefault();
            $("#address").text("Address is required");
            $("#address").show(500);
                $("#Address").focus();
            
        }
        else if ($("#City").val() == "") {
            event.preventDefault();
            $("#city").text("City is required");
            $("#city").show(500);
                $("#City").focus();
            
        }
        else if ($("#Subject").val() == "") {
            event.preventDefault();
            $("#subject").text("Subject is required");
            $("#subject").show(500);
                $("#Subject").focus();
           
        }
        else if ($("#Message").val() == "") {
            event.preventDefault();
            $("#message").text("Message is required");
            $("#message").show(500);
                $("#Message").focus();
           
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
                    Address: $("#Address").val(),
                    Job_Title: $("#Job_Title").val(),
                    Company_Name: $("#Company_Name").val(),
                    City: $("#City").val(),
                    postal_code: $("#postal_code").val(),
                    Subject: $("#Subject").val(),
                    Message: $("#Message").val(),
                },

                success: function (data) {
                    if (data == "true")
                        swal("Success", "Successfully Submitted", "success");
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
    function resetfunction() {
        $("#FullName").val(""),
       $("#Email").val(""),
           $("#Phone").val(""),
          $("#Address").val(""),
          $("#Job_Title").val(""),
          $("#Company_Name").val(""),
          $("#City").val(""),
         $("#postal_code").val(""),
          $("#Subject").val(""),
           $("#Message").val(""),
         $("#UserStatus_Id").val(""),
        $("#UserType_Id").val(""),
         $("#name").hide();
        $("#email").hide();
        $("#phone").hide();
        $("#address").hide();
        $("#city").hide();
        $("#subject").hide();
        $("#message").hide();


    }

});
