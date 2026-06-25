using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shubhdecoration.Data.Account
{
    public class AccountModel
    { 
    }
    public class SessionModel
    {
        public string UserName { get; set; }=string.Empty;
        public string Role { get; set; } = string.Empty;
        public long UserId { get; set; }
    } 
    public class SignupModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        public int UserRole { get; set; }  
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty; 
        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)] 
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        public bool TermsAccepted { get; set; } 
    }    
    public class LoginModel
    {

        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Role")]
        public int Role { get; set; }
        public bool RememberMe { get; set; }
    }  
    public class UserProfile
    {
        public int UserId { get; set; }
        public string FName { get; set; } = string.Empty;
        public string LName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public int UserRole { get; set; }
    } 


    public class UserListModel
    {
        public int UserId { get; set; }
        public string FName { get; set; } = string.Empty;
        public string LName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public int UserRole { get; set; }
        public bool IsActive { get; set; }

    } 
    public class DashboardModel
    {
        public int TotalUsers { get; set; }
        public int TotalDecorations { get; set; }
        public int TotalCategories { get; set; }
        public int TotalOrders { get; set; } 
        public List<UserListModel> UserList { get; set; } = new List<UserListModel>();
    }

    public class EnquiryViewModel
    {
        [Required(ErrorMessage = "Please enter your full name.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please select an event type.")]
        [Display(Name = "Event Type")]
        public string EventType { get; set; }

        [Required(ErrorMessage = "Please select your event date.")]
        [DataType(DataType.Date)]
        [Display(Name = "Event Date")]
        public DateTime? EventDate { get; set; }

        [Required(ErrorMessage = "Please select an estimated budget.")]
        [Display(Name = "Budget Range")]
        public string BudgetRange { get; set; }

        [Required(ErrorMessage = "Please provide some details about your vision.")]
        [StringLength(1000, ErrorMessage = "Details cannot exceed 1000 characters.")]
        [Display(Name = "Inquiry Details")]
        public string Message { get; set; }
    }
}
