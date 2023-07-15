using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.Visitors
{
    public class CreateModel : PageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public CreateModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Visitor Visitor { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Visitor == null || Visitor == null)
            {
                return Page();
            }

            _context.Visitor.Add(Visitor);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
