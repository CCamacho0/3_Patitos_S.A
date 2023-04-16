$(document).ready(function () {
    $('#tblEUsuarios').DataTable({
        language: {
            "decimal": "",
            "emptyTable": "No hay Información",
            "info": "Mostrando _START_ A _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrando de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": "",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando",
            "search": "Buscar",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });
});

var DelConfirm = function (id) {
    $("#_ide").val(id);
}

var EditEUsuario = function (_id) {
    $.ajax({
        url: "Update",
        type: "GET",
        data: { id: _id },
        dataType: "json",
        success: function (eUsuarios) {
            $("#EditModal").modal('show');
            $("#_id").val(eUsuarios.id_estado_usuario);
            $("#_nombre").val(eUsuarios.nombre_estado);
        },
        error: function (msj) {
            alert(msj);
        }
    })
}