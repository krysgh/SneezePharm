using SneezePharm.PastaCliente;
using SneezePharm.PastaCompra;
using SneezePharm.PastaFornecedor;
using SneezePharm.PastaMedicamento;
using SneezePharm.PastaPrincipioAtivo;
using SneezePharm.PastaProducao;
using SneezePharm.PastaVenda;

namespace SneezePharm.Menu;

internal class SistemaSneezePharm
{
    public ServicosCliente ServicosCliente { get; set; } = new ServicosCliente();
    public ServicosFornecedor ServicosFornecedor { get; set; } = new ServicosFornecedor();
    public ServicosCompra ServicosCompra { get; set; } = new ServicosCompra();
    public ServicosMedicamento ServicosMedicamento { get; set; } = new ServicosMedicamento();
    public ServicosPrincipioAtivo ServicosPrincipioAtivo { get; set; } = new ServicosPrincipioAtivo();
    public ServicosProducao ServicosProducao { get; set; } = new ServicosProducao();
    public ServicosVenda ServicosVenda { get; set; } = new ServicosVenda();

    public void CriarTodosArquivos()
    {
        // Criar arquivos Cliente
        ServicosCliente.CriarArquivosCliente();
        ServicosCliente.CriarArquivosClientesBloqueados();

        // Criar arquivos Fornecedor
        ServicosFornecedor.CriarArquivosFornecedor();
        ServicosFornecedor.CriarArquivosFornecedoresBloqueados();
    }
}
