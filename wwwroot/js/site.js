// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.getElementById("reingresoContrasena").addEventListener("input", validarReingreso);

function validarReingreso() {
    var contrasena = document.getElementById("contrasena").value;
    var reingresoContrasena = document.getElementById("reingresoContrasena").value;
    var mensajeSpan = document.querySelector("span[data-valmsg-for='UsContrasena']");

    if (contrasena === reingresoContrasena) {
        mensajeSpan.textContent = ""; // Borrar mensaje de error
    } else {
        mensajeSpan.textContent = "Las contraseñas no coinciden.";
    }
}
function limitarDni(element) {
    // Eliminar cualquier carácter que no sea un número
    element.value = element.value.replace(/[^0-9]/g, '');

    // Asegurarse de que no comience con 0
    if (element.value.length > 0 && element.value[0] === '0') {
        element.value = element.value.slice(1);
    }

    // Limitar la longitud a 8 caracteres
    if (element.value.length > 8) {
        element.value = element.value.slice(0, 8);
    }
}
function limitarTexto(element) {

    var inputValue = element.value;
    // Eliminar caracteres no permitidos
    var cleanedValue = inputValue.replace(/[^\sa-zA-ZáéíóúüÁÉÍÓÚÜñÑ]/g, '');

    // Asegurarse de que la primera letra sea mayúscula
    cleanedValue = cleanedValue.charAt(0).toUpperCase() + cleanedValue.slice(1);

    // Actualizar el valor en el campo
    element.value = cleanedValue;

    // Limitar la longitud a 50 caracteres
    if (element.value.length > 50) {
        element.value = element.value.slice(0, 50);
    }
}

function validarDomicilio(element) {
    var inputValue = element.value;
    // Eliminar caracteres no permitidos
    var cleanedValue = inputValue.replace(/[^A-Za-záéíóúüÁÉÍÓÚÜñÑ\s\d°/:;,.\-_\+()]/g, '');

    // Actualizar el valor en el campo
    element.value = cleanedValue;
}

function validarTelefono(element) {


    // Eliminar cualquier carácter que no sea un número
    element.value = element.value.replace(/[^0-9]/g, '');

    // Asegurarse de que no comience con 0
    if (element.value.length > 0 && element.value[0] === '0') {
        element.value = element.value.slice(1);
    }

    // Limitar la longitud a 10 caracteres
    if (element.value.length > 10) {
        element.value = element.value.slice(0, 10);
    }
}
function validarEmail(input) {
    var valor = input.value;
    // Usar una expresión regular para eliminar caracteres no permitidos
    var cleanedValue = valor.replace(/[^\w\s.@-]/g, '');
    input.value = cleanedValue;
}
function actualizarDatos() {
    $.ajax({
        url: '@Url.Action("GetLatestData", "Asistencias")', // Cambia la URL a tu acción que obtiene los datos actualizados
        success: function (data) {
            $('#ubicacionMsg').text(data.ubicacionMsg);
            setTimeout(actualizarDatos, 60000); // 60000 ms = 1 minuto
        },
        error: function () {
            console.log('Error al obtener datos actualizados');
        }
    });
}
$(document).ready(function () {
    actualizarDatos();
});

document.addEventListener('DOMContentLoaded', () => {
    const verificarBtn = document.getElementById('verificarBtn');
    const cargarBtn = document.getElementById('cargarBtn');
    const inputSalida = document.getElementById('inputSalida');

    verificarBtn.addEventListener('click', async () => {
        // Aquí iría tu lógica de verificación de ubicación

        // Luego de la verificación
        cargarBtn.disabled = false; // Habilitar el botón de Iniciar
        setTimeout(() => {
            inputSalida.disabled = false; // Habilitar el campo de Egreso
        }, 30 * 60 * 1000); // 30 minutos en milisegundos
    });

    cargarBtn.addEventListener('click', () => {
        // Aquí podrías realizar cualquier lógica de validación adicional antes de enviar el formulario
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const inputEgreso = document.getElementById("inputSalida");
    const finalizarBtn = document.getElementById("finalizarBtn");

    inputEgreso.addEventListener("input", function () {
        if ((new Date(inputEgreso.value) - new Date(asistencia.AsIngreso)) >= 30 * 60 * 1000) {
            finalizarBtn.removeAttribute("disabled");
        } else {
            finalizarBtn.setAttribute("disabled", "disabled");
        }
    });
});

