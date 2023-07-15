﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prison.Data;
using Prison.Models;

namespace Prison.Pages.ReeducationStaffs
{
    public class IndexModel : PageModel
    {
        private readonly Prison.Data.PrisonContext _context;

        public IndexModel(Prison.Data.PrisonContext context)
        {
            _context = context;
        }

        public IList<ReeducationStaff> ReeducationStaff { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ReeducationStaff != null)
            {
                ReeducationStaff = await _context.ReeducationStaff.ToListAsync();
            }
        }
    }
}
