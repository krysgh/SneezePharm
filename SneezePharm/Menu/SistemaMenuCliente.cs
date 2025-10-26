using SneezePharm.PastaCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.Menu
{
    public class SistemaMenuCliente
    {
        public string Titulo { get; private set; }
        public List<string> Opcoes { get; private set; }
        public int Opcao { get; private set; }

        public SistemaMenuCliente()
        {
            Titulo = "=== Menu Cliente ===";
            Opcoes = [
                "Cadastrar Cliente",
                "Localizar Cliente",
                "Alterar Cliente",
                "Imprimir Clientes",
                "Bloquear Cliente",
                "Desbloquear Cliente",
                "Imprimir Clientes Bloquedos",
                "Voltar ao Menu Principal"
                ];

        }

        public void MenuCliente(ServicosCliente cliente)
        { 
            do
            {
                Console.Clear();
                Opcao = Menu.Menus(Titulo, Opcoes);
                switch (Opcao)
                {
                    case 1:
                        cliente.IncluirCliente();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\nCliente Adicionado com sucesso!");
                        Console.ResetColor();
                        break;
                    case 2:
                        cliente.LocalizarCliente();
                        break;
                    case 3:
                        cliente.AlterarCliente();
                        break;
                    case 4:
                        cliente.ImprimirClientes();
                        break;
                    case 5:
                        cliente.BloquearCliente();
                        break;
                    case 6:
                        cliente.DesbloquearCliente();
                        break;
                    case 7:
                        cliente.ImprimirClienteBloqueado();
                        break;
                    case 8:
                        Console.WriteLine("Voltando ao menu principal...");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Comando nao encontrado!");
                        Console.ResetColor();
                        break;
                }
                Console.ReadKey();

            } while (Opcao != 8);
        }

    }
}
