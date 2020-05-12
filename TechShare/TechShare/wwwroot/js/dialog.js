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
$(".delete-post").on('click', function () {
    $("#dialog-delete-post").dialog("open");
});
/*Dialog xóa nhân viên*/
$("#dialog-delete-employee").dialog({
    autoOpen: false,
    resizable: false,
    width: 400,
    height: "auto",
    modal: true   
});
/*Dialog chi tiết thông tin nhân viên*/
$("#dialog-detail-employee").dialog({
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
