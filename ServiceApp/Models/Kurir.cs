using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApp.Models
{
    public class Kurir
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(119)]
        public string Ime { get; set; }
        [Required]
        [Range(1940, 2004)]
        public int GodinaRodjenja { get; set; }

    }
}
