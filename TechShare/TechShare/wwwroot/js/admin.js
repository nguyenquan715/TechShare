url="/api/admin/"
$(document).ready(function () {
    /*Lấy thông tin toàn bộ nhân viên*/
    $.ajax({
        method: "GET",
        url: url+"employee"        
    }).done(function (res) {                
        for (let i = 0; i < res.length; i++) {
            BindingIntoEmployeeTable(res[i]);
        }
    }).fail(function (err) {
        console.log(err);
    });

    /*Xem chi tiết thông tin user*/
    $(document).on("click",".detail-employee", function () {
        $("#dialog-detail-employee").dialog("open");        
        let employeeId = $(this).parent().parent().data("EmployeeID");
        $.ajax({
            method: "GET",
            url: url + employeeId
        }).done((res) => {
            BindingIntoDetailDialog(res);
        }).fail((err) => {
            console.log(err);
        });
    });
});
/*Binding dữ liệu vào table*/
var BindingIntoTable=function(user) {
    let row = $("<tr></tr>").data("EmployeeID", user["id"]);
    let gender = "Nam";
    if (user.gender) gender = "Nữ";
    let inner = `<td>${user.lastName} ${user.firstName}</td><td>${user.userName}</td><td>${gender}</td><td>${user.email}</td>
                <td><span class="detail-employee">Xem</span><span class="delete-employee">Xóa</span></td>`;
    row.html(inner);
    $("table#tbl-employee tbody").append(row);
}
/*Binding dữ liệu vào dialog*/
var BindingIntoDetailDialog=function(res) {
    $("#dialog-detail-employee #dia-fullname").text(res.lastName + " " + res.firstName);
    $("#dialog-detail-employee #dia-nickname").text(res.userName);
    $("#dialog-detail-employee #dia-email").text(res.email);
    let gender = "Nam";
    if (res.gender) gender = "Nữ";
    $("#dialog-detail-employee #dia-gender").text(gender);
    $("#dialog-detail-employee #dia-job").text(res.job);
}