url = "/api/member/";
$(document).ready(function () { 
    userId = $("#user-id").val();
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
    /*Danh sách bài viết của user*/
    GetListOfPosts(1, 10);

    /*Lưu bài viết*/
    $("#btn-save-post").on("click", () => {
        CreateNewPost(0, null, SavePostSucceed, SavePostFail);
    });
    /*Gửi bài viết*/
    /*Nhấn vào nút gửi bài viết thì bài viết sẻ được lưu vào db với status=1
     * Nếu lưu thành công, sẻ gọi API để gửi message từ member đến employee
     * Ngược lại sẻ yêu cầu người dùng gửi lại bài viết
     */
    /*Xóa bài viết*/
    $(document).on("click", ".delete-post", function () {
        let postId = $(this).parent().parent().parent().parent().data("post")["Id"];                
        $("#dialog-delete-post").dialog("open");
        $("#dialog-delete-post").data("postId", postId);
    });
    CancelDialog("#dialog-delete-post", "#dia-del-post-cancel");
    $(document).on("click", "#dialog-delete-post #dia-del-post-confirm", function () {
        let postId = $("#dialog-delete-post").data("postId");
        $.ajax({
            method: "DELETE",
            url: url + `deletepost?postId=${postId}&userId=${userId}`
        }).done((res) => {
            $("#dialog-delete-post").dialog("close");
            GetListOfPosts(1, 5);
        }).fail((err) => {
            console.log(err);
        })
    });    
});
/*Binding dữ liệu danh sách bài viết*/
var BindingListPosts = function (res) {
    $("#list-posts").empty();
    for (var i = 0; i < res.length; i++) {
        let post = $(`<div class="post col-md-12 mb-3"></div>`).data("post", res[i]);        
        let categoriesArr = FormatDataRows(res[i]["CategoriesName"], "<Name>", "</Name>");        
        let categories = categoriesArr.join(" | ");
        if (categoriesArr.length == 1) categories = categoriesArr[0];
        let status = "";
        let action = "";
        let date = "";
        switch (res[i]["Status"]) {
            case 0:
                status = "Đã lưu";
                action = `<a title="Gửi bài viết" class="btn-tool submit-post" href="#"><i class="fa fa-share-square"></i></a>
                        <a title="Sửa bài viết" class="btn-tool edit-post" href="Member/EditPost?postId=${res[i].Id}"}><i class="fa fa-edit"></i></a>
                        <span title="Xóa bài viết" class="btn-tool delete-post"><i class="fa fa-trash"></i></span>`;
                date = FormatDate(res[i]["UpdatedAt"]);
                break;
            case 1:
                status = "Đã gửi";
                action = `<a title="Xem bài viết" class="btn-tool detail-post" href="#"><i class="fa fa-eye"></i></a>                        
                        <a title="Gửi lại" class="btn-tool submit-post" href="#"><i class="fa fa-share-square"></i></a>`;
                date = FormatDate(res[i]["SubmitedAt"]);
                break;
            case 2:
                status = "Đã đăng";
                action = `<a title="Xem bài viết" class="btn-tool detail-post" href="Home/Post/${res[i].Id}"><i class="fa fa-eye"></i></a>
                        <a title="Sửa bài viết" class="btn-tool edit-post" href="#"><i class="fa fa-edit"></i></a>
                        <a title="Thu hồi bài viết" class="btn-tool revoke-post" href="#"><i class="fa fa-reply"></i></a>`;
                date = FormatDate(res[i]["PublishedAt"]);
                break;
            default:                
        }               
        post.html(        
           `<div class= "card card-border shadow">
                <div class="row">
                    <div class="col-md-10">
                        <div class="card card-border card-post">
                            <div class="card-header">
                                <a class="card-title" href="#">${res[i].Title}</a>
                            </div>
                            <div class="card-body card-info">
                                <div class="card-info-first text-muted">
                                    <div class="status-post">${status}</div>
                                    <div class="date-post">${date}</div>
                                </div>
                                <div class="card-info-second">
                                    <div class="categories-post">${categories}</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2 card-tool">
                        ${action}
                    </div>
                </div>
            </div >`);
        $("#list-posts").append(post);
    }
}
/*Lấy danh sách bài viết */
function GetListOfPosts(pageNumber,size) {
    $.ajax({
        method: "GET",
        url: url + `listposts?userId=${userId}&pageNumber=${pageNumber}&size=${size}`
    }).done((res) => {
        BindingListPosts(res[0]);
    }).fail((err) => {
        console.log(err);
    });
}
/*Reset form viết bài viết mới */
function ResetFormPost() {
    $("#InputTitle").val("");
    $("#InputCategory").val([]);    
    $("#InputAvatar").val("");
    tinymce.activeEditor.setContent("");
}
/*Tạo một bài viết mới*/
function CreateNewPost(status, submitedAt, callback1, callback2) {
    /*Dữ liệu bài viết*/
    var Post = {
        Title: $("#InputTitle").val(),
        CategoriesID: $("#InputCategory").val(),
        Status: status,
        SubmitedAt: submitedAt,
        Avatar: $("#InputAvatar").val(),
        Content: tinymce.activeEditor.getBody().innerHTML,
        UserId: $("#user-id").val()
    }
    $.ajax({
        method: "POST",
        url: url + "newpost",
        contentType: 'application/json; charset=UTF-8',
        dataType: "json",
        data: JSON.stringify(Post)
    }).done((res) => callback1(res))
      .fail((err) => callback2(err));
}
/*Lưu bài viết thành công*/
function SavePostSucceed(res) {
    console.log(res);
    ResetFormPost();
    GetListOfPosts(1, 5);
    $("#dialog-action-status").dialog({ title: "Lưu bài viết" }).dialog("open");
    $("#dialog-action-status #dia-content").text("Lưu bài viết thành công");
}
/*Lưu bài viết thất bại*/
function SavePostFail(err) {
    console.log(err);
    $("#dialog-action-status").dialog({ title: "Lưu bài viết" }).dialog("open");
    $("#dialog-action-status #dia-content").text("Lưu bài viết thất bại! Bạn hãy thử lại");
}