using SneezePharm;
using SneezePharm.Menu;

SistemaSneezePharm sneeze = new();

sneeze.CriarDiretorio();
sneeze.CriarTodosArquivos();

var menuPrincipal = new SistemaMenuPrincipal();
var working = true;
do
{
    //Console.Clear();
    var opcao = Menu.Menus(menuPrincipal.Titulo, menuPrincipal.Opcoes);
    switch (opcao)
    {
        case 1:
            sneeze.ServicosCliente.Menu.MenuCliente(sneeze.ServicosCliente);
            break;
        case 2:
            sneeze.ServicosFornecedor.Menu.MenuFornecedor(sneeze.ServicosFornecedor);
            break;
        case 3:
            sneeze.ServicosPrincipioAtivo.Menu.MenuPrincipioAtivo(sneeze.ServicosPrincipioAtivo);
            break;
        case 4:
            sneeze.ServicosMedicamento.Menu.MenuMedicamento(sneeze.ServicosMedicamento);
            break;
        case 5:
            sneeze.ServicosVenda.Menu.MenuVenda(sneeze.ServicosVenda); // acho q seria uma boa colocar o menu de item venda dentro do menu Venda
            break;
        case 6:
            sneeze.ServicosVenda.MenuItem.MenuItemVenda(sneeze.ServicosVenda);
            break;
        case 7:
            sneeze.ServicosCompra.Menu.MenuCompra(sneeze.ServicosCompra);
            break;
        case 8:
            sneeze.ServicosCompra.MenuItem.MenuItemCompra(sneeze.ServicosCompra);
            break;
        case 9:
            sneeze.ServicosProducao.Menu.MenuProducao(sneeze.ServicosProducao, sneeze.ServicosMedicamento.Medicamentos, sneeze.ServicosPrincipioAtivo.PrincipiosAtivos);
            break;
        case 10:
            sneeze.ServicosProducao.MenuItem.MenuItemProducao(sneeze.ServicosProducao, sneeze.ServicosMedicamento.Medicamentos, sneeze.ServicosPrincipioAtivo.PrincipiosAtivos);
            break;
        case 11:
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

