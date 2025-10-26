using SneezePharm.PastaProducao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.Menu
{
    public class SistemaMenuItemProducao
    {
        public string Titulo { get; private set; }
        public List<string> Opcoes { get; private set; }
        public int Opcao { get; private set; }

        public SistemaMenuItemProducao()
        {
            Titulo = "=== Menu Item Produção ===";
            Opcoes = [            
                "Cadastrar Item Produção",
                "Localizar Item Produção",
                "Alterar Item Produção",
                "Imprimir Item Produção",
                "Sair"
                ];

        }

        public void MenuItemProducao(ServicosProducao itemProducao)
        {
            do
            {
                Opcao = Menu.Menus(Titulo, Opcoes);

                switch (Opcao)
                {
                    case 1:
                        itemProducao.IncluirProducao();
                        break;
                    case 2:
                        itemProducao.LocalizarItemProducao();
                        break;
                    case 3:
                        itemProducao.AlterarItemProducao();
                        break;
                    case 4:
                        itemProducao.ImprimirItemProducao();
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
