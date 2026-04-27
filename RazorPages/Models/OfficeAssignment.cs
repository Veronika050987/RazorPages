using System.ComponentModel.DataAnnotations;

namespace RazorPages.Models
{
	public class OfficeAssignment
	{
		[Key]
		public int InstructorID { get; set; }

		[StringLength(50)]
		[Display(Name = "Расположение офиса")]
		public string Location { get; set; }

		//Navigation properties:
		public Instructor Instructor { get; set; }
	}
}
