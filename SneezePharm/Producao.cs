using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm
{
    public class Producao
    {
        public int Id { get; private set; }
        public DateOnly DataProducao { get; private set; } = DateOnly.FromDateTime(DateTime.Now);
        public string CDB { get; private set; }
        public int Quantidade { get; private set; }

        public Producao(
            int id,
            string cdb, 
            int quantidade
            )
        {
            this.Id = id;
            this.CDB = cdb;
            this.Quantidade = quantidade;
        }

        public Producao(
            string id, 
            string dataProducao, 
            string cdb, 
            string quantidade
            )
        {
            this.Id = int.Parse(id);
            this.DataProducao = DateOnly.ParseExact(dataProducao, "ddMMyyyy");
            this.CDB = cdb;
            this.Quantidade = int.Parse(quantidade);
        }

        public void AlterarQuantidade(int quantidade)
        {
            this.Quantidade = quantidade;
        }

        public override string ToString()
        {
            return $"ID: {this.Id:D5}" +
                $"\nData de Produção: {this.DataProducao}" +
                // $"\nMedicamento: {this.CDB}" +                    // Make this show the medicine name and not just bar code
                $"\nQuantidade: {this.Quantidade}";
        }

        // Retorna todos os dados da produção num string só para armazenar em arquivo
        public string ToFile()
        {
            return $"{this.Id:D5}{this.DataProducao.ToString().Replace("/", "")}{this.CDB}{this.Quantidade:D3}";
        }
    }
}
