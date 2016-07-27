using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVC5.Models
{
    public class UserAccount
    {
        [Key]
        [Display(Name="UserID")]
        public int Userid { get; set; }
                                     [Required(ErrorMessage ="* Please Enter First Name")]
                                     [Display(Name = "First Name")]
        public string FirstName { get; set; }
                                     [Required(ErrorMessage = "* Please Enter Last Name")]
                                     [Display(Name = "Last Name")]
        public string LastName { get; set; }
                                     [Required(ErrorMessage = "* Please Enter Valid Email Address")]
                                     [DataType(DataType.EmailAddress)]
                                     [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
                                     [Required(ErrorMessage = "* Please Enter User Name")]
                                     [Display(Name = "User Name")]
        public string UserName { get; set; }
                                     [Required(ErrorMessage = "* Please Enter Password")]
                                     [DataType(DataType.Password)]
        public string Password { get; set; }
                                     [Required(ErrorMessage = "* Please Enter the Same Password")]
                                     [DataType(DataType.Password)]
                                     [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

    }
}