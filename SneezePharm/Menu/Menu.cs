using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.Menu
{
    public static class Menu
    {
        public static int Menus(string titulo, List<string> opcoes)
        {
            Console.WriteLine(titulo);
            for (int i = 0; i < opcoes.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {opcoes[i]}");
            }
            Console.Write("\nEscolha uma opção valida: ");
            if (!int.TryParse(Console.ReadLine(), out var opcao))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro. Digite uma das opcões!\n");
                Console.ResetColor();
                Console.Write("\nEscolha uma opção valida: ");
                while (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erro. Digite uma das opcões!\n");
                    Console.ResetColor();
                    Console.Write("\nEscolha uma opção valida: ");
                }
            }
            Console.WriteLine();
            return opcao;
        }
    }
}
