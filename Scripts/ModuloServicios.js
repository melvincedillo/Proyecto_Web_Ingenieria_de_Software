var productos = [];

$(function () {
    $("#addInsumo").click(function () {
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
    let elementos = [];
    for (const d of productos) {
        if (d.id != id) {
            elementos.push(d);
        }
    }
    productos = elementos;
    $("#" + id).remove();
    console.log(productos);
}

function addProduct(data) {
    let esta = false;
    let produ;
    let elementos = [];
    for (const d of productos) {
        if (d.id == data.id) {
            esta = true;
            produ = d;
        } else {
            elementos.push(d);
        }
    }

    if (esta == true) {
        produ.cantidad = produ.cantidad + data.cantidad;
        produ.total = produ.total + data.total;
        elementos.push(produ);
        addInTabla(produ);
        productos = elementos;
    } else {
        productos.push(data);
        addInTabla(data);
    }
}

function addInTabla(data) {
    $("#" + data.id).remove();
    $("#tableProducts").append(
        '<tr id = "' + data.id + '"><td>'
        + data.nombre + '</td><td>'
        + data.cantidad + '</td><td>'
        + data.precio + '</td><td>'
        + data.total + '</td>' +
        '<td><button class="btn btn-sm btn-danger" type="button" onclick="deleteProduct(' + data.id + ');">Quitar</button></td></tr>'
    );
}