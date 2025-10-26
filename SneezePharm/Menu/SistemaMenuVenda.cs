using SneezePharm.PastaVenda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.Menu
{
    public class SistemaMenuVenda
    {
        public string Titulo { get; private set; }
        public List<string> Opcoes { get; private set; }
        public int Opcao { get; private set; }

        public SistemaMenuVenda()
        {
            Titulo = "=== Menu Venda ===";
            Opcoes = [
                "Cadastrar Venda",
                "Localizar Venda",
                "Alterar Venda",
                "Imprimir Vendas",
                "Voltar ao Menu Principal"
                ];
        }

        public void MenuVenda(ServicosVenda venda)
        {
            do
            {
                Opcao = Menu.Menus(Titulo, Opcoes);

                switch (Opcao)
                {
                    case 1:
                        Console.WriteLine("Cadastrar Venda...");
                        break;
                    case 2:
                        Console.WriteLine("Localizar Venda...");
                        break;
                    case 3:
                        Console.WriteLine("Alterar Venda...");
                        break;
                    case 4:
                        Console.WriteLine("Imprimir Vendas...");
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
