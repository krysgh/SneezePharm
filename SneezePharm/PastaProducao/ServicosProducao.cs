using SneezePharm.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.PastaProducao
{
    public class ServicosProducao
    {
        public List<Producao> Producoes { get; set; } = [];
        public List<ItemProducao> ItensProducao { get; set; } = [];
        public SistemaMenuProducao Menu { get; private set; }
        public SistemaMenuItemProducao MenuItem { get; private set; }


        public ServicosProducao()
        {
            Producoes = LerArquivoProducao();
            ItensProducao = LerArquivoItemProducao();
            Menu = new SistemaMenuProducao();
            MenuItem = new SistemaMenuItemProducao();
        }

        public void IncluirProducao()
        {
            Console.Write("Insere o código de barras do medicamento em produção: ");
            string cdb = Console.ReadLine()!;
            //if (Medicamentos.Find(m => m.CDB == cdb) is null)
            //{
            //    Console.WriteLine("Não existe um medicamento com esse código de barras!\nCancelando operação...");
            //    return;
            //}

            Console.Write("Insere a quantidade do produto produzido (entre 0 e 1000): ");
            int quantidade = int.Parse(Console.ReadLine() ?? "0");
            if ((quantidade < 1) || (quantidade > 999))
            {
                Console.WriteLine("Quantidade inválida! A quantidade precisa ser entre 0 e 1000.\nCancelando operação...");
                return;
            }

            int id;
            try
            {
                id = Producoes.Last().Id + 1;
            }
            catch
            {
                id = 0;
            }

            this.Producoes.Add(new(id, cdb, quantidade));
        }

        private Producao? LocalizarProducao(int id)
        {
            return Producoes.Find(p => p.Id == id);
        }
        public void ImprimirProducaoLocalizado()
        {
            Console.Write("Insere o ID da produção: ");
            int id = int.Parse(Console.ReadLine() ?? "-1");
            var producao = LocalizarProducao(id);
            if (producao is null)
            {
                Console.WriteLine("Produção não encontrado!");
            }
            else
            {
                Console.WriteLine(producao);                            // Falta imprimir o princípio ativo da produção junto                        
            }
        }
        public void AlterarProducao()
        {
            Console.WriteLine("Insere o ID da produção: ");
            int id = int.Parse(Console.ReadLine() ?? "-1");
            var producao = LocalizarProducao(id);
            if (producao is not null)
            {
                Console.WriteLine("Insere a quantidade nova da produção (entre 0 e 1000): ");
                int quantidade = int.Parse(Console.ReadLine() ?? "0");
                if ((quantidade < 1) || (quantidade > 999))
                {
                    Console.WriteLine("Quantidade inválida! A quantidade precisa ser entre 0 e 1000.\nCancelando operação...");
                    return;
                }
                else
                {
                    Console.WriteLine("Deseja mesmo alterar a quantidade da produção? (0 - cancelar, 1 - confirmar)");
                    var confirmar = Console.ReadLine() ?? "0";
                    if (confirmar == "1")
                    {
                        producao.AlterarQuantidade(quantidade);
                        Console.WriteLine("\nQuantidade da produção alterada com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("\nOperação cancelada. Retornando para menu...");
                    }
                }
            }
            else
            {
                Console.WriteLine("Produção não encontrado!");
            }
        }

        public void ImprimirProducoes()
        {
            Console.WriteLine("-=-=- Lista de Produções -=-=-");
            if (Producoes.Count == 0)
            {
                Console.WriteLine("Nenhuma producao registrada.");
            }
            foreach (var producao in Producoes)
            {
                Console.WriteLine(producao + "\n");                            // Falta imprimir o princípio ativo da produção junto
            }
        }
        public string CriarArquivosProducao()
        {
            string diretorio = @"C:\SneezePharma\Files";
            string arquivoProducao = "Produce.data";

            var diretorioProducao = Path.Combine(diretorio, arquivoProducao);
            if (!File.Exists(diretorioProducao))
            {
                using StreamWriter sw = File.CreateText(diretorioProducao);
                Console.WriteLine("Arquivo criado com sucesso");
                Console.ReadKey();
            }
            return diretorioProducao;
        }
        public List<Producao> LerArquivoProducao()
        {
            StreamReader reader = new(CriarArquivosProducao());
            using (reader)
            {
                List<Producao> producoes = [];

                while (reader.Peek() >= 0)
                {
                    var linha = reader.ReadLine();

                    string id = linha.Substring(0, 5);
                    string dataProducao = linha.Substring(5, 8);
                    string cdb = linha.Substring(13, 13);
                    string quantidade = linha.Substring(21, 3);

                    Producao producao = new(
                        id,
                        dataProducao,
                        cdb,
                        quantidade
                        );

                    producoes.Add(producao);
                }
                reader.Close();
                return producoes;

            }
        }

        public void GravarArquivoProducao()
        {
            StreamWriter sw = new(CriarArquivosProducao());

            using (sw)
            {
                foreach (var producao in this.Producoes)
                {
                    sw.WriteLine(producao.ToFile());
                }
                sw.Close();
            }
        }

        // falta validacao de princípio ativo e verificacao de producao existente
        public void IncluirItemProducao(int idProducao)
        {
            Console.Write("Informe o ID do princípio ativo: ");
            var idPrincipioAtivo = Console.ReadLine() ?? "";

            while (idPrincipioAtivo == "")
            {
                Console.WriteLine("Erro. Informe um ID valido!\n");
                Console.Write("Informe o ID do princípio ativo: ");
                idPrincipioAtivo = Console.ReadLine() ?? "";
            }

            Console.Write("Informe a quantidade em gramas do princípio ativo: (max: 9999) ");
            if (!int.TryParse(Console.ReadLine(), out var quantidadePrincipio))
            {
                Console.WriteLine("Erro. Informe uma quantidade valida!\n");
                Console.Write("Informe a quantidade em gramas do princípio ativo: (max: 9999) ");
                while (!int.TryParse(Console.ReadLine(), out quantidadePrincipio))
                {
                    Console.WriteLine("Erro. Informe uma quantidade valida!\n");
                    Console.Write("Informe a quantidade em gramas do princípio ativo: (max: 9999) ");
                }
            }

            while (quantidadePrincipio > 9999 || quantidadePrincipio <= 0)
            {
                Console.WriteLine("Erro. A quantidade deve ser menor que 10000 e maior que zero!\n");
                Console.Write("Informe a quantidade em gramas do princípio ativo: (max: 9999) ");
                while (!int.TryParse(Console.ReadLine(), out quantidadePrincipio))
                {
                    Console.WriteLine("Erro. Informe uma quantidade valida!\n");
                    Console.Write("Informe a quantidade em gramas do princípio ativo: (max: 9999) ");
                }
            }

            ItemProducao itemProducao = new ItemProducao(idProducao, idPrincipioAtivo, quantidadePrincipio);
            ItensProducao.Add(itemProducao);

        }
        public void LocalizarItemProducao()
        {
            Console.Write("Informe o ID da produção: ");
            if (!int.TryParse(Console.ReadLine(), out var idProducao))
            {
                Console.WriteLine("Erro. Informe um ID válido!\n");
                Console.Write("Informe o ID da produção: ");
                while (!int.TryParse(Console.ReadLine(), out idProducao))
                {
                    Console.WriteLine("Erro. Informe um ID válido!\n");
                    Console.Write("Informe o ID da produção: ");
                }
            }
            // logica para ver  se a producao existe
            //Console.WriteLine(idProducao) //Mostrar os dados da producao

            var itensProducao = ItensProducao.FirstOrDefault(x => x.IdProducao == idProducao);

            Console.WriteLine(itensProducao);
        }
        public void AlterarItemProducao()
        {
            Console.Write("Informe o ID da produção: ");
            if (!int.TryParse(Console.ReadLine(), out var idProducao))
            {
                Console.WriteLine("Erro. Informe um ID válido!\n");
                Console.Write("Informe o ID da produção: ");
                while (!int.TryParse(Console.ReadLine(), out idProducao))
                {
                    Console.WriteLine("Erro. Informe um ID válido!\n");
                    Console.Write("Informe o ID da produção: ");
                }
            }
            // logica para ver  se a producao existe
            //Console.WriteLine(idProducao) //Mostrar os dados da producao

            var itensProducao = ItensProducao.FirstOrDefault(x => x.IdProducao == idProducao);

            Console.WriteLine(itensProducao);

            Console.WriteLine("Caso não queira fazer alteração, aperte o enter!");
            Console.Write("Informe o novo ID do Princípio: ");
            var principioAtivo = Console.ReadLine() ?? "";

            if (principioAtivo == "")
                principioAtivo = itensProducao.Principio;

            // logica para ver se o principio ativo existe e se esta ativo

            Console.Write("Informe a quantidade em gramas do princípio ativo: (max: 9999) ");
            var quantidadePrincipio = Console.ReadLine() ?? "";

            if (quantidadePrincipio == "")
                quantidadePrincipio = itensProducao.QuantidadePrincipio.ToString();

            if (!int.TryParse(quantidadePrincipio, out var quantidade))
            {
                Console.WriteLine("Erro. Informe uma quantidade valida!\n");
                Console.Write("Informe a quantidade em gramas do princípio ativo: (max: 9999) ");
                while (!int.TryParse(Console.ReadLine(), out quantidade))
                {
                    Console.WriteLine("Erro. Informe uma quantidade valida!\n");
                    Console.Write("Informe a quantidade em gramas do princípio ativo: (max: 9999) ");
                }
            }

            while (quantidade > 9999 || quantidade <= 0)
            {
                Console.WriteLine("Erro. Informe uma quantidade valida!\n");
                Console.Write("Informe a quantidade em gramas do princípio ativo: (max: 9999) ");
                while (!int.TryParse(Console.ReadLine(), out quantidade))
                {
                    Console.WriteLine("Erro. Informe uma quantidade valida!\n");
                    Console.Write("Informe a quantidade em gramas do princípio ativo: (max: 9999) ");
                }
            }

            itensProducao.SetPrincipio(principioAtivo);
            itensProducao.SetQuantidadePrincipio(quantidade);

            Console.WriteLine("Alteração concluída com sucesso!");
            Console.WriteLine(itensProducao);
        }
        public void ImprimirItemProducao()
        {
            if (ItensProducao is null)
                Console.WriteLine("Não existe item de produção");
            else
                ItensProducao.ForEach(x => Console.WriteLine(x));
        }

        public string CriarArquivosItemProducao()
        {
            string diretorio = @"C:\SneezePharma\Files";
            string arquivoItemCompra = "ProduceItem.data";

            var diretorioItemCompra = Path.Combine(diretorio, arquivoItemCompra);
            if (!File.Exists(diretorioItemCompra))
            {
                using StreamWriter sw = File.CreateText(diretorioItemCompra);
                Console.WriteLine("Arquivo criado com sucesso");
                Console.ReadKey();
            }

            return diretorioItemCompra;
        }
        public List<ItemProducao> LerArquivoItemProducao()
        {
            var caminho = CriarArquivosItemProducao();

            StreamReader reader = new(caminho);
            using (reader)
            {
                List<ItemProducao> itensProducao = new();

                while (reader.Peek() >= 0)
                {
                    var linha = reader.ReadLine();

                    string idProducao = linha.Substring(0, 5);
                    string idPrincipio = linha.Substring(5, 6);
                    string quantidadePrincipio = linha.Substring(11, 4);

                    ItemProducao itemCompra = new(
                        int.Parse(idProducao),
                        idPrincipio,
                        int.Parse(quantidadePrincipio)
                        );

                    itensProducao.Add(itemCompra);
                }
                reader.Close();
                return itensProducao;
            }

        }
        public void GravarArquivoItemProducao()
        {
            var caminho = CriarArquivosItemProducao();

            StreamWriter writer = new(caminho);
            using (writer)
            {
                foreach (var item in ItensProducao)
                {
                    writer.WriteLine(item.ToFile());
                }
                writer.Close();
            }
        }
    }
}
