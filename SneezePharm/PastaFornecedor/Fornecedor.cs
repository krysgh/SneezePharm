using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.PastaFornecedor
{
    public class Fornecedor
    {
        public string Cnpj { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Pais { get; private set; }
        public DateOnly DataAbertura { get; private set; }
        public DateOnly UltimoFornecimento { get; private set; }
        public DateOnly DataCadastro { get; private set; } = DateOnly.FromDateTime(DateTime.Now);
        public char Situacao { get; private set; } = 'A';

        public Fornecedor(string cnpj,
            string razaoSocial,
            string pais,
            DateOnly dataAbertura,
            DateOnly ultimoFornecimento
            )
        {
            Cnpj = cnpj;
            RazaoSocial = razaoSocial;
            Pais = pais;
            DataAbertura = dataAbertura;
            UltimoFornecimento = ultimoFornecimento;
        }

        public Fornecedor(string cnpj,
            string razaoSocial,
            string pais,
            string dataAbertura,
            string ultimoFornecimento,
            string dataCadastro,
            string situacao
            )
        {
            Cnpj = cnpj;
            RazaoSocial = razaoSocial.Trim();
            Pais = pais.Trim();
            DataAbertura = DateOnly.ParseExact(dataAbertura, "ddMMyyyy");
            UltimoFornecimento = DateOnly.ParseExact(ultimoFornecimento, "ddMMyyyy"); ;
            DataCadastro = DateOnly.ParseExact(dataCadastro, "ddMMyyyy"); ;
            Situacao = char.Parse(situacao);
        }

        // Altera a situação do fornecedor de 'A' para 'I' e vice-versa
        public void AlterarSituacao()
        {
            if (Situacao == 'A')
            {
                Situacao = 'I';
            }
            else
            {
                Situacao = 'A';
            }
        }

        // Valida CNPJ por tamanho, por dígito verificador, e se tiver todos os números iguais
        public static bool ValidarCNPJ(string cnpj)
        {
            if (cnpj.All(char.IsDigit))
            {
                return false;
            }

            int[] cnpjNumeros = cnpj.ToCharArray().Select(c => (int)char.GetNumericValue(c)).ToArray();
            if (cnpjNumeros.Length != 14)
            {
                return false;
            }
            if (cnpjNumeros.Distinct().Count() == 1)
            {
                return false;
            }


            int calculo = cnpjNumeros[0] * 6 + cnpjNumeros[1] * 7 + cnpjNumeros[2] * 8 + cnpjNumeros[3] * 9 + cnpjNumeros[4] * 2
                + cnpjNumeros[5] * 3 + cnpjNumeros[6] * 4 + cnpjNumeros[7] * 5 + cnpjNumeros[8] * 6 + cnpjNumeros[9] * 7
                + cnpjNumeros[10] * 8 + cnpjNumeros[11] * 9;


            calculo = calculo % 11;
            if (calculo == 10)
                calculo = 0;

            if (calculo != cnpjNumeros[12])
            {
                return false;
            }

            calculo = cnpjNumeros[0] * 5 + cnpjNumeros[1] * 6 + cnpjNumeros[2] * 7 + cnpjNumeros[3] * 8 + cnpjNumeros[4] * 9
                + cnpjNumeros[5] * 2 + cnpjNumeros[6] * 3 + cnpjNumeros[7] * 4 + cnpjNumeros[8] * 5 + cnpjNumeros[9] * 6
                + cnpjNumeros[10] * 7 + cnpjNumeros[11] * 8 + cnpjNumeros[12] * 9;

            calculo = calculo % 11;
            if (calculo == 10)
                calculo = 0;

            if (calculo != cnpjNumeros[13])
            {
                return false;
            }

            return true;
        }

        // Valida a data de abertura, retorna true se tiver pelo menos 2 anos; senão, retorna false
        public static bool ValidarDataAbertura(DateOnly data)
        {
            data = data.AddYears(2);
            if (data > DateOnly.FromDateTime(DateTime.Now))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override string ToString()
        {
            return $"Razão Social: {RazaoSocial}\n" +
                $"CNPJ: {Cnpj}\n" +
                $"País: {Pais}\n" +
                $"Data de Abertura: {DataAbertura}\n" +
                $"Data do Último Fornecimento: {UltimoFornecimento}\n" +
                $"Data de Cadastro: {DataCadastro}\n" +
                $"Situação: " + (Situacao == 'A' ? "Ativo" : "Inativo");
        }

        // Retorna todos os dados do fornecedor num string só para armazenar em arquivo
        public string ToFile()
        {
            return $"{Cnpj}{RazaoSocial,-50}{Pais,-20}" +
                DataAbertura.ToString().Replace("/", "") +
                UltimoFornecimento.ToString().Replace("/", "") +
                DataCadastro.ToString().Replace("/", "") +
                Situacao;
            ;
        }
    }
}
