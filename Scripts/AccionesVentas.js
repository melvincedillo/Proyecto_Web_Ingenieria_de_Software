var productos = [];

$(function () {

    $("#addProducto").click(function () {
        if (id != "" && nombre != "" && precio != "" && cantidad != "") {
            var id = $("#idProduct").val();
            var nombre = $("#nombreProduct").val();
            var precio = $("#precioProduct").val();
            var cantidad = $("#cantProduct").val();
            var total = parseFloat(precio) * parseFloat(cantidad);

            var data = {
                id: id,
                nombre: nombre,
                precio: parseFloat(precio),
                cantidad: parseFloat(cantidad),
                total: total
            };

            addProduct(data);
            limpiar();
            calcularTotal();
        }
    });


});

function agregarTablaHtml(data) {

}