using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneezePharm.PastaProducao
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
            Id = id;
            CDB = cdb;
            Quantidade = quantidade;
        }

        public Producao(
            string id,
            string dataProducao,
            string cdb,
            string quantidade
            )
        {
            Id = int.Parse(id);
            DataProducao = DateOnly.ParseExact(dataProducao, "ddMMyyyy");
            CDB = cdb;
            Quantidade = int.Parse(quantidade);
        }

        public void AlterarQuantidade(int quantidade)
        {
            Quantidade = quantidade;
        }

        public override string ToString()
        {
            
            return $"ID: {Id:D5}" +
                $"\nData de Produção: {DataProducao}" +
                $"\nMedicamento Código: {this.CDB}" +
                $"\nQuantidade: {Quantidade}";
        }

        // Retorna todos os dados da produção num string só para armazenar em arquivo
        public string ToFile()
        {
            return $"{Id:D5}{DataProducao.ToString().Replace("/", "")}{CDB}{Quantidade:D3}";
        }
    }
}
