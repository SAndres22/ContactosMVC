/*Script de busqueda */

$(document).ready(function () {
    // Función para filtrar los elementos de la lista
    function filtrarLista() {
        var valorBusqueda = $('#busqueda').val().toLowerCase(); // Obtener el valor de búsqueda y convertirlo a minúsculas
        $('.card').each(function () { // Iterar sobre cada tarjeta
            var textoTarjeta = $(this).text().toLowerCase(); // Obtener el texto de la tarjeta y convertirlo a minúsculas
            if (textoTarjeta.indexOf(valorBusqueda) === -1) { // Si el texto de la tarjeta no contiene el valor de búsqueda
                $(this).hide(); // Ocultar la tarjeta
            } else {
                $(this).show(); // Mostrar la tarjeta
            }
        });
    }

    // Evento de entrada en el campo de búsqueda
    $('#busqueda').on('input', function () {
        filtrarLista(); // Filtrar la lista cuando se ingresa texto en el campo de búsqueda
    });
});

/* Script de ver datos del usuario */

document.querySelectorAll('.verMas').forEach(item => {
    item.addEventListener('click', function (event) {
        event.preventDefault(); // Para evitar que el enlace redireccione a una nueva página

        // Obtener la tarjeta correspondiente al botón "Ver más" clicado
        const card = this.closest('.card');

        // Obtener la información del modelo de la tarjeta
        const nombre = card.querySelector('.card-title').textContent;
        const idContacto = card.querySelector('.card-text:nth-of-type(1)').textContent.replace('Contacto # ', '');
        const celular = card.querySelector('.card-text:nth-of-type(2)').textContent;
        const email = card.querySelector('.card-text:nth-of-type(3)').textContent;
        const fechaCreacion = card.querySelector('.card-text:nth-of-type(4)').textContent;

        // Obtener la URL de la imagen
        const fotografia = card.querySelector('.img-fluid').src;

        // Construir el HTML para el SweetAlert2
        const htmlContent = `
                    <div class="row">
                        <div class="col-md-4">
                            <img src="${fotografia}" class="img-fluid rounded-start" alt="fotografia de ${nombre}">
                        </div>
                        <div class="col-md-8">
                            <p><strong>Nombre:</strong> ${nombre}</p>
                            <p><strong>Contacto:</strong> ${idContacto}</p>
                            <p><strong>Celular:</strong> ${celular}</p>
                            <p><strong>Email:</strong> ${email}</p>
                            <p><strong>Fecha de creación:</strong> ${fechaCreacion}</p>
                        </div>
                    </div>
                `;

        Swal.fire({
            title: "<strong>Detalles del contacto</strong>",
            html: htmlContent,
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
            confirmButtonText: "Cerrar"
        });
    });
});

/* Script para eliminar un contacto */
function confirmarEliminar(id) {
    Swal.fire({
        title: '¿Estás seguro?',
        text: 'Esta acción eliminará el contacto permanentemente.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Sí, eliminar'
    }).then((result) => {
        if (result.isConfirmed) {
            // Si el usuario confirma, redirige al controlador para eliminar el contacto
            window.location.href = "https://localhost:7233/Home/borrarContacto/" + id;
        }
    });
}
