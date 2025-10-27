using SneezePharm.PastaCompra;
using SneezePharm.PastaPrincipioAtivo;
using SneezePharm.PastaFornecedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.Menu
{
    public class SistemaMenuCompra
    {
        public string Titulo { get; private set; }
        public List<string> Opcoes { get; private set; }
        public int Opcao { get; private set; }
        public SistemaMenuItemCompra MenuItem { get; private set; }

        public SistemaMenuCompra()
        {
            MenuItem = new SistemaMenuItemCompra();
            Titulo = "=== Menu Compra ===";
            Opcoes = [
                "Cadastrar Compra",
                "Localizar Compra",
                "Imprimir Compra",
                "Item Compra",
                "Sair"
                ];
        }
        public void MenuCompra(ServicosCompra compra, ServicosFornecedor fornecedores, List<PrincipioAtivo> principios)
        {
            do
            {
                Console.Clear();
                Opcao = Menu.Menus(Titulo, Opcoes);

                switch (Opcao)
                {
                    case 1:
                        compra.IncluirCompra(fornecedores, principios);
                        break;
                    case 2:
                        compra.LocalizarCompra();
                        break;
                    case 3:
                        compra.ImprimirCompras();
                        break;
                    case 4:
                        MenuItem.MenuItemCompra(compra, fornecedores, principios);
                        break;
                    case 5:
                        Console.WriteLine("Voltando ao menu principal...");
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Comando não encontrado!");
                        Console.ResetColor();
                        break;
                }
                Console.ReadKey();

            } while (Opcao != 5);
        }
    }
}
