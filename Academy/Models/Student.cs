using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Academy.Models
{
	public class Student
	{
		[Key]
		public int stud_id { get; set; }

		[Required(ErrorMessage = "Без фамилии не бывает никого, даже 🐱!")]
		[StringLength(15, ErrorMessage = "Фамилия не может превышать 15 символов")]
		[Display(Name = "Фамилия")]
		[RegularExpression(@"^[A-ZА-Я]+[a-zа-я]*$", ErrorMessage = "Строка содержит недопустимые символы")]
		public string last_name { get; set; }

		[Required(ErrorMessage = "Потому что имя должно быть даже у 🦔")]
		[StringLength(15, ErrorMessage = "Имя не может превышать 15 символов")]
		[Display(Name = "Имя")]
		[RegularExpression(@"^[A-ZА-Я]+[a-zа-я]*$", ErrorMessage = "Строка содержит недопустимые символы")]
		public string first_name { get; set; }
		
		[Display(Name = "Отчество")]
		public string? middle_name { get; set; }

		[Required(ErrorMessage = "Только 🧟 не имеют дату рождения")]
		[DataType(DataType.Date)]
		[Display(Name = "Дата рождения")]
		public DateOnly birth_date { get; set; }
		//public string? email { get; set; }
		//public string? phone { get; set; }
		//public byte[]? photo { get; set; }
		//[Required, ForeignKey(nameof(Group))]
		//public int group { get; set; }
		//Navigation properties
	}
}
