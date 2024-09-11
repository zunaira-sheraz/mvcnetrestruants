using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
  // Import the custom validation namespace

namespace projectmvcrestruants.Models
{
    public class AccountContext
    {
        public int id { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter a username")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Username can only contain letters, numbers, and spaces")]
        [StringLength(50,ErrorMessage ="Name cant be too long")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter an email address")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "email  cant be too long")]
        public string email { get; set; } = string.Empty;

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Please enter your date of birth")]
        [Validation.DateNotInFuture(ErrorMessage="Date cant be greater than today")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime? dob { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(25, ErrorMessage = "Password must be between 6 and 25 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string password { get; set; } = string.Empty;

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please confirm your password")]
        [NotMapped]
        [Compare("password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string confirmpassword { get; set; } = string.Empty;
    }
}
