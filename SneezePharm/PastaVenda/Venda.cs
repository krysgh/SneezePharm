using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.PastaVenda
{
    public class Venda
    {


        #region Propriedades
        public int Id { get; private set; }

        public DateOnly DataVenda { get; private set; }

        public string CPF { get; private set; }

        public decimal ValorTotal { get; private set; }

        #endregion

        #region Construtores
        //Construtor aquivo => lista
        public Venda(
            string id,
            string dataVenda,
            string cpf,
            string valorTotal
            )
        {
            this.Id = Convert.ToInt32(id.Trim());
            this.DataVenda = DateOnly.ParseExact(dataVenda, "ddMMyyyy");
            this.CPF = cpf.Trim();
            this.ValorTotal = Convert.ToDecimal(valorTotal);
        }

        public Venda(
            string cpf
            )
        {
            this.CPF = cpf;
            this.DataVenda = DateOnly.FromDateTime(DateTime.Now);
        }
        #endregion



        #region Alteracao
        public void AlterarData(DateOnly dataVenda)
        {
            this.DataVenda = dataVenda;

        }
        #endregion

        #region Exibicao

        public override string ToString()
        {
            return $"ID da Venda: {this.Id}\n" +
                $"Data da Venda: {this.DataVenda}\n" +
                $"CPF do cliente: {this.CPF}\n" +
                $"Valor Total? {this.ValorTotal}\n";
        }

        public string ToFile()
        {
            return this.Id.ToString().PadLeft(5) +
                this.DataVenda.ToString().Replace("/", "") +
                this.CPF +
                this.ValorTotal.ToString("F2").Replace(',', '.').PadLeft(8);
        }
        #endregion

    }
}
