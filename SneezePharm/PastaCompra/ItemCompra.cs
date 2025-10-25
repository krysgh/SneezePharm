namespace SneezePharm.PastaCompra
{
    public class ItemCompra
    {
        public int IdCompra { get; private set; }
        public string Ingrediente { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public decimal TotalItem { get; private set; }

        public ItemCompra(int idCompra, string idIngrediente, int quantidade, decimal valorUnitario)
        {
            IdCompra = idCompra;
            Ingrediente = idIngrediente;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;

            TotalItem = quantidade * valorUnitario;
        }

        public void SetIngrediente(string ingrediente)
        {
            Ingrediente = ingrediente;
        }

        public void SetQuantidade(int quantidade)
        {
            Quantidade = quantidade;
        }

        public void SetValorUnitario(decimal valorUnitario)
        {
            ValorUnitario = valorUnitario;
        }

        public void SetTotalItem()
        {
            TotalItem = Quantidade * ValorUnitario;
        }

        public string ToFile()
        {
            string idCompra = IdCompra.ToString().PadLeft(5);
            string idIngrediente = Ingrediente.ToString().PadLeft(6);
            string quantidade = Quantidade.ToString().PadLeft(4);
            string valorUnitario = ValorUnitario.ToString().PadLeft(6);
            string totalItem = TotalItem.ToString().PadLeft(8);

            return $"{idCompra}{idIngrediente}{quantidade}{valorUnitario}{totalItem}";

        }

        public override string ToString()
        {
            return $"\nId da compra: {IdCompra}\n\r" +
                $"Ingrediente: {Ingrediente}\n\r" +
                $"Quantidade: {Quantidade}\n\r" +
                $"Valor unitario: {ValorUnitario:c}\n\r" +
                $"Valor Total: {TotalItem:c}\n\r";
        }
    }
}
