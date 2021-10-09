using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.NET.Domain.Models{
    [Table("Let")]
    public class Let{
        [Key]
        public int LetID { get; set; }

        public int MestoPolaskaId { get; set; }

        public Mesto MestoPolaska { get; set; }


        public Mesto MestoDolaska { get; set; }

        public int MestoDolaskaId { get; set; }

        public int BrojPresedanje { get; set; }

        public DateTime DatumPolaska { get; set; }

        public int BrojMesta { get; set; }

        public List<Rezervacija> Lista_Rezervacija { get; set; }

    }
}
