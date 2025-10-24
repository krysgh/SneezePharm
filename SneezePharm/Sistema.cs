using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm
{
    public class Sistema
    {

        public List<PrincipioAtivo> PrincipiosAtivos { get; set; } = [];
        public List<Medicamento> Medicamentos { get; set; } = [];
        public List<Compra> Compras { get; private set; } = [];
        public List<ItemCompra> ItensCompra { get; private set; } = [];

        public Sistema() { 
            PrincipiosAtivos = LerArquivoPrincipioAtivo();
            Medicamentos = LerArquivoMedicamento();
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

            Medicamento achado = Medicamentos.Find(m => m.CDB == cdb);

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
            if(Medicamentos.Count == 0)
            {
                Console.WriteLine("Não tem medicamentos cadastrados.");
                return;
            }

            Console.WriteLine("Lista de todos os medicamntos: ");
            foreach(var medicamento in Medicamentos)
            {
                Console.WriteLine(medicamento.ToString());
            }
        }

        public void ImprimirMedicamentosPorSituacao(char situacao)
        {
            var medicamentosFiltrados = Medicamentos.Where(p => p.Situacao == situacao).ToList();

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

            PrincipiosAtivos.Add(novoAtivo);

            Console.WriteLine("\nRegistro adicionado com sucesso!");
            Console.WriteLine(novoAtivo.ToString());

            GravarArquivoPrincipioAtivo();
        }

        public void LocalizarPrincipioAtivo()
        {
            Console.WriteLine("Digite o id do principio ativo: ");
            string id = Console.ReadLine().ToUpper();

            PrincipioAtivo achado = PrincipiosAtivos.Find(p =>p.Id == id);

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
                while(reader.Peek() >= 0)
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
            StreamWriter writer = new StreamWriter(CarregarMedicamento());

            using (writer)
            {
                foreach (Medicamento medicamento in Medicamentos)
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
                foreach (PrincipioAtivo principioAtivo in PrincipiosAtivos)
                {
                    writer.WriteLine(principioAtivo.ToFile());
                }
                writer.Close();
            }
        }

        #endregion

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

        #region Métodos Compra

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

            if(fornecedor is null)
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
        #endregion
    }
}
