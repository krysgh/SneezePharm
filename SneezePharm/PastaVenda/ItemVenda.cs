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

            this.SetQuantidade(quantidade);

            this.SetValorUnitario(valorUnitario);

        }


        //Construtor para criação de objeto arquivo => lista
        public ItemVenda(
            string linhaArquivo
            )
        {
            if (linhaArquivo.Length < 41)
                throw new Exception("Linha de arquivo inválida!");

            Id = Convert.ToInt32(linhaArquivo.Substring(0, 5).Trim());
            IdVenda = Convert.ToInt32(linhaArquivo.Substring(5, 5).Trim());
            Medicamento = linhaArquivo.Substring(10, 13).Trim();
            SetQuantidade(Convert.ToInt32(linhaArquivo.Substring(23, 3).Trim()));
            SetValorUnitario(Convert.ToDecimal(linhaArquivo.Substring(26, 7).Trim()));
        }
        #endregion


        #region Validacoes


        public bool SetQuantidade(int q)
        {
            if (q <= 0 || q > 999)
            {
                Console.WriteLine("Quantidade inválida! Deve ser entre 1 e 999.");
                return false;
            }
            Quantidade = q;
            AtualizarValorTotal();
            return true;
        }

        public bool SetValorUnitario(decimal v)
        {
            if (v <= 0 || v > 9999.99m)
            {
                Console.WriteLine("Valor unitário inválido! Deve ser entre 0.01 e 9999.99.");
                return false;
            }
            ValorUnitario = v;
            AtualizarValorTotal();
            return true;
        }

        private void AtualizarValorTotal()
        {
            decimal total = Quantidade * ValorUnitario;
            if (total > 99999.99m)
            {
                Console.WriteLine("Valor total do item excede o limite de 99999.99.");
                return;
            }
            ValorTotalItem = total;
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
