var DelConfirm = function (id) {
    $("#_ide").val(id);
}

var EditProducto = function (_id) {
    $.ajax({
        url: "Update",
        type: "GET",
        data: { id: _id },
        dataType: "json",
        success: function (producto) {
            $("#EditModal").modal('show');
            $("#_id").val(producto.iD_Producto);
            $("#_nombre").val(producto.nombre_producto);
            $("#_cant").val(producto.cantidad);
            $("#_e").val(producto.iD_Estado);
            $("#_precio").val(producto.precio);
            $("#_imagen").val(producto.imagen);
            $("#_detalle").val(producto.detalles);
            $("#_ubi").val(producto.id_Ubicacion);
        },
        error: function (msj) {
            alert(msj);
        }
    })
}

var DetailProducto = function (_id) {
    $.ajax({
        url: "Update",
        type: "GET",
        data: { id: _id },
        dataType: "json",
        success: function (producto) {
            $("#DetailModal").modal('show');
            $("#_id2").val(producto.iD_Producto);
            $("#_nombre2").val(producto.nombre_producto);
            $("#_cant2").val(producto.cantidad);
            $("#_e2").val(producto.iD_Estado);
            $("#_precio2").val(producto.precio);
            $("#_detalle2").val(producto.detalles);
            $("#_ubi2").val(producto.id_Ubicacion);
        },
        error: function (msj) {
            alert(msj);
        }
    })
}