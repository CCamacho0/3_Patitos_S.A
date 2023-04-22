$(document).ready(function () {
    $('#tblCategoria').DataTable({
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

var DeleteCat = function (id) {
    $("#_idc").val(id);
}

var EditCategoria = function (_id) {
    $.ajax({
        url: "Update",
        type: "GET",
        data: { id: _id },
        dataType: "json",
        success: function (cat) {
            $("#EditModal").modal('show');
            $("#_Id").val(cat.id_categoria);
            $("#_Nombre").val(cat.nombre_categoria);
            $("#_des").val(cat.descuento);
        },
        error: function (msj) {
            alert(msj);
        }
    })
}