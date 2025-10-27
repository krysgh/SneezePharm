using SneezePharm.PastaFornecedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.Menu
{
    public class SistemaMenuFornecedor
    {
        public string Titulo { get; private set; }
        public List<string> Opcoes { get; private set; }
        public int Opcao { get; private set; }

        public SistemaMenuFornecedor()
        {
            Titulo = "=== Menu Fornecedor ===";
            Opcoes = [
                "Cadastrar Fornecedor",
                "Localizar Fornecedor",
                "Alterar Fornecedor",
                "Imprimir Fornecedores",
                "Bloquear Fornecedor",
                "Desbloquear Fornecedor",
                "Imprimir Fornecedores Bloqueados",
                "Voltar ao Menu Principal"
                ];
        }

        public void MenuFornecedor(ServicosFornecedor fornecedor)
        {
            do
            {
                Console.Clear();
                Opcao = Menu.Menus(Titulo, Opcoes);

                switch (Opcao)
                {
                    case 1:
                        fornecedor.IncluirFornecedor();
                        break;
                    case 2:
                        fornecedor.ImprimirFornecedorLocalizado();
                        break;
                    case 3:
                        fornecedor.AlterarFornecedor();
                        break;
                    case 4:
                        fornecedor.ImprimirFornecedores();
                        break;
                    case 5:
                        fornecedor.BloquearFornecedor();
                        break;
                    case 6:
                        fornecedor.DesbloquearFornecedor();
                        break;
                    case 7:
                        fornecedor.ImprimirFornecedoresBloqueados();
                        break;
                    case 8:
                        Console.WriteLine("Voltando ao menu principal...");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Comando não encontrado!");
                        Console.ResetColor();
                        break;
                }
                Console.ReadKey();

            } while (Opcao != 8);
        }
    }
}
