function btnLogin_Click() {

    showLoader();

    var d = {
        username: $('#txtUserName').val(),
        password: $('#txtPassword').val()
    };

    var res = null;

    $.ajax({
        url: "/Home/Authenticate",
        type: "POST",
        cache: false,
        data: d,
        //contentType: false,
        enctype: 'multipart/form-data',
        async: true,
        //processData: false,
        success: function (data) {

            // debugger;
            console.log(data);
            //res = data;
            hideLoader();
            if (data["ResStatus"] == true) {
                window.location.href = '/Home/Index';
                console.log(data);
            }
            else {
                swal({
                    text: data['ResMsg'],
                    button: "Ok",
                    icon: "error",
                    timer: 3000,
                });
                //window.location.href = '/Home/Login';
            }

        }
        , error: function (xhr, status, error2) {
            debugger;

            //console.log(xhr.statusText);
            //console.log(status);
            //console.log(error2);

            //alert(xhr.responseText);

            hideLoader();
            swal({
                text: xhr.responseText,
                button: "Ok",
                icon: "error",
                timer: 3000,
            });
        }
    }).done(function (data) {
        debugger;
        hideLoader();
        //console.log(data);
        //swal({
        //    text: "unexpected error occured",
        //    button: "Ok",
        //    icon: "error",
        //    timer: 3000,
        //});
        //hideLoader();
    });
}
