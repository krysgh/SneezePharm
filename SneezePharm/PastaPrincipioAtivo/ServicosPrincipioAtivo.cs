using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.PastaPrincipioAtivo
{
    public class ServicosPrincipioAtivo
    {
        public List<PrincipioAtivo> PrincipiosAtivos { get; set; } = [];

        public void IncluirPrincipioAtivo()
        {
            string nome;
            char situacao;

            do
            {
                Console.Write("Digite o nome do princípio ativo (alfanumerico e até 20 caracteres): ");
                nome = Console.ReadLine();

                if (nome.Length > 20)
                {
                    Console.WriteLine("Nome inválido! O nome deve ter no máximo 20 caracteres.");
                }
                else if (!PrincipioAtivo.VerificarSeAlfanumerico(nome))
                {
                    Console.WriteLine("Nome inválido! Use apenas letras, números e espaços.");
                }
            } while (nome.Length > 20 || !PrincipioAtivo.VerificarSeAlfanumerico(nome));

            do
            {
                Console.WriteLine("Digite a situação ('A' para Ativo, 'I' para Inativo):");
                string inputSituacao = Console.ReadLine().ToUpper();

                if (inputSituacao == "A" || inputSituacao == "I")
                {
                    situacao = inputSituacao[0];
                    break;
                }
                else
                {
                    Console.WriteLine("Situação inválida! Digite apenas 'A' ou 'I'.");
                }
            } while (true);

            PrincipioAtivo novoAtivo = new PrincipioAtivo(nome, situacao);

            PrincipiosAtivos.Add(novoAtivo);

            Console.WriteLine("\nRegistro adicionado com sucesso!");
            Console.WriteLine(novoAtivo.ToString());

            GravarArquivoPrincipioAtivo();
        }

        public void LocalizarPrincipioAtivo()
        {
            Console.WriteLine("Digite o id do principio ativo: ");
            string id = Console.ReadLine().ToUpper();

            PrincipioAtivo achado = PrincipiosAtivos.Find(p => p.Id == id);

            if (achado != null)
            {
                Console.WriteLine("Principio ativo foi achado: ");
                Console.WriteLine(achado.ToString());
            }
            else
            {
                Console.WriteLine("Principio ativo não foi achado");
            }
        }

        public void AlterarPrincipioAtivo()
        {
            Console.Write("Digite o ID do princípio ativo a ser alterado: ");
            string id = Console.ReadLine().ToUpper();

            PrincipioAtivo achado = PrincipiosAtivos.Find(p => p.Id == id);

            if (achado != null)
            {
                Console.WriteLine("Principio ativo foi achado! O que você deseja alterar?");

                char novaSituacao;
                string inputSituacao;
                do
                {
                    Console.Write("Digite a nova situação ('A' para Ativo, 'I' para Inativo): ");
                    inputSituacao = Console.ReadLine().ToUpper();

                    if (inputSituacao == "A" || inputSituacao == "I")
                    {
                        novaSituacao = inputSituacao[0];
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Situação inválida! Digite apenas  ('A' para Ativo, 'I' para Inativo)");
                    }

                } while (true);

                achado.SetSituacao(novaSituacao);

                Console.WriteLine("\nAlteração realizada com sucesso!");
                Console.WriteLine(achado.ToString());

                GravarArquivoPrincipioAtivo();
            }
            else
            {
                Console.WriteLine("Principio ativo não foi achado");
            }
        }

        public void ImprimirPrincipiosAtivos()
        {
            Console.WriteLine("Escolha uma opção: \n1 - Imprimir todos os princípios ativos\n2 - Imprimir apenas os princípios ativos ativos\n3 - Imprimir apenas os princípios ativos inativos");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    ImprimirTodosPrincipiosAtivos();
                    break;
                case "2":
                    ImprimirPrincipiosAtivoscPorSituacao('A');
                    break;
                case "3":
                    ImprimirPrincipiosAtivoscPorSituacao('I');
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }

        public void ImprimirTodosPrincipiosAtivos()
        {
            if (PrincipiosAtivos.Count == 0)
            {
                Console.WriteLine("Não tem principios ativos cadastrados.");
                return;
            }

            Console.WriteLine("Lista de todos os principios ativos:");
            foreach (var principioAtivo in PrincipiosAtivos)
            {
                Console.WriteLine(principioAtivo.ToString());
            }
        }

        public void ImprimirPrincipiosAtivoscPorSituacao(char situacao)
        {
            var ativosFiltrados = PrincipiosAtivos.Where(p => p.Situacao == situacao).ToList();

            if (ativosFiltrados.Count == 0)
            {
                Console.WriteLine($"Não tem princípios ativos com a situação '{situacao}'.");
                return;
            }

            Console.WriteLine($"Lista de principios ativos com a situação: {situacao}");
            foreach (var principioAtivo in ativosFiltrados)
            {
                Console.WriteLine(principioAtivo.ToString());
            }
        }

        public string CarregarPrincipioAtivo()
        {
            string diretorio = @"C:\SneezePharma\Files";
            if (!Directory.Exists(diretorio))
                Directory.CreateDirectory(diretorio);

            string arquivoPrincipioAtivo = "Ingredient.data";
            var caminho = Path.Combine(diretorio, arquivoPrincipioAtivo);

            if (!File.Exists(caminho))
            {
                using (StreamWriter sw = File.CreateText(caminho))
                {
                    Console.WriteLine("Arquivo criado com sucesso");
                    Console.ReadKey();
                }
            }
            return caminho;
        }

        public List<PrincipioAtivo> LerArquivoPrincipioAtivo()
        {
            var caminho = CarregarPrincipioAtivo();

            List<PrincipioAtivo> lista = new();

            StreamReader reader = new(caminho);

            using (reader)
            {

                while (reader.Peek() >= 0)
                {
                    var linha = reader.ReadLine();

                    string id = linha.Substring(0, 6);
                    string nome = linha.Substring(6, 20);
                    string ultimaCompra = linha.Substring(26, 8);
                    string dataCadastro = linha.Substring(34, 8);
                    string situacao = linha.Substring(42, 1);

                    DateOnly uc = DateOnly.ParseExact(ultimaCompra, "ddMMyyyy");
                    DateOnly dc = DateOnly.ParseExact(dataCadastro, "ddMMyyyy");

                    PrincipioAtivo principioAtivo = new PrincipioAtivo(
                        id, nome, uc, dc, char.Parse(situacao)
                        );

                    lista.Add(principioAtivo);
                }
                reader.Close();
                return lista;
            }
        }

        public void GravarArquivoPrincipioAtivo()
        {

            StreamWriter writer = new StreamWriter(CarregarPrincipioAtivo());

            using (writer)
            {
                foreach (PrincipioAtivo principioAtivo in PrincipiosAtivos)
                {
                    writer.WriteLine(principioAtivo.ToFile());
                }
                writer.Close();
            }
        }
    }
}
