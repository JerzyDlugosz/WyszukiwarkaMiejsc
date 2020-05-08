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
    public class DeleteModel : PageModel
    {
        private readonly ProjektProgramowanie.Data.ApplicationDbContext _context;

        public DeleteModel(ProjektProgramowanie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Miejsca Miejsca { get; set; }

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Miejsca = await _context.Miejsca.FindAsync(id);

            if (Miejsca != null)
            {
                _context.Miejsca.Remove(Miejsca);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
