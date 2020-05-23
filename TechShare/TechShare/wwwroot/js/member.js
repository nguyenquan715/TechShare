url="/api/member/"
$(document).ready(function () {    
    /*Binding các category vào select*/
    $.ajax({
        method: "GET",
        url: url + "categories"
    }).done((res) => {
        BindingDataIntoSelect("#InputCategory", res);
        //Multiple select 
        $('#InputCategory').selectpicker();
    }).fail((err) => {
        console.log(err);
    });
    
    /*Lưu bài viết*/
    $("#btn-save-post").on("click", function () {
        /*Dữ liệu bài viết*/
        var Post = {
            Title: $("#InputTitle").val(),
            CategoriesID: $("#InputCategory").val(),
            Status: 0,
            SubmitedAt: null,
            Content: tinymce.activeEditor.getContent(),
            UserId: $("#user-id").val()
        }
        $.ajax({
            method: "POST",
            url: url + "newpost",
            contentType:'application/json; charset=UTF-8',
            dataType: "json",
            data: JSON.stringify(Post)
        }).done((res) => {
            console.log(res);
            ResetFormPost();
            $("#dialog-action-status").dialog({ title: "Lưu bài viết" }).dialog("open");
            $("#dialog-action-status #dia-content").text("Lưu bài viết thành công");
        }).fail((err) => {
            console.log(err);
            $("#dialog-action-status").dialog({ title: "Lưu bài viết" }).dialog("open");
            $("#dialog-action-status #dia-content").text("Lưu bài viết thất bại! Bạn hãy thử lại");
        });
    });    
});
var BindingDataIntoSelect = function (selectId, res) {
    let data = "";
    for (var i = 0; i < res.length; i++) {
        data+=`<option value="${res[i].id}">${res[i].name}</option>`
    }
    $(selectId).html(data);
}
function ResetFormPost() {
    $("#InputTitle").val("");
    $("#InputCategory").val([]);    
    tinymce.activeEditor.setContent("");
}