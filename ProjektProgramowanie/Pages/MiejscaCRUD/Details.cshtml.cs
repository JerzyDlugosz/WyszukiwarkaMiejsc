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
    public class DetailsModel : PageModel
    {
        private readonly ProjektProgramowanie.Data.ApplicationDbContext _context;

        public DetailsModel(ProjektProgramowanie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Miejsca Miejsca { get; set; }
        [BindProperty]
        public Komentarze KomentarzeZm { get; set; }
        public IList<Komentarze> KomentarzeList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Miejsca = await _context.Miejsca.FirstOrDefaultAsync(m => m.Id == id);
            KomentarzeList = await _context.Komentarze.Where(x => x.IdMiejsca.Contains(id.ToString())).ToListAsync();
            ZmiennaGlob.Id = (int)id;

            if (Miejsca == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostKomentuj()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            KomentarzeZm.IdMiejsca = ZmiennaGlob.Id.ToString();
            KomentarzeZm.Data = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            KomentarzeZm.Autor = User.Identity.Name;


            _context.Komentarze.Add(KomentarzeZm);
            await _context.SaveChangesAsync();

            return await OnGetAsync(ZmiennaGlob.Id);
        }

        public async Task<IActionResult> OnPostUsun(int id)
        {
            KomentarzeZm = await _context.Komentarze.FindAsync(id);

            if (KomentarzeZm != null)
            {
                _context.Komentarze.Remove(KomentarzeZm);
                await _context.SaveChangesAsync();
            }
            return await OnGetAsync(ZmiennaGlob.Id);
        }



    }
}
