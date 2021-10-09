using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hello.NET.Domain.Models {
    [Table("Korisnik")]
    public class Korisnik {
        [Key]
        public int KorisnikID { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string Email { get; set; }

        public string Sifra { get; set; }

        public List<Rezervacija> Lista_Rezervacija { get; set; }
    }
}