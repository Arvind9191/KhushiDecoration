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

        [Required(ErrorMessage = "Email ID is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid corporate or standard email layout.")]
        [Display(Name = "Email Id")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Invalid Mobile Number. Must be 10 digits starting with 6, 7, 8, or 9.")]
        [Display(Name = "Mobile Number")]
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
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email ID is required.")] 
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")] 
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid corporate or standard email layout.")]
        [Display(Name = "Email Id")]
        public string Email { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Mobile number is required.")] 
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Invalid Mobile Number. Must be 10 digits starting with 6, 7, 8, or 9.")]
        [Display(Name = "Mobile Number")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select an event type.")]
        [Display(Name = "Event Type")]
        public string EventType { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please select your event date.")]
        [DataType(DataType.Date)]
        [Display(Name = "Event Date")]
        public DateTime? EventDate { get; set; }
        [Required(ErrorMessage = "Please select an estimated budget.")]
        [Display(Name = "Budget Range")]
        public string BudgetRange { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please provide some details about your vision.")]
        [StringLength(1000, ErrorMessage = "Details cannot exceed 1000 characters.")]
        [Display(Name = "Inquiry Details")]
        public string Message { get; set; } = string.Empty;
    }
}
