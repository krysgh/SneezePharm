using SneezePharm.PastaCliente;
using SneezePharm.PastaPrincipioAtivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.PastaVenda
{
    public class ServicosVenda
    {
        private List<PrincipioAtivo> _principioAtivo { get; set; } = [];

        public List<Venda> Vendas { get; private set; } = [];
        public List<ItemVenda> ItensVenda { get; set; } = [];

        public ServicosVenda()
        {
            ItensVenda = LerItensVenda();
        }

        public string CriarArquivosItensVenda()
        {
            Vendas = venda;
        }
        public void SetItensVenda(List<ItemVenda> itensVenda)
        {
            var diretorioItemVenda = CriarArquivosItensVenda();

            var cliente = _cliente.BuscarCliente(cpf);

            if (cliente is null)
            {
                Console.WriteLine("Cliente não encotrado!");
                return;
            }

            char resp;
            int cont = 1;

            do
            {
                Console.WriteLine("\nAdicione o item");
                var item = IncluirItemVenda(id);

                if (item == null)
                {
                    return;
                }

                ItensVenda.Add(item);
                cont++;

                if (cont <= 3)
                {
                    Console.Write("Deseja adicionar outro produto? (s/n) ");
                    resp = char.Parse(Console.ReadLine()!.ToLower().Trim());
                }
                else
                {
                    Console.WriteLine("Você atingiu o limite maáimo de itens!");
                    break;
                }

            } while (resp == 's');

        public void GravaritensVenda()
        {
            var diretorioItemVenda = CriarArquivosItensVenda();

            var valorTotalItens = 0.0m;

            foreach (var item in valorItens)
                valorTotalItens += item;

            Vendas.Add(new(id, cliente.Cpf, valorTotalItens));
        }
        

        public void IncluirItemVenda(int idVenda)
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

                ItensVenda.Add(new ItemVenda(idVenda, cdb, quantidade, valorUnitario));

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

        public void AlterarItemVenda()
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

    }
    */
