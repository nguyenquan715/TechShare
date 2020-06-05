url="/api/admin/"
$(document).ready(function () {
    /*Lấy thông tin toàn bộ nhân viên*/
    GetAllUsersByRole("employee", "#tbl-employee");
    /*Lấy thông tin toàn bộ thành viên*/
    GetAllUsersByRole("member", "#tbl-member");
    /*Xem chi tiết thông tin user*/
    $(document).on("click",".detail-user", function () {
        $("#dialog-detail").dialog("open");        
        let userId = $(this).parent().parent().data("UserID");
        $.ajax({
            method: "GET",
            url: url + userId
        }).done((res) => {
            BindingIntoDetailDialog(res,"#dialog-detail");
        }).fail((err) => {
            console.log(err);
        });
    });

    /**
     * Tác vụ thêm nhân viên
     * */
    /*Hiển thị dialog thêm nhân viên*/
    $(document).on("click", ".member-to-employee", function () {        
        let me = $(this);
        DisplayDialog(me, "#dialog-add-employee", "#dia-member-name", "#dia-add-confirm");
    });
    /*Không xác nhận thêm nhân viên*/
    CancelDialog("#dialog-add-employee", "#dia-add-cancel");
    /*Xác nhận việc thêm nhân viên*/
    $(document).on("click", "#dialog-add-employee #dia-add-confirm", function () {        
        let me = $(this);
        ConfirmDialog(me, url + "memtoemp/", "PUT", "#dialog-add-employee",1);
    });

    /**
     * Tác vụ xóa nhân viên
     * */
    /*Hiển thị dialog xóa nhân viên*/
    $(document).on("click", ".delete-employee", function () {        
        let me = $(this);
        DisplayDialog(me, "#dialog-delete-employee", "#dia-employee-name", "#dia-del-confirm");
    });
    /*Không xác nhận xóa nhân viên*/
    CancelDialog("#dialog-delete-employee", "#dia-del-cancel");
    /*Xác nhận việc thêm nhân viên*/
    $(document).on("click", "#dialog-delete-employee #dia-del-confirm", function () {        
        let me = $(this);
        ConfirmDialog(me, url + "emptomem/", "PUT", "#dialog-delete-employee",1);
    });

    /**
     * Tác vụ chặn thành viên
     * */
    /*Hiển thị dialog chặn thành viên*/
    $(document).on("click", ".block-member", function () {
        let me = $(this);
        DisplayDialog(me, "#dialog-block-member", "#dia-member-name", "#dia-block-confirm");
    });
    /*Hủy việc chặn thành viên*/
    CancelDialog("#dialog-block-member", "#dia-block-cancel");
    /*Xác nhận việc chặn thành viên*/
    $(document).on("click", "#dialog-block-member #dia-block-confirm", function () {
        let me = $(this);
        ConfirmDialog(me, url + "block/", "PUT", "#dialog-block-member",0);
    });

    /**
     * Tác vụ bỏ chặn thành viên
     * */
    /*Hiển thị dialog bỏ chặn thành viên*/
    $(document).on("click", ".unblock-member", function () {
        let me = $(this);
        DisplayDialog(me, "#dialog-unblock-member", "#dia-member-name", "#dia-unblock-confirm");
    });
    /*Hủy việc bỏ chặn thành viên*/
    CancelDialog("#dialog-unblock-member", "#dia-unblock-cancel");
    /*Xác nhận việc bỏ chặn thành viên*/
    $(document).on("click", "#dialog-unblock-member #dia-unblock-confirm", function () {
        let me = $(this);
        ConfirmDialog(me, url + "block/", "PUT", "#dialog-unblock-member",0);
    });
});
/*Binding dữ liệu vào table*/
var BindingIntoTable=function(user,tblId,role) {
    let row = $("<tr></tr>").data("UserID", user["id"]);
    let gender = "Nam";
    if (user.gender) gender = "Nữ";
    let inner = `<td>${user.lastName} ${user.firstName}</td><td>${user.userName}</td><td>${gender}</td><td>${user.email}</td>`;    
    if (role == "employee") {
        inner += `<td class="text-center"><span class="detail-user mr-2">Xem</span><span class="delete-employee">Xóa</span></td>`;
    }
    if (role == "member") {        
        if (user.blocked) {
            inner += `<td class="text-center"><span class="detail-user mr-2">Xem</span><span class="unblock-member mr-2">Bỏ Chặn</span></td>`;
        } else {
            inner += `<td class="text-center"><span class="detail-user mr-2">Xem</span><span class="block-member mr-2">Chặn</span><span class="member-to-employee">Thêm nhân viên</span></td>`;
        }
    }
    row.html(inner);
    $("table" + tblId +" tbody").append(row);
}
/*Binding dữ liệu vào dialog*/
var BindingIntoDetailDialog = function (res, diaId) {
    $(diaId + " #dia-fullname").text(res.lastName + " " + res.firstName);
    $(diaId + " #dia-nickname").text(res.userName);
    $(diaId+" #dia-email").text(res.email);
    let gender = "Nam";
    if (res.gender) gender = "Nữ";
    $(diaId + " #dia-gender").text(gender);
    $(diaId+" #dia-job").text(res.job);
}
/*Lấy thông tin tất cả user theo role*/
var GetAllUsersByRole = function (role,tblId) {
    $.ajax({
        method: "GET",
        url: url + role
    }).done(function (res) {
        for (let i = 0; i < res.length; i++) {
            BindingIntoTable(res[i], tblId, role);
        }
    }).fail(function (err) {
        console.log(err);
    });
}
/*Hiển thị Dialog*/
function DisplayDialog(me,diaId,nameId,btnConf) {
    $(diaId).dialog("open");
    let userId = me.parent().parent().data("UserID");
    let userName = me.parent().parent().children().first().text();
    $(diaId + " " + nameId).text(userName);
    $(diaId + " " + btnConf).data("UserID", userId);
}

/*Xác nhận tác vụ*/
function ConfirmDialog(me,url,method,diaId,mode) {
    let userId = me.data("UserID");
    $.ajax({
        method: method,
        url: url + userId
    }).done((res) => {
        $(diaId).dialog("close");
        //Nếu mode=1 thì reset lại cả hai bảng
        //Nếu không thì chỉ reset lại bảng member
        if (mode == 1) {
            ResetTable("#tbl-employee");
            GetAllUsersByRole("employee", "#tbl-employee");
        }
        ResetTable("#tbl-member");
        GetAllUsersByRole("member", "#tbl-member");        
        console.log(res);
    }).fail((err) => {
        console.log(err);
    });
}
function BindingError(componentId, err) {   
    $(componentId + " .error").html(`<li>${err}</li>`);
}