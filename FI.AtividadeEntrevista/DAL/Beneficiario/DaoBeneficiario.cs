using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FI.AtividadeEntrevista.DML;


namespace FI.AtividadeEntrevista.DAL
{
    internal class DaoBeneficiario
    {
        internal void Incluir(
            Beneficiario beneficiario,
            long idCliente,
            SqlConnection conexao,
            SqlTransaction transacao)
        {
            string sql = @"INSERT INTO BENEFICIARIOS
                           (CPF, NOME, IDCLIENTE)
                           VALUES (@CPF, @NOME, @IDCLIENTE)";

            using (SqlCommand comando = new SqlCommand(
                sql, conexao, transacao))
            {
                comando.Parameters.Add("@CPF", SqlDbType.VarChar, 11)
                    .Value = beneficiario.CPF;

                comando.Parameters.Add("@NOME", SqlDbType.VarChar, 50)
                    .Value = beneficiario.Nome;

                comando.Parameters.Add("@IDCLIENTE", SqlDbType.BigInt)
                    .Value = idCliente;

                comando.ExecuteNonQuery();
            }
        }


        internal List<Beneficiario> ListarPorCliente(long idCliente)
        {
            List<Beneficiario> lista = new List<Beneficiario>();

            string conexaoBanco = System.Configuration.ConfigurationManager
                .ConnectionStrings["BancoDeDados"].ConnectionString;

            using (SqlConnection conexao = new SqlConnection(conexaoBanco))
            {
                string sql = @"SELECT ID, CPF, NOME, IDCLIENTE
                       FROM BENEFICIARIOS
                       WHERE IDCLIENTE = @IDCLIENTE
                       ORDER BY ID";

                using (SqlCommand comando = new SqlCommand(sql, conexao))
                {
                    comando.Parameters.Add("@IDCLIENTE", SqlDbType.BigInt)
                        .Value = idCliente;

                    conexao.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Beneficiario
                            {
                                Id = reader.GetInt64(0),
                                CPF = reader.GetString(1),
                                Nome = reader.GetString(2),
                                IdCliente = reader.GetInt64(3)
                            });
                        }
                    }
                }
            }

            return lista;
        }


        internal void Sincronizar(
            List<Beneficiario> beneficiarios,
            long idCliente,
            SqlConnection conexao,
            SqlTransaction transacao)
        {
            beneficiarios = beneficiarios ?? new List<Beneficiario>();

            // Exclui apenas beneficiários removidos da lista.
            var ids = beneficiarios
                .Where(b => b.Id > 0)
                .Select(b => b.Id)
                .ToList();

            string sqlExcluir = @"SELECT ID
                          FROM BENEFICIARIOS
                          WHERE IDCLIENTE = @IDCLIENTE";

            List<long> idsBanco = new List<long>();

            using (SqlCommand comando = new SqlCommand(
                sqlExcluir, conexao, transacao))
            {
                comando.Parameters.AddWithValue("@IDCLIENTE", idCliente);

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                        idsBanco.Add(reader.GetInt64(0));
                }
            }

            foreach (long id in idsBanco)
            {
                if (!ids.Contains(id))
                {
                    using (SqlCommand comando = new SqlCommand(
                        @"DELETE FROM BENEFICIARIOS
                  WHERE ID = @ID AND IDCLIENTE = @IDCLIENTE",
                        conexao, transacao))
                    {
                        comando.Parameters.AddWithValue("@ID", id);
                        comando.Parameters.AddWithValue("@IDCLIENTE", idCliente);
                        comando.ExecuteNonQuery();
                    }
                }
            }

            foreach (var beneficiario in beneficiarios)
            {
                if (beneficiario.Id == 0)
                {
                    Incluir(beneficiario, idCliente, conexao, transacao);
                }
                else
                {
                    using (SqlCommand comando = new SqlCommand(
                        @"UPDATE BENEFICIARIOS
                  SET CPF = @CPF, NOME = @NOME
                  WHERE ID = @ID AND IDCLIENTE = @IDCLIENTE",
                        conexao, transacao))
                    {
                        comando.Parameters.AddWithValue("@CPF", beneficiario.CPF);
                        comando.Parameters.AddWithValue("@NOME", beneficiario.Nome);
                        comando.Parameters.AddWithValue("@ID", beneficiario.Id);
                        comando.Parameters.AddWithValue("@IDCLIENTE", idCliente);

                        if (comando.ExecuteNonQuery() != 1)
                            throw new InvalidOperationException(
                                "Beneficiário não encontrado para este cliente.");
                    }
                }
            }
        }


    }
}
