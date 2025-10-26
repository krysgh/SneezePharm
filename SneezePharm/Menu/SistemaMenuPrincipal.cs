using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.Menu
{
    public class SistemaMenuPrincipal
    {
        public string Titulo { get; private set; }
        public List<string> Opcoes { get; private set; }
        

        public SistemaMenuPrincipal()
        {
            Titulo = "=== Menu Principal ===";
            Opcoes = [
                "Menu Clientes",
                "Menu Fornecedores",
                "Menu Principio Ativo",
                "Menu Medicamento",
                "Menu Venda",
                "Menu Item Venda",
                "Menu Compra",
                "Menu Item Compra",
                "Menu Produção",
                "Menu Item Produção",
                "Sair"
                ];
        }

     
    }
}
