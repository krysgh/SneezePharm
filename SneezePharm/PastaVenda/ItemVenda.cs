using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.PastaVenda
{
    public class ItemVenda
    {

        #region Propriedades

        public int IdVenda { get; private set; }

        public string Medicamento { get; private set; }

        public int Quantidade { get; private set; }

        public decimal ValorUnitario { get; private set; }

        public decimal ValorTotalItem { get; private set; }

        #endregion


        #region Construtor

        public ItemVenda(
            int idVenda,
            string medicamento,
            int quantidade,
            decimal valorUnitario
            )
        {

            this.IdVenda = idVenda;

            this.Medicamento = medicamento;

            this.Quantidade = quantidade;

            this.ValorUnitario = valorUnitario;

            this.ValorTotalItem = quantidade * valorUnitario;

        }

        #endregion

        public void SetMedicamento(string medicamento)
        {
            this.Medicamento = medicamento;
        }

        public void SetQuantidade(int quantidade)
        {
            this.Quantidade = quantidade;
        }

        public void SetValorUnitario(decimal valorUnitario)
        {
            this.ValorUnitario = valorUnitario;
        }

        public void SetValorTotalItem()
        {
            this.ValorTotalItem = this.Quantidade * this.ValorUnitario;
        }

        #region ExibicaoGravacao
        public override string ToString()
        {
            return $"Id da Venda: {this.IdVenda}\n\r" +
                $"Medicamento: {this.Medicamento}\n\r" +
                $"Quantidade: {this.Quantidade}\n\r" +
                $"ValorUnitario: {this.ValorUnitario:c}\n\r" +
                $"TotalItem: {this.ValorTotalItem:c}\r";
        }

        public string ToFile()
        {
            return this.IdVenda.ToString().PadLeft(5) +
                   this.Medicamento.PadLeft(13) +
                   this.Quantidade.ToString().PadLeft(3) +
                   this.ValorUnitario.ToString().PadLeft(7) +
                   this.ValorTotalItem.ToString().PadLeft(8);
        }

        #endregion

    }
}
