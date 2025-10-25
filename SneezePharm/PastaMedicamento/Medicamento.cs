using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SneezePharm.PastaMedicamento
{
    public class Medicamento
    {
        public string CDB { get; private set; }
        public string Nome { get; private set; }
        public char Categoria { get; private set; }
        public decimal ValorVenda { get; private set; }
        public DateOnly UltimaVenda { get; private set; } 
        public DateOnly DataCadastro { get; private set; } 
        public char Situacao { get; private set; }

        List<string> codigosUsados = new List<string>();

        public Medicamento(string nome, char cataegoria, decimal valorVenda, char situacao)
        {
            CDB = VerificarExistencia();
            Nome = nome;
            Categoria = cataegoria;
            ValorVenda = valorVenda;
            UltimaVenda = DateOnly.FromDateTime(DateTime.Now);
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = situacao;
        }

        public Medicamento(string cdb, string nome, char categoria, decimal valorVenda, DateOnly ultimaVenda, DateOnly dataCadastro, char situacao)
        {
            CDB = cdb;
            Nome = nome;
            Categoria = categoria;
            ValorVenda = valorVenda;
            UltimaVenda = ultimaVenda;
            DataCadastro = dataCadastro;
            Situacao = situacao;
        }

        public void SetSituacao(char situacao)
        {
            Situacao = situacao;
        } 
        public void SetCategoria(char categoria)
        {
            Categoria = categoria;
        }
        public void SetNome(string nome)
        {
            if(nome.Length > 40)
            {
                Console.WriteLine("Nome não pode ter mais que 40 caracteres");
            }
            else
            {
                Nome = nome;
            }
        }
        public void SetValorVenda(decimal valorVenda)
        {
            ValorVenda = valorVenda;
        }
        public override string ToString()
        {
            return $"CDB: {CDB}\nNome: {Nome}\nCategoria: {Categoria}\nValorVenda: {ValorVenda}\nUltima venda: {UltimaVenda:dd/MM/yyyy}\nData cadastro: {DataCadastro:dd/MM/yyy}\nSituacao: {Situacao}";
        }

        public string ToFile()
        {

            string nome = Nome;
            nome = nome.Length > 40 ? nome.Substring(0, 40) : nome.PadRight(40, ' ');

            string ultimaVenda = UltimaVenda.ToString("ddMMyyyy");

            string dataCadastro = DataCadastro.ToString("ddMMyyyy");

            string valorVenda = ValorVenda.ToString("F2");
            valorVenda = valorVenda.PadLeft(7);


            return $"{CDB}{nome}{Categoria}{valorVenda}{ultimaVenda}{dataCadastro}{Situacao}";
        }

        public string GerarCDB()
        {
            string prefixoFixo = "78912345";

            int numero1 = Random.Shared.Next(0, 10);
            int numero2 = Random.Shared.Next(0, 10);
            int numero3 = Random.Shared.Next(0, 10);
            int numero4 = Random.Shared.Next(0, 10);

            string produto = numero1.ToString() + numero2.ToString() + numero3.ToString() + numero4.ToString();

            string codigoParcial = prefixoFixo + produto;

            int somaPares = 0;
            int somaImpares = 0;

            for (int i = 0; i < codigoParcial.Length; i++)
            {
                int digito = int.Parse(codigoParcial[i].ToString());

                if (i % 2 == 0)
                {
                    somaImpares += digito;
                }
                else
                {
                    somaPares += digito;
                }
            }
            int resultado = somaImpares + somaPares * 3;
            int resto = resultado % 10;
            int digitoVerificador = 10 - resto;

            if (digitoVerificador == 10)
            {
                digitoVerificador = 0;
            }

            string codigoFinal = codigoParcial + digitoVerificador.ToString();

            return codigoFinal;

        }

        public string VerificarExistencia()
        {
            string codigo;

            do
            {
                codigo = GerarCDB();
            } while (codigosUsados.Contains(codigo));

            codigosUsados.Add(codigo);

            return codigo;
        }

        public static bool VerificarSeAlfanumericoMed(string nome)
        {
            string padraoAlfanumerico = "^[a-zA-Z0-9]+$";

            if (Regex.IsMatch(nome, padraoAlfanumerico))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool VerificarValorVenda(decimal valorVenda)
        {
            if(valorVenda > 0 && valorVenda <= 9999.99m)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}