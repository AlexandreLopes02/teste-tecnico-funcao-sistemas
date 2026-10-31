using System.Linq;

namespace FI.AtividadeEntrevista
{
    public static class ValidadorCpf
    {
        public static bool Validar(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            if (!cpf.All(char.IsDigit))
                return false;

            if(cpf.All(c => c == cpf[0]))
                return false;

            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                int numero = int.Parse(cpf[i].ToString());
                soma += numero * (10 - i);
            }

            int resto = soma % 11;

            int primeiroDigito = resto < 2 ? 0 : 11 - resto;

            if (primeiroDigito != int.Parse(cpf[9].ToString()))
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                int numero = int.Parse(cpf[i].ToString());
                soma += numero * (11 - i);
            }

            resto = soma % 11;

            int segundoDigito = resto < 2 ? 0 : 11 - resto;

            if (segundoDigito != int.Parse(cpf[10].ToString()))
                return false;

            return true;
        }
    }
}
