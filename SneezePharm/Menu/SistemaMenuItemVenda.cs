using SneezePharm.PastaCliente;
using SneezePharm.PastaMedicamento;
using SneezePharm.PastaPrincipioAtivo;
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

        public void MenuItemVenda(ServicosVenda itemVenda,ServicosCliente clientes, List<Medicamento> medicamentos )
        {
            do
            {
                Opcao = Menu.Menus(Titulo, Opcoes);

                switch (Opcao)
                {
                    case 1:
                        itemVenda.IncluirVenda(clientes, medicamentos);
                        break;
                    case 2:
                        itemVenda.LocalizarItemVenda(medicamentos);
                        break; 
                    case 3:
                        itemVenda.AlterarItemVenda(medicamentos);
                        break;
                    case 4:
                        itemVenda.ImprimirItemVenda();
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
