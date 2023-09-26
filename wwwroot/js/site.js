// Documentación de código

// Función para limitar la entrada de DNI
function limitarDni(element) {
    element.value = element.value.replace(/[^0-9]/g, '');
    if (element.value.length > 0 && element.value[0] === '0') {
        element.value = element.value.slice(1);
    }
    if (element.value.length > 8) {
        element.value = element.value.slice(0, 8);
    }
}

// Función para limitar la entrada de texto
function limitarTexto(element) {
    var inputValue = element.value;
    var cleanedValue = inputValue.replace(/[^\sa-zA-ZáéíóúüÁÉÍÓÚÜñÑ]/g, '');
    cleanedValue = cleanedValue.charAt(0).toUpperCase() + cleanedValue.slice(1);
    element.value = cleanedValue;
    if (element.value.length > 50) {
        element.value = element.value.slice(0, 50);
    }
}

// Función para validar la entrada de domicilio
function validarDomicilio(element) {
    var inputValue = element.value;
    var cleanedValue = inputValue.replace(/[^A-Za-záéíóúüÁÉÍÓÚÜñÑ\s\d°/:;,.\-_\+()]/g, '');
    element.value = cleanedValue;
}

// Función para limitar la entrada de teléfono
function validarTelefono(element) {
    element.value = element.value.replace(/[^0-9]/g, '');
    if (element.value.length > 0 && element.value[0] === '0') {
        element.value = element.value.slice(1);
    }
    if (element.value.length > 10) {
        element.value = element.value.slice(0, 10);
    }
}

// Función para validar la entrada de correo electrónico
function validarEmail(input) {
    var valor = input.value;
    var cleanedValue = valor.replace(/[^\w\s.@-]/g, '');
    input.value = cleanedValue;
}

// Función para validar contraseña de reingreso
function validarReingreso() {
    var contrasena = document.getElementById("contrasena");
    var reingresoContrasena = document.getElementById("reingresoContrasena");
    var mensajeSpan = document.querySelector("span[data-valmsg-for='UsContrasena']");

    if (contrasena && reingresoContrasena && mensajeSpan) {
        if (contrasena.value === reingresoContrasena.value) {
            mensajeSpan.textContent = ""; // Borrar mensaje de error
        } else {
            mensajeSpan.textContent = "Las contraseñas no coinciden.";
        }
    }
}

// Manejo de eventos al cargar la página
document.addEventListener('DOMContentLoaded', function () {
    const verificarBtn = document.getElementById('verificarBtn');
    const cargarBtn = document.getElementById('cargarBtn');
    const inputSalida = document.getElementById('inputSalida');

    verificarBtn.addEventListener('click', async () => {
        // Lógica de verificación de ubicación

        // Habilitar el botón de Iniciar después de la verificación
        cargarBtn.disabled = false;

        // Habilitar el campo de Egreso después de 30 minutos
        setTimeout(() => {
            inputSalida.disabled = false;
        }, 30 * 60 * 1000);
    });

    cargarBtn.addEventListener('click', () => {
        // Lógica de validación adicional antes de enviar el formulario
    });

    const inputEgreso = document.getElementById("inputSalida");
    const finalizarBtn = document.getElementById("finalizarBtn");

    inputEgreso.addEventListener("input", function () {
        // Lógica para habilitar el botón de Finalizar
        if ((new Date(inputEgreso.value) - new Date(asistencia.AsIngreso)) >= 30 * 60 * 1000) {
            finalizarBtn.removeAttribute("disabled");
        } else {
            finalizarBtn.setAttribute("disabled", "disabled");
        }
    });

    const checkboxes = document.querySelectorAll('.dias-checkbox');
    const seDiaInput = document.getElementById('seDia');

    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function () {
            // Lógica para actualizar el valor de seDiaInput
            const selectedDays = Array.from(checkboxes)
                .filter(checkbox => checkbox.checked)
                .map(checkbox => checkbox.value);

            seDiaInput.value = selectedDays.join(', ');
        });
    });
});

// Manejo de eventos al cargar la página
$(document).ready(function () {
    // Evento que se dispara cuando cambia la provincia seleccionada
    $("#provinciaSelect").change(function () {
        var provinciaId = $(this).val();

        // Ocultar o mostrar el campo de localidad según si se seleccionó una provincia
        if (provinciaId !== "") {
            $("#localidadDiv").show();
        } else {
            $("#localidadDiv").hide();
        }

        // Realizar una solicitud AJAX para cargar las localidades solo si se seleccionó una provincia
        if (provinciaId !== "") {
            $.ajax({
                url: "/Usuarios/GetLocalidadesByProvincia", // Ruta a tu acción en el controlador Usuarios
                type: "GET",
                data: { provinciaId: provinciaId },
                success: function (data) {
                    // Limpiar el select de localidades
                    $("#localidadSelect").empty();
                    $("#localidadSelect").append('<option value="">Seleccione una localidad</option>');

                    // Llenar el select de localidades con los datos recibidos
                    $.each(data, function (i, localidad) {
                        $("#localidadSelect").append('<option value="' + localidad.value + '">' + localidad.text + '</option>');
                    });
                }
            });
        }
    });

    // Disparar el evento 'change' al cargar la página para asegurar que las localidades se oculten inicialmente
    $("#provinciaSelect").trigger("change");
});

// Función para actualizar datos
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

// Evento para actualizar datos al cargar la página
$(document).ready(function () {
    actualizarDatos();
});
