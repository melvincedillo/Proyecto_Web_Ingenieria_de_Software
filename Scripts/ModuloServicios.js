

$(function () {
    $("searchBnt").click(function () {
        var url = '@Url.Action';
    });
});

function buscarInsumo(url) {
    var buscar = $("search").val;
    var data = { search: buscar }
    $.post(url, data).done(function (data) {
        alert("Exito")
    });
}