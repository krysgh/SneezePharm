using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm
{
    public class Sistema
    {
        #region Listas de Dados
        public List<Fornecedor> Fornecedores { get; set; } = [];
        public List<string> FornecedoresBloqueados { get; set; } = [];
        public List<ItemProducao> ItensProducao { get; set; } = [];

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

        // falta validacao de princípio ativo e verificacao de producao existente
        public void IncluirItemProducao(int idProducao)
        {
            Console.Write("Informe o ID do princípio ativo: ");
            var idPrincipioAtivo = Console.ReadLine() ?? "";

            while(idPrincipioAtivo == "")
            {
                Console.WriteLine("Erro. Informe um ID valido!\n");
                Console.Write("Informe o ID do princípio ativo: ");
                idPrincipioAtivo = Console.ReadLine() ?? "";
            }

            Console.Write("Informe a quantidade em gramas do princípio ativo: (max: 9999) ");
            if(!int.TryParse(Console.ReadLine(), out var quantidadePrincipio))
            {
                Console.WriteLine("Erro. Informe uma quantidade valida!\n");
                Console.Write("Informe a quantidade em gramas do princípio ativo: (max: 9999) ");
                while(!int.TryParse(Console.ReadLine(), out quantidadePrincipio))
                {
                    Console.WriteLine("Erro. Informe uma quantidade valida!\n");
                    Console.Write("Informe a quantidade em gramas do princípio ativo: (max: 9999) ");
                }
            }

            while(quantidadePrincipio > 9999 || quantidadePrincipio <= 0)
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
            if(ItensProducao is null)
                Console.WriteLine("Não existe item de produção");
            else
                ItensProducao.ForEach(x => Console.WriteLine(x));
        }

        // carregar item da producao
        public string CarregarItemProducao()
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

        // ler arquivo item da producao
        public List<ItemProducao> LerArquivoItemProducao()
        {
            var caminho = CarregarItemProducao();

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

        // gravar arquivo item da producao
        public void GravarArquivoItemProducao(List<ItemProducao> itensProducao)
        {
            var caminho = CarregarItemProducao();

            StreamWriter writer = new(caminho);
            using (writer)
            {
                foreach (var item in itensProducao)
                {
                    writer.WriteLine(item.ToFile());
                }
                writer.Close();
            }
        }


    }
}