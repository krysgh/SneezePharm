using SneezePharm.PastaMedicamento;
using SneezePharm.PastaPrincipioAtivo;
using SneezePharm.PastaProducao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.Menu
{
    public class SistemaMenuProducao
    {
        public string Titulo { get; private set; }
        public List<string> Opcoes { get; private set; }
        public int Opcao { get; private set; }

        public SistemaMenuProducao()
        {
            Titulo = "=== Menu Produção ===";
            Opcoes = [
                "Cadastrar Produção",
                "Localizar Produção",
                "Alterar Produção",
                "Imprimir Produção",
                "Sair"
                ];

        }

        public void MenuProducao(ServicosProducao producao, List<Medicamento> medicamentos, List<PrincipioAtivo> principios)
        {
            do
            {
                Opcao = Menu.Menus(Titulo, Opcoes);

                switch (Opcao)
                {
                    case 1:
                        producao.IncluirProducao(medicamentos, principios);
                        break;
                    case 2:
                        producao.ImprimirProducaoLocalizado(medicamentos, principios);
                        break;
                    case 3:
                        producao.AlterarProducao();
                        break;
                    case 4:
                        producao.ImprimirProducoes(principios);
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
