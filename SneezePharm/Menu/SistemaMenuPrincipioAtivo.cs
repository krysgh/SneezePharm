using SneezePharm.PastaPrincipioAtivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.Menu
{
    public class SistemaMenuPrincipioAtivo
    {
        public string Titulo { get; private set; }
        public List<string> Opcoes { get; private set; }
        public int Opcao { get; private set; }

        public SistemaMenuPrincipioAtivo()
        {
            Titulo = "=== Menu Principio Ativo ===";
            Opcoes = [
                "Cadastrar Principio Ativo",
                "Localizar Principio Ativo",
                "Alterar Principio Ativo",
                "Imprimir Principios Ativos",
                "Voltar ao Menu Principal"
                ];
        }

        public void MenuPrincipioAtivo(ServicosPrincipioAtivo principioAtivo)
        {
            do
            {
                Opcao = Menu.Menus(Titulo, Opcoes);

                switch (Opcao)
                {
                    case 1:
                        principioAtivo.IncluirPrincipioAtivo();
                        break;
                    case 2:
                        principioAtivo.LocalizarPrincipioAtivo();
                        break;
                    case 3:
                        principioAtivo.AlterarPrincipioAtivo();
                        break;
                    case 4:
                        principioAtivo.ImprimirPrincipiosAtivos();
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
