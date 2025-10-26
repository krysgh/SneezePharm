//using SneezePharm.Menu;
//using SneezePharm.PastaCliente;
//using SneezePharm.PastaFornecedor;
//using SneezePharm.PastaMedicamento;
//using SneezePharm.PastaPrincipioAtivo;
//using SneezePharm.PastaProducao;

//var sneezePharm = new SistemaSneezePharm();

//sneezePharm.CriarDiretorio();
//sneezePharm.CriarTodosArquivos();



//List<string> menuCliente = new List<string>()
//{
//    "Cadastrar Cliente",
//    "Localizar Cliente",
//    "Alterar Cliente",
//    "Imprimir Clientes",
//    "Bloquear Cliente",
//    "Desbloquear Cliente",
//    "Imprimir Clientes Bloquedos",
//    "Voltar ao Menu Principal"
//};

//List<string> menuFornecedor = new List<string>()
//{
//    "Cadastrar Fornecedor",
//    "Localizar Fornecedor",
//    "Alterar Fornecedor",
//    "Imprimir Fornecedores",
//    "Bloquear Fornecedor",
//    "Desbloquear Fornecedor",
//    "Imprimir Fornecedores Bloqueados",
//    "Voltar ao Menu Principal"
//};

//List<string> menuPrincipioAtivo = new List<string>()
//{
//    "Cadastrar Principio Ativo",
//    "Localizar Principio Ativo",
//    "Alterar Principio Ativo",
//    "Imprimir Principios Ativos",
//    "Voltar ao Menu Principal"
//};

//List<string> menuMedicamento = new List<string>()
//{
//    "Cadastrar Medicamento",
//    "Localizar Medicamento",
//    "Alterar Medicamento",
//    "Imprimir Medicamentos",
//    "Voltar ao Menu Principal"
//};

//List<string> menuVenda = new List<string>()
//{
//    "Cadastrar Venda",
//    "Localizar Venda",
//    "Alterar Venda",
//    "Imprimir Vendas",
//    "Voltar ao Menu Principal"
//};

//List<string> menuItemVenda = new List<string>()
//{
//    "Cadastrar Item Venda",
//    "Localizar Item Venda",
//    "Alterar Item Venda",
//    "Imprimir Item Vendas",
//    "Voltar ao Menu Principal"
//};

//List<string> menuCompra = new List<string>()
//{
//    "Cadastrar Compra",
//    "Localizar Compra",
//    "Alterar Compra",
//    "Imprimir Compra",
//    "Sair"
//};

//List<string> menuItemCompra = new List<string>()
//{
//    "Cadastrar Item Compra",
//    "Localizar Item Compra",
//    "Alterar Item Compra",
//    "Imprimir Item Compra",
//    "Sair"
//};

//List<string> menuProducao = new List<string>()
//{
//    "Cadastrar Produção",
//    "Localizar Produção",
//    "Alterar Produção",
//    "Imprimir Produção",
//    "Sair"
//};

//List<string> menuItemProducao = new List<string>()
//{
//    "Cadastrar Item Produção",
//    "Localizar Item Produção",
//    "Alterar Item Produção",
//    "Imprimir Item Produção",
//    "Sair"
//};


//void MenuFornecedor()
//{
//    int opcao;
//    do
//    {
//        opcao = Menu.Menus("=== Menu Fornecedor ===", menuFornecedor);

//        switch (opcao)
//        {
//            case 1:
//                fornecedor.IncluirFornecedor();
//                break;
//            case 2:
//                fornecedor.ImprimirFornecedorLocalizado();
//                break;
//            case 3:
//                fornecedor.AlterarFornecedor();
//                break;
//            case 4:
//                fornecedor.ImprimirFornecedores();
//                break;
//            case 5:
//                fornecedor.BloquearFornecedor();
//                break;
//            case 6:
//                fornecedor.DesbloquearFornecedor();
//                break;
//            case 7:
//                fornecedor.ImprimirFornecedoresBloqueados();
//                break;
//            case 8:
//                Console.WriteLine("Voltando ao menu principal...");
//                break;
//        }

//    } while (opcao != 8);
//}

//void MenuPrincipioAtivo()
//{
//    int opcao;
//    do
//    {
//        opcao = Menu.Menus("=== Menu Principio Ativo ===", menuPrincipioAtivo);

//        switch (opcao)
//        {
//            case 1:
//                principioAtivo.IncluirPrincipioAtivo();
//                break;
//            case 2:
//                principioAtivo.LocalizarPrincipioAtivo();
//                break;
//            case 3:
//                principioAtivo.AlterarPrincipioAtivo();
//                break;
//            case 4:
//                principioAtivo.ImprimirPrincipiosAtivos();
//                break;
//            case 5:
//                Console.WriteLine("Voltando ao menu principal...");
//                break;
//        }

//    } while (opcao != 5);
//}
//void MenuMedicamento()
//{
//    int opcao;
//    do
//    {
//        opcao = Menu.Menus("=== Menu Medicamento ===", menuMedicamento);

//        switch (opcao)
//        {
//            case 1:
//                medicamento.IncluirMedicamento();
//                break;
//            case 2:
//                medicamento.LocalizarMedicamento();
//                break;
//            case 3:
//                medicamento.AlterarMedicamento();
//                break;
//            case 4:
//                medicamento.ImprimirMedicamentos();
//                break;
//            case 5:
//                Console.WriteLine("Voltando ao menu principal...");
//                break;
//        }

//    } while (opcao != 5);
//}

//void MenuVenda()
//{
//    int opcao;
//    do
//    {
//        opcao = Menu.Menus("=== Menu Venda ===", menuVenda);

//        switch (opcao)
//        {
//            case 1:
//                Console.WriteLine("Cadastrar Venda...");
//                break;
//            case 2:
//                Console.WriteLine("Localizar Venda...");
//                break;
//            case 3:
//                Console.WriteLine("Alterar Venda...");
//                break;
//            case 4:
//                Console.WriteLine("Imprimir Vendas...");
//                break;
//            case 5:
//                Console.WriteLine("Voltando ao menu principal...");
//                break;
//        }

//    } while (opcao != 5);
//}

//void MenuItemVenda()
//{
//    int opcao;
//    do
//    {
//        opcao = Menu.Menus("=== Menu Item Venda ===", menuItemVenda);

//        switch (opcao)
//        {
//            case 1:
//                Console.WriteLine("Cadastrar Item Venda...");
//                break;
//            case 2:
//                Console.WriteLine("Localizar Item Venda...");
//                break;
//            case 3:
//                Console.WriteLine("Alterar Item Venda...");
//                break;
//            case 4:
//                Console.WriteLine("Imprimir Item Vendas...");
//                break;
//            case 5:
//                Console.WriteLine("Voltando ao menu principal...");
//                break;
//        }

//    } while (opcao != 5);
//}
//void MenuCompra()
//{
//    int opcao;
//    do
//    {
//        opcao = Menu.Menus("=== Menu Compra ===", menuCompra);

//        switch (opcao)
//        {
//            case 1:
//                Console.WriteLine("Cadastrar Compra...");
//                break;
//            case 2:
//                Console.WriteLine("Localizar Compra...");
//                break;
//            case 3:
//                Console.WriteLine("Alterar Compra...");
//                break;
//            case 4:
//                Console.WriteLine("Imprimir Compras...");
//                break;
//            case 5:
//                Console.WriteLine("Voltando ao menu principal...");
//                break;
//        }

//    } while (opcao != 5);
//}

//void MenuItemCompra()
//{
//    int opcao;
//    do
//    {
//        opcao = Menu.Menus("=== Menu Compra ===", menuItemCompra);

//        switch (opcao)
//        {
//            case 1:
//                Console.WriteLine("Cadastrar Item Compra...");
//                break;
//            case 2:
//                Console.WriteLine("Localizar Item Compra...");
//                break;
//            case 3:
//                Console.WriteLine("Alterar Item Compra...");
//                break;
//            case 4:
//                Console.WriteLine("Imprimir Item Compras...");
//                break;
//            case 5:
//                Console.WriteLine("Voltando ao menu principal...");
//                break;
//        }

//    } while (opcao != 5);
//}

//void MenuProducao()
//{
//    int opcao;
//    do
//    {
//        opcao = Menu.Menus("=== Menu Produção ===", menuProducao);

//        switch (opcao)
//        {
//            case 1:
//                producao.IncluirProducao();
//                break;
//            case 2:
//                producao.ImprimirProducaoLocalizado();
//                break;
//            case 3:
//                producao.AlterarProducao();
//                break;
//            case 4:
//                producao.ImprimirProducoes();
//                break;
//            case 5:
//                Console.WriteLine("Voltando ao menu principal...");
//                break;
//        }

//    } while (opcao != 5);
//}

//void MenuItemProducao(int idProducao)
//{
//    int opcao;
//    do
//    {
//        opcao = Menu.Menus("=== Menu Item Produção ===", menuItemProducao);

//        switch (opcao)
//        {
//            case 1:
//                producao.IncluirItemProducao(idProducao);
//                break;
//            case 2:
//                producao.LocalizarItemProducao();
//                break;
//            case 3:
//                producao.AlterarItemProducao();
//                break;
//            case 4:
//                producao.ImprimirItemProducao();
//                break;
//            case 5:
//                Console.WriteLine("Voltando ao menu principal...");
//                break;
//        }

//    } while (opcao != 5);
//}

//using SneezePharm.Menu;

//List<string> menuPrincipal = new List<string>()
//{
//    "Menu Clientes",
//    "Menu Fornecedores",
//    "Menu Principio Ativo",
//    "Menu Medicamento",
//    "Menu Venda",
//    "Menu Item Venda",
//    "Menu Compra",
//    "Menu Item Compra",
//    "Menu Produção",
//    "Menu Item Produção",
//    "Sair"
//};

//int opcaoPrincipal;

//do
//{
//    opcaoPrincipal = Menu.Menus("=== Menu Principal ===", menuPrincipal);

//    switch (opcaoPrincipal)
//    {
//        case 1:
//            ();
//            break;
//        case 2:
//            MenuFornecedor();
//            break;
//        case 3:
//            MenuPrincipioAtivo();
//            break;
//        case 4:
//            MenuMedicamento();
//            break;
//        case 5:
//            MenuVenda();
//            break;
//        case 6:
//            MenuItemVenda();
//            break;
//        case 7:
//            MenuCompra();
//            break;
//        case 8:
//            MenuItemCompra();
//            break;
//        case 9:
//            MenuProducao();
//            break;
//        case 10:
//            Console.Write("Informe o ID da produção que deseja gerenciar: ");
//            if (int.TryParse(Console.ReadLine(), out var idProducao))
//            {
//                MenuItemProducao(idProducao);
//            }
//            else
//            {
//                Console.WriteLine("ID inválido!");
//            }
//            break;
//        case 11:
//            Console.WriteLine("Saindo...");
//            break;
//        default:
//            Console.WriteLine("Opção invalida");
//            break;
//    }

//} while (opcaoPrincipal != 11);


using SneezePharm;
using SneezePharm.Menu;

SistemaSneezePharm sneeze = new();


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
        //case 2:
        //    MenuFornecedor();
        //    break;
        //case 3:
        //    MenuPrincipioAtivo();
        //    break;
        //case 4:
        //    MenuMedicamento();
        //    break;
        //case 5:
        //    MenuVenda();
        //    break;
        //case 6:
        //    MenuItemVenda();
        //    break;
        //case 7:
        //    MenuCompra();
        //    break;
        //case 8:
        //    MenuItemCompra();
        //    break;
        //case 9:
        //    MenuProducao();
        //    break;
        //case 10:
        //    Console.Write("Informe o ID da produção que deseja gerenciar: ");
        //    if (int.TryParse(Console.ReadLine(), out var idProducao))
        //    {
        //        MenuItemProducao(idProducao);
        //    }
        //    else
        //    {
        //        Console.WriteLine("ID inválido!");
        //    }
        //    break;
        case 11:
            Console.WriteLine("Saindo...");
            working = false;
            Console.ReadKey();
            break;
        default:
            Console.WriteLine("Opção invalida");
            Console.ReadKey();
            break;
    }

    

} while (working);
        
