using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjektProgramowanie.Data;
using ProjektProgramowanie.Model;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace ProjektProgramowanie.Pages.MiejscaCRUD
{
    public class CreateModel : PageModel
    {
        private readonly ProjektProgramowanie.Data.ApplicationDbContext _context;

        public CreateModel(ProjektProgramowanie.Data.ApplicationDbContext context)
        {
            _context = context;

        }

        public IActionResult OnGet()
        {
            
            return Page();
        }
        [BindProperty]
        public Miejsca Miejsca { get; set; }
        public IFormFile Zdjecie { get; set; }
        public string[] Opcje = new[] { "Gory", "Zbiorniki wodne"};
        public string[] Opcje2 = new[] { "Zabytki", "Agroturystyka" };

        public async Task<IActionResult> OnPostAsync()
        {

            Miejsca.Data = DateTime.Today.ToLongDateString();
            Miejsca.Autor = User.Identity.Name;

            if (!(Zdjecie == null))
            { 
                string url = Path.Combine(Environment.CurrentDirectory, "wwwroot/images", Zdjecie.FileName);
                using (var sciezka = new FileStream(url, FileMode.Create))
                {
                    await Zdjecie.CopyToAsync(sciezka);
                }

                Miejsca.Zdjecie = Zdjecie.FileName;
            }

            if (!ModelState.IsValid && Zdjecie == null)
            {
                return Page();
            }


            _context.Miejsca.Add(Miejsca);
            await _context.SaveChangesAsync();

            ZmiennaGlob.Kategoria = null;
            return RedirectToPage("./Index");
        }
    }
}