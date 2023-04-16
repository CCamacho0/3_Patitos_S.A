$(document).ready(function () {
    $('#tblUbicacion').DataTable({
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
    $("#_idu").val(id);
}

var EditUbi = function (_id) {
    $.ajax({
        url: "Update",
        type: "GET",
        data: { id: _id },
        dataType: "json",
        success: function (ubicacion) {
            $("#EditModal").modal('show');
            $("#_id").val(ubicacion.id_Ubicacion);
            $("#_nombre").val(ubicacion.nombre_Ubicacion);
        },
        error: function (msj) {
            alert(msj);
        }
    })
}