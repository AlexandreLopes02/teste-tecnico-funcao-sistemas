using System;
using System.Collections.Generic;
using System.Linq;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        public void Validar(List<DML.Beneficiario> beneficiarios, string cpfCliente)
        {
            if (beneficiarios == null)
                return;

            var cpfs = new HashSet<string>();

            foreach (var beneficiario in beneficiarios)
            {
                beneficiario.CPF = beneficiario.CPF?
                    .Replace(".", "")
                    .Replace("-", "");


                if (!ValidadorCpf.Validar(beneficiario.CPF))
                    throw new ArgumentException(
                        "CPF inválido para o beneficiário.");

                if (string.IsNullOrWhiteSpace(beneficiario.Nome))
                    throw new ArgumentException(
                        "O nome do beneficiário é obrigatório.");

                if (beneficiario.Nome.Length > 50)
                    throw new ArgumentException(
                        "O nome do beneficiário deve ter até 50 caracteres.");

                if (!cpfs.Add(beneficiario.CPF))
                    throw new ArgumentException(
                        "CPF de beneficiário duplicado para este cliente.");

                if (beneficiario.CPF == cpfCliente)
                {
                    throw new ArgumentException(
                        "O beneficiário não pode ter o mesmo CPF do cliente.");
                }
            }
        }

        public List<DML.Beneficiario> ListarPorCliente(long idCliente)
        {
            DAL.DaoBeneficiario dao = new DAL.DaoBeneficiario();
            return dao.ListarPorCliente(idCliente);
        }
    }
}
