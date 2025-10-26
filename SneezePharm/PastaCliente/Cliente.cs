using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.PastaCliente
{
    public class Cliente
    {
        #region gets e sets
        public string Cpf { get; private set; }
        public string Nome { get; private set; }
        public DateOnly DataNascimento { get; private set; }
        public string Telefone { get; private set; }
        public DateOnly UltimaCompra { get; private set; }
        public DateOnly DataCadastro { get; private set; }
        public char Situacao { get; private set; }

        public void setSituacao(char situacao)
        {
            Situacao = situacao;
        }
        #endregion

        #region construtor
        public Cliente(string cpf, string nome, DateOnly dataNascismento,
            string telefone, DateOnly ultimaCompra, DateOnly dataCadastro, char situacao)
        {
            Cpf = cpf;
            Nome = nome;
            DataNascimento = dataNascismento;
            Telefone = telefone;
            UltimaCompra = DateOnly.FromDateTime(DateTime.Now);
            DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            Situacao = 'A';
        }
        #endregion

        #region toString
        public override string ToString() // ver de usar o trim o algo do tipo para exibir o nome
        {
            return $"\nCPF: {Cpf}\nNome: {Nome.Trim()}\nData de Nascimento: {DataNascimento}\nTelefone: {Telefone}\n" +
                   $"Ultima compra: {UltimaCompra}\nData de Cadastro: {DataCadastro}\nSituação: {Situacao}\n";
        }
        #endregion

        #region toFile
        public string ToFile()
        {
            return $"{Cpf}{Nome,-50}{DataNascimento.ToString("ddMMyyyy")}{Telefone}{UltimaCompra.ToString("ddMMyyyy")}{DataCadastro.ToString("ddMMyyyy")}{Situacao}";
        }
        #endregion

    }
}
