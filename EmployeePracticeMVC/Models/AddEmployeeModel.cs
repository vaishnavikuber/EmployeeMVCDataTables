using System.ComponentModel.DataAnnotations;

namespace EmployeePracticeMVC.Models
{
    public class AddEmployeeModel
    {

        [RegularExpression(@"^[A-Z][a-z]{2,}$", ErrorMessage = "name should starts with capital letter has min 3 characters")]
        public string EmployeeName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string Email { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public int Salary { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
        [Required]
        public string Gender { get; set; }


    }
}
