# Teste Técnico — Função Sistemas

Projeto desenvolvido em ASP.NET MVC como parte do processo seletivo da Função Sistemas.

## Tecnologias utilizadas

- C# / .NET Framework 4.8
- ASP.NET MVC
- SQL Server LocalDB
- ADO.NET e Stored Procedures
- JavaScript, jQuery e Bootstrap

## Funcionalidades implementadas

**Etapa 1 — Validação de CPF**
- Inclusão do CPF no cadastro e na alteração de clientes.
- Validação dos dígitos verificadores do CPF.
- Bloqueio de CPFs duplicados entre clientes.

**Etapa 2 — Gerenciamento de beneficiários**
- Cadastro de beneficiários vinculados a clientes.
- Inclusão, alteração e exclusão por meio de uma janela modal.
- Validação de CPF e prevenção de duplicidade por cliente.
- Persistência das alterações ao salvar o formulário principal.
- Utilização de transações SQL para garantir a consistência dos dados.

## Estrutura do projeto

O projeto mantém a separação em camadas:

- **MVC:** interface e controle das requisições.
- **BLL:** validações e regras de negócio.
- **DAL:** acesso e persistência dos dados.
- **DML:** modelos de dados.

## Execução

1. Abrir a solução no Visual Studio.
2. Restaurar os pacotes NuGet.
3. Utilizar o banco SQL Server LocalDB fornecido com o projeto original.
4. Aplicar as alterações necessárias na estrutura do banco e as procedures SQL disponíveis no projeto.
5. Compilar e executar a aplicação.

**Observação:** os arquivos locais do banco de dados (`.mdf` e `.ldf`) não são versionados no repositório.
