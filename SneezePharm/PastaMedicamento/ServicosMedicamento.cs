using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.PastaMedicamento
{
    public class ServicosMedicamento
    {
        public List<Medicamento> Medicamentos { get; set; } = [];

        public ServicosMedicamento()
        {
            Medicamentos = LerArquivoMedicamento();
        }

        public void IncluirMedicamento()
        {
            string nome;
            char situacao;
            char categoria;
            decimal valorVenda;

            do
            {
                Console.WriteLine("Digite o nome do medicameno (alfanumerico e até 40 caracteres): ");
                nome = Console.ReadLine();

                if (nome.Length > 40)
                {
                    Console.WriteLine("Nome invalido, O nome deve ter no maximo 40 caracteres");
                }
                else if (!Medicamento.VerificarSeAlfanumericoMed(nome))
                {
                    Console.WriteLine("Nome invalido, use apenas letras e numeros");
                }


            } while (nome.Length > 40 || !Medicamento.VerificarSeAlfanumericoMed(nome));

            do
            {
                Console.WriteLine("Digite a situação ('A' para Ativo, 'I' para Inativo): ");
                string inputSituacao = Console.ReadLine().ToUpper();

                if (inputSituacao == "A" || inputSituacao == "I")
                {
                    situacao = inputSituacao[0];
                    break;
                }
                else
                {
                    Console.WriteLine("Situação inválida! Digite apenas 'A' ou 'I'");
                }

            } while (true);

            do
            {
                Console.WriteLine("Digite a categoria ('A' para Analgésico, 'B' para Antibiótico, 'I' para Anti-inflamatório, 'V' para Vitamina)");
                string inputCategoria = Console.ReadLine().ToUpper();

                if (inputCategoria == "A" || inputCategoria == "B" || inputCategoria == "I" || inputCategoria == "V")
                {
                    categoria = inputCategoria[0];
                    break;
                }
                else
                {
                    Console.WriteLine("Situação invalida, digite apenas 'A', 'B', 'I' ou 'V'");
                }

            } while (true);

            do
            {
                Console.WriteLine("Digite o valor da venda: ");
                string inputValor = Console.ReadLine();

                if (decimal.TryParse(inputValor, out valorVenda) && valorVenda > 0 && valorVenda <= 9999.99m && inputValor.Length <= 7)
                {
                    if (Medicamento.VerificarValorVenda(valorVenda))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Valor invalido, digite um valor entre 0 e 9999.99");
                    }
                }
                else
                {
                    Console.WriteLine("Valor invalido, o valor deve ser maior que 0 e menor que 9999.99 e também possuir menos de 7 caracteres");
                }

            } while (true);

            Medicamento novoMed = new Medicamento(nome, categoria, valorVenda, situacao);

            Medicamentos.Add(novoMed);

            Console.WriteLine("\nRegistro adicionado com sucesso: ");
            Console.WriteLine(novoMed.ToString());

            GravarArquivoMedicamento();
        }
        public void LocalizarMedicamento()
        {
            Console.WriteLine("Digite o codigo de barras do medicamento: ");
            string cdb = Console.ReadLine();

            Medicamento achado = Medicamentos.Find(m => m.CDB == cdb);

            if (achado != null)
            {
                Console.WriteLine("O medicameto foi achado: ");
                Console.WriteLine(achado.ToString());
            }
            else
            {
                Console.WriteLine("O medicamento não foi achado");
            }
        }
        public void AlterarMedicamento()
        {
            Console.WriteLine("Digite o codigo de barras do medicamento: ");
            string cdb = Console.ReadLine();

            Medicamento achado = Medicamentos.Find(m => m.CDB == cdb);

            if (achado != null)
            {
                Console.WriteLine("Medicamento foi achado: ");

                char novaSituacao;
                string inputSituacao;

                do
                {
                    Console.WriteLine("Digite a nova situação ('A' para Ativo, 'I' para Inativo): ");
                    inputSituacao = Console.ReadLine().ToUpper();

                    if (inputSituacao == "A" || inputSituacao == "I")
                    {
                        novaSituacao = inputSituacao[0];
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Situação invalida, digite apenas  ('A' para Ativo, 'I' para Inativo)");
                    }


                } while (true);
                achado.SetSituacao(novaSituacao);

                decimal novoValorVenda;

                do
                {
                    Console.WriteLine("Digite o novo valor da venda: ");
                    string inputValor = Console.ReadLine();

                    if (decimal.TryParse(inputValor, out novoValorVenda) && novoValorVenda > 0 && novoValorVenda <= 9999.99m && inputValor.Length <= 7)
                    {
                        if (Medicamento.VerificarValorVenda(novoValorVenda))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Valor invalido, digite um valor entre 0 e 9999.99");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Valor invalido, o valor deve ser maior que 0 e menor que 9999.99 e também possuir menos de 7 caracteres");
                    }

                } while (true);
                achado.SetValorVenda(novoValorVenda);

                Console.WriteLine("\nAlteração realizada com sucesso");
                Console.WriteLine(achado.ToString());

                GravarArquivoMedicamento();
            }
            else
            {
                Console.WriteLine("Medicamento não foi achado");
            }

        }

        public void ImprimirMedicamentos()
        {
            Console.WriteLine("Escolha uma opção: \n1 - Imprimir todos os medicamentos\n2 - Imprimir apenas os medicamentos ativos\n3 - Imprimir apenas os medicamentos inativos");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    ImprimirTodosMedicamentos();
                    break;
                case "2":
                    ImprimirMedicamentosPorSituacao('A');
                    break;
                case "3":
                    ImprimirMedicamentosPorSituacao('I');
                    break;
                default:
                    Console.WriteLine("Opção invalida");
                    break;
            }

        }

        public void ImprimirTodosMedicamentos()
        {
            if (Medicamentos.Count == 0)
            {
                Console.WriteLine("Não tem medicamentos cadastrados.");
                return;
            }

            Console.WriteLine("Lista de todos os medicamntos: ");
            foreach (var medicamento in Medicamentos)
            {
                Console.WriteLine(medicamento.ToString());
            }
        }
        public void ImprimirMedicamentosPorSituacao(char situacao)
        {
            var medicamentosFiltrados = Medicamentos.Where(p => p.Situacao == situacao).ToList();

            if (medicamentosFiltrados.Count == 0)
            {
                Console.WriteLine($"Não tem medicamentos com a situacao '{situacao}'");
                return;
            }

            Console.WriteLine($"Lista de medicamentos com a situação '{situacao}'");
            foreach (var medicamento in medicamentosFiltrados)
            {
                Console.WriteLine(medicamento.ToString());
            }

        }
        public string CriarArquivosMedicamento()
        {
            string diretorio = @"C:\SneezePharma\Files";

            string arquivoMedicamento = "Medicine.data";

            var caminho = Path.Combine(diretorio, arquivoMedicamento);
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
        public List<Medicamento> LerArquivoMedicamento()
        {

            var caminho = CriarArquivosMedicamento();

            List<Medicamento> listaMed = new();

            StreamReader reader = new(caminho);

            using (reader)
            {
                while (reader.Peek() >= 0)
                {

                    string linha = reader.ReadLine();
                    string cDB = linha.Substring(0, 13);
                    string nome = linha.Substring(13, 40);
                    string categoria = linha.Substring(53, 1);
                    string valorVenda = linha.Substring(54, 7);
                    string ultimaVenda = linha.Substring(61, 8);
                    string dataCadastro = linha.Substring(69, 8);
                    string situacao = linha.Substring(77, 1);

                    DateOnly uv = DateOnly.ParseExact(ultimaVenda, "ddMMyyyy");
                    DateOnly dc = DateOnly.ParseExact(dataCadastro, "ddMMyyyy");

                    Medicamento medicamento = new(
                        cDB, nome, char.Parse(categoria), decimal.Parse(valorVenda), uv, dc, char.Parse(situacao)
                        );

                    listaMed.Add(medicamento);
                }



            }
            reader.Close();
            return listaMed;
        }

        public void GravarArquivoMedicamento()
        {
            StreamWriter writer = new StreamWriter(CriarArquivosMedicamento());

            using (writer)
            {
                foreach (Medicamento medicamento in Medicamentos)
                {
                    writer.WriteLine(medicamento.ToFile());
                }
                writer.Close();
            }
        }
    }
}
