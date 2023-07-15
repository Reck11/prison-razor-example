using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.Prisoners
{
    public class IndexModel : PrisonerPage {
        private readonly Prison.Data.PrisonContext _context;

        public IndexModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        public IList<Prisoner> Prisoner { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Prisoner != null)
            {
                Prisoner = await _context.Prisoner
                .Include(p => p.Cell)
                .Include(p => p.Crime).ToListAsync();
            }
        }
    }
}
