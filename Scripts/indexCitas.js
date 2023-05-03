
function cancelarCita(url) {
    swal({
        title: "¿Desea cancelar esta cita?",
        text: "Una vez cancelada una cita esta no puede ser modificada nuevamente.",
        icon: "warning",
        buttons: true,
        dagerMode: true,
    }).then((isOkay) => {
        if (isOkay != null) {
            document.location.href = url;
        }
    });
}

function facturarCita(url) {
    swal({
        title: "¿Desea facturar esta cita?",
        text: "Se cargaran los servicios en una factura nueva",
        icon: "warning",
        buttons: true,
        dagerMode: true,
    }).then((isOkay) => {
        if (isOkay != null) {
            document.location.href = url;
        }
    });
}