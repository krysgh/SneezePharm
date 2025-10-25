using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.PastaFornecedor
{
    public class ServicosFornecedor
    {
        public List<Fornecedor> Fornecedores { get; set; } = [];
        public List<string> FornecedoresBloqueados { get; set; } = [];

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

        public Fornecedor? LocalizarFornecedor(string cnpj)
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
                var confirma = Console.ReadLine() ?? "0";
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
                    var confirma = Console.ReadLine() ?? "0";
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
                    var confirma = Console.ReadLine() ?? "0";
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
            Console.WriteLine("-=-=- Lista de Fornecedores Bloqueados -=-=-");
            if (FornecedoresBloqueados.Count == 0)
            {
                Console.WriteLine("Nenhum fornecedor bloqueado.");
            }
            foreach (var cnpj in FornecedoresBloqueados)
            {
                Console.WriteLine(LocalizarFornecedor(cnpj));
            }
        }

        public string CriarArquivosFornecedor()
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

        public string CriarArquivosFornecedoresBloqueados()
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
            StreamReader reader = new(CriarArquivosFornecedor());
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
            StreamReader reader = new(CriarArquivosFornecedoresBloqueados());
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
            StreamWriter sw = new StreamWriter(CriarArquivosFornecedor());

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
            StreamWriter sw = new StreamWriter(CriarArquivosFornecedoresBloqueados());

            using (sw)
            {
                foreach (var bloqueado in this.FornecedoresBloqueados)
                {
                    sw.WriteLine(bloqueado);
                }
                sw.Close();
            }
        }
    }
}