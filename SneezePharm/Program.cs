using SneezePharm;

char verificar;
int opcao;
List<Cliente> clientes = LerArquivo();
List<string> clientesBloqueados = new List<string>();

Cliente BuscarCliente(string cpf)
{
    return clientes.Find(c => c.Cpf == cpf);
}

void AlterarCliente()
{
    Console.WriteLine("Informe o CPF do cliente que deseja alterar a situação: ");
    string cpf = Console.ReadLine()!;
    var Cliente = BuscarCliente(cpf);

    if (Cliente == null)
    {
        Console.WriteLine("Cliente não encontrado!\n");
    }
    else
    {
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
}

void ImprimirClienteLocalizado()
{
    Console.Write("Informe o CPF do cliente: ");
    string cpf = Console.ReadLine()!;
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

string CarregarPrograma()
{
    string diretorio = @"C:\SneezePharma\Files\";
    string arquivo = "Customers.data";

    if (!Directory.Exists(diretorio))
    {
        Directory.CreateDirectory(diretorio);
    }
    if (!File.Exists(Path.Combine(diretorio, arquivo)))
    {
        using (File.Create(Path.Combine(diretorio, arquivo))) { }
    }

    return Path.Combine(diretorio, arquivo);
}

List<Cliente> LerArquivo()
{
    var caminhoCompleto = CarregarPrograma();


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

void GravarArquivo(List<Cliente> clientes)
{
    var caminho = CarregarPrograma();

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



do
{
    Console.WriteLine("1 - cadastrar cliente");
    Console.WriteLine("2 - listar clientes");
    Console.WriteLine("3 - Alterar situação do cliente");
    Console.WriteLine("4 - Localizar cliente");
    Console.WriteLine("5 - Bloquear cliente");
    Console.WriteLine("6 - Desbloquear cliente");
    Console.WriteLine("7 - Listar clientes bloqueados");
    Console.WriteLine("8 - sair");
    opcao = Convert.ToInt32(Console.ReadLine());

    switch (opcao)
    {
        case 1:
            Cliente novoCliente = Cliente.IncluirCliente();
            if (novoCliente != null)
            {
                if (BuscarCliente == null)
                {
                    clientes.Add(novoCliente);
                }
                else
                {
                    Console.WriteLine("CPF de cliente já cadastrado!\n");
                }
            }
            break;
        case 2:
            foreach (Cliente cliente in clientes)
            {
                Console.WriteLine(cliente);
            }
            Console.WriteLine("");
            break;
        case 3:
            AlterarCliente();
            break;
        case 4:
            ImprimirClienteLocalizado();
            break;
        case 5:
            break;
        case 6:
            break;
        case 7:
            Console.WriteLine("Salvando e saindo...");
            break;
        default:
            Console.WriteLine("Informe entre 1 e 5!");
            break;
    }
} while (opcao != 7);

GravarArquivo(clientes);
