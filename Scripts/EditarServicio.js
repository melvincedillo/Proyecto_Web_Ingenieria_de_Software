var productos = [];
var numero = /^\d*\.?\d*$/;

function addProducto() {
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
}

function getAllProducts(url) {
    let idService = $("#idServicio").val();
    let data = {
        id: parseInt(idService)
    };

    $.get(url, data).done(function (resp) {
        productos = resp;
        for (const p of productos) {
            addInTabla(p);
        }
    });
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

function calcularTotal() {
    let total = 0;
    let precio = $("#precioProducto").val();
    for (const tol of productos) {
        total = total + tol.total;
    }
    if (numero.test(precio) && precio != "") {
        total = total + parseFloat(precio);
    }

    $("#totalServicio").val(total);
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

function deleteProduct(id) {
    let elementos = [];
    for (const d of productos) {
        if (d.id != id) {
            elementos.push(d);
        }
    }
    productos = elementos;
    $("#" + id).remove();
    calcularTotal();
}

function limpiar() {
    $("#idProduct").val("");
    $("#nombreProduct").val("");
    $("#precioProduct").val("");
    $("#cantProduct").val("");
}

function buscarProducto(url) {
    if ($("#search").val() == "") {
        alert("Campo de busqueda vacio");
        $("#idProduct").val("");
        $("#nombreProduct").val("");
        $("#precioProduct").val("");
        $("#cantProduct").val("");
    } else {
        var url = url;
        var buscar = $("#search").val();
        var data = { codigo: buscar };

        $.get(url, data).done(function (data) {
            if (data.encontrado == true) {
                $("#idProduct").val(data.ID);
                $("#nombreProduct").val(data.ProductName);
                $("#precioProduct").val(data.Price);
            } else {
                $("#idProduct").val("");
                $("#nombreProduct").val("");
                $("#precioProduct").val("");
                $("#cantProduct").val("");
                alert("No se encontro ningun producto");
            }
        });
    }
}

function edit(url, urlPOST) {
    let codigo = $("#codigoServicio").val();
    let nombre = $("#nombreServicio").val();
    let descripcion = $("#descripServicio").val();
    let skill = $("#skillServicio").val();
    let precio = $("#precioProducto").val();
    let total = $("#totalServicio").val();
    let idService = $("#idServicio").val();

    let data = {
        Servicio: {
            codigo: codigo,
            nombre: nombre,
            descripcion: descripcion,
            skill: parseInt(skill),
            precio: parseFloat(precio),
            total: parseFloat(total),
            products: productos
        },
        id: parseInt(idService)
    }

    $.post(url, data).done(function (dt) {
        alert(dt);
        location.href = urlPOST;
    });
}