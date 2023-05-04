using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApp.Models
{
    public class PaketDTO
    {
        public int Id { get; set; }

        public string Posiljalac { get; set; }


        public string Primalac { get; set; }

        public double Tezina { get; set; }

        public int CenaPostarine { get; set; }


        public int KurirId { get; set; }

        public string KurirIme { get; set; }

        public override bool Equals(object obj)
        {
            return obj is PaketDTO dTO &&
                   Id == dTO.Id &&
                   Posiljalac == dTO.Posiljalac &&
                   Primalac == dTO.Primalac &&
                   Tezina == dTO.Tezina &&
                   CenaPostarine == dTO.CenaPostarine &&
                   KurirId == dTO.KurirId &&
                   KurirIme == dTO.KurirIme;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Posiljalac, Primalac, Tezina, CenaPostarine, KurirId, KurirIme);
        }
    }
}
