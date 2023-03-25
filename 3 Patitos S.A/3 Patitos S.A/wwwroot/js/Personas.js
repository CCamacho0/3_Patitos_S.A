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

    $("#btnDelPersona").click(function () {
        var Id_delete = $("#idDel_Persona").val();
        $.ajax({
            type: "POST",
            url: "/Persona/DeletePersona",
            data: {
                id: Id_delete
            },
            success: function () {
                $("#idDel_Persona").val(null);
                window.location.href = "/Persona/IndexPersonas";
            }
        })
    })

    $("#btnEditPersona").click(function () {
        var form = $("#EditForm").serialize();
        $.ajax({
            type: "POST",
            url: "/Personas/UpdatePersona",
            data: form,
            success: function () {
                window.location.href = "/Personas/IndexPersonas";
            }
        })
    })
});

var DelConfirm = function (id) {
    $("#idDel_Persona").val(id);
}

var EditPersona = function (id_psona) {
    $.ajax({
        type: "POST",
        url: "/Persona/GetPersona",
        data: { id: id_psona },
        success: function (persona) {
            console.log(persona);
            $("#EditModal").modal('show');
            $("#id_persona").val(persona.Id_Persona);
            $("#id_r").val(persona.Rol);
            $("#nombre").val(persona.Nombre_Persona);
            $("#dir").val(persona.Direccion);
            $("#telefono").val(persona.Telefono);
            $("#email").val(persona.Correo);
            $("#contrasena").val(persona.Contrasena);
            $("#id_e").val(persona.Id_Estado_Usuario);
            $("#id_c").val(persona.Id_Categoria);
        },
        error: function () {
            alert("Malio sal");
        }
    })
}