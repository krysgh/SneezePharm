using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm
{
    public static class Menu
    {
        public static int Menus(string titulo, List<string> opcao)
        {
            Console.WriteLine(titulo);
            for(int i = 0; i < opcao.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {opcao[i]}");
            }
            Console.WriteLine("Escolha uma opção valida: ");
            return int.Parse( Console.ReadLine() ?? "0");
        }
    }
}
