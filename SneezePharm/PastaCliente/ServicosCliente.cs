using SneezePharm.PastaCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.PastaCliente
{
    public class ServicosCliente
    {
        public List<Cliente> Clientes { get; private set; }
        public List<string> ClientesBloqueados { get; private set; }

        public ServicosCliente()
        {
            Clientes = LerArquivo();
            ClientesBloqueados = LerArquivoClientesBloqueados();
        }

        private Cliente BuscarCliente(string cpf)
        {
            return Clientes.Find(c => c.Cpf == cpf);
        }

        #region AlterarCliente
        public void AlterarCliente()
        {
            Console.WriteLine("Informe o CPF do cliente que deseja alterar a situação: ");
            string cpf = Console.ReadLine()!;
            var Cliente = BuscarCliente(cpf);

            if (Cliente == null)
            {
                Console.WriteLine("Cliente não encontrado!\n");
                return;
            }

            char verificar;
            int opcao;

            do
            {

                if (Cliente.Situacao == 'A')
                {
                    Console.WriteLine($"Cliente {Cliente.Nome.Trim()} encontrado, ele está ATIVO atualmente, deseja alterar para INATIVO? (S/N)");
                    verificar = Convert.ToChar(Console.ReadLine()!);
                }
                else
                {
                    Console.WriteLine($"Cliente {Cliente.Nome.Trim()} encontrado, ele está INATIVO atualmente, deseja alterar para ATIVO? (S/N)");
                    verificar = Convert.ToChar(Console.ReadLine()!);
                }

                switch (verificar)
                {
                    case 'S' or 's':
                        if (Cliente.Situacao == 'I')
                        {
                            Cliente.setSituacao('A');
                            Console.WriteLine("Cliente alterado para Ativo com sucesso!\n");
                            break;
                        }
                        else
                        {
                            Cliente.setSituacao('I');
                            Console.WriteLine("Cliente alterado para Inativo com sucesso!\n");
                            break;
                        }
                    case 'N' or 'n':
                        Console.WriteLine("Operação cancelada!\n");
                        break;
                    default:
                        Console.WriteLine("Informe uma resposta válida (S - sim / N - não)");
                        break;
                }
            } while (verificar != 'S' && verificar != 's' && verificar != 'n' && verificar != 'N');
        }
        #endregion

        #region ImprimirClienteLocalizado
        public void ImprimirClienteLocalizado()
        {
            Console.Write("Informe o CPF do cliente: ");
            string cpf = Console.ReadLine() ?? "";
            var c = BuscarCliente(cpf);
            if (c is null)
            {
                Console.WriteLine("Cliente não encontrado!\n");
            }
            else
            {
                Console.WriteLine("\nCliente encontrado!\n" + c);
            }
        }
        #endregion

        #region CarregarPrograma
        public string CriarArquivosCliente()
        {
            string diretorio = @"C:\SneezePharma\Files\";
            string arquivo = "Customers.data";

            if (!File.Exists(Path.Combine(diretorio, arquivo)))
            {
                using (File.Create(Path.Combine(diretorio, arquivo))) { }
            }

            return Path.Combine(diretorio, arquivo);
        }
        #endregion

        #region CarregarProgramaBloqueado
        public string CriarArquivosClientesBloqueados()
        {
            string diretorio = @"C:\SneezePharma\Files\";
            string arquivoBloqueado = "RestrictedCustomers.data";

            if (!File.Exists(Path.Combine(diretorio, arquivoBloqueado)))
            {
                using (File.Create(Path.Combine(diretorio, arquivoBloqueado))) { }
            }

            return Path.Combine(diretorio, arquivoBloqueado);
        }
        #endregion

        #region LerArquivoBloqueado
        public List<string> LerArquivoClientesBloqueados()
        {
            var caminhoCompletoBloqueado = CriarArquivosClientesBloqueados();

            StreamReader sr = new StreamReader(caminhoCompletoBloqueado);

            using (sr)
            {
                List<string> clientes = new List<string>();

                while (sr.Peek() >= 0)
                {
                    string linha = sr.ReadLine();

                    clientes.Add(linha);
                }
                sr.Close();
                return clientes;
            }
        }
        #endregion

        #region GravarArquivoBloqueado
        public void GravarArquivoBloqueado(List<string> clienteBloqueados)
        {
            var caminho = CriarArquivosClientesBloqueados();

            StreamWriter sw = new StreamWriter(caminho);

            using (sw)
            {
                foreach (string cpf in clienteBloqueados)
                {
                    sw.WriteLine(cpf);
                }
                sw.Close();
            }
        }
        #endregion

        #region LerArquivo
        public List<Cliente> LerArquivoCliente()
        {
            var caminhoCompleto = CriarArquivosCliente();


            StreamReader sr = new StreamReader(caminhoCompleto);

            using (sr)
            {
                List<Cliente> clientes = new List<Cliente>();

                while (sr.Peek() >= 0)
                {
                    string linha = sr.ReadLine();

                    string cpf = linha.Substring(0, 11);
                    string nome = linha.Substring(11, 50);
                    DateOnly dataNascimento = DateOnly.ParseExact(linha.Substring(61, 8), "ddMMyyyy");
                    string telefone = linha.Substring(69, 11);
                    DateOnly ultimaCompra = DateOnly.ParseExact(linha.Substring(80, 8), "ddMMyyyy");
                    DateOnly dataCadastro = DateOnly.ParseExact(linha.Substring(88, 8), "ddMMyyyy");
                    char situacao = char.Parse(linha.Substring(96));

                    Cliente cliente = new Cliente(cpf, nome, dataNascimento, telefone, ultimaCompra, dataCadastro, situacao);
                    clientes.Add(cliente);
                }
                sr.Close();
                return clientes;
            }
        }
        #endregion

        #region BloquearCliente
        public void BloquearCliente()
        {
            Console.Write("Informe o CPF do cliente: ");
            string cpf = Console.ReadLine()!;
            var cliente = BuscarCliente(cpf);
            if (cliente is not null)
            {
                if (!ClientesBloqueados.Contains(cpf))
                {
                    Console.WriteLine($"Deseja mesmo bloquear o cliente {cliente.Nome.Trim()}? (0 - cancelar, 1 - confirmar)");
                    var confirma = Console.ReadLine() ?? "";
                    if (confirma == "1")
                    {
                        ClientesBloqueados.Add(cliente.Cpf);
                        Console.WriteLine("Cliente bloqueado com sucesso!\n");
                    }
                    else
                    {
                        Console.WriteLine("Operação cancelada. Retornando para menu...");
                    }
                }
                else
                {
                    Console.WriteLine("Esse cliente já está bloqueado!\n");
                }
            }
            else
            {
                Console.WriteLine("\nCliente não encontrado!");
            }

        }
        #endregion

        #region DesbloquearCliente
        public void DesbloquearCliente()
        {
            Console.Write("Informe o CPF do cliente: ");
            string cpf = Console.ReadLine()!;
            var cliente = BuscarCliente(cpf);
            if (cliente is not null)
            {
                if (ClientesBloqueados.Contains(cpf))
                {
                    Console.WriteLine($"Deseja desbloquear o cliente {cliente.Nome.Trim()}? (0 - cancelar, 1 - confirmar)");
                    var confirma = Console.ReadLine() ?? "";
                    if (confirma == "1")
                    {
                        ClientesBloqueados.Remove(cliente.Cpf);
                        Console.WriteLine($"\nCliente {cliente.Nome.Trim()} desbloqueado!\n");
                    }
                    else
                    {
                        Console.WriteLine("\nOperação cancelada!\n");
                    }
                }
                else
                {
                    Console.WriteLine("\nEsse cliente não está bloqueado!\n");
                }
            }
            else
            {
                Console.WriteLine("\nCliente não encontrado!\n");
            }
        }
        #endregion

        #region ImprimirClienteBloqueado
        public void ImprimirClienteBloqueado()
        {
            if (ClientesBloqueados.Count == 0)
            {
                Console.WriteLine("\nLista vazia!\n");
            }
            foreach (var cpf in ClientesBloqueados)
            {
                Console.WriteLine(BuscarCliente(cpf).ToString());
            }
        }
        #endregion

        #region GravarArquivo
        public void GravarArquivoCliente(List<Cliente> clientes)
        {
            var caminho = CriarArquivosCliente();

            StreamWriter sw = new StreamWriter(caminho);

            using (sw)
            {
                foreach (Cliente cliente in clientes)
                {
                    sw.WriteLine(cliente.ToFile());
                }
                sw.Close();
            }
        }
        #endregion

    }
}