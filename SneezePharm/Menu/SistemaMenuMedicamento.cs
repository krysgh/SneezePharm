using SneezePharm.PastaMedicamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.Menu
{
    public class SistemaMenuMedicamento
    {
        public string Titulo { get; private set; }
        public List<string> Opcoes { get; private set; }
        public int Opcao { get; private set; }

        public SistemaMenuMedicamento()
        {
            Titulo = "=== Menu Medicamento ===";
            Opcoes = [
                "Cadastrar Medicamento",
                "Localizar Medicamento",
                "Alterar Medicamento",
                "Imprimir Medicamentos",
                "Voltar ao Menu Principal"
                ];
        }

        public void MenuMedicamento(ServicosMedicamento medicamento)
        {
            do
            {
                Opcao = Menu.Menus(Titulo, Opcoes);

                switch (Opcao)
                {
                    case 1:
                        medicamento.IncluirMedicamento();
                        break;
                    case 2:
                        medicamento.LocalizarMedicamento();
                        break;
                    case 3:
                        medicamento.AlterarMedicamento();
                        break;
                    case 4:
                        medicamento.ImprimirMedicamentos();
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
