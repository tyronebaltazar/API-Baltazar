var basePath = 'https://localhost:44303/api/'
function fnArmarTabla(categorias) {
    var html = '';

    for (var i = 0; i < categorias.length; i++) {
        var item = categorias[i];
        html += `
        <tr>
            <td>${i + 1}</td>
            <td>${item.CodCategoria}</td>
            <td>${item.Nombre}</td>
            <td>${item.Estado}</td>
        </tr>`;
    }

    $('#tbody').html(html);
}
function fnMostrarMensajeError(mensaje) {
    alert(mensaje)
}

$.ajax({
    type: 'GET',
    url: basePath + 'Categoria'
}).done(function (result) {
    switch (result.CodigoRespuesta) {
        case '01':
            fnArmarTabla(result.Categorias)
            break;
        case '99':
            fnMostrarMensajeError(result.NombreRespuesta)
            break;
    }
}).fail(function (jqXHR, textStatus, errorThrown) {
    console.log("The following error occured: " + textStatus + " " + errorThrown);
});