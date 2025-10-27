using SneezePharm.PastaCliente;
using SneezePharm.PastaCompra;
using SneezePharm.PastaFornecedor;
using SneezePharm.PastaMedicamento;
using SneezePharm.PastaPrincipioAtivo;
using SneezePharm.PastaProducao;
using SneezePharm.PastaVenda;

namespace SneezePharm;

public class SistemaSneezePharm
{
    public ServicosCliente ServicosCliente { get; set; } = new ServicosCliente();
    public ServicosFornecedor ServicosFornecedor { get; set; } = new ServicosFornecedor();
    public ServicosCompra ServicosCompra { get; set; } = new ServicosCompra();
    public ServicosMedicamento ServicosMedicamento { get; set; } = new ServicosMedicamento();
    public ServicosPrincipioAtivo ServicosPrincipioAtivo { get; set; } = new ServicosPrincipioAtivo();
    public ServicosProducao ServicosProducao { get; set; } = new ServicosProducao();
    public ServicosVenda ServicosVenda { get; set; } = new ServicosVenda();

    // Todas as classes iniciam populadas pelo proprio construtor.

    public void CriarDiretorio()
    {
       
    }
    public void CriarTodosArquivos()
    {
        // Criar arquivos Cliente
        ServicosCliente.CriarArquivosCliente();
        ServicosCliente.CriarArquivosClientesBloqueados();

        // Criar arquivos Fornecedor
        ServicosFornecedor.CriarArquivosFornecedor();
        ServicosFornecedor.CriarArquivosFornecedoresBloqueados();

        // Criar arquivos Compra 
        ServicosCompra.CriarArquivosCompra();
        ServicosCompra.CriarArquivosItensCompra();

        // Criar arquivos Medicamento
        ServicosMedicamento.CriarArquivosMedicamento();

        // Criar arquivos Principio Ativo
        ServicosPrincipioAtivo.CriarArquivosPrincipioAtivo();

        // Criar arquivos Producao
        ServicosProducao.CriarArquivosProducao();
        ServicosProducao.CriarArquivosItemProducao();

        // Criar arquivos Venda
        ServicosVenda.CriarArquivoVenda();
        ServicosVenda.CriarArquivoItensVenda();
    }
    public void GravarTodosArquivos()
    {
        // Gravar arquivos Cliente
        ServicosCliente.GravarArquivoCliente();
        ServicosCliente.GravarArquivoBloqueado();

        // Gravar arquivos Fornecedor
        ServicosFornecedor.GravarArquivoFornecedor();
        ServicosFornecedor.GravarArquivoFornecedorBloqueado();

        // Gravar arquivos Compra
        ServicosCompra.GravarArquivoCompra();
        ServicosCompra.GravarArquivoItemCompra();

        // Gravar arquivos Medicamento
        ServicosMedicamento.GravarArquivoMedicamento();

        // Gravar aquivos Principio Ativo
        ServicosPrincipioAtivo.GravarArquivoPrincipioAtivo();

        // Gravar arquivos Producao
        ServicosProducao.GravarArquivoProducao();
        ServicosProducao.GravarArquivoItemProducao();

        // Gravar arquivos Venda
        ServicosVenda.GravarArquivoVenda();
        ServicosVenda.GravarArquivoItemVenda();
    }

}