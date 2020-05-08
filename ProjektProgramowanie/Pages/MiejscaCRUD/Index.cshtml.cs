using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie.Data;
using ProjektProgramowanie.Model;

namespace ProjektProgramowanie.Pages.MiejscaCRUD
{
    public class IndexModel : PageModel
    {
        private readonly ProjektProgramowanie.Data.ApplicationDbContext _context;

        public IndexModel(ProjektProgramowanie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Miejsca> MiejscaZm { get; set; }
        public static bool Refresh { get; set; }
        [BindProperty]
        public Wiersze Wr { get; set; }
        public List<string> Lista { get; set; }
        [BindProperty]
        public string Wybor {get; set;}
        public IQueryable<Miejsca> MiejscaQuery { get; set; }
        public IList<Miejsca> MiejscaCount { get; set; }
        public int Strona;
        public int MaxStrona;

        public async Task OnGetAsync()
        {
            MiejscaZm = await _context.Miejsca.ToListAsync();


            Lista = new List<string> { "Gory", "Zbiorniki wodne", "Zabytki", "Agroturystyka" };
            Wybor = ZmiennaGlob.Kategoria;
            MiejscaQuery = MiejscaZm.AsQueryable();


            if (!String.IsNullOrEmpty(Wybor))
            {
                MiejscaQuery = MiejscaQuery.Where(Miejsca => Miejsca.Kategoria.Contains(Wybor));
                MiejscaZm = MiejscaQuery.ToList();
            }

            MaxStrona = (MiejscaZm.Count + (ZmiennaGlob.LiczbaKolumn * ZmiennaGlob.LiczbaWierszy)) / (ZmiennaGlob.LiczbaKolumn * ZmiennaGlob.LiczbaWierszy);

            if (Refresh == false)
            {
                if (ZmiennaGlob.Zmienna == (ZmiennaGlob.LiczbaKolumn * ZmiennaGlob.LiczbaWierszy))
                {
                    MiejscaZm = MiejscaZm.Take(ZmiennaGlob.Zmienna).ToList<Miejsca>();
                }
                if (ZmiennaGlob.Zmienna > (ZmiennaGlob.LiczbaKolumn * ZmiennaGlob.LiczbaWierszy))
                {
                    MiejscaZm = MiejscaZm.Skip(ZmiennaGlob.Zmienna - (ZmiennaGlob.LiczbaKolumn * ZmiennaGlob.LiczbaWierszy)).Take((ZmiennaGlob.LiczbaKolumn * ZmiennaGlob.LiczbaWierszy)).ToList<Miejsca>();
                }
                Refresh = true;
            }
            else
            {
                ZmiennaGlob.Zmienna = (ZmiennaGlob.LiczbaKolumn * ZmiennaGlob.LiczbaWierszy);
                MiejscaZm = MiejscaZm.Take(ZmiennaGlob.Zmienna).ToList<Miejsca>();
            }

            Strona = ZmiennaGlob.Zmienna / (ZmiennaGlob.LiczbaKolumn * ZmiennaGlob.LiczbaWierszy);
        }
        public async Task<IActionResult> OnPostDodaj()
        {

            MiejscaZm = await _context.Miejsca.ToListAsync();

            Wybor = ZmiennaGlob.Kategoria;
            MiejscaQuery = MiejscaZm.AsQueryable();
            if (!String.IsNullOrEmpty(Wybor))
            {
                MiejscaQuery = MiejscaQuery.Where(Miejsca => Miejsca.Kategoria.Contains(Wybor));
                MiejscaZm = MiejscaQuery.ToList();
            }

            MiejscaZm = MiejscaZm.Skip(ZmiennaGlob.Zmienna).ToList<Miejsca>();
            if (MiejscaZm.Count > 0)
            { 
               ZmiennaGlob.Zmienna = ZmiennaGlob.Zmienna + (ZmiennaGlob.LiczbaKolumn * ZmiennaGlob.LiczbaWierszy);
            }
            Refresh = false;
             
            return RedirectToPage("Index");
        }

        public IActionResult OnPostCofnij()
        {

            if (ZmiennaGlob.Zmienna - (ZmiennaGlob.LiczbaKolumn * ZmiennaGlob.LiczbaWierszy) > 0)
            {
                ZmiennaGlob.Zmienna = ZmiennaGlob.Zmienna - (ZmiennaGlob.LiczbaKolumn * ZmiennaGlob.LiczbaWierszy);
            }
            Refresh = false;

            return RedirectToPage("Index");
        }

        public IActionResult OnPostLiczbaWierszy()
        {
            ZmiennaGlob.LiczbaWierszy = Wr.Wiersz;
            return RedirectToPage("Index");
        }

        public IActionResult OnPostKategoria()
        {

            ZmiennaGlob.Kategoria = Wybor;
            return RedirectToPage("Index");
        }
    }
}
