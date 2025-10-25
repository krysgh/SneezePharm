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
        private string CarregarProducao()
        {
            string diretorio = @"C:\SneezePharma\Files";
            string arquivoProducao = "Produce.data";
            var diretorioProducao = Path.Combine(diretorio, arquivoProducao);
            if (!File.Exists(diretorioProducao))
            {
                using (StreamWriter sw = File.CreateText(diretorioProducao))
                {
                    Console.WriteLine("Arquivo criado com sucesso");
                    Console.ReadKey();
                }
            }
            return diretorioProducao;
        }
        public void LerArquivoProducao()
        {
            StreamReader reader = new(CarregarProducao());
            using (reader)
            {
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

                    Producoes.Add(producao);
                }
                reader.Close();

            }
        }

        public void GravarArquivoProducao()
        {
            StreamWriter sw = new(CarregarProducao());

            using (sw)
            {
                foreach (var producao in this.Producoes)
                {
                    sw.WriteLine(producao.ToFile());
                }
                sw.Close();
            }
        }
    }
}
