using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Academy.Data;
using Academy.Models;

using Microsoft.Extensions.Configuration;


namespace Academy.Pages.Students
{
	public class IndexModel : PageModel
	{
		private readonly Academy.Data.AcademyContext _context;

		public IndexModel(Academy.Data.AcademyContext context, IConfiguration configuration)
		{
			_context = context;
			this.configuration = configuration;
		}
		//Search & Sorting
		public string NameSort { get; set; }
		public string DateSort { get; set; }
		public string CurrentFilter { get; set; }
		public string CurrentSort { get; set; }

		readonly IConfiguration configuration;
		public PaginatedList<Student> Students { get; set; }
		public int PageSize;
		public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex, int pageSize = 5)
		{
			CurrentSort = sortOrder;
			NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			DateSort = sortOrder == "Date" ? "date_desc" : "Date";

			if (searchString != null) pageIndex = 1;
			else searchString = currentFilter;
			CurrentFilter = searchString;

			IQueryable<Student> students = from student in _context.Students select student;

			if (!String.IsNullOrEmpty(CurrentFilter))
			{
				students = students.Where(s => s.last_name.Contains(CurrentFilter) || s.first_name.Contains(CurrentFilter));
			}

			switch (sortOrder)
			{
				case "name_desc": students = students.OrderByDescending(s => s.last_name); break;
				case "date_desc": students = students.OrderByDescending(s => s.birth_date); break;
				case "Date": students = students.OrderBy(s => s.birth_date); break;
				default: students = students.OrderBy(s => s.stud_id); break;
			}

			//int pageSize = configuration.GetValue("PageSize", 10);
			PageSize = pageSize;
			Students = await PaginatedList<Student>.CreateAsync(students.AsNoTracking(), pageIndex ?? 1, PageSize);
		}
	}
}
