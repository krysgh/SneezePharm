using SneezePharm;

char verificar;
List<Cliente> clientes = LerArquivo();

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
            Console.WriteLine($"Cliente {Cliente.Nome.Trim()} encontrado, ele está {Cliente.Situacao} atualmente, deseja alterar? (S/N)");
            verificar = Convert.ToChar(Console.ReadLine()!);

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

int opcao;

do
{
    Console.WriteLine("1 - cadastrar cliente");
    Console.WriteLine("2 - listar");
    Console.WriteLine("3 - Alterar situação de ativo do cliente");
    Console.WriteLine("4 - sair");
    opcao = Convert.ToInt32(Console.ReadLine());

    switch (opcao)
    {
        case 1:
            Cliente novoCliente = Cliente.IncluirCliente();
            if (novoCliente != null)
            {
                if(BuscarCliente == null)
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
            Console.WriteLine("Salvando e saindo...");
            break;
        default:
            Console.WriteLine("Informe entre 1 e 3!");
            break;
    }
} while (opcao != 4);

GravarArquivo(clientes);
