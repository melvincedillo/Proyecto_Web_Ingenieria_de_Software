
var idServicio = 0; //Guearda el id del servicio seleccionado

var idDia = 0; //Guarda el id del dia de la semana seleccionado
var fechaSeleccionada = null;
var idSkillService = 0;

var numServicio = 0; //Apoyo para el manejo de la tabla y el arreglo de servicios 
var servicios = []; //Servicios solicitados por el cliente
var horas = []; //Horas disponibles segun la fecha elegida.

function CambiarVisibleSeccion() {
    let x = document.getElementById("addServiceSeccion");
    x.style.display = "none";
    idServicio = 0;
}

function addServicio() {
    let hr = "";
    for (const h of horas) {
        if (h.id == parseInt($("#horaServicio").val())) { hr = h.hora;}
    }
    let data = {
        id: idServicio,
        name: $("#nombreServicio").val(),
        descripcion: $("#descripcionServicio").val(),
        precio: parseFloat($("#precioServicio").val()),
        idhora: parseInt($("#horaServicio").val()),
        hora: hr,
        numero: numServicio
    }
    servicios.push(data);
    numServicio = numServicio + 1;
    CambiarVisibleSeccion();
    addTable(data);
    console.log(servicios);
}

function deleteServicio(num) {
    let aux = [];
    for (const x of servicios) {
        if (x.numero != num) {
            aux.push(x);
        }
    }
    servicios = aux;
    $("#" + num).remove();
    console.log(servicios);
}

function addTable(data) {
    $("#tableServicios tbody").append(
        '<tr id = "' + data.numero + '"><td>'
        + data.name + '</td><td>'
        + data.descripcion + '</td><td>'
        + data.precio + '</td><td>'
        + data.hora + '</td>' +
        '<td><button class="btn btn-sm btn-danger" type="button" onclick="deleteServicio(' + data.numero + ');">Quitar</button></td></tr>'
    );
}

function cargarServicio(url, url2) {
    let data = { id: idServicio }

    $.get(url, data).done(function (resp) {
        $("#nombreServicio").val(resp.name);
        $("#descripcionServicio").val(resp.descripcion);
        $("#precioServicio").val(resp.precio);

        idSkillService = resp.idSkill;

        GetHoras(url2);
        let x = document.getElementById("addServiceSeccion");
        x.style.display = "block";
    });
}

function comprobarFecha(url) {
    fechaSeleccionada = $("#fecha").val();
    let data = { fecha: fechaSeleccionada };

    $.get(url, data).done(function (resp) {
        if (resp.disponible == false) {
            alert("Lo sentimos la fecha ingresada no es laborable");
            $("#fecha").val("");
            fechaSeleccionada = null;
            $("#btnModal").attr('disabled', true);
        } else {
            $("#btnModal").attr('disabled', false);
            idDia = resp.idDay;
        }
    });
}

function GetHoras(url) {
    let data = {
        idHorario: idDia,
        idSkill: idSkillService,
        fecha: fechaSeleccionada
    };
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
        if (h.personal > 0) {
            horasSelect.innerHTML += `<option value="${h.id}">${h.hora}</option>`;
        }
    }
}

function limpiarSelect() {
    let horasSelect = document.getElementById("horaServicio");
    horasSelect.innerHTML = `<option>Seleccione una hora</option>`;
}

function agendarCita(url, url2) {
    let s = [];
    for (const xs of servicios) {
        s.push(
            {
                id: xs.id,
                hora: xs.idhora
            }
        );
    }

    let data = {
        Cita: {
            name: $("#nameCliente").val(),
            phone: $("#phoneCliente").val(),
            fecha: fechaSeleccionada,
            servicios: s
        }
    }

    $.post(url, data).done(function (resp) {
        alert(resp);
        location.href = url2;
    });
}