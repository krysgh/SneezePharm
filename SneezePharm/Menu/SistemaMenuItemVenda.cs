using SneezePharm.PastaVenda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.Menu
{
    public class SistemaMenuItemVenda
    {
        public string Titulo { get; private set; }
        public List<string> Opcoes { get; private set; }
        public int Opcao { get; private set; }

        public SistemaMenuItemVenda()
        {
            Titulo = "=== Menu Item Venda ===";
            Opcoes =  [
                "Cadastrar Item Venda",
                "Localizar Item Venda",
                "Alterar Item Venda",
                "Imprimir Item Vendas",
                "Voltar ao Menu Principal"
                ];
        }

        public void MenuItemVenda(ServicosVenda itemVenda)
        {
            do
            {
                Opcao = Menu.Menus(Titulo, Opcoes);

                switch (Opcao)
                {
                    case 1:
                        Console.WriteLine("Cadastrar Item Venda...");
                        break;
                    case 2:
                        Console.WriteLine("Localizar Item Venda...");
                        break;
                    case 3:
                        Console.WriteLine("Alterar Item Venda...");
                        break;
                    case 4:
                        Console.WriteLine("Imprimir Item Vendas...");
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
