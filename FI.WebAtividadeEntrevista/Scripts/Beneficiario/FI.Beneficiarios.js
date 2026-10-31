
var beneficiarios = [];
var indiceEdicao = -1;

$(document).ready(function () {

    $('#CpfBeneficiario').on('input', function () {
        var cpf = $(this).val().replace(/\D/g, '').substring(0, 11);

        cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
        cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2');
        cpf = cpf.replace(/(\d{3})(\d{1,2})$/, '$1-$2');

        $(this).val(cpf);
    });

    $('#btnIncluirBeneficiario').click(function () {
        var cpf = $('#CpfBeneficiario').val().replace(/\D/g, '');
        var nome = $('#NomeBeneficiario').val().trim();

        var cpfCliente = $('#CPF').val().replace(/\D/g, '');

        if (cpf === cpfCliente) {
            alert('O beneficiário não pode ter o mesmo CPF do cliente.');
            return;
        }

        if (!validarCpfBeneficiario(cpf)) {
            alert('CPF do beneficiário inválido.');
            return;
        }

        if (!nome || nome.length > 50) {
            alert('Informe um nome de até 50 caracteres.');
            return;
        }

        var duplicado = beneficiarios.some(function (item, indice) {
            return item.CPF === cpf && indice !== indiceEdicao;
        });

        if (duplicado) {
            alert('CPF já cadastrado para este cliente.');
            return;
        }

        var beneficiario = {
            Id: indiceEdicao >= 0 ? beneficiarios[indiceEdicao].Id : 0,
            CPF: cpf,
            Nome: nome
        };

        if (indiceEdicao >= 0) {
            beneficiarios[indiceEdicao] = beneficiario;
        } else {
            beneficiarios.push(beneficiario);
        }

        indiceEdicao = -1;
        $('#CpfBeneficiario').val('');
        $('#NomeBeneficiario').val('');
        $('#btnIncluirBeneficiario').text('Incluir');

        atualizarListaBeneficiarios();
    });

    $('#listaBeneficiarios').on('click', '.btn-alterar', function () {
        indiceEdicao = Number($(this).attr('data-indice'));

        var item = beneficiarios[indiceEdicao];

        $('#CpfBeneficiario').val(formatarCpfBeneficiario(item.CPF));
        $('#NomeBeneficiario').val(item.Nome);
        $('#btnIncluirBeneficiario').text('Salvar alteração');
    });

    $('#listaBeneficiarios').on('click', '.btn-excluir', function () {
        var indice = Number($(this).attr('data-indice'));

        if (!confirm('Deseja excluir este beneficiário?'))
            return;

        beneficiarios.splice(indice, 1);
        indiceEdicao = -1;

        $('#CpfBeneficiario').val('');
        $('#NomeBeneficiario').val('');
        $('#btnIncluirBeneficiario').text('Incluir');

        atualizarListaBeneficiarios();
    });
});

function formatarCpfBeneficiario(cpf) {
    cpf = (cpf || '').replace(/\D/g, '');

    return cpf.replace(/^(\d{3})(\d{3})(\d{3})(\d{2})$/,
        '$1.$2.$3-$4');
}

function atualizarListaBeneficiarios() {
    var corpo = $('#listaBeneficiarios');
    corpo.empty();

    beneficiarios.forEach(function (item, indice) {
        var linha = $('<tr>');

        $('<td>').text(formatarCpfBeneficiario(item.CPF))
            .appendTo(linha);

        $('<td>').text(item.Nome).appendTo(linha);

        var acoes = $('<td>');

        $('<button type="button" class="btn btn-xs btn-primary btn-alterar">')
            .text('Alterar')
            .attr('data-indice', indice)
            .appendTo(acoes);

        acoes.append(' ');

        $('<button type="button" class="btn btn-xs btn-danger btn-excluir">')
            .text('Excluir')
            .attr('data-indice', indice)
            .appendTo(acoes);

        linha.append(acoes);
        corpo.append(linha);
    });
}

function validarCpfBeneficiario(cpf) {
    cpf = (cpf || '').replace(/\D/g, '');

    if (cpf.length !== 11 || /^(\d)\1{10}$/.test(cpf))
        return false;

    var soma = 0;

    for (var i = 0; i < 9; i++)
        soma += Number(cpf[i]) * (10 - i);

    var resto = (soma * 10) % 11;
    if (resto === 10) resto = 0;

    if (resto !== Number(cpf[9]))
        return false;

    soma = 0;

    for (var j = 0; j < 10; j++)
        soma += Number(cpf[j]) * (11 - j);

    resto = (soma * 10) % 11;
    if (resto === 10) resto = 0;

    return resto === Number(cpf[10]);
}
