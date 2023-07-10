$(document).ready(function () {
    alert('Emp');
    showAllEmployee();


    $('#btnempDelete').click(function () {
        var id = $('#txtempidDelete').val();

        $.ajax({
            type: 'DELETE',
            url: '/Employees/DeleteEmployee',
            data: { id: id },
            success: function () {
                alert('deleted successfully.');
                location.reload();
            },
            error: function () {
                alert('Error deleting department.');
            }
        });
    });



    //$("form").submit(function (e) {
    //    //e.preventDefault(); // Prevent form submission

    //    // Check if a gender is selected
    //    if (!$("input[name='gender']:checked").val()) {
    //        alert("Please select a gender.");
    //        return;
    //    }

  
    //});


    $('#btnEmpcreate').click(function () {
        $.ajax({
            url: '/Employees/GetDropdownData',
            type: 'GET',
            success: function (data) {
                if (data) {
                    var dropdownData = JSON.parse(data);

                    $.each(dropdownData, function (index, item) {
                        $('#dropdownList').append($('<option>').text(item.name).val(item.id));
                    });

                    $('#createEmployeeModal').modal('show');
                }
            },

            error: function () {
                alert("error");
            }
        });
    });



    $(document).on('click', '.empedit-btn', function () {

         //Get the row data
        var row = $(this).closest('tr');
        var id = row.find('td:eq(0)').text();
        var name = row.find('td:eq(1)').text();
        var email = row.find('td:eq(2)').text();
        var mobile = row.find('td:eq(3)').text();
        //var gender = row.find('td:eq(4)').text();
        var departmentid = row.find('td:eq(5)').text();


        // Set the data in the edit modal
        $("#txtempIdupdate").val(id);
        $("#txtempnameupdate").val(name);
        $("#txtempemailupdate").val(email);
        $("#txtempmobupdate").val(mobile);
/*        $("#female").val(gender);*/
        $("#dropdownListedit").val(departmentid);

        $.ajax({
            url: '/Employees/GetDropdownData',
            type: 'GET',
            success: function (data) {
                if (data) {
                    var dropdownData = JSON.parse(data);

                    $.each(dropdownData, function (index, item) {
                        $('#dropdownListedit').append($('<option>').text(item.name).val(item.id));
                    });
                  /*  updateeee();*/
                $('#editempModal').modal('show');
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

});






$(document).on('click', '.deleteEmp-btn', function () {
    // Get the row data
    var row = $(this).closest('tr');
    var id = row.find('td:eq(0)').text();
    var name = row.find('td:eq(1)').text();
    var email = row.find('td:eq(2)').text();
    var mobile = row.find('td:eq(3)').text();

    // Set the data in the edit modal
    $("#txtempidDelete").val(id);
    $("#txtempnameDelete").val(name);
    $("#txtempemailDelete").val(email);
    $("#txtempmobileDelete").val(mobile);

    // Show the edit modal
    $('#modalempDelete').modal('show');
});




function showAllEmployee() {
    var url = '/Employees/getEmployees';
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (result) {
            var empList = result;
            var object = '';
            $.each(empList, function (index, item) {
                object += '<tr>';
                object += '<td>' + item.id + '</td>';
                object += '<td>' + item.name + '</td>';
                object += '<td>' + item.email + '</td>';
                object += '<td>' + item.mobile + '</td>';
                object += '<td>' + item.gender + '</td>';
                object += '<td class=d-none>' + item.departmentId + '</td>';
                object += '<td>' + item.departmentName + '</td>';
                object += '<td><a href="#" class="btn btn-primary empedit-btn">Edit</a> || <a href="#" class="btn btn-danger deleteEmp-btn">Delete</a></td>';
                object += '</tr>';
            });
            $('#tableEmp_data').html(object);
        },
        error: function () {
            alert('Data Not Found');
        }
    });
}



    









function UpdateEmployee() {

    var ele = document.getElementsByName('genderedit');
    for (i = 0; i < ele.length; i++) {
        if (ele[i].checked)
            var gender1 = ele[i].value;
    }
    var objdata = {
        Id: $('#txtempIdupdate').val(),
        Name: $('#txtempnameupdate').val(),
        email: $('#txtempemailupdate').val(),
        Mobile: $('#txtempmobupdate').val(),
        Gender: gender1,
        DepartmentId: $("#dropdownListedit").val()

    };

    $.ajax({
        type: "POST",
        url: "/Employees/UpdateEmployees",
        data: JSON.stringify(objdata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            alert("Data Updated");
            location.reload();
        },
        error: function () {
            alert("Error");
        }
    });
}


function AddEmployee() {
    var gender1;

    var ele = document.getElementsByName('gender');
    for (i = 0; i < ele.length; i++) {
        if (ele[i].checked) 
            gender1  =  ele[i].value;
    }
    var objdata = {
        name: $('#txtempnamecreate').val(),
        email: $('#txtempemailcreate').val(),
        mobile: $('#txtempmob').val(),
        gender: gender1 ,
        departmentid: $('#dropdownList').val(),


    }

    $.ajax({
        url: '/Employees/InsertEmployee',
        type: 'Post',
        data: JSON.stringify(objdata),
        contentType: 'application/xxx-www-form-urlencoded;charset=utf-8;',
        datatype: 'json',
        success: function () {
            alert("Data Saved");
            location.reload();
        },
        error: function () {
            alert("Error");


        }
    });
};











 function validateFormEmployeeCreate() {
        // Retrieve form inputs
         var name = document.getElementById("txtempnamecreate").value;
         var email = document.getElementById("txtempemailcreate").value;
         var mobile = document.getElementById("txtempmob").value;


        // Perform validation
        if (name === "") {
            alert("Please enter your name.");
            return false;
        }

        if (email === "") {
            alert("Please enter your email.");
            return false;
        }

        // Validate email format using a regular expression
        var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(email)) {
            alert("Please enter a valid email address.");
            return false;
        }

        if (mobile === "") {
            alert("Please enter your mobile number.");
            return false;
        }

        // Validate mobile number format using a regular expression
        var mobileRegex = /^\d{10}$/;
        if (!mobileRegex.test(mobile)) {
            alert("Please enter a valid 10-digit mobile number.");
            return false;
        }


  
     AddEmployee();
     
}


function validateFormEmployeeUpdate() {
    // Retrieve form inputs
    var name = document.getElementById("txtempnameupdate").value;
    var email = document.getElementById("txtempemailupdate").value;
    var mobile = document.getElementById("txtempmobupdate").value;


    // Perform validation
    if (name === "") {
        alert("Please enter your name.");
        return false;
    }

    if (email === "") {
        alert("Please enter your email.");
        return false;
    }

    // Validate email format using a regular expression
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
        alert("Please enter a valid email address.");
        return false;
    }

    if (mobile === "") {
        alert("Please enter your mobile number.");
        return false;
    }

    // Validate mobile number format using a regular expression
    var mobileRegex = /^\d{10}$/;
    if (!mobileRegex.test(mobile)) {
        alert("Please enter a valid 10-digit mobile number.");
        return false;
    }



    UpdateEmployee();

}

