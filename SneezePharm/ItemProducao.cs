namespace SneezePharm;

public  class ItemProducao
{
    public int IdProducao { get; private set; }
    public string Principio { get; private set; }
    public int QuantidadePrincipio { get; private set; }

    public ItemProducao(int idProducao, string principio, int quantidadePrincipio)
    {
        IdProducao = idProducao;
        Principio = principio;
        QuantidadePrincipio = quantidadePrincipio;
    }

    public void SetPrincipio(string principio)
    {
        Principio = principio;
    }

    public void SetQuantidadePrincipio(int quantidadePrincipio)
    {
        QuantidadePrincipio = quantidadePrincipio;
    }

    public string ToFile()
    {
        string idProducao = IdProducao.ToString().PadLeft(5);
        string idPrincipio = Principio.ToString().PadLeft(6);
        string quantidadePrincipio = QuantidadePrincipio.ToString().PadLeft(4);

        return $"{idProducao}{idPrincipio}{quantidadePrincipio}";
    }

    public override string ToString()
    {
        return $"\nId da Produção: {IdProducao.ToString().Trim()}\n\r" +
            $"Princípio: {Principio.ToString().Trim()}\n\r" +
            $"Quantidade de Princípio: {QuantidadePrincipio.ToString().Trim()}\n\r";
    }


}
