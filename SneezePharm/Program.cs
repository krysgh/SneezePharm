using System.Collections.Generic;
using SneezePharm;
using SneezePharm.Menu;
using SneezePharm.PastaMedicamento;

string diretorio = @"C:\SneezePharma\Files\";
if (!Directory.Exists(diretorio))
{
    Directory.CreateDirectory(diretorio);
}

SistemaSneezePharm sneeze = new();

sneeze.CriarTodosArquivos();

var menuPrincipal = new SistemaMenuPrincipal();
var working = true;
do
{
    //Console.Clear();
    var opcao = Menu.Menus(menuPrincipal.Titulo, menuPrincipal.Opcoes);
    switch (opcao)
    {
        // Menu cliente
        case 1:
            sneeze.ServicosCliente.Menu.MenuCliente(sneeze.ServicosCliente);
            break;
        // Menu Fornecedor
        case 2:
            sneeze.ServicosFornecedor.Menu.MenuFornecedor(sneeze.ServicosFornecedor);
            break;

        // Menu Principio Ativo
        case 3:
            sneeze.ServicosPrincipioAtivo.Menu.MenuPrincipioAtivo(sneeze.ServicosPrincipioAtivo);
            break;

        // Menu Medicamento
        case 4:
            sneeze.ServicosMedicamento.Menu.MenuMedicamento(sneeze.ServicosMedicamento);
            break;

        // Menu Venda
        case 5:
            sneeze.ServicosVenda.Menu.MenuVenda(sneeze.ServicosVenda, sneeze.ServicosCliente, sneeze.ServicosMedicamento, sneeze.ServicosProducao.Producoes);
            break;

        // Menu Compra
        case 6:
            sneeze.ServicosCompra.Menu.MenuCompra(sneeze.ServicosCompra, sneeze.ServicosFornecedor, sneeze.ServicosPrincipioAtivo.PrincipiosAtivos);
            break;
        // Menu Producao
        case 7:
            sneeze.ServicosProducao.Menu.MenuProducao(sneeze.ServicosProducao, sneeze.ServicosMedicamento.Medicamentos, sneeze.ServicosPrincipioAtivo.PrincipiosAtivos);
            break;

        // Sair
        case 8:
            working = false;
            Console.WriteLine("Saindo...");
            sneeze.GravarTodosArquivos();
            Console.ReadKey();
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Comando não encontrado!");
            Console.ResetColor();
            Console.ReadKey();
            break;
    }
} while (working);

