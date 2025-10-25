using SneezePharm.PastaCliente;


#region Inicialização
char verificar;
int opcao;
List<Cliente> clientes = LerArquivo();
List<string> clientesBloqueados = LerArquivoBloqueado();
#endregion

#region BuscarCliente
Cliente BuscarCliente(string cpf)
{
    return clientes.Find(c => c.Cpf == cpf);
}
#endregion

#region AlterarCliente
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
#endregion

#region ImprimirClienteLocalizado
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
#endregion

#region CarregarPrograma
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
#endregion

#region CarregarProgramaBloqueado
string CarregarProgramaBloqueado()
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
List<string> LerArquivoBloqueado()
{
    var caminhoCompletoBloqueado = CarregarProgramaBloqueado();

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
void GravarArquivoBloqueado(List<string> clienteBloqueados)
{
    var caminho = CarregarProgramaBloqueado();

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
#endregion

#region BloquearCliente
void BloquearCliente()
{
    Console.Write("Informe o CPF do cliente: ");
    string cpf = Console.ReadLine()!;
    var cliente = BuscarCliente(cpf);
    if (cliente is not null)
    {
        if (!clientesBloqueados.Contains(cpf))
        {
            Console.WriteLine($"Deseja mesmo bloquear o cliente {cliente.Nome.Trim()}? (0 - cancelar, 1 - confirmar)");
            var confirma = Console.ReadLine() ?? "";
            if (confirma == "1")
            {
                clientesBloqueados.Add(cliente.Cpf);
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
void DesbloquearCliente()
{
    Console.Write("Informe o CPF do cliente: ");
    string cpf = Console.ReadLine()!;
    var cliente = BuscarCliente(cpf);
    if (cliente is not null)
    {
        if (clientesBloqueados.Contains(cpf))
        {
            Console.WriteLine($"Deseja desbloquear o cliente {cliente.Nome.Trim()}? (0 - cancelar, 1 - confirmar)");
            var confirma = Console.ReadLine() ?? "";
            if (confirma == "1")
            {
                clientesBloqueados.Remove(cliente.Cpf);
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
void ImprimirClienteBloqueado()
{
    if (clientesBloqueados.Count == 0)
    {
        Console.WriteLine("\nLista vazia!\n");
    }
    foreach (var cpf in clientesBloqueados)
    {
        Console.WriteLine(BuscarCliente(cpf).ToString());
    }
}
#endregion

#region GravarArquivo
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
#endregion

#region Menu Cliente (Necessario fazer a classe Menu)
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
            if (novoCliente == null)
            {
                break;
            }

            var clienteExistente = BuscarCliente(novoCliente.Cpf);

            if (clienteExistente == null)
            {
                clientes.Add(novoCliente);
                Console.WriteLine("Cliente cadastrado com sucesso!\n");
            }
            else
            {
                Console.WriteLine("CPF de cliente já cadastrado!\n");
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
            BloquearCliente();
            break;
        case 6:
            DesbloquearCliente();
            break;
        case 7:
            ImprimirClienteBloqueado();
            break;
        case 8:
            Console.WriteLine("Salvando e saindo...");
            break;
        default:
            Console.WriteLine("Informe entre 1 e 8!");
            break;
    }
} while (opcao != 8);
#endregion

#region GravaçãoDosArquivos
GravarArquivo(clientes);
GravarArquivoBloqueado(clientesBloqueados);
#endregion

