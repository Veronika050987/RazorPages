using RazorPages.Data;
using RazorPages.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RazorPages.Pages.Courses
{
	public class DepartmentNamePageModel:PageModel
	{
		public SelectList DepartmentNameSL { get; set; }
		public void PopulateDepartmentsDropdownList(ContosoUniversityContext _context, object selectedDepartment=null)
		{
			IQueryable<Department> departmentsQuery = from d in _context.Departments orderby d.Name select d;
			DepartmentNameSL = new SelectList
				(
				departmentsQuery.AsNoTracking(),
				nameof(Department.DepartmentID),
				nameof(Department.Name),
				selectedDepartment
				);
		}
	}
}
