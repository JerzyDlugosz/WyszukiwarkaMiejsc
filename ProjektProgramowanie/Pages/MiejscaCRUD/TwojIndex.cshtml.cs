using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie.Data;
using ProjektProgramowanie.Model;

namespace ProjektProgramowanie.Pages.MiejscaCRUD
{
    public class TwojIndexModel : PageModel
    {
        private readonly ProjektProgramowanie.Data.ApplicationDbContext _context;

        public TwojIndexModel(ProjektProgramowanie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Miejsca> Miejsca { get;set; }
        public static bool Refresh { get; set; }
        [BindProperty]
        public Wiersze Wr { get; set; }
        public int Strona;
        public int MaxStrona;

        public async Task OnGetAsync()
        {
            Miejsca = await _context.Miejsca.Where(x => x.Autor == User.Identity.Name).ToListAsync();
            MaxStrona = (Miejsca.Count + ZmiennaGlob.TwojIndexLiczbaMiejscEqu) / ZmiennaGlob.TwojIndexLiczbaMiejscEqu;
            if (Refresh == false)
            {
                if (ZmiennaGlob.TwojIndexLiczbaMiejsc == ZmiennaGlob.TwojIndexLiczbaMiejscEqu)
                {
                    Miejsca = Miejsca.Take(ZmiennaGlob.TwojIndexLiczbaMiejsc).ToList<Miejsca>();
                }
                if (ZmiennaGlob.TwojIndexLiczbaMiejsc > ZmiennaGlob.TwojIndexLiczbaMiejscEqu)
                {
                    Miejsca = Miejsca.Skip(ZmiennaGlob.TwojIndexLiczbaMiejsc - ZmiennaGlob.TwojIndexLiczbaMiejscEqu).Take(ZmiennaGlob.TwojIndexLiczbaMiejscEqu).ToList<Miejsca>();
                }
                Refresh = true;
            }
            else
            {
                ZmiennaGlob.TwojIndexLiczbaMiejsc = ZmiennaGlob.TwojIndexLiczbaMiejscEqu;
                Miejsca = Miejsca.Take(ZmiennaGlob.TwojIndexLiczbaMiejsc).ToList<Miejsca>();
            }
            Strona = ZmiennaGlob.TwojIndexLiczbaMiejsc / ZmiennaGlob.TwojIndexLiczbaMiejscEqu;

        }
        public async Task<IActionResult> OnPost()
        {

            Miejsca = await _context.Miejsca.Where(x => x.Autor == User.Identity.Name).ToListAsync();
            Miejsca = Miejsca.Skip(ZmiennaGlob.TwojIndexLiczbaMiejscEqu).ToList<Miejsca>();
            if (Miejsca.Count + ZmiennaGlob.TwojIndexLiczbaMiejscEqu > ZmiennaGlob.TwojIndexLiczbaMiejsc)
            {
                ZmiennaGlob.TwojIndexLiczbaMiejsc += ZmiennaGlob.TwojIndexLiczbaMiejscEqu;
            }
            Refresh = false;

            return RedirectToPage("TwojIndex");
        }

        public IActionResult OnPostLiczbaWierszy()
        {
            ZmiennaGlob.TwojIndexLiczbaMiejscEqu = Wr.Wiersz;
            return RedirectToPage("TwojIndex");
        }

        public async Task<IActionResult> OnPostDodaj()
        {

            Miejsca = await _context.Miejsca.Where(x => x.Autor == User.Identity.Name).ToListAsync();


            Miejsca = Miejsca.Skip(ZmiennaGlob.TwojIndexLiczbaMiejsc).ToList<Miejsca>();
            if (Miejsca.Count > 0)
            {
                ZmiennaGlob.TwojIndexLiczbaMiejsc += ZmiennaGlob.TwojIndexLiczbaMiejscEqu;
            }
            Refresh = false;

            return RedirectToPage("TwojIndex");
        }

        public IActionResult OnPostCofnij()
        {

            if (ZmiennaGlob.TwojIndexLiczbaMiejsc - ZmiennaGlob.TwojIndexLiczbaMiejscEqu > 0)
            {
                ZmiennaGlob.TwojIndexLiczbaMiejsc -= ZmiennaGlob.TwojIndexLiczbaMiejscEqu;
            }
            Refresh = false;

            return RedirectToPage("TwojIndex");
        }
    }
}
