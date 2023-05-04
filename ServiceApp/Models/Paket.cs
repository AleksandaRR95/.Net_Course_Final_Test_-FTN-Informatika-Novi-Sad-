using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApp.Models
{
    public class Paket
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(90), MinLength(2)]
        public string Posiljalac { get; set; }
        [Required]
        [MaxLength(120), MinLength(4)]
        public string Primalac { get; set; }
        [Required]
        [Range(0.1 ,9.99)]
        public double Tezina { get; set; }
        [Required]
        [Range(250, 10000)]
        public int CenaPostarine { get; set; }

        public int KurirId { get; set; }

        public Kurir Kurir { get; set; }
    }
}
