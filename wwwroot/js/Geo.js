// Obtener las coordenadas del centro desde los atributos de datos
const centroLatitud = parseFloat(document.getElementById('centroLatitud').getAttribute('data-latitud'));
const centroLongitud = parseFloat(document.getElementById('centroLongitud').getAttribute('data-longitud'));

// Radio en metros
const radioEnMetros = 50;

// Función para verificar si una ubicación está dentro del radio alrededor del centro
function ubicacionDentroDelRadio(latitud, longitud) {
    const distancia = calcularDistanciaEnMetros(latitud, longitud, centroLatitud, centroLongitud);
    return distancia <= radioEnMetros;
}

// Función para calcular la distancia entre dos puntos en la superficie de la Tierra
function calcularDistanciaEnMetros(latitud1, longitud1, latitud2, longitud2) {
    const radioTierra = 6371000; // Radio medio de la Tierra en metros
    const deltaLatitud = toRad(latitud2 - latitud1);
    const deltaLongitud = toRad(longitud2 - longitud1);
    const a = Math.sin(deltaLatitud / 2) * Math.sin(deltaLatitud / 2) +
        Math.cos(toRad(latitud1)) * Math.cos(toRad(latitud2)) *
        Math.sin(deltaLongitud / 2) * Math.sin(deltaLongitud / 2);
    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    return radioTierra * c;
}

// Función auxiliar para convertir grados a radianes
function toRad(grados) {
    return grados * (Math.PI / 180);
}

// Obtener referencias a los botones/checklist
const verificarBtn = document.getElementById('verificarBtn');
const selectEspacio = document.getElementById('selectEspacio')
const presenteCheck = document.getElementById('presenteCheck')
const inputIngreso = document.getElementById('inputIngreso');
//const inputSalida = document.getElementById('inputSalida');
const ubicacionMsg = document.getElementById('ubicacionMsg');
const cargarBtn = document.getElementById('cargarBtn');
const finalizarBtn = document.getElementById('finalizarBtn')

// Función para manejar el clic en el botón "Marcar asistencia"
verificarBtn.addEventListener('click', () => {
    // Obtener las coordenadas de la ubicación actual del usuario
    if (navigator.geolocation && inputIngreso) {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                const latitud = position.coords.latitude;
                const longitud = position.coords.longitude;

                //const ubicacionActual = document.getElementById('ubicacionActual');
                //ubicacionMsg.innerHTML = `Ubicación actual: Latitud ${latitud}, Longitud ${longitud}`;

                //const resultado = document.getElementById('resultado');
                const estaDentroDelRadio = ubicacionDentroDelRadio(latitud, longitud);

                //// Mostrar la distancia al radio
                //const distanciaAlRadio = calcularDistanciaEnMetros(latitud, longitud, centroLatitud, centroLongitud);
                //resultado.innerHTML = `Estás a ${distanciaAlRadio.toFixed(2)} metros de tu lugar de trabajo`;
                                
                const ingresoFecha = new Date(inputIngreso.value);
                const tiempoLimite = new Date(ingresoFecha.getTime() + 30 * 60 * 1000); // Sumar 30 minutos

                if (estaDentroDelRadio) {
                    ubicacionMsg.classList.add('text-danger')
                    ubicacionMsg.innerHTML = 'Estás dentro de Fundación Mazzieri';
                    ubicacionMsg.classList.remove('text-danger')
                    selectEspacio.disabled = false
                    cargarBtn.disabled = false;
                    // Comprobar si se encuentra en el rango de 30 minutos mínimos desde su ingreso.
                    if (new Date() >= tiempoLimite) {
                        finalizarBtn.disabled = false;                        
                        if (cargarBtn == null) {
                            presenteCheck.disabled = false;
                            presenteCheck.checked = true;
                            finalizarBtn.disabled = false;
                            selectEspacio.disabled = true
                        } else {
                            selectEspacio.disabled = true
                        }                       
                    /*marcarSalidaBtn.disabled = false;*/
                    }                    
                }
                else {                    
                    ubicacionMsg.innerHTML = 'Estas demasiado lejos de Fundacion Mazzieri!';
                }
            },
            (error) => {
                //ubicacionActual.innerHTML = '';
                ubicacionMsg.innerHTML = `Error al obtener la ubicación: ${error.message}`;
            }
        );
    } else {
        //ubicacionActual.innerHTML = '';
        ubicacionMsg.innerHTML = 'Error de Verificación, recuerdo que no puede finalizar hasta tanto no hayan pasado 30 minutos desde el ingreso.';
    }
});

//// Función para manejar el clic en el botón "Marcar salida"
//marcarSalidaBtn.addEventListener('click', () => {
//    presenteCheck.disabled = false;
//    marcarSalidaBtn.disabled = true;
//    const resultado = document.getElementById('resultado');
//    resultado.innerHTML = 'Has marcado la salida.';

    // Agregar acá cualquier otra lógica que necesites para el proceso de marcar salida.
//});
