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

using Microsoft.Extensions.Configuration;

namespace RazorPages.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly RazorPages.Data.ContosoUniversityContext _context;

        public IndexModel(RazorPages.Data.ContosoUniversityContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }
        //Search & Sorting
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        //public IList<Student> Students { get;set; } = default!;


        //Pagination:
        readonly IConfiguration configuration;
        public PaginatedList<Student> Students { get; set; }
        public int PageSize;
        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex, int pageSize=5)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null) pageIndex = 1;
            else searchString = currentFilter;
            CurrentFilter = searchString;

            IQueryable<Student> students = from student in _context.Students select student;
            
            if(!String.IsNullOrEmpty(CurrentFilter))
            {
                students = students.Where(s => s.LastName.Contains(CurrentFilter) || s.FirstName.Contains(CurrentFilter));
            }

            switch (sortOrder)
            {
                case "name_desc":   students = students.OrderByDescending(s => s.LastName);         break;
                case "date_desc":   students = students.OrderByDescending(s => s.EnrollmentDate);   break;
                case "Date":        students = students.OrderBy(s => s.EnrollmentDate);             break;
                default:            students = students.OrderBy(s => s.ID);                         break;
            }

            //int pageSize = configuration.GetValue("PageSize", 10);
            PageSize = pageSize;
            Students = await PaginatedList<Student>.CreateAsync(students.AsNoTracking(), pageIndex ?? 1, PageSize);
            //Students = await students.AsNoTracking().ToArrayAsync();
            //Students = await _context.Students.ToListAsync();
        }
        //public async Task OnPostAsync(string sortOrder, string searchString)
        //{
        //    NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    DateSort = sortOrder == "Date" ? "date_desc" : "Date";
        //    CurrentFilter = searchString;
            
        //    IQueryable<Student> students = from student in _context.Students select student;
            
        //    if(!String.IsNullOrEmpty(CurrentFilter))
        //    {
        //        students = students.Where(s => s.LastName.Contains(CurrentFilter) || s.FirstName.Contains(CurrentFilter));
        //    }

        //    switch (sortOrder)
        //    {
        //        case "name_desc":   students = students.OrderByDescending(s => s.LastName);         break;
        //        case "date_desc":   students = students.OrderByDescending(s => s.EnrollmentDate);   break;
        //        case "Date":        students = students.OrderBy(s => s.EnrollmentDate);             break;
        //        default:            students = students.OrderBy(s => s.ID);                         break;
        //    }

        //    Students = await students.AsNoTracking().ToArrayAsync();
        //    //Students = await _context.Students.ToListAsync();
        //}
    }
}
