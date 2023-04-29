
var idServicio = 0;
var idDia = 0;
var servicios = [];
var horas = []

function CambiarVisibleSeccion() {
    let x = document.getElementById("addServiceSeccion");
    x.style.display = "none";
    idServicio = 0;
}

function cargarServicio(url) {
    let data = { id: idServicio }

    $.get(url, data).done(function (resp) {
        console.log(resp);
        $("#nombreServicio").val(resp.name);
        $("#descripcionServicio").val(resp.descripcion);
        $("#precioServicio").val(resp.precio);

        let x = document.getElementById("addServiceSeccion");
        x.style.display = "block";
    });
}

function comprobarFecha(url, url2) {
    let date = $("#fecha").val();
    let data = { fecha: date };

    $.get(url, data).done(function (resp) {
        if (resp.disponible == false) {
            alert("Lo sentimos la fecha ingresada no es laborable");
            $("#fecha").val("");
            $("#btnModal").attr('disabled', true);
        } else {
            $("#btnModal").attr('disabled', false);
            idDia = resp.idDay;
            GetHoras(url2);
        }
    });
}

function GetHoras(url) {
    let data = { id: idDia };
    $.get(url, data).done(function (resp) {
        horas = resp;
        limpiarSelect();
        cargarSelect();
    });
}

function limpiar() {
    $("#nombreServicio").val("");
    $("#descripcionServicio").val("");
    $("#precioServicio").val("");
}

function cargarSelect() {
    let horasSelect = document.getElementById("horaServicio");
    for (const h of horas) {
        horasSelect.innerHTML += `<option value="${h.id}">${h.hora}</option>`;
    }
}

function limpiarSelect() {
    let horasSelect = document.getElementById("horaServicio");
    horasSelect.innerHTML = `<option>Seleccione una hora</option>`;
}