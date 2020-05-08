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

namespace ProjektProgramowanie.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ProjektProgramowanie.Data.ApplicationDbContext _context;

        public IndexModel(ProjektProgramowanie.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Miejsca> MiejscaZm { get; set; }
        public Random Random { get; set; }
        public int[] Rand { get; set; }
        public Miejsca Ms { get; set; }
       

        public async Task OnGetAsync()
        {
            Random = new Random();
            int[] Rand = new int[3];
            Miejsca[] Ms = new Miejsca[3];
            MiejscaZm = await _context.Miejsca.ToListAsync();

            Rand[0] = Random.Next(MiejscaZm.Count);
            do
            {
                Rand[1] = Random.Next(MiejscaZm.Count);
            } while (Rand[1] == Rand[0]);

            do
            {
                do
                {
                    Rand[2] = Random.Next(MiejscaZm.Count);
                } while (Rand[2] == Rand[1]);
            } while (Rand[2] == Rand[0]);




            for (int i = 0; i < 3; i++)
            {
                Ms[i] = MiejscaZm.ElementAt(Rand[i]);
            }
            MiejscaZm.Clear();
            for (int i = 0; i < 3; i++)
            {
                MiejscaZm.Add(Ms[i]);
            }
        }
    }
}
