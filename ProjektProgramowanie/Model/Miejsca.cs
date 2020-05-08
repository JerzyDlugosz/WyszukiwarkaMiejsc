using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektProgramowanie.Model
{
    public class Miejsca
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Wpisz Miejsce")]
        [Display(Name = "Miejsce")]
        public string Miejsce { get; set; }

        [Required(ErrorMessage = "Wpisz Opis")]
        [Display(Name = "Opis")]
        public string Opis { get; set; }

        [Display(Name = "Zdjecie")]
        public string Zdjecie { get; set; }

        [Required(ErrorMessage = "Wpisz Kraj")]
        [Display(Name = "Kraj")]
        public string Kraj { get; set; }

        [Required(ErrorMessage = "Wpisz Trase")]
        [Display(Name = "Trasa")]
        public string Trasa { get; set; }

        [Required]
        [Display(Name = "Data")]
        public string Data { get; set; }

        [Required]
        [Display(Name = "Autor")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "Dodaj Kategorie")]
        [Display(Name = "Kategoria")]
        public string Kategoria { get; set; }
    }
}
