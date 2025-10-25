using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.PastaVenda
{
    public class ServicosVenda
    {

        public List<ItemVenda> ItensVenda = new List<ItemVenda>();
        public int idItensVenda = 0;
        public int id = 0;


        public string CarregarItensVenda()
        {
            string diretorio = @"C:\SneezePharma\Files\";
            if (!Directory.Exists(diretorio))
                Directory.CreateDirectory(diretorio);
            string arquivo = "SaleItems.data";
            var diretorioItemVenda = Path.Combine(diretorio, arquivo);
            if (!File.Exists(diretorioItemVenda))
            {
                using (StreamWriter sw = File.CreateText(diretorioItemVenda))
                {
                    sw.Close();
                }

            }

            return diretorioItemVenda;
        }

        public List<ItemVenda> LerItensVenda()
        {
            var diretorioItemVenda = CarregarItensVenda();

            StreamReader sr = new StreamReader(diretorioItemVenda);

            using (sr)
            {

                List<ItemVenda> Itens = new List<ItemVenda>();

                int numRegistros = 0;

                while (sr.Peek() >= 0)
                {
                    string linha = sr.ReadLine();
                    string id = linha.Substring(0, 5);
                    string idVenda = linha.Substring(5, 5);
                    string medicamento = linha.Substring(10, 13);
                    string quantidade = linha.Substring(23, 3);
                    string valorUnitario = linha.Substring(26, 8);
                    string totalItem = linha.Substring(34, 8);

                    Itens.Add(new ItemVenda(Convert.ToInt32(id), Convert.ToInt32(idVenda), medicamento, Convert.ToInt32(quantidade), Convert.ToDecimal(valorUnitario)));

                    numRegistros++;
                }

                idItensVenda = numRegistros;
                sr.Close();
                return Itens;
            }
        }

        public void GravaritensVenda()
        {
            var diretorioItemVenda = CarregarItensVenda();

            using (StreamWriter sw = new StreamWriter(diretorioItemVenda))
            {
                foreach (ItemVenda item in ItensVenda)
                {
                    sw.WriteLine(item.ToFile());
                }
                sw.Close();
            }
        }

        public void IncluirItemVenda()
        {
            int qntItens = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("=============== INSERIR ITEM ===============");
                Console.Write("Informe o código de barras do medicamento: ");
                string cdb = Console.ReadLine()!;
                Console.Write("Informe a quantidade: ");
                int quantidade = Convert.ToInt32(Console.ReadLine());
                Console.Write("Informe o valor unitário: R$ ");
                decimal valorUnitario = Convert.ToDecimal(Console.ReadLine());

                ItensVenda.Add(new ItemVenda(id, idItensVenda, cdb, quantidade, valorUnitario));

                Console.Clear();

                Console.WriteLine("Item cadastrado com sucesso!\n");

                char opcao;
                do
                {
                    Console.Write("Deseja inserir outro tipo de item?  [S]Sim  [N] Não: ");
                    opcao = Convert.ToChar(Console.ReadLine()!);

                    if (opcao == 'S' || opcao == 's')
                        qntItens++;
                    else if (opcao == 'N' || opcao == 'n')
                    {
                        qntItens = 3;
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Digite uma opção válida!");
                    }

                } while (opcao != 'N' && opcao != 'n' && opcao != 'S' && opcao != 's');


            } while (qntItens <= 3);


        }

        public ItemVenda? LocalizarItemVenda(int id)
        {
            return ItensVenda.Find(i => i.IdVenda == id);
        }

        void AlterarItemVenda()
        {
            Console.WriteLine("=== ALTERAR ITEM ===");
            Console.Write("Informe o ID do item: ");
            int id = Convert.ToInt32(Console.ReadLine()!);

            var item = LocalizarItemVenda(id);

            Console.Write("Informe a nova quantidade: ");
        }

        public void ImprimirItensVenda()
        {
            Console.WriteLine("=== LISTA DE ITENS DE VENDAS ===");

            foreach (var item in ItensVenda)
            {
                if (ItensVenda.Count() == 0)
                    Console.WriteLine("Lista Vazia");
                else
                    Console.WriteLine(item + "\n");
            }

        }

        public void MenuItensVenda()
        {
            Console.WriteLine("=== MENU ITENS DE VENDAS ===");
            Console.Write("1 - Incluir item\n2 - Localizar\n3 - Alterar item\n4 - Imprimir itens\n5 - Voltar\n\nDigite a opção desejada: ");
            int opcao = Convert.ToInt32(Console.ReadLine()!);
            do
            {
                switch (opcao)
                {
                    case 1:
                        IncluirItemVenda();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        ImprimirItensVenda();
                        break;

                    case 5:
                        break;
                    default:
                        Console.WriteLine("Informe uma opção válida!");
                        break;

                }

            } while (opcao < 1 || opcao > 5);

        }
    }
}
