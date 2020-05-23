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
ConfigDialog("#dialog-delete-employee", 400,"auto");
/*Dialog thêm nhân viên*/
ConfigDialog("#dialog-add-employee", 400, "auto");
/*Dialog chặn nhân viên*/
ConfigDialog("#dialog-block-member", 400, "auto");
/*Dialog bỏ chặn nhân viên*/
ConfigDialog("#dialog-unblock-member", 400, "auto");
/*Dialog trạng thái tác vụ*/
$("#dialog-action-status").dialog({
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

function ConfigDialog(diaId, width, height) {
    $(diaId).dialog({
        autoOpen: false,
        resizable: false,
        width: width,
        height: height,        
        modal: true
    });
}
