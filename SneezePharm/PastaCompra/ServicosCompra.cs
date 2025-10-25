using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SneezePharm.PastaFornecedor;
using SneezePharm.PastaPrincipioAtivo;

namespace SneezePharm.PastaCompra
{
    public class ServicosCompra
    {
        public List<Compra> Compras { get; private set; } = [];
        public List<ItemCompra> ItensCompra { get; private set; } = [];
        


        public void SetCompras(List<Compra> compra)
        {
            Compras = compra;
        }
        public void SetItensCompra(List<ItemCompra> itensCompra)
        {
            ItensCompra = itensCompra;
        }

        // CRUD Compras
        public void IncluirCompra()
        {
            int id = 1;
            if (Compras.Select(x => x.Id).Any())
                id = Compras.Select(x => x.Id).Last() + 1;

            Console.WriteLine($"Id da compra: {id}");
            Console.Write("Informe o CNPJ do cliente: ");

            var fornecedorCnpj = Console.ReadLine() ?? "";

            var fornecedor = LocalizarFornecedor(fornecedorCnpj);

            if (fornecedor is null)
            {
                Console.WriteLine("Fornecedor não encotrado!");
                return;
            }

            char resp;
            int cont = 1;

            do
            {
                Console.WriteLine("\nAdicione o produto");
                var item = IncluirItem(id);

                if (item == null)
                {
                    return;
                }

                ItensCompra.Add(item);
                cont++;

                if (cont <= 3)
                {
                    Console.Write("Deseja adicionar outro produto? (s/n) ");
                    resp = char.Parse(Console.ReadLine()!.ToLower().Trim());
                }
                else
                {
                    Console.WriteLine("Você atingiu o limite maximo de produtos!");
                    break;
                }

            } while (resp == 's');

            var valorItens = ItensCompra.Where(x => x.IdCompra == id).Select(x => x.TotalItem);

            var valorTotalItens = 0.0m;
            foreach (var item in valorItens)
                valorTotalItens += item;

            Compra compra = new(id, fornecedor.Cnpj, valorTotalItens);

            Compras.Add(compra);
        } // falta verificacao do cnpj
        public void LocalizarCompra()
        {
            Console.Write("Informe o ID da compra: ");
            var id = int.Parse(Console.ReadLine()!);
            var idCompra = BuscarCompra(id);

            if (idCompra is null)
                Console.WriteLine("Nao foi encontrada nenhuma compra com esse endereco ID!");
            else
                Console.WriteLine(idCompra);
        }
        public void AlterarCompra()
        {
            Console.Write("Informe o id da compra: ");
            var id = int.Parse(Console.ReadLine()!);

            var idCompra = BuscarCompra(id);

            if (idCompra is null)
            {
                Console.WriteLine("Compra não encontrada");
                return;
            }

            Console.WriteLine(idCompra);
            Console.Write("Informe o novo CNPJ: ");

            var fornecedorCnpj = Console.ReadLine() ?? "";
            var fornecedor = LocalizarFornecedor(fornecedorCnpj);

            if (fornecedor is null)
            {
                Console.WriteLine("Fornecedor não encotrado!");
                return;
            }

            idCompra.SetCnpj(fornecedor.Cnpj);
            Console.WriteLine("Alteracao concluida com sucesso!");

            Console.WriteLine(idCompra);
        } // falta verificacao do cnpj
        public void ImprimirCompras()
        {
            if (Compras is null)
                Console.WriteLine("Nao foi encontrada nenhuma compra!");
            else
                foreach (Compra compra in Compras)
                {
                    Console.WriteLine("=== Compra ===");
                    Console.WriteLine(compra);

                    var idCompra = compra.Id;
                    var idVenda = ItensCompra.Where(x => x.IdCompra == idCompra);

                    Console.WriteLine("=== Itens ===");
                    foreach (ItemCompra itemCompra in idVenda)
                        Console.WriteLine(itemCompra);

                }
        }

        // buscas privadas
        private Compra BuscarCompra(int id)
        {
            return Compras.Find(x => x.Id == id)!;
        }
        private PrincipioAtivo BuscarPA(string idPrincipioAtivo)
        {
            return PrincipiosAtivos.Find(x => x.Id == idPrincipioAtivo)!;
        }

        // CRUD Itens da compra
        private ItemCompra IncluirItem(int idCompra)
        {
            Console.Write("Informe o Id do Ingrediente/Principio Ativo: (ex: AI0001) ");
            var idPrincipioAtivo = Console.ReadLine()!.ToUpper();

            var principioAtivo = BuscarPA(idPrincipioAtivo);

            if (principioAtivo is null)
            {
                Console.WriteLine("Id não encontrado");
                return null;
            }
            if (principioAtivo.Situacao == 'I')
            {
                Console.WriteLine("Produto Inativo");
                return null;
            }

            Console.Write("Informe a quantidade: (max: 9999) ");
            // verificando se o usuario está colocando um numero inteiro
            if (!int.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out var quantidade))
            {
                Console.WriteLine("Erro. Informe uma quantidade válida!");
                Console.Write("Informe a quantidade: (max: 9999) ");
                while (!int.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out quantidade))
                {
                    Console.WriteLine("Erro. Informe uma quantidade válida!");
                    Console.Write("Informe a quantidade: (max: 9999) ");
                }
            }

            // verificando se a quantidade é maior que 9999 ou se eh menor que 0
            while (quantidade > 9999 || quantidade <= 0)
            {
                Console.WriteLine("A quantidade não pode ultrapassar 9999 itens, e não pode ser menor ou igual a zero");
                Console.Write("Informe a quantidade: (max: 9999) ");
                // verificando se o usuario esta colocando um numero inteiro
                while (!int.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out quantidade))
                {
                    Console.WriteLine("Erro. Informe uma quantidade válida!");
                    Console.Write("Informe a quantidade: (max: 9999) ");
                }
            }

            Console.Write($"Valor unitario: (max 999.99) ");
            // verificando se o usuario digitou um numero 
            if (!decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out var valorUnitario))
            {
                Console.WriteLine("Erro. Informe um valor unitário válido!");
                Console.Write("Informe o valor unitario do ingrediente: (max: 999.99) ");
                while (!decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out valorUnitario))
                {
                    Console.WriteLine("Erro. Informe um valor unitário válido!");
                    Console.Write("Informe o valor unitario do ingrediente: (max: 999.99) ");
                }
            }

            while (valorUnitario > 999.99m || valorUnitario <= 0)
            {
                Console.WriteLine("O valor unitario não pode ultrapassar R$ 999.99, e não pode ser menor ou igual a zero");
                Console.Write("Informe o valor unitario do ingrediente: (max: 999.99) ");
                while (!decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out valorUnitario))
                {
                    Console.WriteLine("Erro. Informe um valor unitario valido!");
                    Console.Write("Informe o valor unitario do ingrediente: (max: 999.99) ");
                }
            }

            return new ItemCompra(idCompra, principioAtivo.Id, quantidade, valorUnitario);
        }
        public void AlterarItemCompra()
        {
            Console.Write("Informe o ID da compra: ");
            var id = int.Parse(Console.ReadLine()!);
            var idCompra = BuscarCompra(id);

            if (idCompra is null)
            {
                Console.WriteLine("Nao foi encontrada nenhuma compra com esse endereco ID!");
                return;
            }

            Console.WriteLine(idCompra);
            var itemCompra = ItensCompra.Where(x => x.IdCompra == idCompra.Id);
            foreach (var item in itemCompra)
                Console.WriteLine(item);

            Console.Write("Informe o ID do Principio Ativo que deseja alterar: ");
            var idPrincipioAtivo = Console.ReadLine()!.ToUpper() ?? "";

            var idItem = BuscarPA(idPrincipioAtivo);

            if (idItem is null)
            {
                Console.WriteLine("Nao foi encontrado nenhum Ingrediente com esse endereco ID!");
                return;
            }
            var itemVendido = itemCompra.FirstOrDefault(x => x.Ingrediente == idItem.Id);

            if (itemVendido is null)
            {
                Console.WriteLine("Nao foi encontrado nenhum item com esse Principio Ativo!");
                return;
            }

            Console.WriteLine("Produto encontrado!");
            Console.WriteLine(itemVendido);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Atencao!");
            Console.WriteLine("Caso não queira alterar, aperte o enter!");
            Console.ResetColor();

            Console.Write("Informe o ID do Principio ativo: ");
            idPrincipioAtivo = Console.ReadLine()!.ToUpper() ?? itemVendido.Ingrediente;

            var principioAtivo = BuscarPA(idPrincipioAtivo);

            while (principioAtivo is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nao foi encontrado nenhum item com esse Principio Ativo!");
                Console.ResetColor();
                Console.WriteLine("\nTente novamente.");
                Console.Write("Informe o ID do Principio ativo: ");
                idPrincipioAtivo = Console.ReadLine()!.ToUpper() ?? itemVendido.Ingrediente;

                principioAtivo = BuscarPA(idPrincipioAtivo);
            }


            Console.Write("Informe a quantidade: (max: 9999) ");
            var quantidadeItem = Console.ReadLine() ?? "";

            // caso nao digite nada, ele recebe o valor que ja tinha 
            if (quantidadeItem is "")
                quantidadeItem = itemVendido.Quantidade.ToString();

            if (!int.TryParse(quantidadeItem, CultureInfo.InvariantCulture, out var quantidade))
            {
                Console.WriteLine("Erro. Informe uma quantidade válida!");
                Console.Write("Informe a quantidade: (max: 9999) ");
                while (!int.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out quantidade))
                {
                    Console.WriteLine("Erro. Informe uma quantidade válida!");
                    Console.Write("Informe a quantidade: (max: 9999) ");
                }
            }

            while (quantidade > 9999 || quantidade <= 0)
            {
                Console.WriteLine("A quantidade não pode ultrapassar 9999 itens, e não pode ser menor ou igual a zero");
                Console.Write("Informe a quantidade: (max: 9999) ");

                while (!int.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out quantidade))
                {
                    Console.WriteLine("Erro. Informe uma quantidade válida!");
                    Console.Write("Informe a quantidade: (max: 9999) ");
                }
            }

            Console.Write($"Informe o valor unitario: (max 999.99) ");
            var valorUnit = Console.ReadLine() ?? "";

            if (valorUnit is "")
                valorUnit = itemVendido.ValorUnitario.ToString();

            if (!decimal.TryParse(valorUnit, out var valorUnitario))
            {
                Console.WriteLine("Erro. Informe um valor unitário válido!");
                Console.Write("Informe o valor unitario do ingrediente: (max: 999.99) ");
                while (!decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out valorUnitario))
                {
                    Console.WriteLine("Erro. Informe um valor unitário válido!");
                    Console.Write("Informe o valor unitario do ingrediente: (max: 999.99) ");
                }
            }

            while (valorUnitario > 999.99m || valorUnitario <= 0.0m)
            {
                Console.WriteLine("O valor unitario não pode ultrapassar R$ 999.99, e não pode ser menor ou igual a zero");
                Console.Write($"Informe o valor unitario: (max: 999.99) ");
                while (!decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out valorUnitario))
                {
                    Console.WriteLine("Erro. Informe um valor unitário válido!");
                    Console.Write("Informe o valor unitario do ingrediente: (max: 999.99) ");
                }
            }

            itemVendido.SetIngrediente(principioAtivo.Id);
            itemVendido.SetQuantidade(quantidade);
            itemVendido.SetValorUnitario(valorUnitario);

            // utiliza a quantidade e valor do proprio item como parametro
            itemVendido.SetTotalItem();

            Console.WriteLine("Item Atualizado com sucesso!");
            Console.WriteLine(itemVendido);
        }
        public void LocalizarItemCompra()
        {
            Console.Write("Informe o ID da compra: ");
            var id = int.Parse(Console.ReadLine()!);
            var idCompra = BuscarCompra(id);

            if (idCompra is null)
            {
                Console.WriteLine("Nao foi encontrada nenhuma compra com esse endereco ID!");
                return;
            }

            Console.WriteLine(idCompra);
            var itemCompra = ItensCompra.Where(x => x.IdCompra == idCompra.Id);
            foreach (var item in itemCompra)
                Console.WriteLine(item);

            Console.Write("Informe o Ingrediente: (ex: AI0001) ");
            var idPrincipioAtivo = Console.ReadLine()!.ToUpper() ?? "";

            var idItem = BuscarPA(idPrincipioAtivo);

            if (idItem is null)
            {
                Console.WriteLine("Nao foi encontrado nenhum Ingrediente com esse endereco ID!");
                return;
            }
            var itemVendido = itemCompra.FirstOrDefault(x => x.Ingrediente == idItem.Id);

            if (itemVendido is null)
            {
                Console.WriteLine("Nao foi encontrado nenhum item com esse Principio Ativo!");
                return;
            }

            Console.WriteLine("Produto encontrado!");
            Console.WriteLine(itemVendido);

        }
        public void ImprimirItemCompra()
        {
            Console.Write("Informe o ID da compra: ");
            var id = int.Parse(Console.ReadLine()!);
            var idCompra = BuscarCompra(id);

            if (idCompra is null)
            {
                Console.WriteLine("Nao foi encontrada nenhuma compra com esse endereco ID!");
                return;
            }

            Console.WriteLine(idCompra);
            var itemCompra = ItensCompra.Where(x => x.IdCompra == idCompra.Id);
            Console.WriteLine("=== Itens ===");
            foreach (var item in itemCompra)
                Console.WriteLine(item);
        }

        // criar arquivos
        public string CarregarCompra()
        {
            string diretorio = @"C:\SneezePharma\Files";

            string arquivoCompra = "Purchases.data";
            var diretorioCompra = Path.Combine(diretorio, arquivoCompra);

            if (!File.Exists(diretorioCompra))
            {
                using StreamWriter sw = File.CreateText(diretorioCompra);
                Console.WriteLine("Arquivo criado com sucesso");
                Console.ReadKey();
            }

            return diretorioCompra;
        }
        public string CarregarItemCompra()
        {
            string diretorio = @"C:\SneezePharma\Files";

            string arquivoItemCompra = "PurchaseItem.data";
            var diretorioItemCompra = Path.Combine(diretorio, arquivoItemCompra);

            if (!File.Exists(diretorioItemCompra))
            {
                using StreamWriter sw = File.CreateText(diretorioItemCompra);
                Console.WriteLine("Arquivo criado com sucesso");
                Console.ReadKey();
            }

            return diretorioItemCompra;
        }

        // leitura de arquivos
        public List<Compra> LerArquivoCompra()
        {
            var caminho = CarregarCompra();

            StreamReader reader = new(caminho);
            using (reader)
            {
                List<Compra> compras = new();

                while (reader.Peek() >= 0)
                {

                    var linha = reader.ReadLine();

                    string id = linha.Substring(0, 5);
                    string dataCompra = linha.Substring(5, 8);
                    string fornecedorCnpj = linha.Substring(13, 14);
                    string valorTotal = linha.Substring(27, 8);

                    Compra compra = new(
                        int.Parse(id),
                        fornecedorCnpj,
                        decimal.Parse(valorTotal),
                        DateOnly.ParseExact(dataCompra, "ddMMyyyy")
                        );

                    compras.Add(compra);
                }
                reader.Close();
                return compras;
            }
        }
        public List<ItemCompra> LerArquivoItemCompra()
        {
            var caminho = CarregarItemCompra();

            StreamReader reader = new(caminho);
            using (reader)
            {
                List<ItemCompra> itensCompra = new();

                while (reader.Peek() >= 0)
                {
                    var linha = reader.ReadLine();

                    string idCompra = linha.Substring(0, 5);
                    string idIgrediente = linha.Substring(5, 6);
                    string quantidade = linha.Substring(11, 4);
                    string valorUnitario = linha.Substring(15, 8);

                    ItemCompra itemCompra = new(
                        int.Parse(idCompra),
                        idIgrediente,
                        int.Parse(quantidade),
                        decimal.Parse(valorUnitario)
                        );

                    itensCompra.Add(itemCompra);
                }
                reader.Close();
                return itensCompra;
            }

        }

        // gravar a lista nos arquivos
        public void GravarArquivoCompra(List<Compra> compras)
        {
            var caminho = CarregarCompra();

            StreamWriter writer = new(caminho);
            using (writer)
            {
                foreach (var compra in compras)
                {
                    writer.WriteLine(compra.ToFile());
                }
                writer.Close();
            }
        }
        public void GravarArquivoItemCompra(List<ItemCompra> itensCompra)
        {
            var caminho = CarregarItemCompra();

            StreamWriter writer = new(caminho);
            using (writer)
            {
                foreach (var item in itensCompra)
                {
                    writer.WriteLine(item.ToFile());
                }
                writer.Close();
            }
        }

        // menu
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

        // menu Compras
        public List<string> OpcoesCompra = [
            "Incluir Compra",
                "Localizar Compra",
                "Alterar Compra",
                "Imprimir Compras",
                "Voltar ao Menu Principal"
            ];

        public List<string> OpcoesItemCompra = [
            "Localizar Item",
                "Alterar Item",
                "Imprimir os Itens",
                "Voltar ao Menu Principal"
            ];
  
    }
}
