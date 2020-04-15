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