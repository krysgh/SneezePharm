using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SneezePharm.Menu;
using SneezePharm.PastaCliente;
using SneezePharm.PastaCompra;
using SneezePharm.PastaMedicamento;
using SneezePharm.PastaProducao;

namespace SneezePharm.PastaVenda
{
    public class ServicosVenda
    {

        #region Propriedades

        public List<Venda> Vendas { get; private set; } = [];

        public List<ItemVenda> ItensVenda { get; set; } = [];

        public SistemaMenuVenda Menu { get; private set; }
        public SistemaMenuItemVenda MenuItem { get; private set; }

        #endregion

        #region Construtor

        //Inicializa as listas com os dados dos arquivos
        public ServicosVenda()
        {
            Vendas = LerArquivoVenda();
            ItensVenda = LerArquivoItemVenda();
            Menu = new SistemaMenuVenda();
            MenuItem = new SistemaMenuItemVenda();
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

        public void GravarArquivoVenda()
        {
            var caminho = CriarArquivoVenda();

            StreamWriter writer = new(caminho);
            using (writer)
            {
                foreach (var venda in Vendas)
                {
                    writer.WriteLine(venda.ToFile());
                }
                writer.Close();
            }
        }
        public void GravarArquivoItemVenda()
        {
            var caminho = CriarArquivoItensVenda();

            StreamWriter writer = new(caminho);
            using (writer)
            {
                foreach (var item in ItensVenda)
                {
                    writer.WriteLine(item.ToFile());
                }
                writer.Close();
            }
        }
        #endregion

        #region CRUD

        public void IncluirVenda(ServicosCliente clientes, List<Medicamento> medicamentos)
        {
            int id = 1;
            if (Vendas.Select(x => x.Id).Any())
                id = Vendas.Select(x => x.Id).Last() + 1;

            Console.WriteLine($"Id da Venda: {id}");
            Console.Write("Informe o CPF do cliente: ");

            var clienteCPF = Console.ReadLine() ?? "";

            var cliente = clientes.BuscarCliente(clienteCPF);

            if (cliente is null)
            {
                Console.WriteLine("Cliente não encotrado!");
                return;
            }

            if (clientes.ClienteEstaBloqueado(clienteCPF))
            {
                Console.WriteLine("Cliente está bloqueado!");
                return;
            }

            if (cliente.Situacao == 'I')
            {
                Console.WriteLine("Cliente está inativo!");
                return;

            }

            char resp = ' ';
            int cont = 1;

            do
            {
                Console.WriteLine("Inclua um item:");
                var item = IncluirItem(id, medicamentos);

                if (item == null)
                {
                    return;
                }

                ItensVenda.Add(item);
                cont++;

                if (cont <= 3)
                {
                    Console.Write("Deseja adicionar outro item? [S]Sim [N]Não ");
                    resp = char.Parse(Console.ReadLine()!);
                }
                else
                {
                    Console.WriteLine("Você atingiu o limite máximo de itens!");
                    break;
                }

            } while (resp == 'S');

            var valorItens = ItensVenda.Where(x => x.IdVenda == id).Select(x => x.ValorTotalItem);

            var valorTotalItens = 0.0m;
            foreach (var item in valorItens)
                valorTotalItens += item;

            Vendas.Add(new(id, cliente.Cpf, valorTotalItens));
            cliente.SetUltimaCompra(DateOnly.FromDateTime(DateTime.Now));
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
        private Medicamento BuscarCDB(string codigoDeBarras, List<Medicamento> medicamentos)
        {
            return medicamentos.Find(x => x.CDB == codigoDeBarras)!;
        }


        #endregion

        #region CRUDITENS

        private ItemVenda IncluirItem(int idVenda, List<Medicamento> medicamentos)
        {
            Console.Write("Informe o código de barras do medicamento: ");
            var codigoDeBarras = Console.ReadLine()!.ToUpper();

            var medicamento = BuscarCDB(codigoDeBarras, medicamentos);

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


            return new ItemVenda(idVenda, medicamento.CDB, quantidade, medicamento.ValorVenda);
        }
        public void AlterarItemVenda(List<Medicamento> medicamentos)
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

            var idItem = BuscarCDB(codigoDeBarras, medicamentos);

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

            var medicamento = BuscarCDB(codigoDeBarras, medicamentos);

            while (medicamento is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Item não localizado!");
                Console.ResetColor();
                Console.WriteLine("\nTente novamente.");
                Console.Write("Informe o código de barras do medicamento: ");
                codigoDeBarras = Console.ReadLine()!.ToUpper() ?? itemVendido.Medicamento;

                medicamento = BuscarCDB(codigoDeBarras, medicamentos);
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

            itemVendido.SetMedicamento(medicamento.CDB);
            itemVendido.SetQuantidade(quantidade);
            itemVendido.SetValorUnitario(medicamento.ValorVenda);

            itemVendido.SetValorTotalItem();

            Console.WriteLine("Item Atualizado com sucesso!");
            Console.WriteLine(itemVendido);
        }
        public void LocalizarItemVenda(List<Medicamento> medicamentos)
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

            var idItem = BuscarCDB(codigoDeBarras, medicamentos);

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

        public void GerarRelatorioVendas()
        {
            Console.Write("Informe a data inicial do periodo (dd/mm/yyyy): ");
            DateOnly dataInicial = DateOnly.Parse(Console.ReadLine()!);
            Console.Write("Informe a data final do periodo (dd/mm/yyyy): ");
            DateOnly dataFinal = DateOnly.Parse(Console.ReadLine()!);

            if (dataInicial > dataFinal)
            {
                Console.WriteLine("\nA data inicial do relatório não pode ser maior que a data final!\n");
                return;
            }

            if (dataFinal > DateOnly.FromDateTime(DateTime.Now) || dataInicial > DateOnly.FromDateTime(DateTime.Now))
            {
                Console.WriteLine("\nAs datas não podem ser maiores que o dia de hoje!\n");
                return;
            }

            var vendasNoPeriodo = Vendas.Where(v => v.DataVenda >= dataInicial && v.DataVenda <= dataFinal).ToList();

            if (vendasNoPeriodo.Count == 0)
            {
                Console.WriteLine("\nNenhuma venda encontrada nesse período!\n");
                return;
            }

            Console.WriteLine("\nRelatório de vendas no periodo selecionado: ");

            foreach (Venda v in vendasNoPeriodo)
            {
                Console.WriteLine("\n" + v);
            }
        }
    }
}

