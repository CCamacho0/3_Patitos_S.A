
function CalPrecioTotal() {
    const cantidadInput = document.getElementById('_cant');
    const precioInput = document.getElementById('_precio');
    const precioTotalInput = document.getElementById('precio');

    const cantidad = parseFloat(cantidadInput.value);
    const precio = parseFloat(precioInput.value);
    const precioTotal = cantidad * precio;

    precioTotalInput.value = precioTotal;
}

const cantidadInput = document.getElementById('_cant');
cantidadInput.oninput = CalPrecioTotal;