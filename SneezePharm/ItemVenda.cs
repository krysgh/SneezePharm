using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm
{
    public class ItemVenda
    {
        public int Id { get; set; }

        public int IdVenda {get;set;}

        public string Medicamento {get;set;}

        public int Quantidade {get;set;}

        public decimal ValorUnitario {get;set;}

        public decimal ValorTotalItem { get;set;}
    }
}
