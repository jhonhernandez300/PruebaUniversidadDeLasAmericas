$(document).ready(function () {    

    $('#btnSiguiente').click(function () {
        var form = $('form');

        // Valida el formulario
        form.validate();

        if (form.valid()) {
            // Habilita y muestra la pestaña de información personal
            $('#personal-tab').removeClass('disabled');
            var personalTab = new bootstrap.Tab($('#personal-tab')[0]); // Asegúrate de que es un elemento DOM
            personalTab.show();

            // Enfoca el primer campo en la pestaña de información personal
            $('#personal').find('input:first').focus();

            // Habilita el botón "Create"
            $('#btnCreate').prop('disabled', false);
        }
    });
});
