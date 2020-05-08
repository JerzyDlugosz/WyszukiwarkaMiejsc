using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektProgramowanie.Data;
using ProjektProgramowanie.Model;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace ProjektProgramowanie.Pages.MiejscaCRUD
{
    public class EditModel : PageModel
    {
        private readonly ProjektProgramowanie.Data.ApplicationDbContext _context;

        public EditModel(ProjektProgramowanie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public string Sprawdz { get; set; }

        [BindProperty]
        public Miejsca Miejsca { get; set; }
        public IFormFile Zdjecie { get; set; }
        public string[] Opcje = new[] { "Gory", "Zbiorniki wodne" };
        public string[] Opcje2 = new[] { "Zabytki", "Agroturystyka" };


        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }


            Miejsca = await _context.Miejsca.FirstOrDefaultAsync(m => m.Id == id);

            if (Miejsca == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!Miejsca.Data.Contains("Zedytowano"))
            {
                Miejsca.Data = Miejsca.Data + " (Zedytowano)";
            }
              
            if (!(Zdjecie == null))
            {
                string url = Path.Combine(Environment.CurrentDirectory, "wwwroot/images", Zdjecie.FileName);
                using (var sciezka = new FileStream(url, FileMode.Create))
                {
                    await Zdjecie.CopyToAsync(sciezka);
                }

                Miejsca.Zdjecie = Zdjecie.FileName;
            }

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            _context.Attach(Miejsca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MiejscaExists(Miejsca.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MiejscaExists(int id)
        {
            return _context.Miejsca.Any(e => e.Id == id);
        }
    }
}
