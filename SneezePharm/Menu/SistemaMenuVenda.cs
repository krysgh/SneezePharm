using SneezePharm.PastaVenda;
using SneezePharm.PastaCliente;
using SneezePharm.PastaMedicamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SneezePharm.PastaProducao;

namespace SneezePharm.Menu
{
    public class SistemaMenuVenda
    {
        public string Titulo { get; private set; }
        public List<string> Opcoes { get; private set; }
        public int Opcao { get; private set; }
        public SistemaMenuItemVenda MenuItem { get; private set; }

        public SistemaMenuVenda()
        {
            MenuItem = new SistemaMenuItemVenda();
            Titulo = "=== Menu Venda ===";
            Opcoes = [
                "Cadastrar Venda",
                "Localizar Venda",
                "Imprimir Vendas",
                "Gerar Relatório de Vendas",
                "Item de Venda",
                "Voltar ao Menu Principal"
                ];
        }

        public void MenuVenda(ServicosVenda venda, ServicosCliente clientes, ServicosMedicamento medicamentos, List<Producao> producoes)
        {
            do
            {
                Console.Clear();
                Opcao = Menu.Menus(Titulo, Opcoes);

                switch (Opcao)
                {
                    case 1:
                        venda.IncluirVenda(clientes, medicamentos.Medicamentos, producoes);
                        break;
                    case 2:
                        venda.LocalizarVenda();
                        break;
                    case 3:
                        venda.ImprimirVendas();
                        break;
                    case 4:
                        venda.GerarRelatorioVendas();
                        break;
                    case 5:
                        MenuItem.MenuItemVenda(venda, medicamentos.Medicamentos);
                        break;
                    case 6:
                        Console.WriteLine("Voltando ao menu principal...");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Comando não encontrado!");
                        Console.ResetColor();
                        break;
                }
                Console.ReadKey();

            } while (Opcao != 6);
        }
    }
}
