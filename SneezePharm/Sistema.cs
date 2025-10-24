using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm
{
    public class Sistema
    {
        #region Listas de Dados
        public List<Fornecedor> Fornecedores { get; set; } = [];
        public List<string> FornecedoresBloqueados { get; set; } = [];
        public List<Producao> Producoes { get; set; } = [];
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
        #endregion

        #region Métodos Produção
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
                if ((quantidade < 1) || (quantidade > 999)) {
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
        #endregion
    }
}