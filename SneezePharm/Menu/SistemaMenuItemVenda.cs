using SneezePharm.PastaMedicamento;
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
                "Localizar Item da Venda",
                "Alterar Item da Venda",
                "Imprimir Itens da Venda",
                "Voltar ao Menu Principal"
                ];
        }

        public void MenuItemVenda(ServicosVenda itemVenda, List<Medicamento> medicamentos)
        {
            do
            {
                Opcao = Menu.Menus(Titulo, Opcoes);

                switch (Opcao)
                {
                    case 1:
                        itemVenda.LocalizarItemVenda(medicamentos);
                        break;
                    case 2:
                        itemVenda.AlterarItemVenda(medicamentos);
                        break;
                    case 3:
                        itemVenda.ImprimirItemVenda();
                        break;
                    case 4:
                        Console.WriteLine("Voltando ao menu principal...");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Comando não encontrado!");
                        Console.ResetColor();
                        break;
                }
                Console.ReadKey();

            } while (Opcao != 4);
        }
    }
}
