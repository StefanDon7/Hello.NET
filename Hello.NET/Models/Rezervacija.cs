using Hello.NET.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.NET.Domain.Models{
    [Table("Rezervacija")]

    public class Rezervacija {
        [Key]
        public int RezervacijaID { get; set; }
        [Column()]
        public int KorisnikID { get; set; }

        public Korisnik Korisnik { get; set; }

        public int LetID { get; set; }

        public Let Let { get; set; }

        public bool Odobreno { get; set; }

        public int AgentID { get; set; }

        public Agent Agent { get; set; }






}
}
