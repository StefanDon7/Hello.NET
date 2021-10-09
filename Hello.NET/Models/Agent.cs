using Hello.NET.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.NET.Models {
    [Table("Agent")]
    public class Agent {

        [Key]
        [Column("agentID")]
        public int AgentID { get; set; }
        [Column("ime")]
        public string Ime { get; set; }
        [Column("prezime")]
        public string Prezime { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("sifra")]
        public string Sifra { get; set; }

        public List<Rezervacija> Lista_Rezervacija { get; set; }

    }
}
