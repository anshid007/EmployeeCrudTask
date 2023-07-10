$(document).ready(function () {
    showAllDepartment();

});


function showAllDepartment() {
    var url = '/Department/getDepartments';
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (result) {
            var departmentList = JSON.parse(result);
            var object = '';
            $.each(departmentList, function (index, item) {
                object += '<tr>';
                object += '<td>' + item.id + '</td>';
                object += '<td>' + item.code + '</td>';
                object += '<td>' + item.name + '</td>';
                object += '<td><a class="btn btn-primary edit-btn">Edit</a> || <a href="#" class="btn btn-danger delete-btn">Delete</a></td>';
                object += '</tr>';
            });
            $('#table_data').html(object);
        },
        error: function () {
            alert('Data Not Found');
        }
    });
}

// Event delegation for edit button click
$(document).on('click', '.edit-btn', function () {
    // Get the row data
    var row = $(this).closest('tr');
    var id = row.find('td:eq(0)').text();
    var code = row.find('td:eq(1)').text();
    var name = row.find('td:eq(2)').text();

    // Set the data in the edit modal
    $("#txtid").val(id);
    $('#txtcodedepupdate').val(code);
    $('#txtnamedeptupdate').val(name);

    // Show the edit modal
    $('#editModal').modal('show');
});


$(document).on('click', '.delete-btn', function () {
    // Get the row data
    var row = $(this).closest('tr');
    var id = row.find('td:eq(0)').text();
    var code = row.find('td:eq(1)').text();
    var name = row.find('td:eq(2)').text();

    // Set the data in the edit modal
    $("#txtiddelete").val(id);
    $('#txtcodedelete').val(code);
    $('#txtnamedelete').val(name);

    // Show the edit modal
    $('#modalDelete').modal('show');
});






function AddDept() {
    alert("adding");
    var objdata = {
        Code: $('#txtcodedeptcreate').val(),
        Name: $('#txtnamedeptcreate').val()

    }

    $.ajax({
        url: '/Department/InsertDepartment',
        type: 'Post',
        data: JSON.stringify(objdata),
        async: true,
        contentType: 'application/xxx-www-form-urlencoded;charset=utf-8;',
        datatype: 'json',
        success: function () {
            location.reload();
            alert("Data Saved");
            location.reload();
        },
        error: function () {
            alert("Error");


        }
    });
};


$('#btncreate').click(function () {
    $('#createModal').modal('show');
});



//function UpdateDept() {
//    var objdata = {
//        Code: $('#txtcode').val(),
//        Name: $('#txtname').val(),

//    }
//    $.ajax({
//        type: "POST",
//        url: "/Department/UpdateDepartment",
//        data: JSON.stringify(objdata),
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function () {
//            alert("Data Saved");
//        },
//        error: function () {
//            alert("Error");
//        }
//    });
//};

function UpdateDept() {
    var objdata = {
        Id: $('#txtid').val(),
        Code: $('#txtcodedepupdate').val(),
        Name: $('#txtnamedeptupdate').val()
        
    };

    $.ajax({
        type: "POST",
        url: "/Department/UpdateDepartment",
        data: JSON.stringify(objdata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            alert("Data Saved");
            location.reload();
        },
        error: function () {
            alert("Error");
        }
    });
}

$('#btnDelete').click(function () {
    var id = $('#txtiddelete').val();

    $.ajax({
        type: 'DELETE',
        url: '/Department/DeleteDept',
        data: { id: id },
        success: function () {
            // Handle success case
            alert('Department deleted successfully.');
            // Refresh the page or update the UI as needed
            location.reload();
        },
        error: function () {
            // Handle error case
            alert('Error deleting department.');
        }
    });
});
