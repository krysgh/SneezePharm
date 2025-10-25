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

        #region Método Incluir Cliente
        
        #endregion

        #region validação cpf
        public static bool ValidarCpf(string cpf)
        {
            #region validação de somente numeros
            if (cpf.All(char.IsDigit))
            {
                return true;
            }
            else
            {
                Console.Write("Informe apenas números - ");
            }
            #endregion

            #region // regiao de verificação de tamanho e números iguais
            cpf.ToCharArray();

            if (cpf.Length != 11)
            {
                Console.WriteLine("Tamanho de CPF insuficiente!\n");
                return false;
            }

            if (cpf.Distinct().Count() == 1)
            {
                Console.WriteLine("CPF não pode ter todos os digitos iguais!\n");
                return false;
            }

            int[] numeros = new int[cpf.Length];
            for (int i = 0; i < cpf.Length; i++)
            {
                numeros[i] = cpf[i] - '0';
            }
            #endregion

            #region // regiao do digito verificador 1
            int[] multiplicacao1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma = soma + numeros[i] * multiplicacao1[i];
            }

            int resto = soma * 10 % 11;
            if (resto == 10)
            {
                resto = 0;
            }

            if (resto != numeros[9])
            {
                Console.WriteLine("CPF inválido, tente novamente!\n");
                return false;
            }
            #endregion

            #region // regiao do digito verificador 2
            soma = 0;
            int[] multiplicacao2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            for (int i = 0; i < 10; i++)
            {
                soma = soma + numeros[i] * multiplicacao2[i];
            }

            int resto2 = soma * 10 % 11;
            if (resto2 == 10)
            {
                resto2 = 0;
            }

            if (resto2 != numeros[10])
            {
                Console.WriteLine("CPF inválido, tente novamente!\n");
                return false;
            }
            #endregion

            return true;
        }
        #endregion

        #region validação idade
        public static bool ValidarIdade(DateOnly dataNascimento) // desenvolver a validação de maior idade
        {

            if (dataNascimento > DateOnly.FromDateTime(DateTime.Now))
            {
                Console.WriteLine("Data de nascimento não pode estar no futuro!\n");
                return false;
            }

            DateOnly diaHoje = DateOnly.FromDateTime(DateTime.Now);
            dataNascimento = dataNascimento.AddYears(18);

            if (dataNascimento > diaHoje)
            {
                Console.WriteLine("Impossivel cadastrar cliente com menos de 18 anos! Retornando ao menu principal!\n");
                //AQUI DEVE CHAMAR O MENU PARA RETORNAR
                return false;
            }

            return true;
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

        public void ClienteMemu()
        {
            int opcao;

            
        }
    }
}
