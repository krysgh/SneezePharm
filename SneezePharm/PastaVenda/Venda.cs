using SneezePharm.PastaFornecedor;
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

        public DateOnly DataVenda { get; private set; } = DateOnly.FromDateTime(DateTime.Now);

        public string CPF { get; private set; }

        public decimal ValorTotal { get; private set; }

        #endregion

        #region Construtores
        public Venda(
            int id,
            string cpf,
            decimal valorTotal
            )
        {
            this.Id = id;
            this.CPF = cpf;
            this.ValorTotal = valorTotal;
        }

        public Venda(
            int id,
            DateOnly dataVenda,
            string cpf,
            decimal valorTotal
            ):
            this(
                id,
                cpf,
                valorTotal
                )
        {
            this.DataVenda = dataVenda;
        }

        #endregion

        public void SetCPF(string cpf)
        {
            this.CPF = cpf;
        }

        #region Exibicao

        public override string ToString()
        {
            return $"ID da Venda: {this.Id}\n\r" +
                $"Data da Venda: {this.DataVenda}\n\r" +
                $"CPF do cliente: {this.CPF}\n\r" +
                $"Valor Total: {this.ValorTotal:c}\n\r";
        }

        public string ToFile()
        {
            return this.Id.ToString().PadLeft(5) +
                this.DataVenda.ToString("ddMMyyyy") +
                this.CPF +
                this.ValorTotal.ToString().PadLeft(8);
        }
        #endregion



    }
}
