using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserRegistrationApp1.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Mandatory")]
        public string FirstName { get; set; } = string.Empty;

       
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Mandatory")]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must end with @gmail.com")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mandatory")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Password must contain at least 8 characters, 1 uppercase, 1 lowercase, 1 number and 1 special character.")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Mandatory")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mandatory")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
    
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Mandatory")]
    
        public string Address { get; set; } = string.Empty;


        [NotMapped]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DOB.Year;
                if (DOB.Date > today.AddYears(-age)) age--;
                return age;
            }
        }
        public bool IsDisabled { get; set; } = false;
        [Required(ErrorMessage = "Mandatory")]

        public string Role { get; set; } = string.Empty;// e.g., "Admin", "User"
    }
}
