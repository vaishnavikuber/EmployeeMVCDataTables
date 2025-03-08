// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//$(document).ready(function () {
//    // Initialize DataTable
//    var table = $('#employeesTable').DataTable({
//        dom: 'lftrip', // Ensures the built-in search box appears
//        paging: true,   // Enables pagination
//        searching: true // Enables the search box
//    });

//    // Custom search (if needed)
//    $('#searchInput').on('keyup', function () {
//        table.search(this.value).draw();
//    });
//});



function AddEmployee() {
    var objData = {
        EmployeeName: $('#name').val(),
        Email: $('#email').val(),
        Age: parseInt($('#age').val()),
        Salary: parseFloat($('#salary').val()),
        City: $('#city').val(),
        Department: $('#department').val(),
        Gender: $('input[name="editGender"]:checked').val()
    }
    $.ajax({
        url: '/Employee/AddEmployee',
        type: 'Post',
        data: JSON.stringify(objData),
        contentType: 'application/json;charset=utf-8',
        dataType: 'json',
        success: function () {
            //alert('Data Saved');
            location.reload();
        },
        error: function () {
            alert('Data Not Saved');
        }
    })
}
$(document).ready(function () {
    $(".btnEdit").click(function () {
        var employeeId = $(this).data("id");

        // Fetch employee data by ID
        $.ajax({
            url: "/Employee/GetDataById?id=" + employeeId,
            type: "GET",
            contentType: "application/json",
            success: function (data) {
                // Populate modal with employee data
                $("#editEmployeeId").val(data.employeeId);
                $("#editName").val(data.employeeName);
                $("#editEmail").val(data.email);
                $("#editAge").val(data.age);
                $("#editSalary").val(data.salary);
                $("#editCity").val(data.city);
                $("#editDepartment").val(data.department);

                // Check gender radio button
                if (data.gender === "Male") {
                    $("#editGenderMale").prop("checked", true);
                } else {
                    $("#editGenderFemale").prop("checked", true);
                }

                // Show the modal
                $("#modalEditEmployee").modal("show");
            },
            error: function () {
                alert("Error fetching employee data.");
            }
        });
    });

    // Save changes when clicking "Save Changes" button
    $("#btnEditSave").click(function () {
        var updatedData = {
            EmployeeId: $("#editEmployeeId").val(),
            EmployeeName: $("#editName").val(),
            Email: $("#editEmail").val(),
            Age: parseInt($("#editAge").val()),
            Salary: parseInt($("#editSalary").val()),
            City: $("#editCity").val(),
            Department: $("#editDepartment").val(),
           
            Gender: $('input[name="editGender"]:checked').val()
        };

        $.ajax({
            url: "/Employee/EditEmployee",
            type: "POST",
            data: JSON.stringify(updatedData),
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (response) {
                //alert(response);
                location.reload();
            },
            error: function () {
                alert("Failed to update employee.");
            }
        });
    });

    // Close modal when clicking "Cancel"
    $("#btnEditCancel").click(function () {
        $("#modalEditEmployee").modal("hide");
    });

    //delete btn
    $(document).on("click", ".btnDelete", function () {
        var employeeId = $(this).data("id");
        $("#modalDeleteEmployee").data("id", employeeId).modal("show");
    });

    $("#btnDeleteFinal").click(function () {
        var employeeId = $("#modalDeleteEmployee").data("id");

        $.ajax({
            url: "/Employee/DeleteEmployee?id=" + employeeId,
            type: "POST",
            success: function () {
                //alert("Employee deleted successfully!");
                location.reload();
            },
            error: function () {
                alert("Something went wrong!");
            }
        });
    });




});


//function SaveEditEmployee() {
//    var objData = {
//        EmployeeName: $('#name').val(),
//        Email: $('#email').val(),
//        Age: parseInt($('#age').val()),
//        Salary: parseFloat($('#salary').val()),
//        City: $('#city').val(),
//        Department: $('#department').val(),
//        Gender: $('input[name="editGender"]:checked').val()
//    }
//    $.ajax({
//        url: '/Employee/EditEmployee',
//        type: 'Post',
//        data: JSON.stringify(objData),
//        contentType: 'application/json;charset=utf-8',
//        dataType: 'json',
//        success: function () {
//            alert('Data Saved');
//            location.reload();
//        },
//        error: function () {
//            alert('Data Not Saved');
//        }
//    })
//}
