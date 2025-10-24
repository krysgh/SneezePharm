using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm
{
    public class Sistema
    {

        public List<PrincipioAtivo> principiosAtivos = new List<PrincipioAtivo>();
        public List<Medicamento> medicamentos = new List<Medicamento>();
        

        public Sistema() { 
            principiosAtivos = LerArquivoPrincipioAtivo();
            medicamentos = LerArquivoMedicamento();
        }

        #region MenuMedicamento
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

                if(inputSituacao == "A" || inputSituacao == "I")
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

                if(inputCategoria == "A" || inputCategoria == "B" || inputCategoria == "I" || inputCategoria == "V")
                {
                    categoria = inputCategoria[0];
                    break;
                }
                else
                {
                    Console.WriteLine("Situação invalida, digite apenas 'A', 'B', 'I' ou 'V'");
                }

            }while (true);

            do
            {
                Console.WriteLine("Digite o valor da venda: ");
                string inputValor = Console.ReadLine();

                if(decimal.TryParse(inputValor, out valorVenda) && valorVenda > 0 && valorVenda <= 9999.99m && inputValor.Length <= 7)
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

            }while (true);

            Medicamento novoMed = new Medicamento(nome, categoria, valorVenda, situacao);

            medicamentos.Add(novoMed);

            Console.WriteLine("\nRegistro adicionado com sucesso: ");
            Console.WriteLine(novoMed.ToString());

            GravarArquivoMedicamento();
        }

        public void LocalizarMedicamento()
        {
            Console.WriteLine("Digite o codigo de barras do medicamento: ");
            string cdb = Console.ReadLine();

            Medicamento achado = medicamentos.Find(m => m.CDB == cdb);

            if(achado != null)
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

            Medicamento achado = medicamentos.Find(m => m.CDB == cdb);

            if(achado != null)
            {
                Console.WriteLine("Medicamento foi achado: ");

                char novaSituacao;
                string inputSituacao;

                do
                {
                    Console.WriteLine("Digite a nova situação ('A' para Ativo, 'I' para Inativo): ");
                    inputSituacao = Console.ReadLine().ToUpper();

                    if(inputSituacao == "A" || inputSituacao == "I")
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
            if(medicamentos.Count == 0)
            {
                Console.WriteLine("Não tem medicamentos cadastrados.");
                return;
            }

            Console.WriteLine("Lista de todos os medicamntos: ");
            foreach(var medicamento in medicamentos)
            {
                Console.WriteLine(medicamento.ToString());
            }
        }

        public void ImprimirMedicamentosPorSituacao(char situacao)
        {
            var medicamentosFiltrados = medicamentos.Where(p => p.Situacao == situacao).ToList();

            if(medicamentosFiltrados.Count == 0)
            {
                Console.WriteLine($"Não tem medicamentos com a situacao '{situacao}'");
                return;
            }

            Console.WriteLine($"Lista de medicamentos com a situação '{situacao}'");
            foreach(var medicamento in medicamentosFiltrados)
            {
                Console.WriteLine(medicamento.ToString());
            }

        }
        #endregion

        #region MenuPrincipioAtivo
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

            principiosAtivos.Add(novoAtivo);

            Console.WriteLine("\nRegistro adicionado com sucesso!");
            Console.WriteLine(novoAtivo.ToString());

            GravarArquivoPrincipioAtivo();
        }

        public void LocalizarPrincipioAtivo()
        {
            Console.WriteLine("Digite o id do principio ativo: ");
            string id = Console.ReadLine().ToUpper();

            PrincipioAtivo achado = principiosAtivos.Find(p =>p.Id == id);

            if(achado != null)
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

            PrincipioAtivo achado = principiosAtivos.Find(p => p.Id == id);

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
            if (principiosAtivos.Count == 0)
            {
                Console.WriteLine("Não tem principios ativos cadastrados.");
                return;
            }

            Console.WriteLine("Lista de todos os principios ativos:");
            foreach (var principioAtivo in principiosAtivos)
            {
                Console.WriteLine(principioAtivo.ToString());
            }
        }

        public void ImprimirPrincipiosAtivoscPorSituacao(char situacao)
        {
            var ativosFiltrados = principiosAtivos.Where(p => p.Situacao == situacao).ToList();

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
        #endregion

        #region ArquivosMedicamento
        public string CarregarMedicamento()
        {
            string diretorio = @"C:\SneezePharma\Files";
            if (!Directory.Exists(diretorio))
                Directory.CreateDirectory(diretorio);

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
            var caminho = CarregarMedicamento();

            List<Medicamento> listaMed = new();

            StreamReader reader = new(caminho);

            using (reader)
            {
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
                reader.Close();
                return listaMed;
            }

        }

        public void GravarArquivoMedicamento()
        {
            StreamWriter writer = new StreamWriter(CarregarMedicamento());

            using (writer)
            {
                foreach (Medicamento medicamento in medicamentos)
                {
                    writer.WriteLine(medicamento.ToFile());
                }
                writer.Close();
            }
        }
        #endregion

        #region ArquivosPricipiosAtivos
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
                foreach (PrincipioAtivo principioAtivo in principiosAtivos)
                {
                    writer.WriteLine(principioAtivo.ToFile());
                }
                writer.Close();
            }
        }
              
        #region Listas de Dados
        public List<Fornecedor> Fornecedores { get; set; } = [];
        public List<string> FornecedoresBloqueados { get; set; } = [];
        #endregion

        #region Métodos Fornecedor
        public void IncluirFornecedor()
        {
            Console.Write("Insere o cnpj da empresa fornecedor: ");
            string cnpj = Console.ReadLine();
            if (!Fornecedor.ValidarCNPJ(cnpj))
            {
                Console.WriteLine("CNPJ inválido! Retornando para menu...");
                return;

            }
            if (LocalizarFornecedor(cnpj) is not null)
            {
                Console.WriteLine("CNPJ já cadastrada! Retornando para menu...");
                return;
            }

            Console.Write("Insere a razão social da empresa: ");
            string razaoSocial = Console.ReadLine();
            if (razaoSocial.Length > 50)
            {
                razaoSocial = razaoSocial.Substring(0, 50);
            }

            Console.Write("Insere o país que a empresa está localizada: ");
            string pais = Console.ReadLine();
            if (pais.Length > 20)
            {
                pais = pais.Substring(0, 20);
            }

            Console.Write("Insere a data de abertura da empresa (formato ddMMyyyy): ");
            if (!DateOnly.TryParseExact(Console.ReadLine(), "ddMMyyyy", out var dataAbertura))
            {
                Console.WriteLine("Data inválida! Cancelando operação...");
                return;
            }
            if (!Fornecedor.ValidarDataAbertura(dataAbertura))
            {
                Console.WriteLine("A empresa precisa ter pelo menos 2 anos de abertura! Cancelando operação...");
                return;
            }

            Console.Write("Insere a data do último fornecimento (formato ddMMyyyy): ");
            if (!DateOnly.TryParseExact(Console.ReadLine(), "ddMMyyyy", out var dataFornecimento))
            {
                Console.WriteLine("Data inválida! Cancelando operação.");
                return;
            }
            if (dataFornecimento > DateOnly.FromDateTime(DateTime.Now))
            {
                Console.WriteLine("A data do último fornecimento não pode ser no futuro. Cancelando operação...");
                return;
            }

            Fornecedores.Add(new(cnpj, razaoSocial, pais, dataAbertura, dataFornecimento));
        }

        private Fornecedor? LocalizarFornecedor(string cnpj)
        {
            return Fornecedores.Find(f => f.Cnpj == cnpj);
        }

        public void ImprimirFornecedorLocalizado()
        {
            Console.WriteLine("Insere o CNPJ do fornecedor: ");
            string cnpj = Console.ReadLine();
            var fornecedor = LocalizarFornecedor(cnpj);
            if (fornecedor is null)
            {
                Console.WriteLine("\nCNPJ não encontrado.");
            }
            else
            {
                Console.WriteLine("\n\tFornecedor\n" + fornecedor);
            }
        }

        public void AlterarFornecedor() 
        {
            Console.WriteLine("Insere o CNPJ do fornecedor: ");
            string cnpj = Console.ReadLine();
            var fornecedor = LocalizarFornecedor(cnpj);
            if (fornecedor is not null)
            {
                Console.WriteLine($"Deseja mesmo alterar a situação do fornecedor {fornecedor.RazaoSocial}? (0 - cancelar, 1 - confirmar)");
                var confirma = Console.ReadLine() ?? "2";
                if (confirma == "1")
                {
                    fornecedor.AlterarSituacao();
                    Console.WriteLine("\nSituação do fornecedor alterado com sucesso!");
                }
                else 
                {
                    Console.WriteLine("\nOperação cancelada. Retornando para menu...");
                }
            }
            else
            {
                Console.WriteLine("\nCNPJ não encontrado.");
            }
        }

        public void ImprimirFornecedores()
        {
            Console.WriteLine("-=-=- Lista de Fornecedores -=-=-");
            if (Fornecedores.Count == 0)
            {
                Console.WriteLine("Nenhum fornecedor cadastrado.");
            }
            foreach (var fornecedor in Fornecedores)
            {
                Console.WriteLine(fornecedor + "\n");
            }
        }

        public void BloquearFornecedor()
        {
            Console.WriteLine("Insere o CNPJ do fornecedor: ");
            string cnpj = Console.ReadLine();
            var fornecedor = LocalizarFornecedor(cnpj);
            if (fornecedor is not null)
            {
                if (!FornecedoresBloqueados.Contains(cnpj))
                {
                    Console.WriteLine($"Deseja mesmo bloquear o fornecedor {fornecedor.RazaoSocial}? (0 - cancelar, 1 - confirmar)");
                    var confirma = Console.ReadLine() ?? "";
                    if (confirma == "1")
                    {
                        FornecedoresBloqueados.Add(fornecedor.Cnpj);
                        Console.WriteLine("\nFornecedor bloqueado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("Operação cancelada. Retornando para menu...");
                    }
                }
                else
                {
                    Console.WriteLine("\nEsse fornecedor já está bloqueado.");
                }
            }
            else
            {
                Console.WriteLine("\nCNPJ não encontrado.");
            }
            
        }

        public void DesbloquearFornecedor()
        {
            Console.WriteLine("Insere o CNPJ do fornecedor: ");
            string cnpj = Console.ReadLine();
            var fornecedor = LocalizarFornecedor(cnpj);
            if (fornecedor is not null)
            {
                if (FornecedoresBloqueados.Contains(cnpj))
                {
                    Console.WriteLine($"Deseja mesmo desbloquear o fornecedor {fornecedor.RazaoSocial}? (0 - cancelar, 1 - confirmar)");
                    var confirma = Console.ReadLine() ?? "";
                    if (confirma == "1")
                    {
                        FornecedoresBloqueados.Remove(fornecedor.Cnpj);
                        Console.WriteLine("\nFornecedor desbloqueado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("\nOperação cancelada. Retornando para menu...");
                    }
                }
                else
                {
                    Console.WriteLine("\nEsse fornecedor não está bloqueado.");
                }
            }
            else
            {
                Console.WriteLine("\nCNPJ não encontrado.");
            }
        }

        public void ImprimirFornecedoresBloqueados()
        {
            Console.WriteLine("-=-=- Lista de Fornecedores  Bloqueados -=-=-");
            if (FornecedoresBloqueados.Count == 0)
            {
                Console.WriteLine("Nenhum fornecedor bloqueado.");
            }
            foreach (var cnpj in FornecedoresBloqueados)
            {
                Console.WriteLine(LocalizarFornecedor(cnpj));
            }
        }
        #endregion

        #region Métodos Arquivos
        private string CarregarFornecedor()
        {
            string diretorio = @"C:\SneezePharma\Files";
            string arquivoFornecedor = "Suppliers.data";
            var diretorioFornecedor = Path.Combine(diretorio, arquivoFornecedor);
            if (!File.Exists(diretorioFornecedor))
            {
                using (StreamWriter sw = File.CreateText(diretorioFornecedor))
                {
                    Console.WriteLine("Arquivo criado com sucesso");
                    Console.ReadKey();
                }
            }
            return diretorioFornecedor;
        }

        private string CarregarFornecedorBloqueado()
        {
            string diretorio = @"C:\SneezePharma\Files";
            string arquivoFornecedor = "RestrictedSuppliers.data";
            var diretorioFornecedor = Path.Combine(diretorio, arquivoFornecedor);
            if (!File.Exists(diretorioFornecedor))
            {
                using (StreamWriter sw = File.CreateText(diretorioFornecedor))
                {
                    Console.WriteLine("Arquivo criado com sucesso");
                    Console.ReadKey();
                }
            }
            return diretorioFornecedor;
        }

        public void LerArquivoFornecedor()
        {
            StreamReader reader = new(CarregarFornecedor());
            using (reader)
            {
                List<Fornecedor> fornecedores = new();

                while (reader.Peek() >= 0)
                {
                    var linha = reader.ReadLine();

                    string cnpj = linha.Substring(0, 14);
                    string nome = linha.Substring(14, 50);
                    string pais = linha.Substring(64, 20);
                    string dataAbertura = linha.Substring(84, 8);
                    string dataFornecimento = linha.Substring(92, 8);
                    string dataCadastro = linha.Substring(100, 8);
                    string situacao = linha.Substring(108);


                    Fornecedor fornecedor = new(
                        cnpj,
                        nome,
                        pais,
                        dataAbertura,
                        dataFornecimento,
                        dataCadastro,
                        situacao
                        );

                    fornecedores.Add(fornecedor);
                }
                reader.Close();
                this.Fornecedores = fornecedores;
            }

        }

        public void LerArquivoFornecedorBloqueado()
        {
            StreamReader reader = new(CarregarFornecedorBloqueado());
            using (reader)
            {
                List<string> bloqueados = new();

                while (reader.Peek() >= 0)
                {
                    var linha = reader.ReadLine();

                    bloqueados.Add(linha);
                }
                reader.Close();
                this.FornecedoresBloqueados = bloqueados;
            }

        }

        public void GravarArquivoFornecedor()
        {
            StreamWriter sw = new StreamWriter(CarregarFornecedor());

            using (sw)
            {
                foreach (var fornecedor in this.Fornecedores)
                {
                    sw.WriteLine(fornecedor.ToFile());
                }
                sw.Close();
            }
        }

        public void GravarArquivoFornecedorBloqueado()
        {
            StreamWriter sw = new StreamWriter(CarregarFornecedorBloqueado());

            using (sw)
            {
                foreach (var bloqueado in this.FornecedoresBloqueados)
                {
                    sw.WriteLine(bloqueado);
                }
                sw.Close();
            }
        }
        #endregion
    }
}
