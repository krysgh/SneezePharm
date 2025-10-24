using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SneezePharm
{
    public class PrincipioAtivo
    {
        public string Id { get; private set; }
        public string Nome { get; private set; } // 20 caracteres
        public DateOnly UltimaCompra { get; private set; } 
        public DateOnly DataCadastro { get; private set; } 
        public char Situacao { get; private set; }

        private static List<string> idsUsados = new();

        public PrincipioAtivo(string nome, char situacao)
        {
            this.Id = VerificarExistenciaId();
            this.Nome = nome;
            this.UltimaCompra = DateOnly.FromDateTime(DateTime.Now);
            this.DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            this.Situacao = situacao;

        }

        public PrincipioAtivo(string id, string nome, DateOnly ultimaCompra, DateOnly dataCadastro, char situacao)
        {
            this.Id = id;
            this.Nome = nome;
            this.UltimaCompra = ultimaCompra;
            this.DataCadastro = dataCadastro;
            this.Situacao = situacao;
        }

        public string GerarId()
        {
            string prefixo = "AI";

            int numero1 = Random.Shared.Next(0, 10);
            int numero2 = Random.Shared.Next(0, 10);
            int numero3 = Random.Shared.Next(0, 10);
            int numero4 = Random.Shared.Next(0, 10);

            string numId = (numero1.ToString()) + (numero2.ToString()) + (numero3.ToString()) + (numero4.ToString());

            return prefixo + numId;
        }

        public string VerificarExistenciaId()
        {
            string numId;

            do
            {
                numId = GerarId();
            } while (idsUsados.Contains(numId));

            idsUsados.Add(numId);

            return numId;
        }

        public static bool VerificarSeAlfanumerico(string nome)
        {
            string padrao = "^[a-zA-Z0-9]+$";
            return Regex.IsMatch(nome, padrao);
        }


        public void SetSituacao(char situacao)
        {
            this.Situacao = situacao;
        }

        public void SetNome(string nome)
        {
            if(nome.Length > 20)
            {
                Console.WriteLine("Nome não pode ter mais de 20 caracteres");
            }
            else
            {
                this.Nome = nome;
            }
        }

        public override string ToString()
        {
            return $"Id: {Id}\nNome: {Nome}\nUltima Compra: {UltimaCompra:dd/MM/yyyy}\nData Cadastro: {DataCadastro:dd/MM/yyyy}\nSituação: {Situacao}";
        }

        public string ToFile()
        {
            string id = Id;
            id = id.PadRight(6);

            string nome = Nome;
            nome = nome.Length > 20 ? nome.Substring(0, 20) : nome.PadRight(20, ' ');

            string ultimaCompra = UltimaCompra.ToString("ddMMyyyy");

            string dataCadastro = DataCadastro.ToString("ddMMyyyy");



            return $"{id}{nome}{ultimaCompra}{dataCadastro}{Situacao}";

        }



    }
}
