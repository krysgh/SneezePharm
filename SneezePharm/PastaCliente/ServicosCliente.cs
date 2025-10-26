using SneezePharm.Menu;
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
        public SistemaMenuCliente Menu { get; private set; }

        public ServicosCliente()
        {
            Clientes = LerArquivoCliente();
            ClientesBloqueados = LerArquivoClientesBloqueados();
            Menu = new SistemaMenuCliente();
        }

        public void SetCliente(List<Cliente> clientes)
        {
            Clientes = clientes;

        }

        // Validacoes
        private static bool ValidarCpf(string cpf)
        {
            #region validação de somente numeros

            #endregion

            #region // regiao de verificação de tamanho e números iguais
            cpf.ToCharArray();

            if (cpf.Length != 11)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Tamanho de CPF insuficiente!\n");
                Console.ResetColor();
                return false;
            }

            if (cpf.Distinct().Count() == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("CPF não pode ter todos os digitos iguais!\n");
                Console.ResetColor();
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
        private static bool ValidarIdade(DateOnly dataNascimento) // desenvolver a validação de maior idade
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
                Console.WriteLine("Impossivel cadastrar cliente com menos de 18 anos!\n");
                //AQUI DEVE CHAMAR O MENU PARA RETORNAR
                return false;
            }

            return true;
        }

        // Busca
        public Cliente BuscarCliente(string cpf)
        {
            return Clientes.Find(c => c.Cpf == cpf);
        }

        public bool ClienteEstaBloqueado(string cpf) // Cliente está bloqueado? Não = false
        {
            if (ClientesBloqueados.Contains(cpf))
            {
                return true;
            }

            return false;
        }

        //Atulizar data de ultima compra - chamar a função quando o cliente fizer a compra
        public void AtulizarUltimaCompraCliente(Cliente c)
        {
            c.SetUltimaCompra(DateOnly.FromDateTime(DateTime.Now));
        }

        // CRUD Clientes
        public void IncluirCliente()
        {
            decimal cpf;
            do
            {
                Console.Write("Informe o cpf: ");

                while (!decimal.TryParse(Console.ReadLine(), out cpf))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erro. Digite apenas números!\n");
                    Console.ResetColor();
                    Console.Write("Informe o cpf: ");
                }

            } while (!ValidarCpf(cpf.ToString()));

            Console.Write("Informe o nome: ");
            string nome = Console.ReadLine() ?? "";

            while(nome is "" || nome.Length < 3)
            {
                Console.WriteLine("Tamanho do nome insuficiente\n");
                Console.Write("Informe o nome: ");
                nome = Console.ReadLine() ?? "";
            }

            if (nome.Length > 50)
            {
                nome = nome.Substring(0, 50);
            }

            DateOnly dataNascimento;
            do
            {
                Console.Write("Informe a data de nascimento (dd/mm/aaaa): ");

                if (!DateOnly.TryParse(Console.ReadLine(), out dataNascimento))
                {
                    Console.WriteLine("Erro. Informe uma data de nascimento valida!");
                    Console.Write("Informe a data de nascimento (dd/mm/aaaa): ");
                    while (!DateOnly.TryParse(Console.ReadLine(), out dataNascimento))
                    {
                        Console.WriteLine("Erro. Informe uma data de nascimento valida!");
                        Console.Write("Informe a data de nascimento (dd/mm/aaaa): ");
                    }
                }

            } while (!ValidarIdade(dataNascimento));

            string telefone;
            do
            {
                Console.Write("Informe o telefone com ddd: ");
                telefone = Console.ReadLine()!;

                if (telefone.Length != 11)
                {
                    Console.WriteLine("\nTelefone deve obrigatóriamente ter 11 dígitos, 2 do DDD e 9 do número! Tente novamente!\n");
                }
            } while (telefone.Length != 11);

            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.Now);

            var cliente = new Cliente(cpf.ToString(), nome, dataNascimento, telefone, dataAtual, dataAtual, 'A');

            Clientes.Add(cliente);
        }
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

            int opcao;
            char verificarTexto;
            do
            {

                if (Cliente.Situacao == 'A')
                {
                    Console.WriteLine($"Cliente {Cliente.Nome.Trim()} está ATIVO(A) atualmente, deseja alterar para INATIVO(A)? (S/N)");
                    if(!char.TryParse(Console.ReadLine()?.ToUpper(), out verificarTexto))
                    {
                        Console.WriteLine("Erro. Insira uma opcao valida!\n");
                        Console.WriteLine($"Deseja alterar para INATIVO(A)? (S/N)");
                        while(!char.TryParse(Console.ReadLine()?.ToUpper(), out verificarTexto))
                        {
                            Console.WriteLine("Erro. Insira uma opcao valida!\n");
                            Console.WriteLine($"Deseja alterar para INATIVO(A)? (S/N)");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Cliente {Cliente.Nome.Trim()}  está INATIVO(A) atualmente, deseja alterar para ATIVO(A)? (S/N)");
                    if (!char.TryParse(Console.ReadLine()?.ToUpper(), out verificarTexto))
                    {
                        Console.WriteLine("Erro. Insira uma opcao valida!\n");
                        Console.WriteLine($"Deseja alterar para ATIVO(A)? (S/N)");
                        while (!char.TryParse(Console.ReadLine()?.ToUpper(), out verificarTexto))
                        {
                            Console.WriteLine("Erro. Insira uma opcao valida!\n");
                            Console.WriteLine($"Deseja alterar para ATIVO(A)? (S/N)");
                        }
                    }
                }
                
                switch (verificarTexto)
                {
                    case 'S':
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
                    case 'N':
                        Console.WriteLine("Operação cancelada!\n");
                        break;
                    default:
                        Console.WriteLine("Informe uma resposta válida (S - sim / N - não)");
                        break;
                }
            } while (verificarTexto != 'S' && verificarTexto != 's' && verificarTexto != 'n' && verificarTexto != 'N');
        }
        public void LocalizarCliente()
        {
            Console.Write("Informe o CPF do cliente: ");
            string cpf = Console.ReadLine() ?? "";
            var c = BuscarCliente(cpf);
            if (c is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cliente não encontrado!\n");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("\nCliente encontrado!\n" + c);
            }
        }
        public void ImprimirClientes()
        {
            if (Clientes is null)
                Console.WriteLine("Não existe nenhum cliente na base de dados!");
            else
            {
                foreach (var cliente in Clientes.OrderBy(x => x.Nome))
                    Console.WriteLine(cliente);
            }

        }

        // CRUD Clientes Bloqueados
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

        // Criando arquivos
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

        // Puxar os clientes para as listas
        private List<Cliente> LerArquivoCliente()
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
        private List<string> LerArquivoClientesBloqueados()
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

        // Armazenar clientes
        public void GravarArquivoCliente()
        {
            var caminho = CriarArquivosCliente();

            StreamWriter sw = new StreamWriter(caminho);

            using (sw)
            {
                foreach (var cliente in Clientes)
                {
                    sw.WriteLine(cliente.ToFile());
                }
                sw.Close();
            }
        }
        public void GravarArquivoBloqueado()
        {
            var caminho = CriarArquivosClientesBloqueados();

            StreamWriter sw = new StreamWriter(caminho);

            using (sw)
            {
                foreach (var cpf in ClientesBloqueados)
                {
                    sw.WriteLine(cpf);
                }
                sw.Close();
            }
        }

    }
}