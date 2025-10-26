using SneezePharm.PastaCliente;
using System;
using System.Collections.Generic;
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
                "Imprimir os Itens",
                "Voltar ao Menu Principal"
            ];

        #endregion
    }
}

