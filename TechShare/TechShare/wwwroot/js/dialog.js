/*Dialog xóa bài viết*/
$("#dialog-delete-post").dialog({
    autoOpen: false,
    resizable: false,
    width: 400,
    height: "auto",
    modal: true,   
    buttons: {
        "Xác nhận": function () {
            $(this).dialog("close");
        },
        "Hủy bỏ": function () {
            $(this).dialog("close");
        }
    }
});
/*Dialog chi tiết thông tin nhân viên*/
$("#dialog-detail").dialog({
    autoOpen: false,
    resizable: false,
    width: 400,
    height: "auto",
    modal: true,
    buttons: {
        "Ok": function () {
            $(this).dialog("close");
        }
    }
});
/*Dialog xóa nhân viên*/
ConfigDialog("#dialog-delete-employee");
/*Dialog thêm nhân viên*/
ConfigDialog("#dialog-add-employee");
/*Dialog chặn nhân viên*/
ConfigDialog("#dialog-block-member");
/*Dialog bỏ chặn nhân viên*/
ConfigDialog("#dialog-unblock-member");


function ConfigDialog(diaId) {
    $(diaId).dialog({
        autoOpen: false,
        resizable: false,
        width: 400,
        height: "auto",
        modal: true
    });
}