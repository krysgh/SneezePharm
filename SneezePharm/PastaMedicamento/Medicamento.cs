using SneezePharm.Menu;
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

        // lista para não ter dois medicamentos com o mesmo codigo
        List<string> codigosUsados = new List<string>();

        // construtor para criar medicamento do zero, que não existe no sistema
        public Medicamento(string nome, char cataegoria, decimal valorVenda, char situacao)
        {
            CDB = VerificarExistencia();//gera o cdb chamando a função
            Nome = nome;
            Categoria = cataegoria;
            ValorVenda = valorVenda;
            UltimaVenda = DateOnly.FromDateTime(DateTime.Now);//define como data atual
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);//define como data atual
            Situacao = situacao;
        }

        //criar medicamento a partir de dados que ja existem, lendo do arquivo
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

        // atualiza a data da ultima venda para a data de hoje
        public void AlterarUltimaVenda()
        {
            UltimaVenda = DateOnly.FromDateTime(DateTime.Now);
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
        public void SetUltimaVenda(DateOnly ultimaVenda)
        {
            this.UltimaVenda = ultimaVenda;
        }
        public override string ToString()
        {
            return $"CDB: {CDB}\nNome: {Nome}\nCategoria: {Categoria}\nValorVenda: {ValorVenda}\nUltima venda: {UltimaVenda:dd/MM/yyyy}\nData cadastro: {DataCadastro:dd/MM/yyy}\nSituacao: {Situacao}";
        }

        public string ToFile()
        {

            string nome = Nome;
            // se o nome tiver mais de 40 caracteres, ele corta para os primeiros 40 (Substring(0,40)), se tiver menos de 40, ele preenche com espaços à direita
            nome = nome.Length > 40 ? nome.Substring(0, 40) : nome.PadRight(40, ' ');

            //converte as datas para sem sepadadores
            string ultimaVenda = UltimaVenda.ToString("ddMMyyyy");

            string dataCadastro = DataCadastro.ToString("ddMMyyyy");

            // converte o decimal para string com 2 casas decimais
            string valorVenda = ValorVenda.ToString("F2");
            //adiciona espaços à esquerda até que a string tenha 7 caracteres.
            valorVenda = valorVenda.PadLeft(7);


            return $"{CDB}{nome}{Categoria}{valorVenda}{ultimaVenda}{dataCadastro}{Situacao}";
        }

        public string GerarCDB()
        {
            string prefixoFixo = "78912345";

            // gera 4 numeros aleatorios
            int numero1 = Random.Shared.Next(0, 10);
            int numero2 = Random.Shared.Next(0, 10);
            int numero3 = Random.Shared.Next(0, 10);
            int numero4 = Random.Shared.Next(0, 10);

            //concatena os 4 numeros e joga na string produto
            string produto = numero1.ToString() + numero2.ToString() + numero3.ToString() + numero4.ToString();

            // junta o prefixo com produto
            string codigoParcial = prefixoFixo + produto;

            // aqui calcula o digito verificador
            int somaPares = 0;
            int somaImpares = 0;

            // for percore cada caractere da string codigoParcial
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
            int resultado = somaImpares + (somaPares * 3);
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

            // gera um codigo e repete enquanto ele estiver na lista de usados
            do
            {
                codigo = GerarCDB();
            } while (codigosUsados.Contains(codigo));

            //depois de gerar um codigo unico ele é adicionado na lista
            codigosUsados.Add(codigo);

            return codigo;
        }

        // metodo para garantir que o nome só tenha letras e numeros
        public static bool VerificarSeAlfanumericoMed(string nome)
        {
            // recebe um padrão regex para validar
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

        // metodo para validar valor da venda
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