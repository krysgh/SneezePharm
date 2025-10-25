using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.PastaVenda
{
    public class ItemVenda
    {

        #region Propriedades
        public int Id { get; private set; }

        public int IdVenda { get; private set; }

        public string Medicamento { get; private set; }

        public int Quantidade { get; private set; }

        public decimal ValorUnitario { get; private set; }

        public decimal ValorTotalItem { get; private set; }

        #endregion


        #region Construtores

        public ItemVenda(
            int id,
            int idVenda,
            string medicamento,
            int quantidade,
            decimal valorUnitario
            )
        {
            this.Id = id;

            this.IdVenda = idVenda;

            this.Medicamento = medicamento;

            this.Quantidade = quantidade;

            this.ValorUnitario = valorUnitario;

            this.ValorTotalItem = quantidade * valorUnitario;
        }


        //Construtor para criação de objeto arquivo => lista
        public ItemVenda(
            string linhaArquivo
            )
        {
            if (linhaArquivo.Length < 41)
                throw new Exception("Linha de arquivo inválida!");

            IdVenda = Convert.ToInt32(linhaArquivo.Substring(0, 5).Trim());
            Id = Convert.ToInt32(linhaArquivo.Substring(5, 5).Trim());
            Medicamento = linhaArquivo.Substring(10, 13).Trim();
            Quantidade = Convert.ToInt32(linhaArquivo.Substring(23, 3).Trim());
            ValorUnitario = Convert.ToDecimal(linhaArquivo.Substring(26, 7).Trim());
            ValorTotalItem = Convert.ToDecimal(linhaArquivo.Substring(33, 8).Trim());
        }
        #endregion


        #region Validacoes


        public bool ValidarQuantidade(int quantidade)
        {
            return quantidade > 0 && quantidade < 1000;
        }

        public bool ValidarValorUniatario(decimal valorUnitario)
        {
            return valorUnitario > 0 && valorUnitario <= Convert.ToDecimal(9999.99);
        }
        public bool ValidarValorTotalItem(decimal valorTotalItem)
        {
            return valorTotalItem > 0 && valorTotalItem < Convert.ToDecimal(99999.99);
        }
        #endregion


        #region ExibicaoGravacao
        public override string ToString()
        {
            return $"Id: {this.Id}\nId da Venda: {this.IdVenda}\nMedicamento: {this.Medicamento}\nQuantidade: {this.Quantidade}\nValorUnitario: {this.ValorUnitario}\nTotalItem: {this.ValorTotalItem}";
        }

        public string ToFile()
        {
            return Id.ToString().PadLeft(5) +
                   IdVenda.ToString().PadLeft(5) +
                   Medicamento.PadLeft(13) +
                   Quantidade.ToString().PadLeft(3) +
                   ValorUnitario.ToString("F2").PadLeft(7) +
                   ValorTotalItem.ToString("F2").PadLeft(8);
        }

        #endregion

    }
}
