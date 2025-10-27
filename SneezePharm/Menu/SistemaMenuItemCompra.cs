using SneezePharm.PastaCompra;
using SneezePharm.PastaFornecedor;
using SneezePharm.PastaPrincipioAtivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.Menu
{
    public class SistemaMenuItemCompra
    {
        public string Titulo { get; private set; }
        public List<string> Opcoes { get; private set; }
        public int Opcao { get; private set; }


        public SistemaMenuItemCompra()
        {
            Titulo = "=== Menu Compra ===";
            Opcoes = [ 
                "Cadastrar Item Compra",
                "Localizar Item Compra",
                "Alterar Item Compra",
                "Imprimir Item Compra",
                "Sair"
                ];

        }

        public void MenuItemCompra(ServicosCompra itemCompra, ServicosFornecedor fornecedores, List<PrincipioAtivo> principios)
        {
            do
            {
                Console.Clear();
                Opcao = Menu.Menus(Titulo, Opcoes);

                switch (Opcao)
                {
                    case 1:
                        itemCompra.IncluirCompra(fornecedores, principios);
                        break;
                    case 2:
                        itemCompra.LocalizarItemCompra(principios);
                        break;
                    case 3:
                        itemCompra.AlterarItemCompra(principios);
                        break;
                    case 4:
                        itemCompra.ImprimirItemCompra();
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
