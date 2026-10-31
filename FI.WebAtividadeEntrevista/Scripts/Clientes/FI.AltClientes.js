
$(document).ready(function () {
    if (obj) {
        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #CPF').val(obj.CPF);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Telefone').val(obj.Telefone);
    }

    beneficiarios = obj && obj.Beneficiarios ? obj.Beneficiarios : [];
    atualizarListaBeneficiarios();

    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: urlPost,
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                Id: obj.Id,
                Nome: $('#Nome').val(),
                Sobrenome: $('#Sobrenome').val(),
                CPF: $('#CPF').val(),
                CEP: $('#CEP').val(),
                Email: $('#Email').val(),
                Nacionalidade: $('#Nacionalidade').val(),
                Estado: $('#Estado').val(),
                Cidade: $('#Cidade').val(),
                Logradouro: $('#Logradouro').val(),
                Telefone: $('#Telefone').val(),
                Beneficiarios: beneficiarios
            }),
            error: function (r) {
                if (r.status === 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else
                    ModalDialog("Ocorreu um erro",
                        "Não foi possível alterar o cliente.");
            },
            success: function (r) {
                ModalDialog("Sucesso!", r);
                window.location.href = urlRetorno;
            }
        });

    })
    
})

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}
