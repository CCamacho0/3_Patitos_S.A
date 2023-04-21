var DelConfirm = function (id) {
    $("#_ide").val(id);
}

var EditPedido = function (_id) {
    $.ajax({
        url: "Update",
        type: "GET",
        data: { id: _id },
        dataType: "json",
        success: function (pedido) {
            $("#EditModal").modal('show');
            $("#_id").val(pedido.id_pedido);
            $("#_producto").val(pedido.iD_Producto);
            $("#_persona").val(pedido.iD_Persona);
            $("#_ePedido").val(pedido.iD_Estado_Pedido);
            $("#_cant").val(pedido.cantidad);
            $("#_precio").val(pedido.precio * pedido.cantidad);
            $("#_entrega").val(pedido.iD_Entrega);
        },
        error: function (msj) {
            alert(msj);
        }
    })
}

var Edit = function (_id) {
    $.ajax({
        url: "Pedidos/Update",
        type: "GET",
        data: { id: _id },
        dataType: "json",
        success: function (pedido) {
            $("#EditModal").modal('show');
            $("#id").val(pedido.id_pedido);
            $("#producto").val(pedido.iD_Producto);
            $("#persona").val(pedido.iD_Persona);
            $("#ePedido").val(pedido.iD_Estado_Pedido);
            $("#_precio_").val(pedido.precio * pedido.cantidad);
            $("#cant").val(pedido.cantidad);
            $("#precio").val(pedido.precio);
            $("#entrega").val(pedido.iD_Entrega);
        },
        error: function (msj) {
            alert(msj);
        }
    })
}

function CalPrecioTotal() {
    const cantidadInput = document.getElementById('cant');
    const precioInput = document.getElementById('precio');
    const precioTotalInput = document.getElementById('_precio_');

    const cantidad = parseFloat(cantidadInput.value);
    const precio = parseFloat(precioInput.value);
    const precioTotal = cantidad * precio;

    precioTotalInput.value = precioTotal;
}

const cantidadInput = document.getElementById('cant');
cantidadInput.oninput = CalPrecioTotal;

