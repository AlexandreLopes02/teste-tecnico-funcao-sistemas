
$(document).ready(function () {
        $('#CPF').on('input', function () {
            var cpf = $(this).val().replace(/\D/g, '').substring(0, 11);

            cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
            cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
            cpf = cpf.replace(/(\d{3})(\d{1,2})$/, '$1-$2');

            $(this).val(cpf);
        });
        $('#formCadastro').submit(function (e) {
        e.preventDefault();
        $.ajax({
              url: urlPost,
              method: "POST",
              contentType: "application/json; charset=utf-8",
              data: JSON.stringify({
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
                  if (r.status === 400) {
                      ModalDialog("Ocorreu um erro", r.responseJSON);
                  } else {
                      ModalDialog("Ocorreu um erro",
                          "Não foi possível salvar o cliente.");
                  }
              },
              success: function (r) {
                  ModalDialog("Sucesso!", r);

                  $('#formCadastro')[0].reset();

                  beneficiarios = [];
                  atualizarListaBeneficiarios();
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
