using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Cliente
    /// </summary>
    internal class DaoCliente : AcessoDados
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal long Incluir(DML.Cliente cliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", cliente.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Sobrenome", cliente.Sobrenome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nacionalidade", cliente.Nacionalidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CEP", cliente.CEP));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Estado", cliente.Estado));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Cidade", cliente.Cidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Logradouro", cliente.Logradouro));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Email", cliente.Email));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Telefone", cliente.Telefone));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", cliente.CPF));

            DataSet ds = base.Consultar("FI_SP_IncClienteV2", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal DML.Cliente Consultar(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            DataSet ds = base.Consultar("FI_SP_ConsCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            return cli.FirstOrDefault();
        }

        internal bool VerificarExistencia(string CPF, long id = 0)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", id));

            DataSet ds = base.Consultar("FI_SP_VerificaCliente", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        internal List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("iniciarEm", iniciarEm));
            parametros.Add(new System.Data.SqlClient.SqlParameter("quantidade", quantidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("campoOrdenacao", campoOrdenacao));
            parametros.Add(new System.Data.SqlClient.SqlParameter("crescente", crescente));

            DataSet ds = base.Consultar("FI_SP_PesqCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return cli;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<DML.Cliente> Listar()
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", 0));

            DataSet ds = base.Consultar("FI_SP_ConsCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            return cli;
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Alterar(DML.Cliente cliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", cliente.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Sobrenome", cliente.Sobrenome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nacionalidade", cliente.Nacionalidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CEP", cliente.CEP));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Estado", cliente.Estado));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Cidade", cliente.Cidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Logradouro", cliente.Logradouro));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Email", cliente.Email));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Telefone", cliente.Telefone));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", cliente.Id));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", cliente.CPF));

            base.Executar("FI_SP_AltCliente", parametros);
        }


        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            base.Executar("FI_SP_DelCliente", parametros);
        }

        private List<DML.Cliente> Converter(DataSet ds)
        {
            List<DML.Cliente> lista = new List<DML.Cliente>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Cliente cli = new DML.Cliente();
                    cli.Id = row.Field<long>("Id");
                    cli.CPF = row.Field<string>("CPF");
                    cli.CEP = row.Field<string>("CEP");
                    cli.Cidade = row.Field<string>("Cidade");
                    cli.Email = row.Field<string>("Email");
                    cli.Estado = row.Field<string>("Estado");
                    cli.Logradouro = row.Field<string>("Logradouro");
                    cli.Nacionalidade = row.Field<string>("Nacionalidade");
                    cli.Nome = row.Field<string>("Nome");
                    cli.Sobrenome = row.Field<string>("Sobrenome");
                    cli.Telefone = row.Field<string>("Telefone");
                    lista.Add(cli);
                }
            }

            return lista;
        }


        internal long IncluirComBeneficiarios(DML.Cliente cliente)
        {
            string conexaoBanco = ConfigurationManager
                .ConnectionStrings["BancoDeDados"].ConnectionString;

            using (SqlConnection conexao = new SqlConnection(conexaoBanco))
            {
                conexao.Open();

                using (SqlTransaction transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        long idCliente;

                        using (SqlCommand comando = new SqlCommand(
                            "FI_SP_IncClienteV2", conexao, transacao))
                        {
                            comando.CommandType = CommandType.StoredProcedure;

                            comando.Parameters.AddWithValue("@Nome", cliente.Nome);
                            comando.Parameters.AddWithValue("@Sobrenome", cliente.Sobrenome);
                            comando.Parameters.AddWithValue("@Nacionalidade", cliente.Nacionalidade);
                            comando.Parameters.AddWithValue("@CEP", cliente.CEP);
                            comando.Parameters.AddWithValue("@Estado", cliente.Estado);
                            comando.Parameters.AddWithValue("@Cidade", cliente.Cidade);
                            comando.Parameters.AddWithValue("@Logradouro", cliente.Logradouro);
                            comando.Parameters.AddWithValue("@Email", cliente.Email);
                            comando.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                            comando.Parameters.AddWithValue("@CPF", cliente.CPF);

                            object resultado = comando.ExecuteScalar();

                            idCliente = Convert.ToInt64(resultado);
                        }

                        DaoBeneficiario daoBeneficiario = new DaoBeneficiario();

                        foreach (var beneficiario in cliente.Beneficiarios)
                        {
                            daoBeneficiario.Incluir(
                                beneficiario,
                                idCliente,
                                conexao,
                                transacao);
                        }

                        transacao.Commit();

                        return idCliente;
                    }
                    catch
                    {
                        transacao.Rollback();
                        throw;
                    }
                }
            }
        }



        internal void AlterarComBeneficiarios(DML.Cliente cliente)
        {
            string conexaoBanco = ConfigurationManager
                .ConnectionStrings["BancoDeDados"].ConnectionString;

            using (SqlConnection conexao = new SqlConnection(conexaoBanco))
            {
                conexao.Open();

                using (SqlTransaction transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand comando = new SqlCommand(
                            "FI_SP_AltCliente", conexao, transacao))
                        {
                            comando.CommandType = CommandType.StoredProcedure;

                            comando.Parameters.AddWithValue("@ID", cliente.Id);
                            comando.Parameters.AddWithValue("@Nome", cliente.Nome);
                            comando.Parameters.AddWithValue("@Sobrenome", cliente.Sobrenome);
                            comando.Parameters.AddWithValue("@Nacionalidade", cliente.Nacionalidade);
                            comando.Parameters.AddWithValue("@CEP", cliente.CEP);
                            comando.Parameters.AddWithValue("@Estado", cliente.Estado);
                            comando.Parameters.AddWithValue("@Cidade", cliente.Cidade);
                            comando.Parameters.AddWithValue("@Logradouro", cliente.Logradouro);
                            comando.Parameters.AddWithValue("@Email", cliente.Email);
                            comando.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                            comando.Parameters.AddWithValue("@CPF", cliente.CPF);

                            comando.ExecuteNonQuery();
                        }

                        DaoBeneficiario daoBeneficiario = new DaoBeneficiario();

                        daoBeneficiario.Sincronizar(
                            cliente.Beneficiarios,
                            cliente.Id,
                            conexao,
                            transacao);

                        transacao.Commit();
                    }
                    catch
                    {
                        transacao.Rollback();
                        throw;
                    }
                }
            }
        }

    }


}
