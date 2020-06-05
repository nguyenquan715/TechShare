/*Binding dữ liệu vào select*/
var BindingDataIntoSelect = function (selectId, res) {
    let data = "";
    for (var i = 0; i < res.length; i++) {
        data += `<option value="${res[i].id}">${res[i].name}</option>`
    }
    $(selectId).html(data);
}
/*Reset table*/
function ResetTable(tblId) {
    $(tblId + " tbody").empty();
}
/*Không xác nhận tác vụ dialog*/
function CancelDialog(diaId, btnCancel) {
    $(document).on("click", diaId + " " + btnCancel, function () {
        $(diaId).dialog("close");
    });
}
/*Format date*/
function FormatDate(date) {
    let year = date.slice(0, 4);
    let month = date.slice(5, 7);
    let day = date.slice(8, 10);
    let hour = date.slice(11, 16);
    let dateFormat = `${hour} ${day}/${month}/${year}`;
    return dateFormat;
}
/*Format dữ liệu kết hợp bởi nhiều hàng ví dụ như <Id>111111</Id><Id>333333</Id>*/
function FormatDataRows(data, tagStart, tagEnd) {
    data = data.split(tagStart).join("");
    data = data.split(tagEnd);
    data.pop();
    return data;
}
