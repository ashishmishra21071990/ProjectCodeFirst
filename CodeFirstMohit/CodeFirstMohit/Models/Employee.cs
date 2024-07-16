using System.ComponentModel.DataAnnotations;

namespace CodeFirstMohit.Models
{
    public class Employee
    {
        [Key]
        public int Eid { get; set; }
        [Required(ErrorMessage ="Enter Name!!")]
        public string ?Name { get; set; }
        [Required(ErrorMessage = "Enter Age!!")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Enter Mobile No!!")]
        public string ?MobileNo { get; set; }
        [Required(ErrorMessage = "Enter Salary!!")]
        public int Salary { get; set; }
        public bool is_del { get; set; }
    }
}
