using SneezePharm;
using SneezePharm.PastaMedicamento;

ServicosMedicamento  medicamento = new ServicosMedicamento();

List<string> menuPrincipal = new List<string>()
{
    "Menu Clientes",
    "Menu Fornecedores",
    "Menu Principio Ativo",
    "Menu Medicamento",
    "Menu Venda",
    "Menu Compra",
    "Menu Produção",
    "Sair"
};

List<string> menuCliente = new List<string>()
{
    "Cadastrar Cliente",
    "Localizar Cliente",
    "Alterar Cliente",
    "Imprimir Clientes",
    "Bloquear Cliente",
    "Desbloquear Cliente",
    "Imprimir Clientes Bloquedos",
    "Voltar ao Menu Principal"
};

List<string> menuFornecedor = new List<string>()
{
    "Cadastrar Fornecedor",
    "Localizar Fornecedor",
    "Alterar Fornecedor",
    "Imprimir Fornecedores",
    "Bloquear Fornecedor",
    "Desbloquear Fornecedor",
    "Imprimir Fornecedores Bloqueados",
    "Voltar ao Menu Principal"
};

List<string> menuPrincipioAtivo = new List<string>()
{
    "Cadastrar Principio Ativo",
    "Localizar Principio Ativo",
    "Alterar Principio Ativo",
    "Imprimir Principios Ativos",
    "Voltar ao Menu Principal"
};

List<string> menuMedicamento = new List<string>()
{
    "Cadastrar Medicamento",
    "Localizar Medicamento",
    "Alterar Medicamento",
    "Imprimir Medicamentos",
    "Voltar ao Menu Principal"
};

List<string> menuVenda = new List<string>()
{
    "Cadastrar Venda",
    "Localizar Venda",
    "Alterar Venda",
    "Imprimir Vendas",
    "Voltar ao Menu Principal"
};

List<string> menuCompra = new List<string>()
{
    "Cadastrar Compra",
    "Localizar Compra",
    "Alterar Compra",
    "Imprimir Compra",
    "Sair"
};



void MenuMedicamento(int opcao)
{
    switch (opcao)
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
    }
}

do
{
    int escolhaMenu = Menu.Menus("=== Menu Principal ===", menuPrincipal);

    switch (escolhaMenu)
    {
        case 1:
            int escolhaCliente = Menu.Menus("=== Menu Cliente ===", menuCliente);
            break;
        case 2:
            int escolhaFornecedor = Menu.Menus("=== Menu Fornecedor ===", menuFornecedor);
            break;
        case 3:
            int escolhaPrincipioAtivo = Menu.Menus("=== Menu Principio Ativo ===", menuPrincipioAtivo);
            break;
        case 4:
            int escolhaMedicamento = Menu.Menus("=== Menu Medicamento ===", menuMedicamento);
            MenuMedicamento(escolhaMedicamento);
            break;

    }
} while (true);