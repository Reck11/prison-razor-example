﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.Wardens
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
        public Warden Warden { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Warden == null || Warden == null)
            {
                return Page();
            }

            _context.Warden.Add(Warden);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
