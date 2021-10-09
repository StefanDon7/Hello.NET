using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.NET.Domain.Models{
    [Table("Mesto")]
    public class Mesto
    {
      
        [Key]
        public int MestoID { get; set; }

        [Required]
        [MaxLength(15,ErrorMessage ="Ime može da ima maksimalo 15 karaktera")]
        public string Naziv { get; set; }

        public List<Let> Lista_letovaSaPolazak { get; set; }

        public List<Let> Lista_letovaSaDolazak { get; set; }


    }
}
