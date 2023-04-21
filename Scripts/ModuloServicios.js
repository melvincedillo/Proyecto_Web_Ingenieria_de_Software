var productos = [];

$(function () {
    $("#addInsumo").click(function () {
        var id = $("#idProduct").val();
        var nombre = $("#nombreProduct").val();
        var precio = $("#precioProduct").val();
        var cantidad = $("#cantProduct").val();
        var total = parseFloat(precio) * parseFloat(cantidad);

        var data = {
            id: id,
            nombre: nombre,
            precio: precio,
            cantidad: cantidad,
            total: total
        };

        productos.push(data);

        if (id != "" && nombre != "" && precio != "" && cantidad != "") {
            $("#tableProducts").append(
                '<tr id = "' + id +'"><td>'
                + id + '</td><td>'
                + nombre + '</td><td>'
                + cantidad + '</td><td>'
                + precio + '</td><td>'
                + total + '</td>' +
                '<td><button class="btn btn-sm btn-danger" onclick="deleteProduct(' + id +');">Quitar</button></td></tr>'
            );
            limpiar();
        } else {
            alert("Campos vacios");
        }
        console.log(productos);
    });
});

function limpiar() {
    $("#idProduct").val("");
    $("#nombreProduct").val("");
    $("#precioProduct").val("");
    $("#cantProduct").val("");
}

function deleteProduct(id) {
    var a = $("#" + id);
    console.log(a);
    $("#" + id).remove();
}