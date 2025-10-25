using System.Globalization;

namespace SneezePharm.PastaCompra;

public class Compra
{
    public int Id { get; private set; }
    public DateOnly DataCompra { get; private set; } = DateOnly.FromDateTime(DateTime.Now);
    public string Fornecedor { get; private set; }
    public decimal ValorTotal { get; private set; }

    public Compra(int id, string cnpj, decimal valorTotal)
    {
        Id = id;
        Fornecedor = cnpj;
        ValorTotal = valorTotal;
    }
    public Compra(int id, string cnpj, decimal valorTotal, DateOnly dataCompra) : this(id, cnpj, valorTotal)
    {
        DataCompra = dataCompra;
    }

    public void SetCnpj(string cnpj)
    {
        Fornecedor = cnpj;
    }

    public string ToFile()
    {
        string id = Id.ToString();
        id = id.PadLeft(5);

        string dataCompra = DataCompra.ToString("ddMMyyyy");
        string valorTotal = ValorTotal.ToString().PadLeft(8);
        return $"{id}{dataCompra}{Fornecedor}{valorTotal}";
    }
    public override string ToString()
    {
        return $"\nId: {Id}\n\r" +
            $"Data Compra: {DataCompra}\n\r" +
            $"CNPJ: {Fornecedor}\n\r" +
            $"Valor Total: {ValorTotal:c}\n\r";
    }
}
