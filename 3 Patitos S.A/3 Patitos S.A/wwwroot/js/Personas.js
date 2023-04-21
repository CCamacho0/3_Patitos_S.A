$(document).ready(function () {
    $('#tblPersonas').DataTable({
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
    $("#_idp").val(id);
}

var EditUsuario = function (_id) {
    $("#EditModalUser").modal('show');
}

var EditPersona = function (_id) {
    $.ajax({
        url: "Update",
        type: "GET",
        data: { id: _id },
        dataType: "json",
        success: function (persona) {
            $("#EditModal").modal('show');
            $("#id_persona").val(persona.id_Persona);
            $("#id_r").val(persona.id_Rol);
            $("#nombre").val(persona.nombre_Persona);
            $("#_fecha").val(persona.fecha_Nacimiento);
            $("#dir").val(persona.direccion);
            $("#telefono").val(persona.telefono);
            $("#email").val(persona.correo);
            $("#contrasena").val(persona.contrasena);
            $("#id_e").val(persona.id_Estado_Usuario);
            $("#id_c").val(persona.id_Categoria);
        },
        error: function (msj) {
            alert(msj);
        }
    })
}