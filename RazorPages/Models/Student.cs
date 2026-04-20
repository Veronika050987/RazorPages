using System.ComponentModel.DataAnnotations;

namespace RazorPages.Models
{
	public class Student
	{
		public int Id { get; set; }

		public string Lastname { get; set; }
		public string FirstName { get; set; }
		public DateTime EnrollmentDate { get; set; }

		//Navigation properties
		public ICollection<Enrollment> Enrollments { get; set; }
	}
}
