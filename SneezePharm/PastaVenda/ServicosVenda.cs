using SneezePharm.PastaCliente;
using SneezePharm.PastaMedicamento;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.PastaVenda
{
    public class ServicosVenda
    {

        #region Propriedades

        public List<Venda> Vendas { get; private set; } = [];

        public List<ItemVenda> ItensVenda { get; set; } = [];

        public ServicosCliente _cliente { get; set; }

        private List<Medicamento> _medicamento { get; set; } = [];

        #endregion

        #region Construtor

        //Inicializa as listas com os dados dos arquivos
        public ServicosVenda()
        {
            Vendas = LerArquivoVenda();
            ItensVenda = LerArquivoItemVenda();
        }
        #endregion

        #region OperacoesComArquivos

        public string CriarArquivoVenda()
        {
            string diretorio = @"C:\SneezePharma\Files";
            string arquivoVenda = "Sales.data";

            var diretorioVenda = Path.Combine(diretorio, arquivoVenda);

            if (!File.Exists(diretorioVenda))
            {
                using StreamWriter sw = File.CreateText(diretorioVenda);
                Console.WriteLine("Arquivo criado com sucesso!");
                Console.ReadKey();
            }
            return diretorioVenda;
        }

        public string CriarArquivoItensVenda()
        {
            string diretorio = @"C:\SneezePharma\Files";

            string arquivoItemVenda = "SaleItems.data";
            var diretorioItemVenda = Path.Combine(diretorio, arquivoItemVenda);

            if (!File.Exists(diretorioItemVenda))
            {
                using StreamWriter sw = File.CreateText(diretorioItemVenda);
                Console.WriteLine("Arquivo criado com sucesso!");
                Console.ReadKey();
            }

            return diretorioItemVenda;
        }

        public List<Venda> LerArquivoVenda()
        {
            var caminho = CriarArquivoVenda();

            StreamReader reader = new(caminho);
            using (reader)
            {
                List<Venda> vendas = new();

                while (reader.Peek() >= 0)
                {

                    var linha = reader.ReadLine();

                    string id = linha.Substring(0, 5);
                    string dataVenda = linha.Substring(5, 8);
                    string clienteCPF = linha.Substring(13, 11);
                    string valorTotal = linha.Substring(24, 8);

                    Venda venda = new(
                        int.Parse(id),
                        DateOnly.ParseExact(dataVenda, "ddMMyyyy"),
                        clienteCPF,
                        decimal.Parse(valorTotal)
                        );

                    vendas.Add(venda);
                }
                reader.Close();
                return vendas;
            }
        }

        public List<ItemVenda> LerArquivoItemVenda()
        {
            var caminho = CriarArquivoItensVenda();

            StreamReader reader = new(caminho);
            using (reader)
            {
                List<ItemVenda> itensVenda = new();

                while (reader.Peek() >= 0)
                {
                    var linha = reader.ReadLine();

                    string idVenda = linha.Substring(0, 5);
                    string medicamento = linha.Substring(5, 13);
                    string quantidade = linha.Substring(18, 3);
                    string valorUnitario = linha.Substring(21, 8);

                    ItemVenda itemVenda = new(
                        int.Parse(idVenda),
                        medicamento,
                        int.Parse(quantidade),
                        decimal.Parse(valorUnitario)
                        );

                    itensVenda.Add(itemVenda);
                }
                reader.Close();
                return itensVenda;
            }

        }

        public void GravarArquivoVenda(List<Venda> vendas)
        {
            var caminho = CriarArquivoVenda();

            StreamWriter writer = new(caminho);
            using (writer)
            {
                foreach (var venda in vendas)
                {
                    writer.WriteLine(venda.ToFile());
                }
                writer.Close();
            }
        }
        public void GravarArquivoItemVenda(List<ItemVenda> itensVenda)
        {
            var caminho = CriarArquivoItensVenda();

            StreamWriter writer = new(caminho);
            using (writer)
            {
                foreach (var item in itensVenda)
                {
                    writer.WriteLine(item.ToFile());
                }
                writer.Close();
            }
        }
        #endregion

        #region CRUD

        public void IncluirVenda()
        {
            int id = 1;
            if (Vendas.Select(x => x.Id).Any())
                id = Vendas.Select(x => x.Id).Last() + 1;

            Console.WriteLine($"Id da Venda: {id}");
            Console.Write("Informe o CPF do cliente: ");

            var cPFCliente = Console.ReadLine() ?? "";

            var cliente = _cliente.BuscarCliente(cPFCliente);

            if (cliente is null)
            {
                Console.WriteLine("Fornecedor não encotrado!");
                return;
            }

            char resp;
            int cont = 1;

            do
            {
                Console.WriteLine("Inclua um item");
                var item = IncluirItem(id);

                if (item == null)
                {
                    return;
                }

                ItensVenda.Add(item);
                cont++;

                if (cont <= 3)
                {
                    Console.Write("Deseja adicionar outro item? [S]Sim [N]Não ");
                    resp = char.Parse(Console.ReadLine()!.ToLower().Trim());
                }
                else
                {
                    Console.WriteLine("Você atingiu o limite maximo de itens!");
                    break;
                }

            } while (resp == 'S');

            var valorItens = ItensVenda.Where(x => x.IdVenda == id).Select(x => x.ValorTotalItem);

            var valorTotalItens = 0.0m;
            foreach (var item in valorItens)
                valorTotalItens += item;

            Vendas.Add(new(id, cliente.Cpf, valorTotalItens));
        }
        public void LocalizarVenda()
        {
            Console.Write("Informe o ID da venda: ");
            var id = int.Parse(Console.ReadLine()!);
            var idVenda = BuscarVenda(id);

            if (idVenda is null)
                Console.WriteLine("!");
            else
                Console.WriteLine(idVenda);
        }
        public void ImprimirVendas()
        {
            if (Vendas is null)
                Console.WriteLine("Venda não encontrada!");
            else
                foreach (var vendas in Vendas)
                {
                    Console.WriteLine("=== Venda ===");
                    Console.WriteLine(vendas);

                    var idVenda = vendas.Id;
                    var Itens = ItensVenda.Where(x => x.IdVenda == idVenda);

                    Console.WriteLine("=== Itens ===");
                    foreach (ItemVenda itemVenda in Itens)
                        Console.WriteLine(itemVenda);

                }
        }

        private Venda BuscarVenda(int id)
        {
            return Vendas.Find(x => x.Id == id)!;
        }
        private Medicamento BuscarCDB(string codigoDeBarras)
        {
            return _medicamento.Find(x => x.CDB == codigoDeBarras)!;
        }

        #endregion

        #region CRUDITENS

        private ItemVenda IncluirItem(int idVenda)
        {
            Console.Write("Informe o código de barras do medicamento: ");
            var codigoDeBarras = Console.ReadLine()!.ToUpper();

            var medicamento = BuscarCDB(codigoDeBarras);

            if (medicamento is null)
            {
                Console.WriteLine("Medicamento não encontrado");
                return null;
            }
            if (medicamento.Situacao == 'I')
            {
                Console.WriteLine("Medicamento Inativo");
                return null;
            }

            Console.Write("Informe a quantidade (máx: 999): ");

            if (!int.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out var quantidade))
            {
                Console.WriteLine("Erro. Informe uma quantidade válida!");
                Console.Write("Informe a quantidade (máx: 999):  ");
                while (!int.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out quantidade))
                {
                    Console.WriteLine("Erro. Informe uma quantidade válida!");
                    Console.Write("Informe a quantidade (máx: 999):  ");
                }
            }

            while (quantidade > 999 || quantidade <= 0)
            {
                Console.WriteLine("A quantidade não pode ultrapassar 999 itens, e não pode ser menor ou igual a zero");
                Console.Write("Informe a quantidade (máx: 999):  ");

                while (!int.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out quantidade))
                {
                    Console.WriteLine("Erro. Informe uma quantidade válida!");
                    Console.Write("Informe a quantidade (máx: 999):");
                }
            }

            Console.Write($"Valor unitário (máx 9999.99): ");

            if (!decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out var valorUnitario))
            {
                Console.WriteLine("Erro. Informe um valor unitário válido!");
                Console.Write("Informe o valor unitario do medicamento (máx 9999.99): ");
                while (!decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out valorUnitario))
                {
                    Console.WriteLine("Erro. Informe um valor unitário válido!");
                    Console.Write("Informe o valor unitario do ingrediente (máx 9999.99): ");
                }
            }

            while (valorUnitario > 9999.99m || valorUnitario <= 0)
            {
                Console.WriteLine("O valor unitario não pode ultrapassar R$ 9999.99, e não pode ser menor ou igual a zero");
                Console.Write("Informe o valor unitario do medicamento (máx: 9999.99): ");
                while (!decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out valorUnitario))
                {
                    Console.WriteLine("Erro. Informe um valor unitário valido!");
                    Console.Write("Informe o valor unitário do medicamento (máx: 999.99): ");
                }
            }

            return new ItemVenda(idVenda, medicamento.CDB, quantidade, valorUnitario);
        }
        public void AlterarItemVenda()
        {
            Console.Write("Informe o ID da venda: ");
            var id = int.Parse(Console.ReadLine()!);
            var idVenda = BuscarVenda(id);

            if (idVenda is null)
            {
                Console.WriteLine("Venda não encontrada!");
                return;
            }

            Console.WriteLine(idVenda);
            var itemVenda = ItensVenda.Where(x => x.IdVenda == idVenda.Id);
            foreach (var item in itemVenda)
                Console.WriteLine(item);

            Console.Write("Informe o código de barras do medicamento que deseja alterar: ");
            var codigoDeBarras = Console.ReadLine()!.ToUpper() ?? "";

            var idItem = BuscarCDB(codigoDeBarras);

            if (idItem is null)
            {
                Console.WriteLine("Medicamento não encontrado!");
                return;
            }
            var itemVendido = itemVenda.FirstOrDefault(x => x.Medicamento == idItem.CDB);

            if (itemVendido is null)
            {
                Console.WriteLine("Item não localizado!");
                return;
            }

            Console.WriteLine("Item encontrado!");
            Console.WriteLine(itemVendido);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Atenção!");
            Console.WriteLine("Caso não queira alterar, aperte o enter!");
            Console.ResetColor();

            Console.Write("Informe o código de barras do medicamento: ");
            codigoDeBarras = Console.ReadLine()!.ToUpper() ?? itemVendido.Medicamento;

            var medicamento = BuscarCDB(codigoDeBarras);

            while (medicamento is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Item não localizado!");
                Console.ResetColor();
                Console.WriteLine("\nTente novamente.");
                Console.Write("Informe o código de barras do medicamento: ");
                codigoDeBarras = Console.ReadLine()!.ToUpper() ?? itemVendido.Medicamento;

                medicamento = BuscarCDB(codigoDeBarras);
            }


            Console.Write("Informe a quantidade (máx: 999): ");
            var quantidadeItem = Console.ReadLine() ?? "";


            if (quantidadeItem is "")
                quantidadeItem = itemVendido.Quantidade.ToString();

            if (!int.TryParse(quantidadeItem, CultureInfo.InvariantCulture, out var quantidade))
            {
                Console.WriteLine("Erro. Informe uma quantidade válida!");
                Console.Write("Informe a quantidade (máx: 999): ");
                while (!int.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out quantidade))
                {
                    Console.WriteLine("Erro. Informe uma quantidade válida!");
                    Console.Write("Informe a quantidade (máx: 999): ");
                }
            }

            while (quantidade > 999 || quantidade <= 0)
            {
                Console.WriteLine("A quantidade não pode ultrapassar 999 itens, e não pode ser menor ou igual a zero");
                Console.Write("Informe a quantidade (máx: 999): ");

                while (!int.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out quantidade))
                {
                    Console.WriteLine("Erro. Informe uma quantidade válida!");
                    Console.Write("Informe a quantidade (máx: 999): ");
                }
            }

            Console.Write($"Informe o valor unitário (máx: 9999.99): ");
            var valorUnit = Console.ReadLine() ?? "";

            if (valorUnit is "")
                valorUnit = itemVendido.ValorUnitario.ToString();

            if (!decimal.TryParse(valorUnit, out var valorUnitario))
            {
                Console.WriteLine("Erro. Informe um valor unitário válido!");
                Console.Write("Informe o valor unitário do medicamento (máx: 9999.99): ");
                while (!decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out valorUnitario))
                {
                    Console.WriteLine("Erro. Informe um valor unitário válido!");
                    Console.Write("Informe o valor unitario do medicamento (máx: 9999.99): ");
                }
            }

            while (valorUnitario > 9999.99m || valorUnitario <= 0.0m)
            {
                Console.WriteLine("O valor unitário não pode ultrapassar R$ 9999.99, e não pode ser menor ou igual a zero");
                Console.Write($"Informe o valor unitário (máx: 9999.99): ");
                while (!decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out valorUnitario))
                {
                    Console.WriteLine("Erro. Informe um valor unitário válido!");
                    Console.Write("Informe o valor unitário do medicamento (máx: 9999.99): ");
                }
            }

            itemVendido.SetMedicamento(medicamento.CDB);
            itemVendido.SetQuantidade(quantidade);
            itemVendido.SetValorUnitario(valorUnitario);

            itemVendido.SetValorTotalItem();

            Console.WriteLine("Item Atualizado com sucesso!");
            Console.WriteLine(itemVendido);
        }
        public void LocalizarItemVenda()
        {
            Console.Write("Informe o ID da venda: ");
            var id = int.Parse(Console.ReadLine()!);
            var idVenda = BuscarVenda(id);

            if (idVenda is null)
            {
                Console.WriteLine("Venda não encontrada!");
                return;
            }

            Console.WriteLine(idVenda);
            var itemVenda = ItensVenda.Where(x => x.IdVenda == idVenda.Id);
            foreach (var item in itemVenda)
                Console.WriteLine(item);

            Console.Write("Informe o código de barras do Medicamento: ");
            var codigoDeBarras = Console.ReadLine()!.ToUpper() ?? "";

            var idItem = BuscarCDB(codigoDeBarras);

            if (idItem is null)
            {
                Console.WriteLine("Medicamento não encontrado!");
                return;
            }
            var itemVendido = itemVenda.FirstOrDefault(x => x.Medicamento == idItem.CDB);

            if (itemVendido is null)
            {
                Console.WriteLine("Não foi encontrado nenhum item!");
                return;
            }

            Console.WriteLine("Item encontrado!");
            Console.WriteLine(itemVendido);

        }
        public void ImprimirItemVenda()
        {
            Console.Write("Informe o ID da venda: ");
            var id = int.Parse(Console.ReadLine()!);
            var idVenda = BuscarVenda(id);

            if (idVenda is null)
            {
                Console.WriteLine("Venda não encontrada");
                return;
            }

            Console.WriteLine(idVenda);
            var itemVenda = ItensVenda.Where(x => x.IdVenda == idVenda.Id);
            Console.WriteLine("=== Itens ===");
            foreach (var item in itemVenda)
                Console.WriteLine(item);
        }

        #endregion


        #region MENU
        public static int Display(string title, List<string> options)
        {
            Console.WriteLine(title);
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
            Console.Write("Escolha uma opção válida: ");
            return int.Parse(Console.ReadLine() ?? "0");
        }

        public List<string> OpcoesVenda = [
            "Incluir Venda",
                "Localizar Venda",
                "Imprimir Vendas",
                "Voltar ao Menu Principal"
            ];

        public List<string> OpcoesItemVenda = [
            "Localizar Item",
                "Alterar Item",
                "Imprimir Itens",
                "Voltar ao Menu Principal"
            ];

        #endregion
    }
}

