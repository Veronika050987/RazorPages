using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RazorPages.Data;
using RazorPages.Models;

namespace RazorPages.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly RazorPages.Data.ContosoUniversityContext _context;

        public IndexModel(RazorPages.Data.ContosoUniversityContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Student> Students { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            IQueryable<Student> students = from student in _context.Students select student;
            switch (sortOrder)
            {
                case "name_desc":   students = students.OrderByDescending(s => s.LastName);         break;
                case "date_desc":   students = students.OrderByDescending(s => s.EnrollmentDate);   break;
                case "Date":        students = students.OrderBy(s => s.EnrollmentDate);             break;
                default:            students = students.OrderBy(s => s.ID);                         break;
            }

            Students = await students.AsNoTracking().ToArrayAsync();
            //Students = await _context.Students.ToListAsync();
        }
    }
}
