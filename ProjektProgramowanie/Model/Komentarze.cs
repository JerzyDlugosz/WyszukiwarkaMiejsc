using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektProgramowanie.Model
{
    public class Komentarze
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Komentarz")]
        public string Komentarz { get; set; }

        public string Autor { get; set; }

        public string Data { get; set; }

        public string IdMiejsca { get; set; }
    }
}
