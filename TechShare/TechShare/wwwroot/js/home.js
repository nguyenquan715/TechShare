url="/api/home/"
$(document).ready(function () {
    $.ajax({
        method: "GET",
        url: url + "numberofpage?size=6"
    }).done((res) => {
        let numberOfPage = parseInt(res["response"][0]["message"]);
        let data = `<li class="page-item"><a href="?page=1" class="page-link active">1</a></li>`;
        for (var i = 2; i <= numberOfPage; i++) {
            data += `<li class="page-item"><a href="?page=${i}" class="page-link">${i}</a></li>`;
        }
        $("#pagination-home").html(data);
    }).fail((err) => {
        console.log(err);
    });
    //$(document).on("click", "#pagination-home .page-item .page-link", function () {
    //    $("#pagination-home .page-item .active").removeClass("active");
    //    $(this).addClass("active");
    //});
});